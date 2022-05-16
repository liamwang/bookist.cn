<template>
  <Popover trigger="click" placement="bottomRight" :arrowPointAtCenter="true" :overlayStyle="{marginRight:'-20px'}">
    <template #content>
      <Empty description="暂无通知" />
    </template>
    <span class="action">
      <BellOutlined />
    </span>
  </Popover>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { Popover, Empty } from 'ant-design-vue'
import { BellOutlined } from '@ant-design/icons-vue'

export default defineComponent({
  components: { Popover, Empty, BellOutlined },
  setup() {},
})
</script>
