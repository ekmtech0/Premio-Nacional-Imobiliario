<template>
  <div class="flex min-h-screen bg-gray-50">
    <!-- Sidebar (desktop) -->
    <SideBar class="" />

    <!-- Main -->
    <main class="flex-1">
      <HeaderADM />

      <div class="lg:p-6 p-4 max-w-7xl mx-auto">
        <!-- Header -->
        <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
          <div>
            <h2 class="text-2xl font-bold text-azul">🏆 Resultados Oficiais</h2>
            <p class="text-sm text-gray-500 mt-1">Gerir decisões do júri, vencedores e publicar no site</p>
          </div>

          <div class="flex items-center gap-3">
            <button
              @click="abrirModalAdicionar"
              class="bg-azul text-white px-3 py-2 rounded-md text-sm hover:bg-blue-800">
              + Adicionar Resultado
            </button>

            <button @click="exportCSV" class="bg-white border rounded-md px-3 py-2 text-sm hover:shadow-sm">Exportar CSV</button>
            <button @click="exportPDF" class="bg-azul text-white rounded-md px-3 py-2 text-sm hover:bg-blue-800">Exportar PDF</button>
          </div>
        </div>

        <!-- Table / Cards -->
        <div class="mt-6 bg-white rounded-xl shadow overflow-hidden">
          <!-- Desktop table -->
          <table class="min-w-full hidden md:table">
            <thead class="bg-gray-100">
              <tr>
                <th class="px-4 py-3 text-left text-xs font-medium text-gray-600 uppercase">Categoria</th>
                <th class="px-4 py-3 text-left text-xs font-medium text-gray-600 uppercase">Status</th>
                <th v-if="!isSpecialCategoryColumn" class="px-4 py-3 text-left text-xs font-medium text-gray-600 uppercase">1º Lugar</th>
                <th v-if="!isSpecialCategoryColumn" class="px-4 py-3 text-left text-xs font-medium text-gray-600 uppercase">2º Lugar</th>
                <th v-if="!isSpecialCategoryColumn" class="px-4 py-3 text-left text-xs font-medium text-gray-600 uppercase">3º Lugar</th>
                <th v-if="isSpecialCategoryColumn" class="px-4 py-3 text-left text-xs font-medium text-gray-600 uppercase">Vencedor</th>
                <th class="px-4 py-3 text-left text-xs font-medium text-gray-600 uppercase">Ata</th>
                <th class="px-4 py-3 text-right"></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(r, i) in resultados" :key="r.categoria" class="border-t hover:bg-gray-50">
                <td class="px-4 py-4 font-medium text-gray-800">{{ r.categoria }}</td>

                <td class="px-4 py-4">
                  <span :class="statusBadge(r.status)" class="px-3 py-1 rounded-full text-xs font-medium">
                    {{ displayStatus(r.status) }}
                  </span>
                </td>

                <!-- Normal categories: show 1º/2º/3º -->
                <template v-if="!isSpecialCategory(r.categoria)">
                  <td class="px-4 py-4 text-gray-700">{{ r.vencedores.primeiro || '—' }}</td>
                  <td class="px-4 py-4 text-gray-700">{{ r.vencedores.segundo || '—' }}</td>
                  <td class="px-4 py-4 text-gray-700">{{ r.vencedores.terceiro || '—' }}</td>
                </template>

                <!-- Special categories: apresenta um unico vencedor -->
                <section v-else>
                  <td class="px-4 py-4 text-gray-700">{{ r.vencedores.primeiro || '—' }}</td>
                </section>

                <td class="px-4 py-4">
                  <div v-if="r.ataName">
                    <a :href="r.ataUrl" target="_blank" class="text-sm text-azul hover:underline">{{ r.ataName }}</a>
                  </div>
                  <div v-else class="text-sm text-gray-500">Sem ata</div>
                </td>

                <td class="px-4 py-4 text-right space-x-2">
                  <button @click="abrirModalEditar(i)" class="text-sm text-yellow-600 hover:text-yellow-700">✏️ Editar</button>
                  <button @click="removerResultado(i)" class="text-sm text-red-600 hover:text-red-700">🗑️ Remover</button>
                  <button
                    @click="togglePublicar(i)"
                    class="text-sm px-2 py-1 rounded-md"
                    :class="r.publicado ? 'bg-green-50 text-green-700' : 'bg-gray-100 text-gray-700'">
                    {{ r.publicado ? 'Publicado' : 'Publicar' }}
                  </button>
                </td>
              </tr>
            </tbody>
          </table>

          <!-- Mobile cards -->
          <div class="md:hidden divide-y">
            <div v-for="(r, i) in resultados" :key="r.categoria" class="p-4">
              <div class="flex items-start justify-between">
                <div class="flex-1 pr-3">
                  <p class="font-semibold text-gray-900">{{ r.categoria }}</p>
                  <p class="text-xs mt-1">
                    <span :class="statusBadge(r.status)" class="px-2 py-1 rounded-full mr-2 text-xs">{{ displayStatus(r.status) }}</span>
                    <span class="text-sm text-gray-600">{{ r.publicado ? '🌐 Publicado' : '—' }}</span>
                  </p>

                  <ul class="mt-2 text-sm text-gray-700 space-y-1">
                    <li v-if="!isSpecialCategory(r.categoria)"><strong>🥇 1º:</strong> {{ r.vencedores.primeiro || '—' }}</li>
                    <li v-if="!isSpecialCategory(r.categoria)"><strong>🥈 2º:</strong> {{ r.vencedores.segundo || '—' }}</li>
                    <li v-if="!isSpecialCategory(r.categoria)"><strong>🥉 3º:</strong> {{ r.vencedores.terceiro || '—' }}</li>

                    <li v-if="isSpecialCategory(r.categoria)"><strong>🥇 Vencedor:</strong> {{ r.vencedores.primeiro || '—' }}</li>
                  </ul>

                  <p class="mt-2 text-sm">
                    <span class="font-medium">Ata:</span>
                    <span v-if="r.ataName"><a :href="r.ataUrl" target="_blank" class="text-azul hover:underline">{{ r.ataName }}</a></span>
                    <span v-else class="text-gray-500"> Sem ata</span>
                  </p>
                </div>

                <div class="flex flex-col items-end gap-2">
                  <button @click="abrirModalEditar(i)" class="text-yellow-600 text-sm">✏️</button>
                  <button @click="removerResultado(i)" class="text-red-600 text-sm">🗑️</button>
                  <button
                    @click="togglePublicar(i)"
                    class="text-sm px-2 py-1 rounded-md"
                    :class="r.publicado ? 'bg-green-50 text-green-700' : 'bg-gray-100 text-gray-700'">
                    {{ r.publicado ? 'Publicado' : 'Publicar' }}
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Nota -->
        <div class="mt-6 text-sm text-gray-500">
          <p>Estados: <span class="px-2 py-1 rounded-full bg-gray-100 text-gray-700 text-xs">Aguardando decisão do júri</span> / <span class="px-2 py-1 rounded-full bg-green-50 text-green-700 text-xs">Decisão finalizada</span></p>
        </div>
      </div>
    </main>

    <!-- Modal Add/Edit -->
    <div v-if="mostrarModal" class="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50 px-4">
      <div class="bg-white rounded-xl shadow-lg w-full max-w-lg max-h-[90vh] overflow-y-auto p-6">
        <h3 class="text-lg font-bold text-azul mb-2">{{ editando ? 'Editar Resultado' : 'Adicionar Resultado' }}</h3>

        <div class="space-y-4">
          <!-- Categoria -->
          <label class="text-sm font-medium text-gray-700">Categoria</label>
          <select v-model="form.categoria" @change="onCategoriaChange" class="border rounded-lg px-3 py-2 w-full">
            <option value="" disabled>Seleciona a categoria</option>
            <option v-for="(c,i) in categoriasBase" :key="i" :value="c">{{ c }}</option>
          </select>

          <!-- Status -->
          <label class="text-sm font-medium text-gray-700">Status</label>
          <select v-model="form.status" class="border rounded-lg px-3 py-2 w-full">
            <option value="aguardando">Aguardando decisão do júri</option>
            <option value="finalizado">Decisão finalizada</option>
          </select>

          <!-- Vencedores: render adaptativo -->
          <div v-if="isSpecialCategory(form.categoria)">
            <label class="text-sm font-medium text-gray-700">Vencedor (único)</label>
            <select v-model="form.vencedores.primeiro" class="border rounded-lg px-3 py-2 w-full">
              <option value="">— Seleciona —</option>
              <option v-for="(n, idx) in nomeadosList" :key="idx" :value="n">{{ n }}</option>
            </select>
          </div>

          <div v-else class="grid grid-cols-1 sm:grid-cols-3 gap-3">
            <div>
              <label class="text-sm font-medium text-gray-700">1º Lugar</label>
              <select v-model="form.vencedores.primeiro" class="border rounded-lg px-3 py-2 w-full">
                <option value="">— Seleciona —</option>
                <option v-for="(n, idx) in nomeadosList" :key="idx" :value="n">{{ n }}</option>
              </select>
            </div>
            <div>
              <label class="text-sm font-medium text-gray-700">2º Lugar</label>
              <select v-model="form.vencedores.segundo" class="border rounded-lg px-3 py-2 w-full">
                <option value="">— Seleciona —</option>
                <option v-for="(n, idx) in nomeadosList" :key="idx" :value="n">{{ n }}</option>
              </select>
            </div>
            <div>
              <label class="text-sm font-medium text-gray-700">3º Lugar</label>
              <select v-model="form.vencedores.terceiro" class="border rounded-lg px-3 py-2 w-full">
                <option value="">— Seleciona —</option>
                <option v-for="(n, idx) in nomeadosList" :key="idx" :value="n">{{ n }}</option>
              </select>
            </div>
          </div>

          <!-- Upload da Ata (PDF) -->
          <div>
            <label class="text-sm font-medium text-gray-700">Upload da ata (PDF)</label>
            <input type="file" accept="application/pdf" @change="onFileChange" class="block w-full mt-2 text-sm" />
            <div v-if="filePreviewName" class="mt-2 text-sm text-gray-700">Ficheiro selecionado: <strong class="text-azul">{{ filePreviewName }}</strong></div>
          </div>

          <!-- Publicado -->
          <div class="flex items-center gap-3">
            <input id="publicado" type="checkbox" v-model="form.publicado" class="h-4 w-4" />
            <label for="publicado" class="text-sm text-gray-700">Publicar no site oficial</label>
          </div>
        </div>

        <!-- Buttons -->
        <div class="flex justify-end gap-3 mt-6">
          <button @click="fecharModal" class="px-4 py-2 bg-gray-200 rounded-lg hover:bg-gray-300">Cancelar</button>
          <button @click="salvarResultado" class="px-4 py-2 bg-azul text-white rounded-lg hover:bg-blue-800">
            {{ editando ? 'Guardar alterações' : 'Salvar resultado' }}
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

