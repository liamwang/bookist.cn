<template>
  <LayoutSider class="SiderMenu" :trigger="null" :collapsed="collapsed" collapsible>
    <Logo />
    <Menu />
  </LayoutSider>
</template>

<script lang="ts">
import './index.less'

import { defineComponent } from 'vue'
import { Layout } from 'ant-design-vue'

import Menu from './Menu'
import Logo from './Logo.vue'
import { menuCollapsed } from '../../store'

export default defineComponent({
  components: { LayoutSider: Layout.Sider, Logo, Menu },
  setup() {
    return {
      collapsed: menuCollapsed,
    }
  },
})
</script>
