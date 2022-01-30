<script lang="ts" setup>
import './editor.less'
import { onMounted, onUnmounted, ref, watch, watchEffect } from 'vue'
import { init } from './editor'

const props = defineProps<{
  modelValue: string
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', html: string): void
}>()

let editorContentEl: HTMLElement
const editorRef = ref<HTMLDivElement>()

watchEffect(() => {
  if (editorContentEl) {
    editorContentEl.innerHTML = props.modelValue || ''
  }
})

onMounted(() => {
  editorContentEl = init({
    element: editorRef.value as HTMLElement,
    initialValue: props.modelValue,
    onChange: (html) => emit('update:modelValue', html),
  })
})
</script>

<template>
  <div ref="editorRef"></div>
</template>
