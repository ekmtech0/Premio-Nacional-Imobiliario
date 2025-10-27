<template>
  <div class="flex min-h-screen bg-gray-100">
    <!-- 🔹 Botão Hambúrguer (Mobile) -->
    <button 
      @click="isOpen = !isOpen"
      class="md:hidden fixed top-4 left-4 z-50 p-3 bg-azul text-white rounded-lg shadow-md transition-transform active:scale-95"
      aria-label="Toggle Menu"
    >
      ☰
    </button>

    <!-- 🔹 Sidebar -->
    <aside
      :class="[
        'fixed top-0 left-0 h-full w-64 bg-azul text-white p-6 shadow-lg transition-transform duration-300 z-40',
        isOpen ? 'translate-x-0' : '-translate-x-full',
        'md:translate-x-0 md:relative md:block'
      ]"
    >
      <h2 class="text-xl font-bold mb-6">🏆 PNI 2025 - Admin</h2>
      <nav class="flex flex-col space-y-1 text-sm lg:text-base font-medium">
        <RouterLink to="/DashboardPNI" class="hover:bg-white/10 p-3 rounded transition">Dashboard</RouterLink>
        <RouterLink to="/categoriasADM" class="hover:bg-white/10 p-3 rounded transition">Categorias</RouterLink>
        <RouterLink to="/NomeadosADM" class="hover:bg-white/10 p-3 rounded transition">Nomeados</RouterLink>
        <RouterLink to="/VotacaoADM" class="hover:bg-white/10 p-3 rounded transition">Votação do Público</RouterLink>
        <RouterLink to="/ResultadoADM" class="hover:bg-white/10 p-3 rounded transition">Resultados</RouterLink>
        <RouterLink to="/UsuarioADM" class="hover:bg-white/10 p-3 rounded transition">Usuários</RouterLink>
        <RouterLink class="hover:bg-white/10 flex items-center p-3 rounded transition" to="/login">Sair</RouterLink>
      </nav>
    </aside>

   
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { RouterLink } from 'vue-router';
const isOpen = ref(false)
</script>
