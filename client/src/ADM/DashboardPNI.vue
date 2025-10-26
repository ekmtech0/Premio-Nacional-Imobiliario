<template>
  <div class="flex min-h-screen bg-gray-50">

    <!-- Sidebar (Desktop / Mobile) -->
    <SideBar />

    <!-- Conteúdo principal -->
    <main class="flex-1 ml-0 lg:ml-4 p-6 ">

      <!-- Header -->
      <header class="flex justify-between items-center bg-white p-4 shadow rounded-xl">
        <h1 class="text-xl font-bold text-azul">Prémio Nacional</h1>
        <div class="flex items-center space-x-6">
          <button class="relative">
            <i class="fa-regular fa-bell text-gray-600 text-lg"></i>
            <!-- Badge -->
            <span class="absolute -top-1 -right-2 w-2 h-2 bg-red-500 rounded-full"></span>
          </button>
          <div class="flex items-center space-x-2">
            <div class="w-8 h-8 rounded-full bg-gray-300"></div>
            <span class="text-sm font-medium">ADM</span>
          </div>
        </div>
      </header>

     <div class="lg:p-12">
       <!-- Título principal -->
      <h2 class="font-bold text-xl text-azul mt-10">Dashboard</h2>

      <!-- Cards Estatísticos -->
      <div class="grid grid-cols-2 md:grid-cols-4 gap-4 mt-6">
        <div
          v-for="(card, index) in Cards"
          :key="index"
          class="bg-white shadow-md rounded-xl p-6 text-center">
          <h3 class="text-xl font-bold text-azul">{{ card.numero }}</h3>
          <p class="text-gray-500 text-sm mt-2">{{ card.titulo }}</p>
        </div>
      </div>

      <!-- Votação do Público -->
      <section class="mt-10">
        <div class="bg-white shadow-md rounded-xl p-6">
          <h3 class="text-azul font-bold text-lg">Votação do Público</h3>

          <div class="bg-gray-100 rounded-lg p-4 flex items-center justify-between mt-4">
            <div class="flex items-center space-x-4">
              <div class="w-14 h-14 bg-gray-300 rounded-full flex items-center justify-center">foto</div>
              <div>
                <h4 class="font-medium">Linear Comunicações</h4>
                <div class="w-32 bg-gray-300 h-2 rounded mt-1">
                  <div class="bg-azul h-2 rounded" style="width: 75%"></div>
                </div>
              </div>
            </div>
            <p class="text-xl font-bold">75%</p>
          </div>

          <p class="text-sm text-gray-600 mt-4">As votações ainda continuam!</p>
        </div>
      </section>

      <!-- Categoria -->
      <section class="mt-8">
        <div class="bg-white shadow-md rounded-xl p-6 flex items-center justify-between">
          <h3 class="text-azul font-bold text-xl">Categoria</h3>
          <button class="bg-azul text-white px-4 py-2 rounded-lg text-sm font-medium hover:opacity-90">
            Adicionar Categoria
          </button>
        </div>
      </section>
     </div>
    </main>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import SideBar from './SideBar.vue'

const Cards = ref([
  { titulo: 'Total de Votos', numero: '1200' },
  { titulo: 'Categorias', numero: '4' },
  { titulo: 'Nomeados', numero: '15' },
  { titulo: 'Visitantes', numero: '2,5mil' }
])
</script>
