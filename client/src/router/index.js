
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
import DashboardPNI from '@/ADM/DashboardPNI.vue'
import LoginPNI from '@/ADM/LoginPNI.vue'
import VerficarEmail from '@/Publico/VerficarEmail.vue'
import SideBar from '@/ADM/SideBar.vue'
import CategoriasADM from '@/ADM/CategoriasADM.vue'
import NomeadosADM from '@/ADM/NomeadosADM.vue'
import VotacaoADM from '@/ADM/VotacaoADM.vue'
import ResultadoADM from '@/ADM/ResultadoADM.vue'
import UsuarioADM from '@/ADM/UsuarioADM.vue'
import HeaderADM from '@/ADM/HeaderADM.vue'
import ProcessarPNI from '@/componentes/ProcessarPNI.vue'
import AdmBaseLayout from '@/ADM/AdmBaseLayout.vue'

export const routes = [
  { path: '/', name: 'home', component: MainHome },
  { path: '/NavBar', name: 'NavBar', component: NavBar },
  { path: '/SobrePremio', name: 'SobrePremio', component: SobrePremio },
  { path: '/categoriaPremio', name: 'categoriaPremio', component: categoriaPremio },
  { path: '/NomeadosPremio', name: 'NomeadosPremio', component: NomeadosPremio },
  { path: '/VotacaoPublic', name: 'VotacaoPublic', component: VotacaoPublic },
  { path: '/JuriSelecao', name: 'JuriSelecao', component: JuriSelecao },
  { path: '/ResultadosAtuais', name: 'ResultadosAtuais', component: ResultadosAtuais },
  { path: '/ParceirosPNI', name: 'ParceirosPNI', component: ParceirosPNI },

  {
    name: 'AdmLayout',
    path: '/adm',
    component: AdmBaseLayout,
    children: [
      { path: 'DashboardPNI', name: 'DashboardPNI', component: DashboardPNI },
      { path: 'CategoriasADM', name: 'CategoriasADM', component: CategoriasADM },
      { path: 'NomeadosADM', name: 'NomeadosADM', component: NomeadosADM },
      { path: 'VotacaoADM', name: 'VotacaoADM', component: VotacaoADM },
      { path: 'ResultadoADM', name: 'ResultadoADM', component: ResultadoADM },
      { path: 'UsuarioADM', name: 'UsuarioADM', component: UsuarioADM },
      { path: 'HeaderADM', name: 'HeaderADM', component: HeaderADM },
    ],
  },

  { path: '/ContactosPNI', name: 'ContactosPNI', component: ContactosPNI },
  { path: '/FooterPNI', name: 'FooterPNI', component: FooterPNI },
  { path: '/login', name: 'LoginPNI', component: LoginPNI },
  { path: '/VerficarEmail', name: 'VerficarEmail', component: VerficarEmail },
  { path: '/SideBar', name: 'SideBar', component: SideBar },
  { path: '/ProcessaPNI', name: 'ProcessarPNI', component: ProcessarPNI },
]

