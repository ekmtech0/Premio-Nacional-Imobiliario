<template>
  <div class="flex min-h-screen bg-gray-50">
    <SideBar />
    <main class="flex-1 w-full">
      <HeaderADM />

      <div class="lg:p-4 px-4 pb-8">
        <!-- Header + ações -->
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
                <option v-for="c in categorias" :key="c.id" :value="c.id">{{ c.nome }}</option>
              </select>
            </div>

            <button
              @click="abrirAdicionar"
              class="bg-azul text-white px-4 py-2 rounded-xl shadow hover:bg-blue-800 transition text-sm w-full sm:w-auto">
              + Adicionar Nomeado
            </button>
          </div>
        </div>

        <!-- Tabela Desktop / Cards Mobile -->
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
              <tr v-for="(n, index) in listaFiltrada" :key="n.id" class="border-t hover:bg-gray-50">
                <td class="px-6 py-4">
                  <img :src="n.photoUrl || ''" class="w-12 h-12 rounded-full object-cover border" />
                </td>
                <td class="px-6 py-4 font-medium text-gray-800">{{ n.nome }}</td>
                <td class="px-6 py-4 text-gray-600 text-sm max-w-sm truncate">{{ n.description }}</td>
                <td class="px-6 py-4 text-gray-700">{{ getCategoriaNome(n.categoria ?? n.categoriaId ?? n.categoria_id) }}</td>
                <td class="px-6 py-4 text-gray-700">{{ n.votos || 0 }}</td>
                <td class="px-6 py-4 text-right space-x-3">
                  <button @click="abrirEditar(index)" class="text-sm text-yellow-600">✏️ Editar</button>
                  <button @click="removerNomeado(index)" class="text-sm text-red-600">🗑️ Remover</button>
                </td>
              </tr>
            </tbody>
          </table>

          <!-- Mobile Cards -->
          <div class="md:hidden divide-y">
            <div v-for="(n, index) in listaFiltrada" :key="n.id" class="p-4">
              <div class="flex items-start gap-3">
                <img :src="n.photoUrl || ''" class="w-14 h-14 rounded-full object-cover border flex-shrink-0" />
                <div class="flex-1">
                  <p class="font-semibold text-gray-900">{{ n.nome }}</p>
                  <p class="text-gray-600 text-sm mt-1 line-clamp-3">{{ n.description }}</p>
                  <p class="text-xs text-gray-500 mt-2"><strong>Categoria:</strong>{{ n.categoria }}</p>
                  <p class="text-xs text-gray-500"><strong>Votos:</strong> {{ n.votos || 0 }}</p>
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

    <!-- Modal -->
    <div v-if="mostrarModal" class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50 px-4">
      <div class="bg-white rounded-xl shadow-lg p-6 w-full max-w-lg max-h-[90vh] overflow-y-auto">
        <h3 class="text-lg font-bold text-azul">
          {{ editando ? '✏️ Editar Nomeado' : '➕ Adicionar Nomeado' }}
        </h3>

        <div class="mt-4 flex flex-col space-y-4">
          <input v-model="form.nome" type="text" placeholder="Nome do Nomeado" class="border rounded-lg px-4 py-2 w-full focus:ring-azul focus:ring-1" />

          <select v-model="form.categoriaId" class="border rounded-lg px-4 py-2 w-full focus:ring-azul focus:ring-1">
            <option disabled value="">Selecione a Categoria</option>
            <option v-for="c in categorias" :key="c.id" :value="c.id">{{ c.nome }}</option>
          </select>

          <textarea v-model="form.description" rows="3" placeholder="Descrição" class="border rounded-lg px-4 py-2 w-full focus:ring-azul focus:ring-1"></textarea>

          <div>
            <label class="text-sm text-gray-600">Foto do Nomeado</label>
            <input type="file" @change="onFileChange" class="block w-full mt-1 text-sm" />
            <div v-if="previewFoto" class="mt-3">
              <img :src="previewFoto" alt="Preview" class="w-20 h-20 object-cover rounded-full border" />
            </div>
          </div>
        </div>

        <div class="flex justify-end gap-3 mt-6">
          <button @click="fecharModal" class="px-4 py-2 bg-gray-200 text-gray-700 rounded-lg hover:bg-gray-300 text-sm">Cancelar</button>
          <button @click="salvar" class="px-4 py-2 bg-azul text-white rounded-lg hover:bg-blue-800 transition text-sm">{{ editando ? 'Guardar Alterações' : 'Salvar' }}</button>
        </div>
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
import { ref, computed, onMounted } from 'vue'
import SideBar from './SideBar.vue'
import HeaderADM from './HeaderADM.vue'
import { http } from '@/Request/api'
import StatusModal from './StatusModal.vue'


// Estados
const categorias = ref([])
const nomeados = ref([])
const filtroCategoria = ref('')
const mostrarModal = ref(false)
const editando = ref(false)
const indexEdit = ref(null)
const previewFoto = ref(null)
const form = ref({ nome: '', categoriaId: '', description: '', votos: 0, photoUrl: '' })

// Buscar categorias
const getCategorias = async () => {
  try {
    const res = await http.get('/categorias/no-user')
    categorias.value = res.data
  } catch (error) {
    console.error('Erro ao buscar categorias:', error)
  }
}

