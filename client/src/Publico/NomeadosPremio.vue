<template>
  <section class="p-4 lg:py-20" id="nomeado">
    <div class="max-w-7xl mx-auto ">
      <h1 class="font-montserrat font-bold text-azul text-2xl">Nomeados</h1>
      <h2 class="font-open text-sm md:text-base text-gray-900 pb-8">
        Conheça os profissionais e empresas que se destacaram.
      </h2>

      <!-- FILTRO -->

     
     
            <label class="block text-sm font-medium text-gray-700 mb-2">
              Ver por categoria
            </label>
      <div class=" lg:grid lg:grid-cols-2">
        <select
          v-model="selectedCategory"
          class="p-3 border border-gray-300 text-sm rounded-lg w-full focus:ring-2 focus:ring-verde focus:border-verde "
        >
          <option value="">Todos os nomeados</option>
          <option
            v-for="(categoria, index) in Categorias"
            :key="categoria.id ?? index"
            :value="categoria.nome"
          >
            {{ categoria.nome }}
          </option>
        </select>
      </div>
      

      <!-- GRID / SCROLL -->
      <div
        ref="scrollContainer"
        class="flex gap-4 overflow-x-auto hide-scrollbar 
          md:grid md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-3 
          md:overflow-visible pt-4"
      >
        <div
          v-for="(Nomeado, index) in filteredNomeados"
          :key="Nomeado.id ?? index"
          class="bg-gray-100 p-4 rounded-xl shadow-sm flex-shrink-0 
            min-w-[250px] w-[250px] md:w-full flex flex-col h-full"
        >
          <div class="flex flex-col items-center">
            <img
              :src="Nomeado.photoUrl || ''"
              class="h-20 w-20 rounded-full object-cover bg-gray-300"
              alt="Foto do nomeado"
            />
            <h1
              class="text-azul font-open font-bold text-lg md:text-xl text-center pt-4"
            >
              {{ Nomeado.nome }}
            </h1>
          </div>

          <p
            class="font-open text-sm md:text-base text-gray-700 text-center pt-2"
          >
            {{ Nomeado.description }}
          </p>
                   
          <p
            class="font-open text-sm md:text-base text-gray-700 text-center pt-4"
          >  
            <span class="font-bold">Categoria:</span> {{ Nomeado.categoria }}
          </p>

          <!-- BOTÃO DE VOTO -->
          <div class="mt-auto pt-6">
            <button
              :disabled="isVoted(Nomeado.id) || loadingMap[Nomeado.id]"
              @click="Votar(Nomeado.id, Nomeado.categoriaId)"
              class="w-full font-montserrat font-semibold p-2 md:p-3 rounded-lg transition flex items-center justify-center gap-2 
                text-white 
                bg-verde hover:bg-green-700
                disabled:bg-gray-400 disabled:text-gray-200 disabled:cursor-not-allowed"
            >
              <template v-if="loadingMap[Nomeado.id]">
                <svg
                  class="animate-spin h-4 w-4"
                  viewBox="0 0 24 24"
                  fill="none"
                >
                  <circle
                    cx="12"
                    cy="12"
                    r="10"
                    stroke="currentColor"
                    stroke-width="4"
                    class="opacity-25"
                  />
                  <path
                    d="M4 12a8 8 0 018-8"
                    stroke="currentColor"
                    stroke-width="4"
                    class="opacity-75"
                  />
                </svg>
                <span>Votando...</span>
              </template>
              <template v-else>
                <span v-if="isVoted(Nomeado.id)">Votado</span>
                <span v-else>Votar</span>
              </template>
            </button>
            
          </div>
        </div>
        
      </div>
      
       <div v-if="Processar" class="flex items-center justify-center pt-20 lg:pt-40">
          <ProcessarPNI/>
      </div>

      <!-- BULLETS MOBILE -->
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

    <!-- SNACKBAR PROFISSIONAL -->
    <transition name="fade-slide">
      <div
        v-if="snackbar.visible"
        :class="[
          'fixed left-1/2 transform -translate-x-1/2 px-4 py-3 rounded-lg shadow-xl text-white font-open text-sm sm:text-base z-50',
          snackbar.type === 'success' ? 'bg-green-600' : 'bg-red-600'
        ]"
        role="status"
        aria-live="polite"
        style="bottom: 1.5rem; max-width: 90%; width: fit-content;"
      >
        {{ snackbar.message }}
      </div>
    </transition>
  </section>
</template>

<script setup>
import { ref, computed, nextTick, watch, onMounted, onBeforeUnmount } from "vue";
import { http } from "@/Request/api";
import { getBrowserId } from "@/utils/getBrowserId";
import ProcessarPNI from "@/componentes/ProcessarPNI.vue";

