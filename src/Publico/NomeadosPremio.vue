<template>
    <section class="border-t border-b border-gray-200 p-4 lg:py-20" id="nomeado">
        <div class="max-w-7xl mx-auto pt-12">
            <h1 class="font-montserrat font-bold text-azul text-2xl">Nomeados</h1>
            <h2 class="font-open text-sm md:text-base text-gray-900 pb-8">
                Conheça os profissionais e empresas que se destacaram.
            </h2>

            <!-- MOBILE = SCROLL HORIZONTAL / DESKTOP = GRID -->
            <div
                ref="scrollContainer"
                class="flex gap-4 overflow-x-auto hide-scrollbar 
                             md:grid md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 
                             md:overflow-visible"
            >
                <div
                    v-for="(Nomeado, index) in Nomeados"
                    :key="index"
                    class="bg-gray-100 p-4 rounded-xl shadow-sm flex-shrink-0 
                                 min-w-[250px] w-[250px] md:w-full flex flex-col h-full"
                >
                    <!-- Foto + Nome -->
                    <div class="flex flex-col items-center">
                        <img
                            :src="Nomeado.Imagem"
                            class="h-20 w-20 rounded-full object-cover bg-gray-300"
                            alt="Foto do Nomeado"
                        />
                        <h1 class="text-azul font-open font-bold text-lg md:text-xl text-center pt-4">
                            {{ Nomeado.Nome }}
                        </h1>
                    </div>

                    <!-- Descrição -->
                    <p class="font-open text-sm md:text-base text-gray-700 text-center pt-2">
                        {{ Nomeado.Descricao }}
                    </p>

                    <!-- Categoria corrigida -->
                    <p class="font-open text-sm md:text-base text-gray-700 text-center pt-4">
                        <span class="font-bold">Categoria:</span> {{ Nomeado.Categoria }}
                    </p>

                    <!-- Botão fixo ao fundo -->
                    <div class="mt-auto pt-6">
                        <button
                            class="bg-verde w-full text-white font-montserrat font-semibold p-2 md:p-3 rounded-lg transition duration-200 hover:bg-green-700 active:scale-95"
                        >
                            Votar
                        </button>
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
</template>

<script setup>
import { ref, computed, onMounted, onBeforeUnmount, nextTick } from 'vue'

const Nomeados = ref([
    {
        Nome: 'Victor Makuka',
        Descricao:
            'Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo explicabo soluta tempore...',
        Categoria: 'Arquitetura Imobiliária de Excelência',
        Imagem: '/Ang.jpeg',
    },
    {
        Nome: 'Linear Comunicações',
        Descricao:
            'Projeto residencial sustentável em Luanda, com foco em eficiência energética...',
        Categoria: 'Serviço Público na Habitação',
        Imagem: '/Angola.jpeg',
    },
    {
        Nome: 'EKM Tech Solutions',
        Descricao:
            'Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo explicabo soluta tempore...',
        Categoria: 'Mediação Imobiliária de Referência',
        Imagem: '/Luanda.jpeg',
    },
    {
        Nome: 'SG Design',
        Descricao:
            'Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo explicabo soluta tempore...',
        Categoria: 'Desenvolvimento Imobiliário Sustentável',
        Imagem: '/Ang.jpeg',
    },
])

const scrollContainer = ref(null)
const activeIndex = ref(0)

// Categorias apenas para contar bullets (pode ser map de categorias ou Nomeados conforme desejado)
const Categorias = computed(() => Nomeados.value.map(n => n.Categoria))

function scrollToCard(i) {
    const el = scrollContainer.value
    if (!el) return
    const child = el.children[i]
    if (!child) return
    // calcular posição relativa e fazer scroll suave
    const left = child.offsetLeft - el.offsetLeft
    el.scrollTo({ left, behavior: 'smooth' })
    activeIndex.value = i
}

function onScroll() {
    const el = scrollContainer.value
    if (!el) return
    // centro visível do container
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

onMounted(() => {
    nextTick(() => {
        if (scrollContainer.value) {
            scrollContainer.value.addEventListener('scroll', onScroll, { passive: true })
            // definir index inicial caso já esteja deslocado
            onScroll()
        }
    })
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
