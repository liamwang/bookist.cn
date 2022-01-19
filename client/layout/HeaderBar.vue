<template>
  <header id="app-Header">
    <div class="left">
      <MenuUnfoldOutlined v-if="collapsed" class="action" @click="toggleCollapsed" />
      <MenuFoldOutlined v-else class="action" @click="toggleCollapsed" />
      <Breadcrumb />
    </div>
    <div class="right">
      <Notification />
      <UserDropdown />
    </div>
  </header>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { MenuUnfoldOutlined, MenuFoldOutlined } from '@ant-design/icons-vue'
import { menuCollapsed } from '../store/app'
import UserDropdown from './UserDropdown.vue'
import Notification from './Notification.vue'
import Breadcrumb from './Breadcrumb.vue'

export default defineComponent({
  components: {
    MenuUnfoldOutlined,
    MenuFoldOutlined,
    Breadcrumb,
    UserDropdown,
    Notification,
  },

  setup() {
    const toggleCollapsed = () => {
      menuCollapsed.value = !menuCollapsed.value
    }

    return {
      collapsed: menuCollapsed,
      toggleCollapsed,
    }
  },
})
</script>

<style lang="less">
@header-height: 48px;
#app-Header {
  position: relative;
  display: flex;
  justify-content: space-between;
  height: @header-height;
  padding: 0;
  background: #fff;
  border-bottom: 1px solid #eee;
  padding-right: 5px;

  .action {
    display: flex;
    align-items: center;
    height: 100%;
    padding: 0 10px;
    cursor: pointer;
    transition: color 0.3s;
    user-select: none;
    &.anticon,
    > .anticon {
      color: rgba(0, 0, 0, 0.8);
      font-size: 18px;
    }
    &:hover {
      background: #f6f6f6;
    }
  }

  .left {
    display: flex;
    align-items: center;
    .ant-breadcrumb {
      padding: 0 8px;
    }
  }

  .right {
    display: flex;
    align-items: center;
  }
}
</style>
