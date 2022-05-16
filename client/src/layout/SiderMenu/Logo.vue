<template>
  <div :class="['SiderMenu-Logo', menuCollapsed ? 'collapsed' : '']">
    <img :src="logo" alt="Logo" />
    <span v-show="!menuCollapsed">极客精神管理后台</span>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { menuCollapsed } from '../../store'
import logo from '../../assets/logo.png'

export default defineComponent({
  setup() {
    return {
      logo,
      menuCollapsed,
    }
  },
})
</script>