/* === Categorias oficiais === */
const categoriasBase = [
  "Arquitetura Imobiliária de Excelência",
  "Serviço Público na Habitação",
  "Mediação Imobiliária de Referência",
  "Prémio Especial: Sustentabilidade & Eficiência"
]

/* === Categorias que têm apenas 1 vencedor === */
const categoriasUnicoVencedor = [
  "Mediação Imobiliária de Referência",
  "Prémio Especial: Sustentabilidade & Eficiência"
]

/* === Lista de nomeados (apenas nomes) para selects de vencedores.
   Substitui por fetch se preferires ligar ao componente Nomeados === */
const nomeadosList = [
  'Victor Makuka',
  'SG Design',
  'EKM Tech Solutions',
  'Linear Comunicação',
  'Karen Eventos'
]

/* === Resultados iniciais (exemplo) === */
const resultados = ref([
  {
    categoria: categoriasBase[0],
    status: 'aguardando',
    vencedores: { primeiro: '', segundo: '', terceiro: '' },
    publicado: false,
    ata: null,
    ataName: '',
    ataUrl: ''
  },
  {
    categoria: categoriasBase[1],
    status: 'finalizado',
    vencedores: { primeiro: 'SG Design', segundo: 'Victor Makuka', terceiro: 'Outra Empresa' },
    publicado: true,
    ata: null,
    ataName: '',
    ataUrl: ''
  },
  {
    categoria: categoriasBase[2], // special: single winner
    status: 'finalizado',
    vencedores: { primeiro: 'SG Design', segundo: '', terceiro: '' },
    publicado: false,
    ata: null,
    ataName: '',
    ataUrl: ''
  }
])

