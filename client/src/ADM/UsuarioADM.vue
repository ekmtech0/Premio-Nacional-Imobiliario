<script setup lang="ts">
import SideBar from './SideBar.vue';
import HeaderADM from './HeaderADM.vue';

</script>

<template >
  <div class="flex min-h-screen bg-gray-50">
    <!-- Categoria -->
     <SideBar/>
      
               <section class="flex-1 ml-0 " id="categoriaADM">
                <HeaderADM/>
                  <div class="py-4 flex-1 ml-0 p-8">
                    <div>
                       
                        <div class="bg-gray-100 p-6 flex justify-between rounded-xl">
                          <h1 class="text-azul font-bold text-xl">Usuarios</h1>
                        <button class="text-sm bg-azul rounded p-2 text-white font-medium"></button>

                        <div >
                            <div class="">
                               <p></p>
                            </div>
                        </div>
                        </div>
                    </div>

                 </div>
               </section>
               </div>
</template>