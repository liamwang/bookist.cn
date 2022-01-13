import type { SubMenu } from '../../models/Menu'
import type MenuModel from '../../models/Menu'

export function isSubMenu(menu: MenuModel): boolean {
  return !!(menu as SubMenu).children
}

export function isPathInMenu(path: string, menu: SubMenu): boolean {
  if (path === menu.path) {
    return true
  }
  for (const item of menu.children) {
    if (isSubMenu(item) && isPathInMenu(path, item as SubMenu)) {
      return true
    } else if (item.path === path) {
      return true
    }
  }
  return false
}

// 获取打开菜单Key数组
export function getOpenMenuKeys(menuKey: string, subMenu: SubMenu): string[] {
  let result: string[] = []
  if (isPathInMenu(menuKey, subMenu)) {
    result.push(subMenu.path)
    for (const item of subMenu.children.filter(isSubMenu)) {
      let childrenResult = getOpenMenuKeys(menuKey, item as SubMenu)
      result = result.concat(childrenResult)
    }
  }
  return result
}

// 获取菜单路径（由上到下）
export function getMenuPathArray(
  destKey: string,
  menuData: MenuModel[]
): MenuModel[] {
  for (const item of menuData) {
    if (item.path == destKey) {
      return [item]
    }
    const subMenu = item as SubMenu
    if (subMenu.children && isPathInMenu(destKey, subMenu)) {
      const childrenResult = getMenuPathArray(destKey, subMenu.children)
      return [item].concat(childrenResult)
    }
  }
  return []
}
