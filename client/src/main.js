import { ViteSSG } from 'vite-ssg'
import App from './App.vue'
import routes from './router'
import './main.css'
import { createHead } from '@vueuse/head'
import { MotionPlugin } from '@vueuse/motion'

// Criar o gerenciador de metadados (SEO)
const head = createHead()

// ✅ Define explicitamente o base path
export const createApp = ViteSSG(
  App,
  {
    routes,
    base: import.meta.env.BASE_URL || '/Premio-Imobiliario-Nacional/',
  },
  (ctx) => {
    ctx.app.use(head)
    ctx.app.use(MotionPlugin)
  }
)
