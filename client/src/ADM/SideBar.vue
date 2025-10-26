<template>
  <div class="flex min-h-screen bg-gray-100">
    <!-- 🔹 Botão Hambúrguer (Mobile) -->
    <button 
      @click="isOpen = !isOpen"
      class="md:hidden fixed top-4 left-4 z-50 p-3 bg-azul text-white rounded-lg shadow-md transition-transform active:scale-95"
      aria-label="Toggle Menu"
    >
      ☰
    </button>

    <!-- 🔹 Sidebar -->
    <aside
      :class="[
        'fixed top-0 left-0 h-full w-64 bg-azul text-white p-6 shadow-lg transition-transform duration-300 z-40',
        isOpen ? 'translate-x-0' : '-translate-x-full',
        'md:translate-x-0 md:relative md:block'
      ]"
    >
      <h2 class="text-xl font-bold mb-6">Prémio Nacional Imobiliário</h2>
      <nav class="flex flex-col space-y-1 text-sm lg:text-base font-medium">
        <a href="#" class="hover:bg-white/10 p-3 rounded transition">Dashboard</a>
        <a href="#categoriaADM" class="hover:bg-white/10 p-3 rounded transition">Categorias</a>
        <a href="#" class="hover:bg-white/10 p-3 rounded transition">Nomeados</a>
        <a href="#" class="hover:bg-white/10 p-3 rounded transition">Votação do Público</a>
        <a href="#" class="hover:bg-white/10 p-3 rounded transition">Resultados</a>
        <a href="#" class="hover:bg-white/10 p-3 rounded transition">Usuários</a>
        <a href="#" class="hover:bg-white/10 flex items-center p-3 rounded transition">Sair</a>
      </nav>
    </aside>

   
  </div>
</template>

<script setup>
import { ref } from 'vue'
const isOpen = ref(false)
</script>
