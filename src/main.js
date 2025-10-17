import { ViteSSG } from 'vite-ssg'
import App from './App.vue'
import routes from './router'
import './main.css'
import { createHead } from '@vueuse/head'
import { MotionPlugin } from '@vueuse/motion'




// Criar o gerenciador de metadados (SEO)
const head = createHead()

// Criar e exportar a app com suporte a Static Site Generation (SSG)
export const createApp = ViteSSG(
  App,
  {
    routes,
    // o vite-ssg já cria internamente o router com base nisso
  },
  (ctx) => {
    // Aqui adicionamos plugins, libs globais, etc.
      ctx.app.use(head)
        ctx.app.use(MotionPlugin)
  }
)