const Categorias = ref([]);
const Nomeados = ref([]);
const selectedCategory = ref("");
const scrollContainer = ref(null);
const activeIndex = ref(0);
const Processar = ref(true);

const browserId = ref(null);
const votedSet = ref(new Set());
const loadingMap = ref({});





const snackbar = ref({ visible: false, message: "", type: "success" });
let snackbarTimer = null;

const getNomeados = async () => {
  try {
    const response = await http.get("/candidatos");
    Nomeados.value = Array.isArray(response.data) ? response.data : [];
    console.log(" Nomeados carregados:", Nomeados.value);
    Processar.value= false
  } catch (error) {
    console.error(" Erro ao buscar nomeados:", error);
  }
};

const CarregarCategorias = async () => {
  try {
    const response = await http.get("/categorias/no-user");
    Categorias.value = Array.isArray(response.data) ? response.data : [];
    console.log(" Categorias carregadas:", Categorias.value);
  } catch (error) {
    console.error(" Erro ao buscar categorias:", error);
  }
};

const storageKeyFor = (bId) => `votedCandidates_${bId ?? "unknown"}`;
const loadVotedFromStorage = (bId) => {
  try {
    const raw = localStorage.getItem(storageKeyFor(bId));
    return raw ? new Set(JSON.parse(raw)) : new Set();
  } catch {
    return new Set();
  }
};
const saveVotedToStorage = (bId, set) => {
  localStorage.setItem(storageKeyFor(bId), JSON.stringify(Array.from(set)));
};

const showSnackbar = (msg, type = "success", duration = 3500) => {
  if (snackbarTimer) clearTimeout(snackbarTimer);
  snackbar.value = { visible: true, message: msg, type };
  snackbarTimer = setTimeout(() => (snackbar.value.visible = false), duration);
};

const Votar = async (cdId, ctId) => {
  if (!cdId || !ctId) return showSnackbar("Erro: candidato inválido.", "error");
  if (isVoted(cdId)) return showSnackbar("Já votaste neste candidato.", "error");

  loadingMap.value = { ...loadingMap.value, [cdId]: true };

  const votoDTO = { browserId: browserId.value, candidatoId: cdId, categoriaId: ctId };
  console.log(" Enviando voto:", votoDTO);

  try {
    const response = await http.post("/votos", votoDTO);
    console.log(" Voto registrado com sucesso:", response.data);

    votedSet.value.add(cdId);
    saveVotedToStorage(browserId.value, votedSet.value);
    showSnackbar("Voto realizado com sucesso!", "success");
  } catch (error) {
    console.error(" Erro ao registrar voto:", error);
    const msg = error?.response?.data?.message ?? "Erro ao registrar voto.";
    showSnackbar(msg, "error");
  } finally {
    loadingMap.value = { ...loadingMap.value, [cdId]: false };
  }
};

const isVoted = (id) => votedSet.value.has(id);

const filteredNomeados = computed(() => {
  if (!selectedCategory.value) return Nomeados.value;
  return Nomeados.value.filter((n) => n.categoria === selectedCategory.value);
});

const scrollToCard = (i) => {
  const el = scrollContainer.value;
  const child = el?.children[i];
  if (el && child)
    el.scrollTo({ left: child.offsetLeft - el.offsetLeft, behavior: "smooth" });
};

const onScroll = () => {
  const el = scrollContainer.value;
  if (!el) return;
  const center = el.scrollLeft + el.clientWidth / 2;
  let closest = 0, minDist = Infinity;
  Array.from(el.children).forEach((child, idx) => {
    const dist = Math.abs(child.offsetLeft + child.clientWidth / 2 - center);
    if (dist < minDist) (minDist = dist), (closest = idx);
  });
  activeIndex.value = closest;
};

watch([selectedCategory, filteredNomeados], () => {
  nextTick(() => {
    activeIndex.value = 0;
    scrollContainer.value?.scrollTo({ left: 0, behavior: "smooth" });
  });
});

onMounted(async () => {
  const bId = await getBrowserId();
  browserId.value = bId;
  votedSet.value = loadVotedFromStorage(bId);
  await getNomeados();
  await CarregarCategorias();

  scrollContainer.value?.addEventListener("scroll", onScroll, { passive: true });
});

onBeforeUnmount(() => {
  scrollContainer.value?.removeEventListener("scroll", onScroll);
  if (snackbarTimer) clearTimeout(snackbarTimer);
});
</script>

<style>
.hide-scrollbar::-webkit-scrollbar {
  display: none;
}
.hide-scrollbar {
  -ms-overflow-style: none;
  scrollbar-width: none;
}

/* Animação snackbar */
.fade-slide-enter-active,
.fade-slide-leave-active {
  transition: all 0.3s ease;
}
.fade-slide-enter-from,
.fade-slide-leave-to {
  opacity: 0;
  transform: translateY(10px);
}
</style>
