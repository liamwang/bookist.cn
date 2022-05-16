<template>
  <ConfigProvider :locale="locale">
    <RouterView />
  </ConfigProvider>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { ConfigProvider } from 'ant-design-vue'

import zhCN from 'ant-design-vue/es/locale/zh_CN'
import dayjs from 'dayjs'
import 'dayjs/locale/zh-cn'
dayjs.locale('zh-cn')

export default defineComponent({
  name: 'App',
  components: { ConfigProvider },
  setup() {
    return {
      locale: zhCN,
    }
  },
})
</script>

<style>
#app {
  height: 100%;
}
</style>
