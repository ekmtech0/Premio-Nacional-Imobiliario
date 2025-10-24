<template>
  <div class="flex">

    <SideBar/>
    

    <!-- Conteúdo Principal -->
    <!-- <main class="flex-1 ml-0 md:ml-64">
        
    <header>
            <nav class="bg-azul lg:h-20 w-full ">
js
            </nav>
        </header>
      <h1 class="text-3xl font-semibold ">Conteúdo da Página</h1>
      <p class="mt-4 text-gray-600">
        Aqui fica o conteúdo principal do site ou dashboard.
      </p>
    </main> -->

     <main class="flex-1 ml-0 bg-">
        
            <header>
               <nav class="lg:bg-azul lg:h-20 lg:w-full ">
                 
              </nav>
            </header>
            <!-- Phone -->
             <!-- Header -->
       <div class="p-8 ">
            <div class="flex justify-between">
               <div>Prémio Nacional Imobiliario</div>
                 <div class="flex text-sm space-x-4">
                     <div>Notificaçoes</div>
                     <div>ADM</div>
                </div>
            </div>  
            
            <div class="flex flex-col py-20 ">
                <h1 class="font-bold text-2xl text-azul">Dashboard</h1>
                
                  <!-- Os cards -->
                <div class="grid grid-cols-2  ">
                    <div v-for="(card, index) in Cards" :key= index  class="bg-gray-100  p-6 shadow rounded-xl mt-6 max-w-md mx-auto lg:mx-0 w-full ">
                       <h1 class="font-bold text-xl text-center text-azul">{{ card.numero }}</h1>
                       <h1 class="text-gray-500 text-center text-sm">{{ card.titulo }}</h1>
                    </div>
                </div>


                <!-- Favorito do Publico -->
                 <div class="py-4">
                    <div>
                        <div class="bg-gray-100 p-6 flex rounded-xl flex flex-col">
                             <h1 class="text-azul font-bold text-lg">Votação do Público</h1>
                             <div class="bg-gray-200 rounded-lg p-2 flex justify-center items-center mt-4">
                                <div class="w-14 h-14 bg-gray-300 rounded-full flex items-center justify-center"> <img src="" alt="">foto</div>
                                 <div class=" flex  flex-col p-4 ">
                                       <h1 class="font-medium ">Linear Comunicações</h1>
                                       <div class="bg-azul h-1 rounded w-full whitespace-nowrap"></div>
                                    
                                 </div>
                                 <p class="text-2xl font-bold mt-2">75%</p>
                               
                             </div>
                             <p class="text-sm text-gray-600 mt-4">As votações ainda continuam!</p>
                        </div>
                    </div>
                 </div>
                <!-- Categoria -->
                 <div class="py-4">
                    <div>
                       
                        <div class="bg-gray-100 p-6 flex justify-between rounded-xl">
                          <h1 class="text-azul font-bold text-xl">Categoria</h1>
                        <button class="text-sm bg-azul rounded p-2 text-white font-medium">Adicionar Categoria</button>

                        <div >
                            <div class="">
                               <p></p>
                            </div>
                        </div>
                        </div>
                    </div>

                 </div>
            </div>
       </div>
    </main>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import SideBar from './SideBar.vue';
import CategoriasADM from './CategoriasADM.vue';



const Cards = ref([

    {
        titulo: 'Total de Votos',
         numero:'1200'
    },
   {
        titulo: 'Categorias',
         numero:'4'
    },
   {
        titulo: 'Nomeados',
         numero:'15'
    },
   {
        titulo: 'Visitantes',
         numero:'2,5mil'
    },
 

])
</script>
