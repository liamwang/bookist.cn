<script lang="tsx" setup>
import { computed, onMounted, reactive } from 'vue'
import { Table, Button, Popconfirm, Tag } from 'ant-design-vue'
import type { ColumnProps } from 'ant-design-vue/lib/table'
import { deleteBook, getBooks } from '../../api/book'
import useAsync from '../../utils/useAsync'

const emit = defineEmits<{
  (e: 'editClick', id: number): void
}>()

const { run, loading } = useAsync()

const data = reactive<any>({})

const pagination = computed(() => ({
  total: data.total,
  current: data.page,
  pageSize: data.size,
}))

const query = async (params = {}) => {
  const page = pagination.value.current || 1
  const size = pagination.value.pageSize || 10
  const result = await run(getBooks({ page, size, ...params }))
  Object.assign(data, result)
}
defineExpose({ query })

const handleTableChange = ({ current, pageSize }: any) => {
  query({ page: current, size: pageSize })
}

const handleDelete = async (id: number) => {
  await run(deleteBook(id))
  query()
}

onMounted(() => {
  query()
})

const columns: ColumnProps[] = [
  {
    title: '封面',
    dataIndex: 'coverUrlSm',
    width: 80,
    customRender: (r: any) => <img src={r.text} style="height:55px" />,
  },
  {
    title: '标题',
    dataIndex: 'title',
    // ellipsis: true,
    width: 320,
    customRender: ({ record }: any) => (
      <>
        <div>{record.title}</div>
        <div style="line-height:1.4;font-size:12px;color:#777;">{record.subtitle}</div>
      </>
    ),
  },
  {
    title: '作者',
    dataIndex: 'author',
    width: 120,
    ellipsis: true,
  },
  {
    title: '出版年月',
    dataIndex: 'pubDateStr',
    width: 100,
    align: 'center',
  },
  {
    title: '标签',
    dataIndex: 'tags',
    width: 140,
    customRender: ({ record }: any) => (
      <>
        {record.tags?.map((x: any) => (
          <Tag>{x.name}</Tag>
        ))}
      </>
    ),
  },
  {
    title: '文件格式',
    dataIndex: 'formats',
    width: 120,
  },
  {
    title: '下载连接',
    dataIndex: 'fetchUrl',
    width: 100,
    align: 'center',
    customRender: ({ text }: any) => (
      <a href={text} target="_blank">
        点击打开
      </a>
    ),
  },
  {
    title: '提取码',
    dataIndex: 'fetchCode',
    width: 100,
    align: 'center',
  },
  {
    title: '操作',
    dataIndex: 'operation',
    width: 135,
    align: 'center',
    customRender: ({ record }: any) => (
      <>
        <Button size="small" onClick={() => emit('editClick', record.id)} style="margin-right:5px">
          编辑
        </Button>
        <Popconfirm title="确定要删除吗？" onConfirm={() => handleDelete(record.id)}>
          <Button size="small" type="dashed">
            删除
          </Button>
        </Popconfirm>
      </>
    ),
  },
]
</script>

<template>
  <Table
    row-key="id"
    :columns="columns"
    :data-source="data.items"
    :pagination="pagination"
    :loading="loading"
    @change="handleTableChange"
  />
</template>
