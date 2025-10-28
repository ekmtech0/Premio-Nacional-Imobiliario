<template>
  <div class="flex min-h-screen bg-gray-50">

    <!-- Sidebar -->
    <SideBar />

    <!-- Conteúdo principal -->
    <main class="flex-1 flex flex-col">

      <!-- Header -->
      <HeaderADM />

      <!-- Área principal -->
      <div class="flex-1 p-6 lg:p-10">
        <!-- Título -->
        <h2 class="font-bold text-2xl text-azul mb-6">Dashboard</h2>

        <!-- Cards Estatísticos -->
        <div class="grid grid-cols-2 md:grid-cols-4 gap-6">
          <div
            v-for="(card, index) in Cards"
            :key="index"
            class="bg-white shadow-md rounded-xl p-6 text-center hover:shadow-lg transition-shadow"
          >
            <h3 class="text-3xl font-bold text-azul">{{ card.numero }}</h3>
            <p class="text-gray-500 text-sm mt-2">{{ card.titulo }}</p>
          </div>
        </div>

        <!-- Sessão de Destaques -->
        <div class="grid grid-cols-1 md:grid-cols-2 gap-6 mt-10">

          <!-- Favorito do Público -->
          <div class="bg-white shadow-md rounded-xl p-6 flex flex-col">
            <span class="text-azul font-bold text-lg">Favorito do Público (Atual)</span>
            <span class="text-azul font-open font-bold text-xl mt-4">SG Design</span>
            <span class="text-sm text-gray-700 mt-2">3 votos</span>
          </div>

          <!-- Votação do Público -->
          <div class="bg-white shadow-md rounded-xl p-6">
            <span class="text-azul font-bold text-lg">Votação do Público</span>

            <div class="rounded-lg p-4 flex items-center justify-between mt-4 bg-gray-50">
              <div class="flex items-center space-x-4">
                <div class="w-14 h-14 bg-gray-300 rounded-full flex items-center justify-center">foto</div>
                <div>
                  <h4 class="font-medium">Linear Comunicações</h4>
                  <div class="w-32 bg-gray-300 h-2 rounded mt-1">
                    <div class="bg-azul h-2 rounded" style="width: 75%"></div>
                  </div>
                </div>
              </div>
              <p class="text-xl font-bold text-azul">75%</p>
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
import HeaderADM from './HeaderADM.vue'
import * as signalR from '@microsoft/signalr'
const qtdVotos = ref(0);
const Cards = ref([
  { titulo: 'Total de Votos', numero: qtdVotos },
  { titulo: 'Categorias', numero: "5" },
  { titulo: 'Nomeados', numero: '15' },
  { titulo: 'Visitantes', numero: '2,5 mil' },
])

const connection = new signalR.HubConnectionBuilder()
  .withUrl('http://localhost:5092/votohub', {
    withCredentials: true // 👈 importante se o servidor usa AllowCredentials()
  })
  .withAutomaticReconnect()
  .configureLogging(signalR.LogLevel.Information)
  .build();

connection.on("ReceiveVoteQtdVotos", (voto) => {
  console.log("📩 Mensagem recebida do servidor:", voto);
  qtdVotos.value = voto;
});

connection.start()
  .then(() => console.log("✅ Conectado ao SignalR!"))
  .catch(err => console.error("❌ Erro ao conectar:", err));


</script>
