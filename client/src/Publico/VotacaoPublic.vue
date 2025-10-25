<template>
  <section class="border-t border-b border-gray-200 p-4 lg:py-20" id="votacao">
    <div class="max-w-7xl mx-auto pt-12">
      <h1 class="font-montserrat font-bold text-azul text-2xl">Votação do Público</h1>
      <h2 class="font-open text-sm md:text-base text-gray-900 pb-8">
        A sua opinião conta!
      </h2>

      <!-- GRID GERAL - Mobile = 1 coluna, Tablet/Desktop = 3 colunas -->
      <div class="grid grid-cols-1 gap-6 lg:grid-cols-3">

        <!-- FORMULÁRIO -->
        <div class="bg-gray-100 flex flex-col p-6 shadow rounded-xl col-span-1 lg:col-span-2">
           
            <!-- Recaptcha -->
        <div v-if="Captcha" @click="FecharCaptcha()">
          <label class="block text-sm font-medium">reCAPTCHA</label>
          <div class="w-full h-24 bg-gray-300 rounded-lg flex items-center justify-center text-gray-600">
              <ReCaptcha
                siteKey="6LfKMPYrAAAAAGumXzOHo99HHawcUaNLNzveM-lT"
                @verified="onVerified"
              />
          </div>
        </div>

        <!-- Primeiro Formulario com Nome e Email -->
           
              <form class="space-y-4"  v-if="Form1">
    <div class="lg:flex lg:justify-between lg:w-full">
      <div class="">
        <label class="block text-sm font-medium">Nome</label>
        <input type="text" placeholder="Nome completo"
          class="p-3 border border-gray-300 text-sm rounded-lg lg:w-[380px] w-full ">
      </div>
  
      <div>
        <label class="block text-sm font-medium">E-mail ou Telemóvel</label>
        <input type="text" placeholder="exemplo@gmail.com"
          class="p-3 border border-gray-300 text-sm rounded-lg lg:w-[400px] w-full">
      </div>
    </div>
  
    <!-- Botão -->
      <div class="pt-6">
        <button class="rounded-lg p-3 bg-azul text-white w-full font-semibold" @click="Avancar()">Avançar</button>
      </div>

  </form>


          
              <!-- O Modal de Verificação de E-mail -->

           <div v-if="VerfEmail" class="fixed inset-0 z-5 flex items-center justify-center bg-black bg-opacity-60">

                <!-- botão de fechar -->
                 <button @click="FecharEmail()" class="absolute top-4 right-4 text-gray-300 hover:text-amarelo">
                    <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"/>
                    </svg>
               </button>
                    <VerficarEmail @AbrirForm3="FecharForm2()"/>
          </div>

          <!-- Ultimo Formulario -->
           
  <form class="space-y-4" v-if="Form3">
    <!-- O nome do Eleitor -->
   <p>Séfora Zípora</p>
    <div>
      <label class="block text-sm font-medium">Escolha o Nomeado</label>
      <select class="p-3 border border-gray-300 text-sm rounded-lg w-full">
        <option value="" disabled selected>Nomeado</option>
        <option>Victor Makuka</option>
        <option>Linear Comunicações</option>
        <option>EKM Tech</option>
        <option>SG Design</option>
      </select>
    </div>
  <!-- Confirma o voto -->

  
      <!-- Mensagem de sucesso -->
      <span class="text-sm text-verde" v-if="VotoRealizado">Obrigado por Votar! O seu voto foi registado com sucesso.</span>
     <!-- Botão -->
      <div class="pt-6">
        <button class="rounded-lg p-3 bg-azul text-white w-full font-semibold" @click="ConfirmarVoto()">Confirmar Voto</button>
      </div>

  
  
  </form>

           
        </div>

        <!-- ÚLTIMOS VOTOS -->
        <div class="bg-gray-100 flex flex-col p-6 shadow rounded-xl lg:h-1/2">
          <span class="text-azul font-open font-semibold md:text-xl pt-2">Últimos Votos</span>

          <div class="pt-4 flex flex-col space-y-2">
            <span class="text-sm text-gray-700">Séfora Zípora → EKM Tech (21/10/2025, 16:20)</span>
            <span class="text-sm text-gray-700">Mvemba Guilherme → SG Design (21/10/2025, 16:18)</span>
            <span class="text-sm text-gray-700">Steoar Nsingi → SG Design (21/10/2025, 16:17)</span>
            <span class="text-sm text-gray-700">Steoar Nsingi → SG Design (21/10/2025, 16:15)</span>
          </div>
        </div>
        </div>
      

      <!-- FAVORITO DO PÚBLICO -->
      <div class="bg-gray-100 flex flex-col p-6 shadow rounded-xl mt-6 max-w-md mx-auto lg:mx-0">
        <span class="text-azul font-open font-semibold md:text-xl">Favorito do Público (Atual)</span>
        <span class="text-azul font-open font-bold text-lg md:text-xl pt-4">SG Design</span>
        <span class="text-sm text-gray-700">3 votos</span>
      </div>

    </div>
  </section>
</template>


<script setup>
import { ref } from 'vue'
import VerficarEmail from './VerficarEmail.vue'
import ReCaptcha from './ReCaptcha.vue'

const Captcha = ref(true)
const Form1 = ref(false)
const VerfEmail = ref(false)
const Form3 = ref(false)
const VotoRealizado = ref(false)



function onVerified(token) {
  console.log('Token recebido:', token)
  // Aqui podes enviar para o backend ASP.NET Core para verificar
  Captcha.value = false
  Form1.value = true
}

function Avancar() {
  Form1.value = false
  VerfEmail.value = true
}

function FecharEmail() {
  VerfEmail.value = false
  Form1.value = true
}

function FecharForm2() {
  VerfEmail.value = false
  Form3.value = true
}

function ConfirmarVoto() {
  VotoRealizado.value = true
  Form3.value =  true
 }
</script>



