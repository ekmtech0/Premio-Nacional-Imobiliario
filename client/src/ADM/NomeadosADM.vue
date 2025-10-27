<template>
  <div class="flex min-h-screen bg-gray-50">
    <!-- Sidebar (somente desktop) -->
    <SideBar class="" />

    <!-- Main -->
    <main class="flex-1 w-full">
      <HeaderADM />

      <div class="lg:p-4 px-4 pb-8">
        <!-- ✅ Header + ações responsivas -->
        <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between mt-8 gap-4">
          <div>
            <h2 class="font-bold text-xl text-azul">🧑‍⚖️ Gestão dos Nomeados</h2>
            <p class="mt-1 text-sm text-gray-500">Gerir nomeados por categoria</p>
          </div>

          <div class="flex flex-col xs:flex-row xs:items-center gap-3 w-full sm:w-auto">
            <div class="flex items-center space-x-2 w-full sm:w-auto">
              <label class="text-sm text-gray-600 whitespace-nowrap">Filtrar:</label>
              <select
                v-model="filtroCategoria"
                class="border rounded-lg px-3 py-2 text-sm w-full sm:w-auto focus:ring-azul focus:ring-1">
                <option value="">Todas Categorias</option>
                <option v-for="(c,i) in categorias" :key="i" :value="c.nome">{{ c.nome }}</option>
              </select>
            </div>

            <button
              @click="abrirAdicionar"
              class="bg-azul text-white px-4 py-2 rounded-xl shadow hover:bg-blue-800 transition text-sm w-full sm:w-auto">
              + Adicionar Nomeado
            </button>
          </div>
        </div>

        <!--  Contadores - mais fluido no mobile -->
        <div class="mt-6 grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 gap-3">
          <div
            v-for="(c, idx) in categorias"
            :key="idx"
            class="bg-white p-4 rounded-xl shadow text-center sm:text-left">
            <p class="text-xs text-gray-500">Categoria</p>
            <h4 class="font-semibold text-gray-900 text-sm truncate">{{ c.nome }}</h4>
            <p class="text-xs text-gray-500 mt-2">Nomeados</p>
            <p class="text-xl font-bold text-azul">{{ contarPorCategoria(c.nome) }}</p>
          </div>
        </div>

        <!--  Tabela ajustada com scroll no mobile se for necessário -->
        <div class="mt-6 bg-white rounded-xl shadow overflow-x-auto">
          <table class="min-w-full hidden md:table">
            <thead class="bg-gray-100">
              <tr>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-600 uppercase">Foto</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-600 uppercase">Nome</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-600 uppercase">Descrição</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-600 uppercase">Categoria</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-600 uppercase">Votos</th>
                <th class="px-6 py-3 text-right"></th>
              </tr>
            </thead>

            <tbody>
              <tr v-for="(n, index) in listaFiltrada" :key="index" class="border-t hover:bg-gray-50">
                <td class="px-6 py-4"><img :src="n.foto" class="w-12 h-12 rounded-full object-cover border" /></td>
                <td class="px-6 py-4 font-medium text-gray-800">{{ n.nome }}</td>
                <td class="px-6 py-4 text-gray-600 text-sm max-w-sm truncate">{{ n.descricao }}</td>
                <td class="px-6 py-4 text-gray-700">{{ n.categoria }}</td>
                <td class="px-6 py-4 text-gray-700">{{ n.votos }}</td>
                <td class="px-6 py-4 text-right space-x-3">
                  <button @click="abrirEditar(index)" class="text-sm text-yellow-600">✏️ Editar</button>
                  <button @click="removerNomeado(index)" class="text-sm text-red-600">🗑️ Remover</button>
                </td>
              </tr>
            </tbody>
          </table>

          <!-- ✅ Mobile Cards -->
          <div class="md:hidden divide-y">
            <div v-for="(n, index) in listaFiltrada" :key="index" class="p-4">
              <div class="flex items-start gap-3">
                <img :src="n.foto" class="w-14 h-14 rounded-full object-cover border flex-shrink-0" />
                <div class="flex-1">
                  <p class="font-semibold text-gray-900">{{ n.nome }}</p>
                  <p class="text-gray-600 text-sm mt-1 line-clamp-3">{{ n.descricao }}</p>
                  <p class="text-xs text-gray-500 mt-2"><strong>Categoria:</strong> {{ n.categoria }}</p>
                  <p class="text-xs text-gray-500"><strong>Votos:</strong> {{ n.votos }}</p>
                </div>
              </div>

              <div class="flex justify-end gap-4 mt-3">
                <button @click="abrirEditar(index)" class="text-yellow-600 text-sm">✏️ Editar</button>
                <button @click="removerNomeado(index)" class="text-red-600 text-sm">🗑️ Remover</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </main>

   <!-- ✅ MODAL FUNCIONAL -->
