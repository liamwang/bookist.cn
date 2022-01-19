<template>
  <Layout class="AppLayout">
    <SiderMenu />
    <Layout>
      <HeaderBar />
      <router-view v-slot="{ Component }">
        <transition name="fade" mode="out-in" appear>
          <component :is="Component" />
        </transition>
      </router-view>
    </Layout>
  </Layout>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { Layout } from 'ant-design-vue'
import HeaderBar from './HeaderBar.vue'
import SiderMenu from './SiderMenu/index.vue'

export default defineComponent({
  components: {
    Layout,
    HeaderBar,
    SiderMenu,
  },
  setup() {
    return {}
  },
})
</script>

<style lang="less">
.AppLayout {
  height: 100%;
}

.fade-enter-active,
.fade-leave-active {
  transition: all 0.25s ease;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
