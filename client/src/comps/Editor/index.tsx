import { Component, defineComponent, onMounted, PropType, ref, watch, watchEffect } from 'vue'
import {
  BoldOutlined,
  CodeOutlined,
  ItalicOutlined,
  LineOutlined,
  LinkOutlined,
  OrderedListOutlined,
  StrikethroughOutlined,
  UnderlineOutlined,
  UnorderedListOutlined,
} from '@ant-design/icons-vue'

import './editor.less'

interface Action {
  name: string
  icon: Component
  title: string
  action: () => void
  active?: () => boolean
}

const queryCommandState = (command: string) => document.queryCommandState(command)
const queryCommandValue = (command: string) => document.queryCommandValue(command)
const formatBlock = (tag: string) => exec('formatBlock', tag)

export const exec = (command: string, value?: string) => document.execCommand(command, false, value)

const classes = {
  editor: 'an-editor',
  actionbar: 'an-editor-actionbar',
  button: 'an-editor-button',
  content: 'an-editor-content',
  selected: 'an-editor-button-selected',
}

const actions: Action[] = [
  {
    name: 'bold',
    icon: <BoldOutlined />,
    title: '粗体',
    active: () => queryCommandState('bold'),
    action: () => exec('bold'),
  },
  {
    name: 'italic',
    icon: <ItalicOutlined />,
    title: '斜体',
    active: () => queryCommandState('italic'),
    action: () => exec('italic'),
  },
  {
    name: 'underline',
    icon: <UnderlineOutlined />,
    title: '下划线',
    active: () => queryCommandState('underline'),
    action: () => exec('underline'),
  },
  {
    name: 'strikethrough',
    icon: <StrikethroughOutlined />,
    title: '删除线',
    active: () => queryCommandState('strikeThrough'),
    action: () => exec('strikeThrough'),
  },
  {
    name: 'heading1',
    icon: (
      <b>
        H<sub>1</sub>
      </b>
    ),
    title: '标题一',
    action: () => formatBlock('<h1>'),
  },
  {
    name: 'heading2',
    icon: (
      <b>
        H<sub>2</sub>
      </b>
    ),
    title: '标题二',
    action: () => formatBlock('<h2>'),
  },
  {
    name: 'heading3',
    icon: (
      <b>
        H<sub>3</sub>
      </b>
    ),
    title: '标题三',
    action: () => formatBlock('<h3>'),
  },
  {
    name: 'paragraph',
    icon: <span>&#182;</span>,
    title: '段落',
    action: () => formatBlock('<p>'),
  },
  {
    name: 'quote',
    icon: <span>&#8220; &#8221;</span>,
    title: 'Quote',
    action: () => formatBlock('<blockquote>'),
  },
  {
    name: 'olist',
    icon: <OrderedListOutlined />,
    title: '有列列表',
    action: () => exec('insertOrderedList'),
  },
  {
    name: 'ulist',
    icon: <UnorderedListOutlined />,
    title: '无序列表',
    action: () => exec('insertUnorderedList'),
  },
  {
    name: 'code',
    icon: <CodeOutlined />,
    title: '代码',
    action: () => formatBlock('<pre>'),
  },
  {
    name: 'line',
    icon: <LineOutlined />,
    title: '水平线',
    action: () => exec('insertHorizontalRule'),
  },
  {
    name: 'link',
    icon: <LinkOutlined />,
    title: '链接',
    action: () => {
      const url = window.prompt('Enter the link URL')
      if (url) exec('createLink', url)
    },
  },
  //   image: {
  //     icon: <span>&#128247;</span>,
  //     title: 'Image',
  //     action: () => {
  //       const url = window.prompt('Enter the image URL')
  //       if (url) exec('insertImage', url)
  //     },
  //   },
]

const defaultEmpty = `<p></p>`
let selectedRange: Range | null = null

export default defineComponent({
  props: {
    modelValue: String,
    actions: Object as PropType<Record<string, Action>>,
    styleWithCSS: Boolean,
  },
  emits: ['update:modelValue'],
  setup(props, { emit }) {
    const contentRef = ref<HTMLDivElement>()

    watch(
      () => props.modelValue,
      (val) => {
        if (val !== getValue()) {
          setValue(val)
        }
      }
    )

    onMounted(() => {
      if (props.styleWithCSS) {
        exec('styleWithCSS')
      }
      setValue(props.modelValue)
    })

    const setValue = (value: string = defaultEmpty) => {
      if (contentRef.value) {
        contentRef.value.innerHTML = value.trim()
        emit('update:modelValue', value)
      }
    }

    const getValue = () => {
      return contentRef.value?.innerHTML.trim()
    }

    const execAction = (action: Action) => {
      // contentRef.value?.focus()
      restoreSelection()
      // action.active && action.active()
      action.action()
      // restoreSelection()
    }

    const saveSelection = () => {
      const sel = window.getSelection ? window.getSelection() : document.getSelection()
      if (sel && sel.rangeCount > 0) {
        selectedRange = sel.getRangeAt(0)
      }
    }

    const restoreSelection = () => {
      const sel = window.getSelection ? window.getSelection() : document.getSelection()
      if (sel && selectedRange) {
        sel?.removeAllRanges()
        sel?.addRange(selectedRange)
      }
    }

    const handleActive = () => {
      for (let a of actions) {
        const el = document.querySelector(`.${classes.button}-${a.name}`)
        el?.classList[a.active && a.active() ? 'add' : 'remove'](classes.selected)
      }
    }

    const handleKeydown = (e: KeyboardEvent) => {
      if (e.key === 'Enter' && queryCommandValue('formatBlock') === 'blockquote') {
        setTimeout(() => exec('formatBlock', '<p>'), 0)
      }
    }

    const handleChange = (e: Event) => {
      const firstChild = (e.target as HTMLElement).firstChild
      if (firstChild && firstChild.nodeType === Node.TEXT_NODE) {
        formatBlock('<p>')
      } else if (getValue() === '<br>') {
        setValue('')
      }
      emit('update:modelValue', getValue())
    }

    const handleBlur = () => {
      if (contentRef.value?.innerText.trim() == '') {
        setValue('')
      }
      saveSelection()
    }

    const handlePaste = (e: ClipboardEvent) => {
      e.stopPropagation()
      e.preventDefault()
      let html = e.clipboardData?.getData('text/html')
      if (html) {
        html = html.replace(/<!--.+?-->/g, '').replace(/style=".+?"/g, '')
        setValue(html)
      }
    }

    return () => (
      <div class={classes.editor}>
        <div class={classes.actionbar}>
          {actions.map((a) => (
            <span
              class={`${classes.button} ${classes.button}-${a.name}`}
              title={a.title}
              onClick={() => execAction(a)}
            >
              {a.icon}
            </span>
          ))}
        </div>
        <div
          ref={contentRef}
          class={classes.content}
          contenteditable
          onClick={handleActive}
          onKeyup={handleActive}
          onMouseup={handleActive}
          onKeydown={handleKeydown}
          onInput={handleChange}
          onBlur={handleBlur}
          onPaste={handlePaste}
        ></div>
      </div>
    )
  },
})