/* === UI state === */
const mostrarModal = ref(false)
const editando = ref(false)
const indexEdit = ref(null)
const filePreviewName = ref('')

const form = ref({
  categoria: '',
  status: 'aguardando',
  vencedores: { primeiro: '', segundo: '', terceiro: '' },
  publicado: false,
  ata: null,
  ataName: '',
  ataUrl: ''
})

/* ---------- Helpers ---------- */
const isSpecialCategory = (categoria) => categoriasUnicoVencedor.includes(categoria)
const isSpecialCategoryColumn = false // not used reactively in header, handled per-row

function displayStatus(s) {
  return s === 'finalizado' ? 'Decisão finalizada' : 'Aguardando decisão do júri'
}
function statusBadge(s) {
  return s === 'finalizado' ? 'bg-green-50 text-green-700' : 'bg-gray-100 text-gray-700'
}

/* ---------- Modal controls ---------- */
function abrirModalAdicionar() {
  editando.value = false
  indexEdit.value = null
  form.value = {
    categoria: '',
    status: 'aguardando',
    vencedores: { primeiro: '', segundo: '', terceiro: '' },
    publicado: false,
    ata: null,
    ataName: '',
    ataUrl: ''
  }
  filePreviewName.value = ''
  mostrarModal.value = true
}

