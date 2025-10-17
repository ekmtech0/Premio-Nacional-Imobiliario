<script setup>
import { useHead } from '@vueuse/head'
import NavBar from '@/componentes/NavBar.vue'
import SobrePremio from '@/Publico/SobrePremio.vue'
import categoriaPremio from '@/Publico/categoriaPremio.vue'
import NomeadosPremio from './NomeadosPremio.vue'

useHead({
  title: 'Prémio Nacional Imobiliário 2025',
  meta: [
    { name: 'description', content: 'Reconhecendo os melhores do mercado imobiliário em Angola.' },
    { property: 'og:title', content: 'Prémio Nacional Imobiliário 2025' },
    { property: 'og:image', content: '/img/banner.jpg' },
    { name: 'keywords', content: 'imobiliário, Angola, prémio, arquitetura, habitação' }
  ]
})
</script>
<template>
     <NavBar/>
<section class="relative w-full min-h-[85vh] flex items-center justify-center overflow-hidden">
  <!-- Imagem de fundo -->
  <img
    src="/src/Img/Angola.jpeg"
    alt="Luanda"
    class="absolute inset-0 w-full h-full object-cover z-0"
  />

  <!-- Camada escura -->
  <div class="absolute inset-0 bg-black bg-opacity-50 z-10"></div>

  <!-- Conteúdo -->
  <div
    class="relative z-20 text-white px-6 sm:px-12 lg:px-24 flex flex-col items-center lg:items-start justify-center w-full max-w-7xl mx-auto"
  >
    <div class="max-w-3xl text-center lg:text-left">
      <h1
        class="font-bold font-montserrat text-3xl sm:text-4xl md:text-5xl lg:text-6xl leading-tight drop-shadow-md"
      >
        RECONHECENDO OS MELHORES DO MERCADO IMOBILIÁRIO EM ANGOLA!
      </h1>

      <h2
        class="mt-4 text-base sm:text-lg lg:text-2xl font-light drop-shadow-md max-w-2xl"
      >
        Prémio Nacional Imobiliário 2025 — celebrando a excelência em arquitetura,
        serviço público, mediação e sustentabilidade.
      </h2>

      <div
        class="flex flex-col sm:flex-row items-center lg:items-start gap-4 mt-8"
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

<SobrePremio/>

 <categoriaPremio/>
 <NomeadosPremio/>
</template>