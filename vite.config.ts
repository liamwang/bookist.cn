import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueJsx from '@vitejs/plugin-vue-jsx'
import styleImport from 'vite-plugin-style-import'

const stylePlugin = styleImport({
  libs: [
    {
      libraryName: 'ant-design-vue',
      esModule: true,
      resolveStyle: (name) => {
        return `ant-design-vue/es/${name}/style`
      },
    },
  ],
})

export default defineConfig({
  plugins: [vue(), vueJsx(), stylePlugin],
  base: '/admin/',
  publicDir: 'client/public',
  build: {
    outDir: 'release/wwwroot/admin',
  },
  server: {
    proxy: {
      '^/(?!admin)': 'http://localhost:5000',
    },
  },
  css: {
    preprocessorOptions: {
      less: {
        javascriptEnabled: true,
      },
    },
  },
})
