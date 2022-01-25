import { createApp } from 'vue'
import router from './router'
import App from './layout/App.vue'
import { regGlobalComponents } from './comps/global'

const app = createApp(App)

regGlobalComponents(app)

app.use(router)
app.mount('#app')
