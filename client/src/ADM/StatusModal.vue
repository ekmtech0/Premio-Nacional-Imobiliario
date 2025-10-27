<template>
  <transition name="fade">
    <div
      v-if="visivel"
      class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-40 z-[9999]"
    >
      <div
        class="bg-white rounded-2xl shadow-lg p-6 w-72 flex flex-col items-center space-y-3 animate-pop"
      >
        <!-- Ícone animado -->
        <div v-if="tipo === 'sucesso'" class="text-green-500 text-6xl animate-bounce">✅</div>
        <div v-else class="text-red-500 text-6xl animate-bounce">❌</div>

        <!-- Mensagem -->
        <h3
          class="text-lg font-bold"
          :class="tipo === 'sucesso' ? 'text-green-600' : 'text-red-600'"
        >
          {{ tipo === 'sucesso' ? 'Concluído!' : 'Falha!' }}
        </h3>

        <p class="text-gray-600 text-sm text-center">
          {{ mensagem }}
        </p>
      </div>
    </div>
  </transition>
</template>

<script setup>
import { ref, watch, onMounted } from 'vue'

// Props recebidas do pai
const props = defineProps({
  visivel: Boolean,
  tipo: {
    type: String,
    default: 'sucesso', // 'sucesso' ou 'erro'
  },
  mensagem: {
    type: String,
    default: '',
  },
  autoFechar: {
    type: Boolean,
    default: true,
  },
  duracao: {
    type: Number,
    default: 2000, // 2 segundos
  },
})

// Emite evento quando fecha
const emit = defineEmits(['fechar'])

// Fecha automaticamente
watch(
  () => props.visivel,
  (novoValor) => {
    if (novoValor && props.autoFechar) {
      setTimeout(() => emit('fechar'), props.duracao)
    }
  }
)
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s ease;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

@keyframes pop {
  0% {
    transform: scale(0.8);
    opacity: 0;
  }
  100% {
    transform: scale(1);
    opacity: 1;
  }
}
.animate-pop {
  animation: pop 0.3s ease;
}
</style>
