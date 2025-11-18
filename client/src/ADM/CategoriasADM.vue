<template>
  <div class="flex min-h-screen bg-gray-50">
    <!-- Sidebar -->
    <SideBar />

    <main class="flex-1 ml-0">
      <!-- Header -->
      <HeaderADM />

      <div class="lg:p-4 px-4">
        <!-- Título + Botão -->
        <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between mt-10 gap-3">
          <h2 class="font-bold text-xl text-azul flex">
            <svg class="w-5 h-5 mt-1 text-azul" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" viewBox="0 0 24 24">
  <path fill-rule="evenodd" d="M9 2.221V7H4.221a2 2 0 0 1 .365-.5L8.5 2.586A2 2 0 0 1 9 2.22ZM11 2v5a2 2 0 0 1-2 2H4v11a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V4a2 2 0 0 0-2-2h-7ZM8 16a1 1 0 0 1 1-1h6a1 1 0 1 1 0 2H9a1 1 0 0 1-1-1Zm1-5a1 1 0 1 0 0 2h6a1 1 0 1 0 0-2H9Z" clip-rule="evenodd"/>
</svg>
 Gestão de Categorias</h2>
          <button
            @click="abrirModalAdicionar"
            class="bg-azul text-white px-4 py-2 rounded-xl shadow hover:bg-blue-800 transition text-sm sm:text-base">
            + Adicionar Categoria
          </button>
        </div>

        <!-- Tabela desktop -->
        <div class="mt-6 bg-white rounded-xl shadow overflow-hidden">
          <table class="min-w-full hidden md:table">
            <thead class="bg-gray-100">
              <tr>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-600 uppercase">Categoria</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-600 uppercase">Descrição</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-600 uppercase">Candidatos</th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="(cat, index) in categorias"
                :key="cat.id"
                class="border-t hover:bg-gray-50 cursor-pointer"
                @click="abrirModalCandidatos(cat)"
              >
                <td class="px-6 py-4 font-medium text-gray-800">{{ cat.nome }}</td>
                <td class="px-6 py-4 text-gray-600 text-sm">{{ cat.description }}</td>
                <td class="px-6 py-4 text-gray-700">{{ cat.candidatos?.length || 0 }} candidatos</td>
                <td class="px-6 py-4 text-right space-x-3">
                  <button
                    @click.stop="abrirModalEditar(index)"
                    class="text-sm text-yellow-600 hover:text-yellow-700 font-medium"
                  >
                    ✏️ Editar
                  </button>
                  <button
                    @click.stop="eliminarCategoria(index)"
                    class="text-sm text-red-600 hover:text-red-700 font-medium"
                  >
                    🗑️ Eliminar
                  </button>
                </td>
              </tr>
            </tbody>
          </table>

          <!-- Mobile Cards -->
          <div class="md:hidden divide-y">
            <div
              v-for="(cat, index) in categorias"
              :key="cat.id"
              class="p-4 cursor-pointer hover:bg-gray-50"
              @click="abrirModalCandidatos(cat)"
            >
              <div class="flex justify-between">
                <p class="font-semibold text-gray-900">{{ cat.nome }}</p>
                <P v-if="cat.isSpecialCetgory">Categoria especial</P>
              </div>
              <p class="text-gray-600 text-sm mt-1">{{ cat.description }}</p>
              <p class="text-gray-700 text-sm mt-2">{{ cat.candidatos?.length || 0 }} candidatos</p>

              <div class="flex justify-end gap-4 mt-3">
                <button @click.stop="abrirModalEditar(index)" class="text-yellow-600 text-sm">✏️ Editar</button>
                <button @click.stop="eliminarCategoria(index)" class="text-red-600 text-sm">🗑️ Eliminar</button>
              </div>
            </div>
          </div>
        </div>
      </div>
           <div v-if="Processar" class="flex items-center justify-center pt-20 lg:pt-40 mb-24">
          <ProcessarPNI/>
      </div>
    </main>

    <!-- Modal: Adicionar / Editar Categoria -->
    <div
      v-if="mostrarModal"
      class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50 px-4"
    >
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
            v-model="formCategoria.description"
            placeholder="Descrição da Categoria"
            rows="3"
            class="border rounded-lg px-4 py-2 w-full focus:outline-none focus:ring-azul focus:ring-1"
          ></textarea>
          <div class="flex justify-between">
            <label for="IsSpecial" >Categoria especial</label>
            <input type="checkbox" id="IsSpecial" class="w-4" v-model="formCategoria.isSpecial">
          </div>
        </div>

        <div class="flex justify-end space-x-3 mt-6">
          <button
            @click="fecharModal"
            class="px-4 py-2 bg-gray-200 text-gray-700 rounded-lg hover:bg-gray-300 text-sm"
          >
            Cancelar
          </button>
          <button
            @click="salvarCategoria"
            class="px-4 py-2 bg-azul text-white rounded-lg hover:bg-blue-800 transition text-sm"
          >
            {{ editando ? 'Guardar Alterações' : 'Salvar' }}
          </button>
        </div>
      </div>
    </div>

    <!-- Modal: Candidatos da Categoria -->
    <div
      v-if="mostrarModalCandidatos"
      class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50 px-4"
    >
      <div class="bg-white rounded-xl shadow-lg p-6 w-full max-w-2xl mx-auto relative">
        <h3 class="text-lg font-bold text-azul mb-4">
          👥 Candidatos de "{{ categoriaSelecionada?.nome }}"
        </h3>

        <div v-if="categoriaSelecionada?.candidatos?.length" class="space-y-3 max-h-[400px] overflow-y-auto">
          <div
            v-for="cand in categoriaSelecionada.candidatos"
            :key="cand.id"
            class="flex items-center justify-between border p-3 rounded-lg"
          >
            <div>
              <p class="font-semibold">{{ cand.nome }}</p>
              <p class="text-gray-600 text-sm">{{ cand.description }}</p>
            </div>
            <div class="w-12 h-12 rounded-full bg-gray-200 flex items-center justify-center text-gray-500 text-xs">
              {{ cand.photoUrl ? '📷' : '—' }}
            </div>
          </div>
        </div>

        <p v-else class="text-gray-600 text-center py-6">Nenhum candidato nesta categoria.</p>

        <button
          @click="fecharModalCandidatos"
          class="absolute top-2 right-3 text-gray-600 hover:text-red-600 text-lg"
        >
          ✖
        </button>
      </div>
    </div>
  </div>
   <StatusModal
  :visivel="mostrarStatus"
  :tipo="statusTipo"
  :mensagem="statusMensagem"
  @fechar="mostrarStatus = false"
