<script setup>
import { useHead } from '@vueuse/head'
import NavBar from '@/componentes/NavBar.vue'
import SobrePremio from '@/Publico/SobrePremio.vue'
import NomeadosPremio from './NomeadosPremio.vue'
import ResultadosAtuais from './ResultadosAtuais.vue'
import ParceirosPNI from './ParceirosPNI.vue'
import FooterPNI from './FooterPNI.vue'

useHead({
  title: 'Prémio Nacional Imobiliário 2025',
  meta: [
    { name: 'description', content: 'Reconhecendo os melhores do mercado imobiliário em Angola.' },
    { property: 'og:title', content: 'Prémio Nacional Imobiliário 2025' },
    { property: 'og:image', content: '/img/banner.jpg' },
    { name: 'keywords', content: 'imobiliário, Angola, prémio, arquitetura, habitação' }
  ]
})

import { ref, onMounted, onUnmounted } from 'vue'

const imagens = [
  
  '/Img/Angola.jpeg',
  '/Img/Luanda.jpeg',
  '/Img/Marginal.jpeg',
  
]

const indexAtual = ref(0)
let intervalo = null

// Trocar de imagem automaticamente a cada 5s
onMounted(() => {
  intervalo = setInterval(() => {
    indexAtual.value = (indexAtual.value + 1) % imagens.length
  }, 4000)
})

onUnmounted(() => {
  clearInterval(intervalo)
})

</script>
<template>
     <NavBar/>
<section class="relative w-full min-h-[85vh] flex items-center justify-center overflow-hidden h-[700px]  lg:h-[800px] lg:px-40" id="inicio">


<!--  IMAGEM PRINCIPAL DO SLIDE -->
<img
  :src="imagens[indexAtual]"
  alt="Slide de Imagens"
  class="slide-transition absolute inset-0 w-full h-[900px] object-cover lg:"
/>

<!--  INDICADORES (bolinhas) -->
<div class="absolute bottom-6 left-1/2 transform -translate-x-1/2 flex gap-3 z-10">
  <span
    v-for="(img, i) in imagens"
    :key="i"
    @click="indexAtual = i"
    :class="[
      'w-3 h-3 rounded-full cursor-pointer transition-all duration-300',
      indexAtual === i ? 'bg-azul scale-125' : 'bg-white/70 hover:bg-white'
    ]"
  ></span>
</div>

  <!-- Camada escura -->
  <div class="absolute inset-0 bg-black bg-opacity-50 z-10"></div>

<div
  class="relative z-20 text-white px-6 sm:px-12 lg:px-24 flex flex-col items-center lg:items-start justify-center w-full max-w-7xl mx-auto"
>
  <div class="max-w-3xl text-center lg:text-left">
    <!-- TÍTULO -->
    <h1
      class="fade-up font-bold font-montserrat text-3xl sm:text-4xl md:text-5xl lg:text-6xl leading-tight drop-shadow-md z-50"
    >
      RECONHECENDO OS MELHORES DO MERCADO IMOBILIÁRIO EM ANGOLA!
    </h1>

    <!-- SUBTÍTULO -->
    <h2
      class="fade-up-delay mt-4 text-base sm:text-lg lg:text-2xl font-light drop-shadow-md max-w-2xl"
    >
      Prémio Nacional Imobiliário 2025 — celebrando a excelência em arquitetura,
      serviço público, mediação e sustentabilidade.
    </h2>

    <!-- BOTÕES -->
    <div
      class="fade-up-delay-2 flex flex-col sm:flex-row items-center lg:items-start gap-4 mt-8"
    >
      <button
        class="bg-verde hover:bg-green-600 px-6 py-3 rounded-lg font-bold font-open transition-transform duration-200 hover:scale-105"
      >
        Votar no Favorito
      </button>
      <button
        class="border border-white hover:bg-white hover:text-azul px-6 py-3 rounded-lg font-bold font-open transition-transform duration-200 hover:scale-105"
      >
        Ver Nomeado
      </button>
    </div>
  </div>
</div>
</section>


  
<section class="w-full border-y border-gray-200">
  <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-40 ">
     <SobrePremio/>
  </div>
</section>


 
  <section class="w-full  border-y border-gray-200 ">
  <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-40">
  <NomeadosPremio/>
  </div>
</section>


 <section class="w-full  border-y border-gray-200">
  <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-40 ">
       <ResultadosAtuais/>
  </div>
</section>

 <section class="w-full  border-y border-gray-200">
  <div class=" ">
 <ParceirosPNI/>
  </div>
</section>

 <section class="w-full ">
  <div class="">
  <FooterPNI/>
  </div>
</section>
</template>

<!-- Animação CSS -->
<style scoped>
@keyframes fadeUp {
  from {
    opacity: 0;
    transform: translateY(30px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

/* Animação que dá zoom suave ao trocar de imagem */
@keyframes zoomFade {
  from {
    opacity: 0;
    transform: scale(1.05); /* Ligeiro aumento no início */
  }
  to {
    opacity: 1;
    transform: scale(1); /* Volta ao tamanho normal */
  }
}

.slide-transition {
  animation: zoomFade 0.8s ease-in-out forwards;
}

/* Aplicação das animações */
.fade-up {
  animation: fadeUp 1s ease-out forwards;
}
.fade-up-delay {
  animation: fadeUp 1s ease-out forwards;
  animation-delay: 0.4s;
}
.fade-up-delay-2 {
  animation: fadeUp 1s ease-out forwards;
  animation-delay: 0.8s;
}
</style>