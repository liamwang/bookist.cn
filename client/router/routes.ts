import type { RouteRecordRaw } from 'vue-router'
import Menu, { MenuItem, SubMenu } from '../models/Menu'
import menu, { homeMenu } from './menu'

const Layout = () => import('../layout/AppLayout.vue')

const menuRoutes: RouteRecordRaw[] = []

const mapRoute = (menu: Menu) => {
  const subMenu = menu as SubMenu
  if (subMenu.children) {
    for (const item of subMenu.children) {
      mapRoute(item)
    }
  } else {
    const menuItem = menu as MenuItem
    menuRoutes.push({
      path: menuItem.path,
      component: menuItem.component,
    })
  }
}

menu.forEach(mapRoute)

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: Layout,
    redirect: homeMenu.path,
    children: menuRoutes,
  },
]

export default routes