<div v-if="mostrarModal" class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50 px-4">
  <div class="bg-white rounded-xl shadow-lg p-6 w-full max-w-lg max-h-[90vh] overflow-y-auto">

    <h3 class="text-lg font-bold text-azul">
      {{ editando ? '✏️ Editar Nomeado' : '➕ Adicionar Nomeado' }}
    </h3>

    <div class="mt-4 flex flex-col space-y-4">
      <!-- Nome -->
      <input
        v-model="form.nome"
        type="text"
        placeholder="Nome do Nomeado"
        class="border rounded-lg px-4 py-2 w-full focus:ring-azul focus:ring-1"
      />

      <!-- Categoria -->
      <select
        v-model="form.categoria"
        class="border rounded-lg px-4 py-2 w-full focus:ring-azul focus:ring-1">
        <option disabled value="">Selecione a Categoria</option>
        <option v-for="(c,i) in categorias" :key="i" :value="c.nome">{{ c.nome }}</option>
      </select>

      <!-- Descrição -->
      <textarea
        v-model="form.descricao"
        rows="3"
        placeholder="Descrição"
        class="border rounded-lg px-4 py-2 w-full focus:ring-azul focus:ring-1"></textarea>

      <!-- Foto -->
      <div>
        <label class="text-sm text-gray-600">Foto do Nomeado</label>
        <input type="file" @change="onFileChange" class="block w-full mt-1 text-sm">
        <div v-if="previewFoto" class="mt-3">
          <img :src="previewFoto" alt="Preview" class="w-20 h-20 object-cover rounded-full border" />
        </div>
      </div>
    </div>

    <!-- Botões -->
    <div class="flex justify-end gap-3 mt-6">
      <button @click="fecharModal" class="px-4 py-2 bg-gray-200 text-gray-700 rounded-lg hover:bg-gray-300 text-sm">
        Cancelar
      </button>
      <button @click="salvar" class="px-4 py-2 bg-azul text-white rounded-lg hover:bg-blue-800 transition text-sm">
        {{ editando ? 'Guardar Alterações' : 'Salvar' }}
      </button>
    </div>
  </div>
</div>

  </div>
</template>


<script setup>
import { ref, computed } from 'vue'
import SideBar from './SideBar.vue'
import HeaderADM from './HeaderADM.vue'

const categorias = ref([
  { nome: "Arquitetura Imobiliária de Excelência" },
  { nome: "Serviço Público na Habitação" },
  { nome: "Mediação Imobiliária de Referência" },
  { nome: "Prémio Especial: Sustentabilidade & Eficiência" }
])

const nomeados = ref([
  { nome: 'Victor Makuka', categoria: 'Arquitetura Imobiliária de Excelência', descricao: 'Project leader e dev full-stack', votos: 0, foto: '' },
  { nome: 'SG Design', categoria: 'Mediação Imobiliária de Referência', descricao: 'Agência de design especializada em branding', votos: 0, foto: '' },
])

const mostrarModal = ref(false)
const editando = ref(false)
const indexEdit = ref(null)
const filtroCategoria = ref('')
const previewFoto = ref(null)

const form = ref({ nome: '', categoria: '', descricao: '', votos: 0, foto: '' })

const listaFiltrada = computed(() => {
  if (!filtroCategoria.value) return nomeados.value
  return nomeados.value.filter(n => n.categoria === filtroCategoria.value)
})

const abrirAdicionar = () => {
  editando.value = false
  indexEdit.value = null
  form.value = { nome: '', categoria: '', descricao: '', votos: 0, foto: '' }
  previewFoto.value = null
  mostrarModal.value = true
}

const abrirEditar = (idx) => {
  editando.value = true
  indexEdit.value = idx
  form.value = { ...nomeados.value[idx] }
  previewFoto.value = nomeados.value[idx].foto || null
  mostrarModal.value = true
}

const onFileChange = (event) => {
  const file = event.target.files[0]
  if (file) {
    const reader = new FileReader()
    reader.onload = e => {
      previewFoto.value = e.target.result
      form.value.foto = e.target.result
    }
    reader.readAsDataURL(file)
  }
}

const salvar = () => {
  if (!form.value.nome.trim()) return alert('Por favor insere o nome.')
  if (!form.value.categoria) return alert('Seleciona a categoria.')

  if (editando.value && indexEdit.value !== null) {
    nomeados.value[indexEdit.value] = { ...form.value }
  } else {
    nomeados.value.push({ ...form.value, votos: 0 })
  }

  fecharModal()
}

const removerNomeado = (idx) => {
  if (confirm('Tens certeza que queres remover este nomeado?')) {
    nomeados.value.splice(idx, 1)
  }
}

const fecharModal = () => {
  mostrarModal.value = false
}

const contarPorCategoria = (categoriaNome) => {
  return nomeados.value.filter(n => n.categoria === categoriaNome).length
}
</script>

<style scoped>
.line-clamp-3 {
  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
</style>
