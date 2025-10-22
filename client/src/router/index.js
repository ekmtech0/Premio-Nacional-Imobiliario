
import MainHome from '@/Publico/MainHome.vue'
import NavBar from '@/componentes/NavBar.vue'
import SobrePremio from '@/Publico/SobrePremio.vue'
import categoriaPremio from '@/Publico/categoriaPremio.vue'
import NomeadosPremio from '@/Publico/NomeadosPremio.vue'
import VotacaoPublic from '@/Publico/VotacaoPublic.vue'
import JuriSelecao from '@/Publico/JuriSelecao.vue'
import ResultadosAtuais from '@/Publico/ResultadosAtuais.vue'
import ParceirosPNI from '@/Publico/ParceirosPNI.vue'
import ContactosPNI from '@/Publico/ContactosPNI.vue'
import FooterPNI from '@/Publico/FooterPNI.vue'

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
    component:NomeadosPremio,
  },
  {
    path: '/VotacaoPublic',
    name: 'VotacaoPublic',
    component: VotacaoPublic,
  },
  {
    path:'/JuriSelecao',
    name:'JuriSelecao',
    component: JuriSelecao,
  },
  {
    path: '/ ResultadosAtuais',
    name: ' ResultadosAtuais',
    component: ResultadosAtuais,
  },

    {
    path: '/ ParceirosPNI',
    name: ' ParceirosPNI',
    component: ParceirosPNI,
  },
  {
      path:'/ContactosPNI',
      name:'ContactosPNI',
      component: ContactosPNI,
  },
  {
    path:'/FooterPNI',
    name:'FooterPNI',
    component: FooterPNI,
  }
  
  
]

//  Dantas, não exportes o router aqui Devido o Prerender.


// ✅ Exporta só o array:
export default routes