// --- Status Modal ---
const mostrarStatus = ref(false)
const statusTipo = ref('')       // 'sucesso' | 'erro'
const statusMensagem = ref('')


// Buscar todos os nomeados
const getNomeados = async () => {
  try {
    const res = await http.get('/candidatos')
    console.log(res.data)
    nomeados.value = res.data
  } catch (error) {
    console.error('Erro ao buscar nomeados:', error)
  }
}

// Nome da categoria pelo Id
// Nome da categoria pelo Id ou objeto
const getCategoriaNome = (categoria) => {
  if (!categoria) return '—'

  // Caso 1: a API retorna um objeto { id, nome }
  if (typeof categoria === 'object') {
    // se o campo nome existir dentro do objeto
    if (categoria.nome) return categoria.nome
    // se o objeto for algo como { categoriaId: 1 }
    const catObjId = categoria.id ?? categoria.categoriaId ?? categoria.categoria_id
    const foundObj = categorias.value.find(c => String(c.id) === String(catObjId))
    return foundObj ? foundObj.nome : '—'
  }

  // Caso 2: se vier como ID (número ou string)
  const found = categorias.value.find(c => String(c.id) === String(categoria))
  if (found) return found.nome

  // Caso 3: se a API já enviou o nome direto como string
  if (typeof categoria === 'string' && isNaN(categoria)) return categoria

  return '—'
}


// Modal adicionar/editar
const abrirAdicionar = () => {
  editando.value = false
  indexEdit.value = null
  form.value = { nome: '', categoriaId: '', description: '', votos: 0, photoUrl: '' }
  previewFoto.value = null
  mostrarModal.value = true
}

const abrirEditar = (idx) => {
  editando.value = true
  indexEdit.value = idx
  const item = nomeados.value[idx]
  // mapear possíveis formatos de categoria para o campo form.categoriaId
  const categoriaId = item.categoriaId ?? item.categoria_id ?? (item.categoria && (item.categoria.id ?? item.categoria)) ?? ''
  form.value = { 
    nome: item.nome || '',
    categoriaId,
    description: item.description || '',
    votos: item.votos || 0,
    photoUrl: item.photoUrl || ''
  }
  previewFoto.value = item.photoUrl || null
  mostrarModal.value = true
}

const fecharModal = () => (mostrarModal.value = false)

// Upload de foto
const onFileChange = (e) => {
  const file = e.target.files[0]
  if (!file) return
  const reader = new FileReader()
  reader.onload = event => {
    previewFoto.value = event.target.result
    form.value.photoUrl = event.target.result
  }
  reader.readAsDataURL(file)
}

// Salvar (POST ou PUT)
const salvar = async () => {
  if (!form.value.nome.trim()) return alert('Por favor insere o nome.')
  if (!form.value.categoriaId) return alert('Seleciona a categoria.')

  try {
    if (editando.value && indexEdit.value !== null) {
      const id = nomeados.value[indexEdit.value].id
      // garantir que enviamos o id da categoria no payload
      const payload = { ...form.value, categoriaId: form.value.categoriaId }
      const res = await http.put(`/candidatos/${id}`, payload)
      nomeados.value[indexEdit.value] = res.data
    } else {
      const payload = { ...form.value, categoriaId: form.value.categoriaId }
      const res = await http.post('/candidatos', payload)
      nomeados.value.push(res.data)
    }
      statusTipo.value = 'sucesso'
      statusMensagem.value = editando.value
  ? 'Nomeado atualizado com sucesso!'
  : 'Nomeado adicionado com sucesso!'
mostrarStatus.value = true
    
    fecharModal()
  } catch (error) {
    console.error('Erro ao salvar nomeado:', error)
    console.error('Erro ao salvar nomeado:', error)
  statusTipo.value = 'erro'
  statusMensagem.value = 'Ocorreu um erro ao salvar o nomeado.'
  mostrarStatus.value = true
  }

}

// Remover nomeado
const removerNomeado = async (idx) => {
  const nomeado = nomeados.value[idx]
  if (!nomeado) return
  if (confirm(`Tens certeza que queres remover "${nomeado.nome}"?`)) {
    try {
      await http.delete(`/candidatos/${nomeado.id}`)
      nomeados.value.splice(idx, 1)
    } catch (error) {
      console.error('Erro ao remover nomeado:', error)
    }
  }
}

// Lista filtrada (comparando IDs como string)
const listaFiltrada = computed(() => {
  if (!filtroCategoria.value) return nomeados.value
  const filtro = String(filtroCategoria.value)
  return nomeados.value.filter(n => {
    // possíveis formatos retornados pela API: n.categoriaId, n.categoria (obj), n.categoria_id, n.categoria (string)
    const candCat = n.categoriaId ?? n.categoria_id ?? n.categoria ?? null
    if (!candCat && candCat !== 0) return false
    // se for objeto com id
    if (typeof candCat === 'object') return String(candCat.id) === filtro || String(candCat.nome) === filtro
    return String(candCat) === filtro
  })
})

// Carregar dados ao montar
onMounted(async () => {
  await getCategorias()
  await getNomeados()
})
</script>

<style scoped>
.line-clamp-3 {
  display: -webkit-box;
  line-clamp: 3;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
</style>