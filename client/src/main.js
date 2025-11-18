// main.js
import { ViteSSG } from 'vite-ssg'
import App from './App.vue'
import { routes } from './router'
import './main.css'

import { createHead } from '@vueuse/head'
import { MotionPlugin } from '@vueuse/motion'
import { VueReCaptcha } from 'vue-recaptcha-v3'
import AOS from 'aos'
import 'aos/dist/aos.css'

import { http } from '@/Request/api'

// SEO + head manager
const head = createHead()

// Criar a aplicação com ViteSSG
export const createApp = ViteSSG(
  App,
  {
    routes,
    base: import.meta.env.BASE_URL || '/',
  },
  (ctx) => {
    // Plugins globais
    ctx.app.use(head)
    ctx.app.use(MotionPlugin)

    // Registrar reCAPTCHA v3 se ativo no .env
    if (import.meta.env.VITE_RECAPTCHA_MODE === 'v3') {
      ctx.app.use(VueReCaptcha, {
        siteKey: import.meta.env.VITE_RECAPTCHA_SITE_KEY
      })
    }

    // 🔥 Guard global para /adm
    ctx.router.beforeEach(async (to, from, next) => {
      if (to.path.startsWith('/adm')) {
        const isLoggedIn = await checkAuth()
        if (!isLoggedIn) {
          return next({ name: 'LoginPNI' })
        }
      }
      next()
    })
  }
)

// Inicializa animações AOS
AOS.init({
  duration: 800,
  easing: 'ease-in-out',
  once: true,
  offset: 100,
})

// Verifica login via cookie HttpOnly
async function checkAuth() {
  try {
    const res = await http.get('adm/me', { withCredentials: true })
    console.log(res.data)
    return !!res.data
  } catch {
    return false
  }
}
