<template>
   <NavBar/>
  <section class="border-t border-b border-t-gray-200 border-b-gray-200 p-4  lg:pt-24 lg:pb-24 mt-20  lg:px-40" id="categoria">

 <!-- Cabeçalho -->


<div class="p-4 max-w-7xl mx-auto lg:mt-24 lg:mb-32">
  <h1 data-aos="fade-up" class="font-montserrat font-bold text-azul text-2xl">Categorias Oficiais</h1>
  <h2 data-aos="fade-right" class="font-open text-sm md:text-base text-gray-900 pb-8">
    As áreas em que o prémio reconhece a excelência.
  </h2>

  <div data-aos="fade-left" v-if="Processar" class="flex items-center justify-center pt-20 lg:pt-40 mb-24">
          <ProcessarPNI/>
      </div>
 <!-- Container dos cards -->
 <div
  class="flex gap-4 overflow-x-auto md:flex-wrap md:overflow-x-visible hide-scrollbar">
  <!-- Card -->

      <div  class="flex gap-4 overflow-x-auto md:flex-wrap md:overflow-x-visible hide-scrollbar" ref="carousel"  style="scroll-snap-type: x mandatory;" >

  <!-- Card -->
  <div
    v-for="(categoria, index) in Categorias"
    :key="index.id"
    class="bg-gray-100 min-w-[250px] max-w-xs flex-shrink-0 p-4 sm:p-6 rounded-lg transition-transform duration-300 hover:scale-105 hover:shadow-lg cursor-pointer flex flex-col justify-between md:flex-1 md:min-w-[280px] md:max-w-[320px] lg:min-w-[300px] lg:max-w-sm lg:p-8"
    style="scroll-snap-align: start;"
  >
    <div>
     <!-- Titulo da Categoria -->
     <h2  class="text-azul font-open font-bold text-lg md:text-xl" >
        {{ categoria.nome }}
     </h2>
     <!-- Descrição -->
     <h3 class="font-open text-sm md:text-base pt-2 text-gray-700">
       {{ categoria.description }}
     </h3>

     <p  class="font-open text-xs md:text-base pt-2 text-gray-900 ">Nomeados: {{ categoria.candidatos?.length || 0 }}</p>
    </div>
    <!-- Botão -->
    <div class="pt-6 flex">
      <div class="flex flex-col w-full">
        <button @click="VerCategorias()"
        class="bg-verde w-full text-white font-montserrat font-semibold p-2 md:p-3 rounded-lg transition-colors duration-200 hover:bg-green-700 active:scale-95 mb-2"
        >
        Ver
        </button>
        <a href="/public/APRESENTAÇÃO_GERAL_DO_PRÊMIO[1].pdf" download v-if="categoria.isSpecialCetgory" class="bg-verde w-full text-white font-montserrat font-semibold p-2 md:p-3 rounded-lg transition-colors duration-200 hover:bg-green-700 active:scale-95 text-center">Baixar apresentção</a>
      </div>
    </div>

  </div>

  </div>
 </div>
</div>

<!-- Bullets -->
    <div class="flex justify-center mt-6 gap-2 md:hidden">
     <button
      v-for="(c, i) in Categorias"
      :key="i"
      @click="scrollToCard(i)"
      class="w-1.5 h-1.5 rounded-full transition-all duration-300"
      :class="activeIndex === i ? 'bg-verde w-1.5' : 'bg-gray-500'"
     ></button>
    </div>
  </section>
   <FooterPNI/>
</template>

<script setup>
import NavBar from '@/componentes/NavBar.vue';
import  FooterPNI  from '@/Publico/FooterPNI.vue'
import { ref, onMounted, onBeforeUnmount, nextTick } from 'vue';
import { http } from '@/Request/api' // Instância do Axios
import ProcessarPNI from '@/componentes/ProcessarPNI.vue';

const emit = defineEmits([ 'CategoriaSelecionado']);


const Categorias = ref([])
const Processar = ref(true)
// const Categoria = ref()

// Buscar categorias da API
const carregarCategorias = async () => {
  try {
    const response = await http.get('/categorias')
    Categorias.value = response.data
    console.log(response.data)
    Processar.value = false
  } catch (error) {
    console.error('Erro ao carregar categorias:', error)
  }
}

const carousel = ref(null)
const activeIndex = ref(0)

function scrollToCard(index) {
  const el = carousel.value
  if (!el) return
  const child = el.children[index]
  if (child) {
   child.scrollIntoView({ behavior: 'smooth', inline: 'start', block: 'nearest' })
   activeIndex.value = index
  }
}


function updateActive() {
  const el = carousel.value
  if (!el) return
  const children = Array.from(el.children)
  if (children.length === 0) {
   activeIndex.value = 0
   return
  }
  const containerRect = el.getBoundingClientRect()
  const containerCenter = (containerRect.left + containerRect.right) / 2
  let closestIndex = 0
  let closestDist = Infinity
  children.forEach((child, idx) => {
   const rect = child.getBoundingClientRect()
   const childCenter = (rect.left + rect.right) / 2
   const dist = Math.abs(childCenter - containerCenter)
   if (dist < closestDist) {
    closestDist = dist
    closestIndex = idx
   }
  })
  activeIndex.value = closestIndex
}

let onScroll
onMounted(async () => {
  await nextTick()
  if (carousel.value) {
   onScroll = () => updateActive()
   carousel.value.addEventListener('scroll', onScroll, { passive: true })
   updateActive()
  }
})

onMounted(() => {
  carregarCategorias()
})

onBeforeUnmount(() => {
  if (carousel.value && onScroll) {
   carousel.value.removeEventListener('scroll', onScroll)
  }
})

function VerCategorias() {
  emit('CategoriaSelecionado', Categorias([]))
}
</script>

<style scoped>
.hide-scrollbar::-webkit-scrollbar {
  display: none;
}
.hide-scrollbar {
  -ms-overflow-style: none;
  scrollbar-width: none;
}
</style>
