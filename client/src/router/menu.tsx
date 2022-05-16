import type Menu from '../models/Menu'

import { AppstoreAddOutlined, TagsOutlined } from '@ant-design/icons-vue'

export const homeMenu: Menu = {
  name: '书籍管理',
  path: '/books',
  icon: <AppstoreAddOutlined />,
  component: () => import('../pages/book/index.vue'),
}

const menu: Menu[] = [
  homeMenu,
  {
    name: '标签管理',
    path: '/tags',
    icon: <TagsOutlined />,
    component: () => import('../pages/tag/index.vue'),
  },
]

export default menu
