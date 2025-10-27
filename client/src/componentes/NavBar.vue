<template>
  <header class="bg-azul text-white shadow-xl fixed top-0 left-0 w-full z-50">
    <div class="max-w-7xl mx-auto flex items-center justify-between px-4 lg:px-8 h-20">
      <!-- LOGO -->
      <img src="" alt="Logotipo do Prémio Nacional Imobiliário" class="h-12 w-auto" />

      <!-- BOTÃO MOBILE -->
      <button
        @click="toggleMenu"
        class="lg:hidden flex items-center justify-center w-10 h-10 text-white focus:outline-none border rounded-lg"
      >
        <svg v-if="!menuAberto" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" class="w-6 h-6">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M4 6h16M4 12h16M4 18h16"/>
        </svg>
        <svg v-else xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" class="w-6 h-6">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M6 18L18 6M6 6l12 12"/>
        </svg>
      </button>

      <!-- NAV DESKTOP -->
      <nav class="hidden lg:flex space-x-4 text-base font-medium">
        <a href="/" class="hover:text-gray-200 transition-colors">Início</a>
        <a href="/#sobre" class="hover:text-gray-200 transition-colors">Sobre</a>
        <a @click="irPara('/categoriaPremio')" class="hover:text-gray-200 transition-colors cursor-pointer">Categorias</a>
        <a href="/#nomeado" class="hover:text-gray-200 transition-colors">Nomeados</a>
        <a href="/#votacao" class="hover:text-gray-200 transition-colors">Votação</a>
        <a @click="irPara('/JuriSelecao')" class="hover:text-gray-200 transition-colors cursor-pointer">Júri</a>
        <a href="/resultado" class="hover:text-gray-200 transition-colors">Resultados</a>
        <a href="/#contato" class="hover:text-gray-200 transition-colors">Contactos</a>
      </nav>
    </div>

    <!-- ✅ SOMENTE O FUNDO DA PÁGINA FICA ESCURO (NAVBAR SEM DESFOQUE, COMPLETAMENTE VISÍVEL) -->
    <div
      v-if="menuAberto"
      @click="fecharMenu"
      class="fixed inset-0 top-20 bg-black/40 z-40 lg:hidden"
    ></div>

    <!-- ✅ MENU MOBILE, TOTALMENTE VISÍVEL JUNTO COM A NAVBAR -->
    <transition name="slide-fade">
      <nav
        v-if="menuAberto"
        class="lg:hidden flex flex-col items-center space-y-4 py-6 bg-azul text-white text-lg font-medium border-t border-white/20 absolute top-20 left-0 w-full z-50"
        @click.stop
      >
        <RouterLink @click.native="fecharMenu" to="/">Início</RouterLink>
        <RouterLink @click.native="fecharMenu" to="/#sobre">Sobre</RouterLink>
        <RouterLink @click.native="fecharMenu" to="/categoriaPremio">Categorias</RouterLink>
        <RouterLink @click.native="fecharMenu" to="/#nomeado">Nomeados</RouterLink>
        <RouterLink @click.native="fecharMenu" to="/#votacao">Votação</RouterLink>
        <RouterLink @click.native="fecharMenu" to="/JuriSelecao">Júri</RouterLink>
        <RouterLink @click.native="fecharMenu" to="/resultado">Resultados</RouterLink>
        <RouterLink @click.native="fecharMenu" to="/#contato">Contactos</RouterLink>
      </nav>
    </transition>
  </header>
</template>



<script setup>
import { ref, watch,  onBeforeUnmount } from 'vue'
import { useRouter } from 'vue-router'

const menuAberto = ref(false)
const router = useRouter()

function irPara(path) {
  router.push(path)
  fecharMenu()
}

function toggleMenu() {
  menuAberto.value = !menuAberto.value
}

// Fechar menu
function fecharMenu() {
  menuAberto.value = false
}

// BLOQUEAR SCROLL QUANDO MENU ABRE
watch(menuAberto, (valor) => {
  if (valor) {
    document.body.classList.add('overflow-hidden')
  } else {
    document.body.classList.remove('overflow-hidden')
  }
})

// Remover quando componente é destruído
onBeforeUnmount(() => {
  document.body.classList.remove('overflow-hidden')
})
</script>

<style scoped>
.slide-fade-enter-active,
.slide-fade-leave-active {
  transition: all 0.3s ease;
}
.slide-fade-enter-from,
.slide-fade-leave-to {
  transform: translateY(-10px);
  opacity: 0;
}
</style>
