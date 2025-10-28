<template>
    <section class="border-t border-b border-gray-200 p-4 lg:py-20" id="nomeado">
        <div class="max-w-7xl mx-auto pt-12 ">
            <h1 class="font-montserrat font-bold text-azul text-2xl">Nomeados</h1>
            <h2 class="font-open text-sm md:text-base text-gray-900 pb-8">
                Conheça os profissionais e empresas que se destacaram.
            </h2>

            <!-- MOBILE = SCROLL HORIZONTAL / DESKTOP = GRID --> 

            <div>
  <div class="mb-6">
    <label class="block text-sm font-medium text-gray-700 mb-2">Ver por categoria</label>
    <select
      v-model="selectedCategory"
      class="p-3 border border-gray-300 text-sm rounded-lg w-full focus:ring-2 focus:ring-verde focus:border-verde"
    >
      <option value="">Todos os nomeados</option>
      <option
        v-for="(categoria, index) in Categorias"
        :key="categoria.id"
        :value="categoria.nome"
      >
        {{ categoria.nome }}
      </option>
    </select>
  </div>

  <div
    ref="scrollContainer"
    class="flex gap-4 overflow-x-auto hide-scrollbar 
           md:grid md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 
           md:overflow-visible pt-4"
  >
    <!-- Mostra apenas os nomeados filtrados -->
    <div
      v-for="(Nomeado, index) in filteredNomeados"
      :key="index"
      class="bg-gray-100 p-4 rounded-xl shadow-sm flex-shrink-0 
             min-w-[250px] w-[250px] md:w-full flex flex-col h-full"
    >
      <div class="flex flex-col items-center">
        <img
          :src="Nomeado.photoUrl"
          class="h-20 w-20 rounded-full object-cover bg-gray-300"
          alt="Foto do nomeado"
        />
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
        <button
          class="bg-verde w-full text-white font-montserrat font-semibold p-2 md:p-3 rounded-lg hover:bg-green-700 transition"
          @click="Votar(Nomeado.id, Nomeado.categoriaId)"
          >
          Votar
        </button>
      </div>
    </div>
  </div>

  <!-- Bullets (mobile) -->
  <div class="flex justify-center mt-6 gap-2 md:hidden">
    <button
      v-for="(n, i) in filteredNomeados"
      :key="i"
      @click="scrollToCard(i)"
      class="w-1.5 h-1.5 rounded-full transition-all duration-300"
      :class="activeIndex === i ? 'bg-verde w-1.5' : 'bg-gray-500'"
    ></button>
  </div>
            </div>

        </div>

      
    </section>
</template>

<script setup>
import { ref, computed, nextTick, watch, onMounted, onBeforeUnmount } from 'vue'
import { http } from '@/Request/api'
import { getBrowserId } from '@/utils/getBrowserId'

// --- Dados ---
const Categorias = ref([])
const Nomeados = ref([])

// Categoria selecionada
const selectedCategory = ref('')

// Scroll e bullet
const scrollContainer = ref(null)
const activeIndex = ref(0)

// --- Funções para buscar dados ---
const getNomeados = async () => {
  try {
    const response = await http.get('/candidatos')
    Nomeados.value = response.data
    console.log('Nomeados carregados:', Nomeados.value)
  } catch (error) {
    console.error('Erro ao buscar nomeados:', error)
  }
}

const CarregarCategorias = async () => {
  try {
    const response = await http.get('/categorias/no-user')
    Categorias.value = response.data
    console.log('Categorias carregadas:', Categorias.value)
  } catch (error) {
    console.error('Erro ao buscar categorias:', error)
  }
}

const Votar = async (cdId, ctId)=>{
  let votoDTO = {
    browserId: await getBrowserId(),
    candidatoId: cdId,
    categoriaId: ctId
  }
  console.log('Votando com DTO:', votoDTO)
  try {
    const response = await http.post('/votos', votoDTO)
    console.log('Voto registrado com sucesso:', response.data)
  } catch (error) {
    console.error('Erro ao registrar voto:', error)
  }
}

// --- Filtragem de nomeados ---
const filteredNomeados = computed(() => {
  if (!selectedCategory.value) return Nomeados.value
  // corrigido: propriedade com 'categoria' minúsculo
  return Nomeados.value.filter(n => n.categoria === selectedCategory.value)
})

// --- Scroll e bullets ---
function scrollToCard(i) {
  const el = scrollContainer.value
  if (!el) return

  const child = el.children[i]
  if (!child) return

  const left = child.offsetLeft - el.offsetLeft
  el.scrollTo({ left, behavior: 'smooth' })
  activeIndex.value = i
}

function onScroll() {
  const el = scrollContainer.value
  if (!el) return

  const center = el.scrollLeft + el.clientWidth / 2
  let closest = 0
  let minDist = Infinity

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

// Reset scroll ao mudar categoria
watch(selectedCategory, () => {
  nextTick(() => {
    activeIndex.value = 0
    if (scrollContainer.value) {
      scrollContainer.value.scrollTo({ left: 0, behavior: 'smooth' })
    }
  })
})

// Reset scroll ao mudar lista filtrada
watch(filteredNomeados, () => {
  nextTick(() => {
    activeIndex.value = 0
    if (scrollContainer.value) {
      scrollContainer.value.scrollTo({ left: 0, behavior: 'smooth' })
    }
  })
})

// --- Ciclo de vida ---
onMounted(async () => {
  nextTick(() => {
    if (scrollContainer.value) {
      scrollContainer.value.addEventListener('scroll', onScroll, { passive: true })
      onScroll()
    }
  })

  await getNomeados()
  await CarregarCategorias()
  console.log('Browser ID:',await getBrowserId())
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