/>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import SideBar from './SideBar.vue'
import HeaderADM from './HeaderADM.vue'
import { http } from '@/Request/api' // Instância do Axios
import StatusModal from './StatusModal.vue'
import ProcessarPNI from '@/componentes/ProcessarPNI.vue'

// Estado principal
const categorias = ref([])
const statusTipo = ref('sucesso') // 'sucesso' ou 'erro'
const statusMensagem = ref('')
const mostrarStatus = ref(false)
// Modais
const mostrarModal = ref(false)
const mostrarModalCandidatos = ref(false)
const Processar = ref(true)

// Controle de edição
const editando = ref(false)
const categoriaEditIndex = ref(null)
const categoriaSelecionada = ref(null)

// Formulário
const formCategoria = ref({ nome: '', description: '', isSpecial:false })

// Buscar categorias da API
const carregarCategorias = async () => {
  try {
    const response = await http.get('/categorias')
    categorias.value = response.data
    Processar.value = false
  } catch (error) {
    console.error('Erro ao carregar categorias:', error)
  }
}

// Abrir modal de adicionar
const abrirModalAdicionar = () => {
  editando.value = false
  formCategoria.value = { nome: '', description: '' }
  mostrarModal.value = true
}

// Abrir modal de edição
const abrirModalEditar = (index) => {
  editando.value = true
  categoriaEditIndex.value = index
  formCategoria.value = { ...categorias.value[index] }
  mostrarModal.value = true
}

// Salvar categoria (POST/PUT)
const salvarCategoria = async () => {
  if (!formCategoria.value.nome.trim()) return

  try {
    if (editando.value) {
      const id = categorias.value[categoriaEditIndex.value].id
      const response = await http.put(`/categorias/${id}`, formCategoria.value)
      categorias.value[categoriaEditIndex.value] = response.data
    } else {
      const response = await http.post('/categorias', formCategoria.value)
      categorias.value.push(response.data)
    }
    statusMensagem.value = editando.value ? 'Categoria atualizada com sucesso!' : 'Categoria adicionada com sucesso!'
    statusTipo.value = 'sucesso'
    mostrarStatus.value = true
    mostrarModal.value = false
  } catch (error) {
    console.error('Erro ao salvar categoria:', error)
    statusMensagem.value = 'Ocorreu um erro ao salvar a categoria.'
    statusTipo.value = 'erro'
    mostrarStatus.value = true
  }
}

// Eliminar categoria
const eliminarCategoria = async (index) => {
  const categoria = categorias.value[index]
  if (confirm(`Tens certeza que queres eliminar "${categoria.nome}"?`)) {
    try {
      await http.delete(`/categorias/${categoria.id}`)
      categorias.value.splice(index, 1)
      statusMensagem.value = 'Categoria eliminada com sucesso!'
      statusTipo.value = 'sucesso'
      mostrarStatus.value = true
    } catch (error) {
      console.error('Erro ao eliminar categoria:', error)
      statusMensagem.value = 'Ocorreu um erro ao eliminar a categoria.'
      statusTipo.value = 'erro'
      mostrarStatus.value = true
    }
  }
}

// Modal de candidatos
const abrirModalCandidatos = (cat) => {
  categoriaSelecionada.value = cat
  mostrarModalCandidatos.value = true
}

const fecharModalCandidatos = () => {
  mostrarModalCandidatos.value = false
  categoriaSelecionada.value = null
}

// Fechar modal geral
const fecharModal = () => (mostrarModal.value = false)

// Buscar categorias ao montar
onMounted(() => {
  carregarCategorias()
})
</script>
