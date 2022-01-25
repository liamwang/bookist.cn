<script lang="ts" setup>
import { ref } from 'vue'
import { Card, Form, Input, Button } from 'ant-design-vue'
import BookTable from './BookTable.vue'
import EditDrawer from './EditDrawer.vue'

const keyword = ref('')
const bookTableRef = ref<InstanceType<typeof BookTable>>()
const editDrawerRef = ref<InstanceType<typeof EditDrawer>>()

const openEdit = (id: number = 0) => {
  editDrawerRef.value?.open(id)
}

const query = () => {
  bookTableRef.value?.query({ keyword: keyword.value })
}
</script>

<template>
  <Card style="margin-bottom: 15px">
    <Form layout="inline">
      <Form.Item label="关键字">
        <Input
          placeholder="书名/作者"
          v-model:value="keyword"
          :allow-clear="true"
          style="width: 240px"
        />
      </Form.Item>
      <Form.Item>
        <Button type="primary" @click="query">搜索</Button>
      </Form.Item>
      <Button @click="openEdit()">新增</Button>
    </Form>
  </Card>
  <Card class="p-book-index">
    <BookTable ref="bookTableRef" @edit-click="openEdit" />
  </Card>
  <EditDrawer ref="editDrawerRef" @finish="query()" />
</template>

<style lang="less">
.p-book-index {
  .ant-table-row td {
    padding-top: 5px;
    padding-bottom: 5px;
  }
}
</style>
