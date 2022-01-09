import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  base: '/admin/',
  build: {
    outDir: 'release/wwwroot/admin',
  },
  server: {
    proxy: {
      '^/(?!admin)': 'http://localhost:5000',
    },
  },
})
