<template>
    <section class="border-t border-b border-gray-200 p-4 lg:py-20" id="nomeado">
        <div class="max-w-7xl mx-auto pt-12 ">
            <h1 class="font-montserrat font-bold text-azul text-2xl">Nomeados</h1>
            <h2 class="font-open text-sm md:text-base text-gray-900 pb-8">
                Conheça os profissionais e empresas que se destacaram.
            </h2>

            <!-- MOBILE = SCROLL HORIZONTAL / DESKTOP = GRID -->
            <div>
              <label class="block text-sm font-medium">Ver por categorias</label>
              <select v-model="selectedCategory"  class="p-3 border border-gray-300 text-sm rounded-lg w-full">
                <option value="">Todos nomeados</option>
                <option v-for="(c, i) in Categorias" :key="i" :value="c">{{ c }}</option>
              </select>
            </div>

            <div
              ref="scrollContainer"
              class="flex gap-4 overflow-x-auto hide-scrollbar 
                     md:grid md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 
                     md:overflow-visible pt-4"
            >
              <!-- mostra apenas os nomeados filtrados -->
              <div
                v-for="(Nomeado, index) in filteredNomeados"
                :key="index"
                class="bg-gray-100 p-4 rounded-xl shadow-sm flex-shrink-0 
                       min-w-[250px] w-[250px] md:w-full flex flex-col h-full"
              >
                <!-- Conteúdo ORIGINAL mantido -->
                <div class="flex flex-col items-center">
                  <img :src="Nomeado.photoUrl" class="h-20 w-20 rounded-full object-cover bg-gray-300" />
                  <h1 class="text-azul font-open font-bold text-lg md:text-xl text-center pt-4">
                    {{ Nomeado.nome }}
                  </h1>
                </div>

                <p class="font-open text-sm md:text-base text-gray-700 text-center pt-2">
                  {{ Nomeado.description }}
                </p>

                <p class="font-open text-sm md:text-base text-gray-700 text-center pt-4">
                  <span class="font-bold">Categoria:</span> {{ Nomeado.categoria }}
                </p>

                <div class="mt-auto pt-6">
                  <button class="bg-verde w-full text-white font-montserrat font-semibold p-2 md:p-3 rounded-lg">
                    Votar
                  </button>
                </div>
              </div>
            </div>
        </div>

        <!-- Bullets (aparecem só no mobile) -->
        <div class="flex justify-center mt-6 gap-2 md:hidden">
          <button
            v-for="(n, i) in filteredNomeados"
            :key="i"
            @click="scrollToCard(i)"
            class="w-1.5 h-1.5 rounded-full transition-all duration-300"
            :class="activeIndex === i ? 'bg-verde w-1.5' : 'bg-gray-500'"
          ></button>
        </div>
    </section>
</template>

<script setup>
import { ref, computed, nextTick, watch, onMounted, onBeforeUnmount } from 'vue'
import axios from 'axios'
import { http } from '@/Request/api'

const Nomeados = ref([
  {
    nome: 'Victor Makuka',
    description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo explicabo soluta tempore...',
    categoria: 'Arquitetura Imobiliária de Excelência',
    photoUrl: '/Ang.jpeg',
  },
  {
    nome: 'Linear Comunicações',
    description: 'Projeto residencial sustentável em Luanda, com foco em eficiência energética...',
    categoria: 'Serviço Público na Habitação',
    photoUrl: '/Angola.jpeg',
  },
  {
    nome: 'EKM Tech Solutions',
    description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo explicabo soluta tempore...',
    categoria: 'Mediação Imobiliária de Referência',
    photoUrl: '/Luanda.jpeg',
  },
  {
    nome: 'SG Design',
    description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo explicabo soluta tempore...',
    categoria: 'Desenvolvimento Imobiliário Sustentável',
    photoUrl: '/Ang.jpeg',
  },
])

// Categoria selecionada no select
const selectedCategory = ref('')

// Lista filtrada automaticamente conforme o select
const filteredNomeados = computed(() => {
  if (!selectedCategory.value) return Nomeados.value
  return Nomeados.value.filter(n => n.Categoria === selectedCategory.value)
})

const scrollContainer = ref(null)
const activeIndex = ref(0)

const Categorias = computed(() => Nomeados.value.map(n => n.Categoria))

function scrollToCard(i) {
  const el = scrollContainer.value
  if (!el) return

  const child = el.children[i]
  if (!child) return

  const left = child.offsetLeft - el.offsetLeft
  el.scrollTo({ left, behavior: 'smooth' })
  activeIndex.value = i
}

// calcula o bullet ativo com base no centro visível do container
function onScroll() {
  const el = scrollContainer.value
  if (!el) return

  const center = el.scrollLeft + el.clientWidth / 2
  let closest = 0
  let minDist = Infinity

  // usar children atuais (filtrados) — funciona com layout mobile (flex)
  Array.from(el.children).forEach((child, idx) => {
    const childCenter = child.offsetLeft + child.clientWidth / 2
    const dist = Math.abs(childCenter - center)
    if (dist < minDist) {
      minDist = dist
      closest = idx
    }
  })

  activeIndex.value = closest
}

// quando mudar de categoria, resetar bullets e scroll para início
watch(selectedCategory, () => {
  nextTick(() => {
    activeIndex.value = 0
    if (scrollContainer.value) {
      scrollContainer.value.scrollTo({ left: 0, behavior: 'smooth' })
    }
  })
})

// também quando a lista filtrada mudar (ex.: pelo select), garantir reset
watch(filteredNomeados, () => {
  nextTick(() => {
    activeIndex.value = 0
    if (scrollContainer.value) {
      scrollContainer.value.scrollTo({ left: 0, behavior: 'smooth' })
    }
  })
})

async function fetchNomeados() {
 try {
   const response = await http.get('/candidatos')
   console.log('Nomeados recebidos:', response.data)
    Nomeados.value = response.data
   return response.data
 } catch (error) {
   console.error('Erro ao buscar nomeados:', error)
   return []
 }
}

onMounted(async () => {
  nextTick(() => {
    if (scrollContainer.value) {
      scrollContainer.value.addEventListener('scroll', onScroll, { passive: true })
      // define o index inicial caso já esteja deslocado
      onScroll()
    }
  })
await fetchNomeados();
})

onBeforeUnmount(() => {
  if (scrollContainer.value) {
    scrollContainer.value.removeEventListener('scroll', onScroll)
  }
})
</script>

<style>
/* Esconder scrollbar apenas no mobile */
.hide-scrollbar::-webkit-scrollbar {
    display: none;
}
.hide-scrollbar {
    -ms-overflow-style: none;
    scrollbar-width: none;
}
</style>
