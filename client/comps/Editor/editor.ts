const formatBlock = 'formatBlock'
const createElement = (tag: string) => document.createElement(tag)
const queryCommandState = (command: string) => document.queryCommandState(command)
const queryCommandValue = (command: string) => document.queryCommandValue(command)

export const exec = (command: string, value?: string) => document.execCommand(command, false, value)

const defaultActions = {
  bold: {
    icon: '<b>B</b>',
    title: 'Bold',
    state: () => queryCommandState('bold'),
    result: () => exec('bold'),
  },
  italic: {
    icon: '<i>I</i>',
    title: 'Italic',
    state: () => queryCommandState('italic'),
    result: () => exec('italic'),
  },
  underline: {
    icon: '<u>U</u>',
    title: 'Underline',
    state: () => queryCommandState('underline'),
    result: () => exec('underline'),
  },
  strikethrough: {
    icon: '<strike>S</strike>',
    title: 'Strike-through',
    state: () => queryCommandState('strikeThrough'),
    result: () => exec('strikeThrough'),
  },
  heading1: {
    icon: '<b>H<sub>1</sub></b>',
    title: 'Heading 1',
    result: () => exec(formatBlock, '<h1>'),
  },
  heading2: {
    icon: '<b>H<sub>2</sub></b>',
    title: 'Heading 2',
    result: () => exec(formatBlock, '<h2>'),
  },
  paragraph: {
    icon: '&#182;',
    title: 'Paragraph',
    result: () => exec(formatBlock, '<p>'),
  },
  quote: {
    icon: '&#8220; &#8221;',
    title: 'Quote',
    result: () => exec(formatBlock, '<blockquote>'),
  },
  olist: {
    icon: '&#35;',
    title: 'Ordered List',
    result: () => exec('insertOrderedList'),
  },
  ulist: {
    icon: '&#8226;',
    title: 'Unordered List',
    result: () => exec('insertUnorderedList'),
  },
  code: {
    icon: '&lt;/&gt;',
    title: 'Code',
    result: () => exec(formatBlock, '<pre>'),
  },
  line: {
    icon: '&#8213;',
    title: 'Horizontal Line',
    result: () => exec('insertHorizontalRule'),
  },
  //   link: {
  //     icon: '&#128279;',
  //     title: 'Link',
  //     result: () => {
  //       const url = window.prompt('Enter the link URL')
  //       if (url) exec('createLink', url)
  //     },
  //   },
  //   image: {
  //     icon: '&#128247;',
  //     title: 'Image',
  //     result: () => {
  //       const url = window.prompt('Enter the image URL')
  //       if (url) exec('insertImage', url)
  //     },
  //   },
}

const defaultClasses = {
  actionbar: 'aneditor-actionbar',
  button: 'aneditor-button',
  content: 'aneditor-content',
  selected: 'aneditor-button-selected',
}

interface Settings {
  element: HTMLElement
  initialValue?: string
  actions?: any[]
  styleWithCSS?: boolean
  classes?: any
  onChange?: (html: string) => void
}

export const init = (settings: Settings) => {
  const actions = settings.actions
    ? settings.actions.map((action) => {
        if (typeof action === 'string') {
          return defaultActions[action]
        } else if (defaultActions[action.name]) {
          return { ...defaultActions[action.name], ...action }
        }
        return action
      })
    : Object.keys(defaultActions).map((action) => defaultActions[action])

  const classes = { ...defaultClasses, ...settings.classes }

  settings.element.className = 'aneditor'

  const actionbar = createElement('div')
  actionbar.className = classes.actionbar
  settings.element.appendChild(actionbar)

  const content = createElement('div')
  content.contentEditable = 'true'
  content.className = classes.content
  content.innerHTML = settings.initialValue || ''
  content.oninput = (e: Event) => {
    const firstChild = (e.target as HTMLElement).firstChild
    if (firstChild && firstChild.nodeType === Node.TEXT_NODE) {
      exec(formatBlock, `<p>`)
    } else if (content.innerHTML === '<br>') {
      content.innerHTML = ''
    }
    settings.onChange && settings.onChange(content.innerHTML)
  }
  content.onkeydown = (e) => {
    if (e.key === 'Enter' && queryCommandValue(formatBlock) === 'blockquote') {
      setTimeout(() => exec(formatBlock, `<p>`), 0)
    }
  }
  settings.element.appendChild(content)

  actions.forEach((action) => {
    const button = createElement('button')
    button.className = classes.button
    button.innerHTML = action.icon
    button.title = action.title
    button.setAttribute('type', 'button')
    button.onclick = () => action.result() && content.focus()

    if (action.state) {
      const handler = () => button.classList[action.state() ? 'add' : 'remove'](classes.selected)
      content.addEventListener('keyup', handler)
      content.addEventListener('mouseup', handler)
      content.addEventListener('click', handler)
    }

    content.onblur = (e) => {
      if (content.innerText.trim() == '') {
        content.innerText = ''
      }
    }

    actionbar.appendChild(button)
  })

  if (settings.styleWithCSS) {
    exec('styleWithCSS')
  }

  exec(formatBlock, '<p>')

  return content
}

export default { exec, init }
