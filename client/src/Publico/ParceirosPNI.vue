<template>
  <section class="w-full mb-12 py-10">
    <div class="max-w-7xl mx-auto px-6 text-center">
      <!-- Título -->
      <h2 data-aos="fade-up" class="text-2xl md:text-3xl font-bold text-azul mb-8">
        Parceiros / Apoios
      </h2>

      <!-- Carrossel de Logos -->
      <div data-aos="fade-left" class="overflow-hidden relative">
        <div
          class="flex items-center gap-12 animate-scroll"
          :style="{ animationDuration: `${velocidade}s` }"
        >
          <!-- Duplicar a lista de logos para efeito infinito suave -->
          <div
            v-for="(logo, i) in [...logos, ...logos]"
            :key="i"
            class="flex-shrink-0"
          >
            <img
              :src="logo"
              alt="Logo Parceiro"
              class="h-14 md:h-20 object-contain grayscale hover:grayscale-0 transition duration-300"
            />
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
import { ref } from "vue";

// Lista de imagens dos parceiros
const logos = ref([
  '/Img/inh.png',
  '/Img/linear2.png',
  '/Img/apima.png',
   '/Img/inh.png',
  '/Img/linear2.png',
  '/Img/apima.png',



]);

// Velocidade da animação (quanto maior, mais lento)
const velocidade = 5; // 25 segundos para passar suavemente toda a faixa
</script>

<style scoped>
@keyframes scroll {
  from {
    transform: translateX(0);
  }
  to {
    transform: translateX(-50%); /* Move apenas metade pois duplicámos os logos */
  }
}

.animate-scroll {
  display: flex;
  animation: scroll linear infinite;
}
</style>
