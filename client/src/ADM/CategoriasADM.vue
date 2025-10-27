<template>
  <div class="flex min-h-screen bg-gray-50">
    <!-- Sidebar -->
    <SideBar  />

    <main class="flex-1 ml-0">
      <!-- Header -->
      <HeaderADM />

      <div class="lg:p-4 px-4">
        <!-- Título + Botão -->
        <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between mt-10 gap-3">
          <h2 class="font-bold text-xl text-azul">🗂 Gestão de Categorias</h2>
          <button
            @click="abrirModalAdicionar"
            class="bg-azul text-white px-4 py-2 rounded-xl shadow hover:bg-blue-800 transition text-sm sm:text-base">
            + Adicionar Categoria
          </button>
        </div>

        <!-- Tabela desktop / Cards mobile -->
        <div class="mt-6 bg-white rounded-xl shadow overflow-hidden">
          <!-- Desktop -->
          <table class="min-w-full hidden md:table">
            <thead class="bg-gray-100">
              <tr>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-600 uppercase">Categoria</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-600 uppercase">Descrição</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-600 uppercase">Nomeados</th>
                <th class="px-6 py-3 text-right"></th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="(cat, index) in categorias"
                :key="index"
                class="border-t hover:bg-gray-50">
                <td class="px-6 py-4 font-medium text-gray-800">{{ cat.nome }}</td>
                <td class="px-6 py-4 text-gray-600 text-sm">{{ cat.descricao }}</td>
                <td class="px-6 py-4 text-gray-700">{{ cat.nomeados.length }} nomeados</td>
                <td class="px-6 py-4 text-right space-x-3">
                  <button @click="abrirModalEditar(index)" class="text-sm text-yellow-600 hover:text-yellow-700 font-medium">✏️ Editar</button>
                  <button @click="eliminarCategoria(index)" class="text-sm text-red-600 hover:text-red-700 font-medium">🗑️ Eliminar</button>
                </td>
              </tr>
            </tbody>
          </table>

          <!-- Mobile Cards -->
          <div class="md:hidden divide-y">
            <div v-for="(cat, index) in categorias" :key="index" class="p-4">
              <p class="font-semibold text-gray-900">{{ cat.nome }}</p>
              <p class="text-gray-600 text-sm mt-1">{{ cat.descricao }}</p>
              <p class="text-gray-700 text-sm mt-2">{{ cat.nomeados.length }} nomeados</p>

              <div class="flex justify-end gap-4 mt-3">
                <button @click="abrirModalEditar(index)" class="text-yellow-600 text-sm">✏️ Editar</button>
                <button @click="eliminarCategoria(index)" class="text-red-600 text-sm">🗑️ Eliminar</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </main>

    <!-- Modal -->
    <div
      v-if="mostrarModal"
      class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50 px-4">
      <div class="bg-white rounded-xl shadow-lg p-6 w-full max-w-lg mx-auto">
        <h3 class="text-lg font-bold text-azul">
          {{ editando ? 'Editar Categoria' : 'Adicionar Nova Categoria' }}
        </h3>

        <div class="mt-4 flex flex-col space-y-4">
          <input
            v-model="formCategoria.nome"
            type="text"
            placeholder="Nome da Categoria"
            class="border rounded-lg px-4 py-2 w-full focus:outline-none focus:ring-azul focus:ring-1"
          />

          <textarea
            v-model="formCategoria.descricao"
            placeholder="Descrição da Categoria"
            rows="3"
            class="border rounded-lg px-4 py-2 w-full focus:outline-none focus:ring-azul focus:ring-1"
          ></textarea>
        </div>

        <div class="flex justify-end space-x-3 mt-6">
          <button @click="fecharModal" class="px-4 py-2 bg-gray-200 text-gray-700 rounded-lg hover:bg-gray-300 text-sm">Cancelar</button>
          <button @click="salvarCategoria" class="px-4 py-2 bg-azul text-white rounded-lg hover:bg-blue-800 transition text-sm">
            {{ editando ? 'Guardar Alterações' : 'Salvar' }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>


<script setup>
import { ref } from 'vue'
import SideBar from './SideBar.vue'
import HeaderADM from './HeaderADM.vue'

const categorias = ref([
  { nome: "Arquitetura Imobiliária de Excelência", descricao: "Projetos de arquitetura inovadora e excelência construtiva.", nomeados: ["Victor Makuka", "SG Design", "EKM Tech Solutions"] },
  { nome: "Serviço Público na Habitação", descricao: "Iniciativas públicas de habitação e impacto social.", nomeados: ["Victor Makuka", "Linear Comunicação"] },
  { nome: "Mediação Imobiliária de Referência", descricao: "Empresas com atuação destacada no setor imobiliário.", nomeados: ["SG Design", "EKM Tech Solutions", "Victor Makuka"] },
  { nome: "Prémio Especial: Sustentabilidade & Eficiência", descricao: "Projetos focados em sustentabilidade e uso eficiente de recursos.", nomeados: ["Linear Comunicação", "SG Design"] }
])

const mostrarModal = ref(false)
const editando = ref(false)
const categoriaEditIndex = ref(null)

const formCategoria = ref({ nome: '', descricao: '', nomeados: [] })

const abrirModalAdicionar = () => {
  editando.value = false
  formCategoria.value = { nome: '', descricao: '', nomeados: [] }
  mostrarModal.value = true
}

const abrirModalEditar = (index) => {
  editando.value = true
  categoriaEditIndex.value = index
  formCategoria.value = { ...categorias.value[index] }
  mostrarModal.value = true
}

const salvarCategoria = () => {
  if (!formCategoria.value.nome.trim()) return

  if (editando.value) {
    categorias.value[categoriaEditIndex.value] = { ...formCategoria.value }
  } else {
    categorias.value.push({ ...formCategoria.value })
  }
  mostrarModal.value = false
}

const eliminarCategoria = (index) => {
  if (confirm('Tens certeza que queres eliminar esta categoria?')) {
    categorias.value.splice(index, 1)
  }
}

const fecharModal = () => (mostrarModal.value = false)
</script>
