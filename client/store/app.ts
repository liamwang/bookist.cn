import { ref, reactive } from 'vue'
import type Menu from '../models/Menu'
import type UserInfo from '../models/UserInfo'

export const menuCollapsed = ref<boolean>(false)

export const menuPathArray = ref<Menu[]>([])

export const userInfo = reactive<UserInfo>({
  displayName: 'Admin',
})
