<template>
  <div class="flex">
       <!-- Botão Hambúrguer (só no mobile) -->
    <button 
      @click="isOpen = !isOpen" 
      class="md:hidden p-3 fixed top-4 left-4 z-50 bg-azul text-white rounded-lg"
    >
      ☰
    </button>

    <!-- Sidebar -->
    <aside
      :class="[
        'bg-azul text-white w-64 h-screen p-6 fixed top-0 left-0 transition-transform duration-300 z-40',
        isOpen ? 'translate-x-0' : '-translate-x-full',
        'md:translate-x-0 md:relative md:block'
      ]"
    >
      <h2 class="text-xl font-bold mb-6  ">Prémio Nacional Imobiliário</h2>
      <nav class="flex flex-col px-4 lg:font-semibold">
        <a href="#" class="hover:bg-gray-700 p-4 rounded">Dashboard</a>
        <a href="#categoriaADM" class="hover:bg-gray-700 p-4 rounded">Categorias</a>
        <a href="#" class="hover:bg-gray-700 p-4 rounded">Nomeados</a>
        <a href="#" class="hover:bg-gray-700 p-4 rounded">Votação do Público</a>
        <a href="#" class="hover:bg-gray-700 p-4 rounded">Resultados</a>
        <a href="#" class="hover:bg-gray-700 p-4 rounded">Usuários</a>
        <a href="#" class="hover:bg-gray-700 p-4 rounded">Configurações</a>
        <a href="#" class="hover:bg-gray-700 flex  p-4 rounded">Sair</a>
      </nav>
    </aside>
  </div>
</template>

<script setup>
import { ref } from 'vue'
const isOpen = ref(false);

</script>