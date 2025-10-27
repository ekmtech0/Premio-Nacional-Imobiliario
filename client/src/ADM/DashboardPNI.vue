<template>
  <div class="flex min-h-screen bg-gray-50">

    <!-- Sidebar (Desktop / Mobile) -->
    <SideBar />

    <!-- Conteúdo principal -->
    <main class="flex-1 ml-0 ">

     <HeaderADM/>

     <div class="lg:p-2">
       <!-- Título principal -->
      <h2 class="font-bold text-xl text-azul mt-10">Dashboard</h2>

      <!-- Cards Estatísticos -->
      <div class="grid grid-cols-2 md:grid-cols-5 gap-4 mt-6">
        <div
          v-for="(card, index) in Cards"
          :key="index"
          class="bg-white shadow rounded-xl p-6 text-center">
          <h3 class="text-xl font-bold text-azul">{{ card.numero }}</h3>
          <p class="text-gray-500 text-sm mt-2">{{ card.titulo }}</p>
        </div>
      </div>
         
          <!-- FAVORITO DO PÚBLICO -->
        <div class="grid grid-cols- md:grid-cols-2 gap-4 lg:gap-0">
            <div class=" flex flex-col p-6 shadow rounded-xl mt-6 max-w-md mx-auto lg:mx-0 bg-white w-full">
               <span class="text-azul font-bold text-lg">Favorito do Público (Atual)</span>
               <span class="text-azul font-open font-bold text-lg md:text-xl pt-4">SG Design</span>
             <span class="text-sm text-gray-700">3 votos</span>
      </div>

             <!-- Votação do publico -->
             <div class="bg-white flex flex-col p-6 shadow rounded-xl mt-6 max-w-md mx-auto lg:mx-0">
               <span class="text-azul font-bold text-lg">Votação do Público</span>
                 <div class="rounded-lg p-4 flex items-center justify-between mt-4">
                    <div class="flex  items-center space-x-4">
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
        </div>      
     </div>
    </main>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import SideBar from './SideBar.vue'
import HeaderADM from './HeaderADM.vue';

const Cards = ref([
  { titulo: 'Total de Votos', numero: '1200' },
  { titulo: 'Categorias', numero: '4' },
  { titulo: 'Nomeados', numero: '15' },
  { titulo: 'Visitantes', numero: '2,5mil' }
])
</script>
