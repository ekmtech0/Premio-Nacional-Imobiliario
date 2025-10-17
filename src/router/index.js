import { createRouter, createWebHistory } from 'vue-router'
import MainHome from '@/Publico/MainHome.vue'
import NavBar from '@/componentes/NavBar.vue'
import SobrePremio from '@/Publico/SobrePremio.vue'
import categoriaPremio from '@/Publico/categoriaPremio.vue'
import NomeadosPremio from '@/Publico/categoriaPremio.vue'

export const routes = [
  {
    path: '/',
    name: 'home',
    component: MainHome,
  },
  {
    path: '/NavBar',
    name: 'NavBar',
    component: NavBar,
  },
  {
    path: '/SobrePremio',
    name: 'SobrePremio',
    component: SobrePremio,
  },
  {
    path: '/categoriaPremio',
    name: 'categoriaPremio',
    component: categoriaPremio,
  },
  {
    path: '/NomeadosPremio',
    name: 'NomeadosPremio',
    component:'NomeadosPremio'
  },
]

// ❌ NÃO exportes o router aqui
// const router = createRouter({
//   history: createWebHistory(import.meta.env.BASE_URL),
//   routes,
//   scrollBehavior(to) {
//     if (to.hash) {
//       return { el: to.hash, behavior: 'smooth' }
//     }
//     return { top: 0 }
//   },
// })

// ✅ Exporta só o array:
export default routes
