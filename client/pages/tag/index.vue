<script lang="ts" setup>
import { onMounted, reactive, ref, toRaw } from 'vue'
import { Card, Form, Input, Button, Modal, Spin, Popconfirm, Empty, Row, Col } from 'ant-design-vue'
import { EditOutlined, DeleteOutlined } from '@ant-design/icons-vue'
import { deleteTag, getTags, saveTag } from '../../api/tag'
import useAsync from '../../utils/useAsync'

const keyword = ref('')
const tags = ref<any[]>([])
const modalVisible = ref(false)

const form = reactive({ id: 0, name: '', slug: '' })
const { resetFields, validate, validateInfos } = Form.useForm(
  form,
  reactive({
    name: [{ required: true, message: '此项不能为空' }],
    slug: [{ required: true, message: '此项不能为空' }],
  })
)

const { run, loading } = useAsync()

const query = async () => {
  tags.value = await run(getTags({ keyword: keyword.value }))
}

const openEdit = (tag?: object) => {
  resetFields(tag)
  modalVisible.value = true
}

const handleSubmit = async () => {
  try {
    await validate()
    await run(saveTag(toRaw(form)))
    modalVisible.value = false
    query()
  } catch (e) {}
}
const handleDelete = async (id: number) => {
  await run(deleteTag(id))
  query()
}

onMounted(() => {
  query()
})
</script>

<template>
  <Card style="margin-bottom: 15px">
    <Form layout="inline">
      <Form.Item label="关键字">
        <Input v-model:value="keyword" :allow-clear="true" style="width: 240px" />
      </Form.Item>
      <Form.Item>
        <Button type="primary" @click="query">搜索</Button>
      </Form.Item>
      <Button @click="openEdit()">新增</Button>
    </Form>
  </Card>

  <Card>
    <Spin :spinning="loading">
      <Empty v-if="tags.length == 0" />
      <Row :gutter="20">
        <Col :span="4" v-for="tag in tags">
          <Card size="small" style="margin-bottom: 15px">
            <Card.Meta :title="tag.name" :description="`${tag.slug} · (${tag.bookCount})`" />
            <template #actions>
              <EditOutlined @click="openEdit(tag)" />
              <Popconfirm title="确定要删除吗？" @confirm="handleDelete(tag.id)">
                <DeleteOutlined />
              </Popconfirm>
            </template>
          </Card>
        </Col>
      </Row>
    </Spin>
  </Card>

  <Modal
    v-model:visible="modalVisible"
    :title="form.id ? '编辑标签' : '新增标签'"
    :confirm-loading="loading"
    @ok="handleSubmit"
  >
    <Form :model="form" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
      <Form.Item label="标签名" v-bind="validateInfos.name">
        <Input v-model:value="form.name" />
      </Form.Item>
      <Form.Item label="标签Slug" v-bind="validateInfos.slug">
        <Input v-model:value="form.slug" />
      </Form.Item>
    </Form>
  </Modal>
</template>

<style lang="less">
.ant-card-actions > li {
  margin: 6px 0;
}
</style>
