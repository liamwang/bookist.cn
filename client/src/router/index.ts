import { createRouter, createWebHashHistory } from 'vue-router'
import routes from './routes'

const publicPath = '/'

const router = createRouter({
  history: createWebHashHistory(publicPath),
  routes: routes,
  strict: true,
  scrollBehavior: () => ({ left: 0, top: 0 }),
})

export default router