function abrirModalEditar(idx) {
  editando.value = true
  indexEdit.value = idx
  const r = resultados.value[idx]
  form.value = {
    categoria: r.categoria,
    status: r.status,
    vencedores: { ...r.vencedores },
    publicado: r.publicado || false,
    ata: r.ata || null,
    ataName: r.ataName || '',
    ataUrl: r.ataUrl || ''
  }
  filePreviewName.value = r.ataName || ''
  mostrarModal.value = true
}

/* Ao trocar categoria no form, limpa campos que não se aplicam */
function onCategoriaChange() {
  if (isSpecialCategory(form.value.categoria)) {
    // se especial, limpa 2º/3º
    form.value.vencedores.segundo = ''
    form.value.vencedores.terceiro = ''
  }
}

/* ---------- File upload ---------- */
function onFileChange(e) {
  const f = e.target.files && e.target.files[0]
  if (!f) return
  if (f.type !== 'application/pdf') {
    alert('Por favor seleciona um ficheiro em formato PDF.')
    return
  }

  // revoke previous preview URL if it was set from this form
  if (form.value.ataUrl && form.value.ata && form.value.ataUrl.startsWith('blob:')) {
    try { URL.revokeObjectURL(form.value.ataUrl) } catch (err) {}
  }

  form.value.ata = f
  form.value.ataName = f.name
  form.value.ataUrl = URL.createObjectURL(f)
  filePreviewName.value = f.name
}

