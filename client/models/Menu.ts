import type { Component } from 'vue'
import type { RouteComponent } from 'vue-router'

declare type Lazy<T> = () => Promise<T>

export interface MenuItem {
  name: string
  path: string
  component: Lazy<RouteComponent>
  icon?: Component<any>
  tag?: {
    dot: boolean
    content: string
    type: 'error' | 'primary' | 'warn' | 'success'
  }
}

export interface SubMenu {
  name: string
  path: string
  icon?: Component<any>
  children: Menu[]
  tag?: {
    dot: boolean
    content: string
    type: 'error' | 'primary' | 'warn' | 'success'
  }
}

declare type Menu = MenuItem | SubMenu

export default Menu
