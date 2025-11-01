import { ViteSSG } from 'vite-ssg'
import App from './App.vue'
import routes from './router'
import './main.css'
import { createHead } from '@vueuse/head'
import { MotionPlugin } from '@vueuse/motion'
import { VueReCaptcha } from 'vue-recaptcha-v3';
import AOS from 'aos'
import 'aos/dist/aos.css'

// Criar o gerenciador de metadados (SEO)
const head = createHead()
// Registrar plugins e criar a app via ViteSSG
// NOTA: não usar `app` aqui antes da criação — registre plugins dentro do callback `ctx`.
// Usa variável de ambiente VITE_RECAPTCHA_SITE_KEY quando disponível.
export const createApp = ViteSSG(
  App,
  {
    routes,
    base: import.meta.env.BASE_URL || '/Premio-Imobiliario-Nacional/',
  },
  (ctx) => {
    ctx.app.use(head)
    ctx.app.use(MotionPlugin)
    // Registrar plugin de reCAPTCHA v3 apenas se explicitamente configurado.
    // Isso evita conflitos quando componentes usam o widget v2 (checkbox).
    if (import.meta.env.VITE_RECAPTCHA_MODE === 'v3') {
      ctx.app.use(VueReCaptcha, {
        siteKey: import.meta.env.VITE_RECAPTCHA_SITE_KEY || '6LcEIfYrAAAAH9ORuP38fdKRkmRP_GZAu9L48hL'
      })
    }
  }
)

// Inicializa o AOS
AOS.init({
  duration: 800, // duração da animação em ms
  easing: 'ease-in-out',
  once: true, // anima apenas na primeira vez
  offset: 100, // distância antes de começar a animação
})