/* ---------- Salvar resultado ---------- */
function salvarResultado() {
  if (!form.value.categoria) {
    alert('Seleciona a categoria.')
    return
  }

  // Se categoria especial, garantimos que só guarda primeiro vencedor
  if (isSpecialCategory(form.value.categoria)) {
    form.value.vencedores.segundo = ''
    form.value.vencedores.terceiro = ''
  }

  const novo = {
    categoria: form.value.categoria,
    status: form.value.status,
    vencedores: { ...form.value.vencedores },
    publicado: !!form.value.publicado,
    ata: form.value.ata || null,
    ataName: form.value.ataName || '',
    ataUrl: form.value.ataUrl || ''
  }

  if (editando.value && indexEdit.value !== null) {
    // revoke previous ataUrl if was temporary and being replaced
    const prev = resultados.value[indexEdit.value]
    if (prev && prev.ataUrl && prev.ataUrl !== novo.ataUrl && prev.ataUrl.startsWith('blob:')) {
      try { URL.revokeObjectURL(prev.ataUrl) } catch (err) {}
    }
    resultados.value[indexEdit.value] = novo
  } else {
    resultados.value.push(novo)
  }

  fecharModal()
}

/* ---------- Remover / Publicar ---------- */
function removerResultado(idx) {
  if (!confirm('Tens certeza que queres remover este resultado?')) return
  const r = resultados.value[idx]
  if (r && r.ataUrl && r.ataUrl.startsWith('blob:')) {
    try { URL.revokeObjectURL(r.ataUrl) } catch (err) {}
  }
  resultados.value.splice(idx, 1)
}

function togglePublicar(idx) {
  resultados.value[idx].publicado = !resultados.value[idx].publicado
}

/* ---------- Fechar modal ---------- */
function fecharModal() {
  mostrarModal.value = false
  filePreviewName.value = ''
}

/* ---------- Export CSV / PDF ---------- */
function exportCSV() {
  const rows = [['Categoria', 'Status', '1º', '2º', '3º', 'Publicado', 'Ata']]
  resultados.value.forEach(r => {
    rows.push([
      r.categoria,
      r.status,
      r.vencedores.primeiro || '',
      r.vencedores.segundo || '',
      r.vencedores.terceiro || '',
      r.publicado ? 'Sim' : 'Não',
      r.ataName || ''
    ])
  })
  const csv = rows.map(r => r.map(cell => `"${String(cell).replace(/"/g, '""')}"`).join(',')).join('\n')
  const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' })
  const link = document.createElement('a')
  link.href = URL.createObjectURL(blob)
  link.download = `resultados_juri_${new Date().toISOString().slice(0,10)}.csv`
  link.click()
  URL.revokeObjectURL(link.href)
}

function exportPDF() {
  const win = window.open('', '_blank')
  const style = `
    <style>
      body{font-family: Inter, system-ui, -apple-system, "Segoe UI", Roboto, "Helvetica Neue", Arial; padding:20px;}
      table{border-collapse:collapse;width:100%}
      th,td{border:1px solid #ddd;padding:8px;text-align:left}
      th{background:#f3f4f6;font-weight:600}
      h1{font-size:18px}
    </style>
  `
  let html = `<html><head><title>Resultados do Júri</title>${style}</head><body>`
  html += `<h1>Resultados Oficiais (Júri Técnico)</h1>`
  html += `<table><thead><tr><th>Categoria</th><th>Status</th><th>1º</th><th>2º</th><th>3º</th><th>Publicado</th><th>Ata</th></tr></thead><tbody>`
  resultados.value.forEach(r => {
    html += `<tr>
      <td>${r.categoria}</td>
      <td>${displayStatus(r.status)}</td>
      <td>${r.vencedores.primeiro || ''}</td>
      <td>${r.vencedores.segundo || ''}</td>
      <td>${r.vencedores.terceiro || ''}</td>
      <td>${r.publicado ? 'Sim' : 'Não'}</td>
      <td>${r.ataName || ''}</td>
    </tr>`
  })
  html += `</tbody></table></body></html>`
  win.document.write(html)
  win.document.close()
  win.focus()
}

/* ---------- Cleanup on unload ---------- */
if (typeof window !== 'undefined') {
  window.addEventListener('beforeunload', () => {
    resultados.value.forEach(r => {
      if (r.ataUrl && r.ataUrl.startsWith('blob:')) {
        try { URL.revokeObjectURL(r.ataUrl) } catch (err) {}
      }
    })
  })
}
</script>

<style scoped>
/* pequena ajuda visual */
</style>
