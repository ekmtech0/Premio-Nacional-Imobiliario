<template>
  <header class="bg-azul text-white shadow-xl">
    <div class="max-w-7xl mx-auto flex items-center justify-between px-4 lg:px-8 h-20">
      <!-- LOGO -->
      <img
        src=""
        alt="Logotipo do Prémio Nacional Imobiliário"
        class="h-12 w-auto"
      />

      <!-- BOTÃO MOBILE -->
      <button
        @click="menuAberto = !menuAberto"
        class="lg:hidden flex items-center justify-center w-10 h-10 text-white focus:outline-none border rounded-lg"
      >
        <svg
          v-if="!menuAberto"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          class="w-6 h-6"
        >
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M4 6h16M4 12h16M4 18h16"/>
        </svg>
        <svg
          v-else
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          class="w-6 h-6"
        >
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M6 18L18 6M6 6l12 12"/>
        </svg>
      </button>

      <!-- NAVIGATION DESKTOP -->
      <nav class="hidden lg:flex space-x-4 text-base font-medium">
        <a href="/"  class="hover:text-gray-200 transition-colors">Início</a>
        <a href="/#sobre" class="whitespace-nowrap hover:text-gray-200 transition-colors"> Sobre o Prémio</a>
        <a @click="irPara('/categoriaPremio')" class="whitespace-nowrap hover:text-gray-200 transition-colors cursor-pointer"> Categorias</a>
        <a href="/#nomeado" class="whitespace-nowrap hover:text-gray-200 transition-colors"> Nomeados</a>
        <a href="/#votacao" class="whitespace-nowrap hover:text-gray-200 transition-colors"> Votação do Público</a>
        <a @click="irPara('/JuriSelecao')" class="whitespace-nowrap hover:text-gray-200 transition-colors cursor-pointer">Júri e Seleção</a>
        <a href="resultado" class="whitespace-nowrap hover:text-gray-200 transition-colors"> Resultados</a>
        <a href="/#contato" class="whitespace-nowrap hover:text-gray-200 transition-colors"> Contactos</a>
      </nav>
    </div>


    <!-- MENU MOBILE -->
    <transition name="slide-fade">
      
      <nav
        v-if="menuAberto"
        class="lg:hidden flex flex-col items-center space-y-4 py-6 bg-azul text-white text-lg font-medium border-t border-white/20 fixed top-20 left-0 w-full z-50"
      >
   
        <RouterLink @click="fecharMenu" to="/">Início</RouterLink>
        <RouterLink @click="fecharMenu" to="/">Sobre o Prémio</RouterLink>
        <RouterLink @click="fecharMenu" to="/categoriaPremio">Categorias</RouterLink>
        <RouterLink @click="fecharMenu" to="/JuriSelecao">Nomeados</RouterLink>
        <RouterLink @click="fecharMenu" to="/">Votação do Público</RouterLink>
        <RouterLink @click="fecharMenu" to="/">Júri e Seleção</RouterLink>
        <RouterLink @click="fecharMenu" to="/">Resultados</RouterLink>
        <RouterLink @click="fecharMenu" to="/">Contactos</RouterLink>
        <RouterLink @click="fecharMenu" to="/DashboardPNI">Dashboard PNI</RouterLink>
      </nav>
    </transition>
   
  </header>
</template>

<script setup>
import { ref } from 'vue'
const menuAberto = ref(false)
import { useRouter } from 'vue-router';
const fecharMenu = () => {
  menuAberto.value = false
}

const router = useRouter();

function irPara(path) {
  router.push(path);
}

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
