<template>
  <div class="flex min-h-screen bg-gray-50">
    <!-- Sidebar (desktop) -->
    <SideBar class="" />

    <!-- Main content -->
    <main class="flex-1">
      <HeaderADM />

      <div class="lg:p-6 p-4 max-w-7xl mx-auto">
        <!-- Header -->
        <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
          <div>
            <h2 class="text-2xl font-bold text-azul t-2">🗳 Votação do Público </h2>
            <p class="text-sm text-gray-500 mt-1">Resumo das votações e distribuição por nomeado</p>
          </div>

          <div class="flex items-center gap-3">
            <div>
              <span :class="voteStatusClass" class="px-3 py-1 rounded-full text-sm font-medium">
                {{ votacaoAberta ? '✅ Votação em curso' : '⛔ Votação encerrada' }}
              </span>
            </div>

            <button @click="exportCSV" class="bg-white border rounded-md px-3 py-2 text-sm hover:shadow-sm">Exportar Excel</button>
            <button @click="exportPDF" class="bg-azul text-white rounded-md px-3 py-2 text-sm hover:bg-blue-800">Exportar PDF</button>
          </div>
        </div>

        <!-- Top area: chart + summary -->
        <div class="mt-6 grid grid-cols-1 md:grid-cols-3 gap-6">
          <!-- Chart card -->
          <div class="bg-white rounded-xl shadow p-4 flex flex-col items-center">
            <div class="relative">
              <svg :width="svgSize" :height="svgSize" viewBox="0 0 200 200" class="mx-auto">
                <!-- background circle -->
                <circle cx="100" cy="100" r="90" fill="#f3f4f6" />
                <!-- slices -->
                <g transform="translate(0,0)">
                  <template v-for="(slice, i) in pieSlices" :key="i">
                    <path
                      :d="slice.path"
                      :fill="slice.color"
                      stroke="white"
                      stroke-width="0.5"
                    />
                  </template>
                </g>
                <!-- centre label -->
                <circle cx="100" cy="100" r="45" fill="white" />
                <text x="100" y="95" text-anchor="middle" class="text-sm fill-gray-700" font-size="10">Votos</text>
                <text x="100" y="112" text-anchor="middle" class="font-bold text-lg fill-gray-900" font-size="16">{{ totalValidVotes }}</text>
              </svg>
            </div>

            <div class="mt-4 text-sm text-gray-600 text-center">
              <p>Total votos válidos: <strong class="text-gray-800">{{ totalValidVotes }}</strong></p>
            </div>
          </div>

          <!-- Ranking list -->
          <div class="md:col-span-2 bg-white rounded-xl shadow p-4">
            <div class="flex items-center justify-between">
              <h3 class="font-semibold text-gray-800">Lista dos Mais Votados</h3>
              <div class="text-sm text-gray-500">Total de participantes: <strong>{{ nomeados.length }}</strong></div>
            </div>

            <!-- Desktop table -->
            <div class="hidden md:block mt-4">
              <table class="min-w-full">
                <thead class="bg-gray-50">
                  <tr>
                    <th class="text-left text-xs text-gray-600 uppercase px-4 py-2">#</th>
                    <th class="text-left text-xs text-gray-600 uppercase px-4 py-2">Nome</th>
                    <th class="text-left text-xs text-gray-600 uppercase px-4 py-2">Votos</th>
                    <th class="text-left text-xs text-gray-600 uppercase px-4 py-2">Percentagem</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(n, idx) in ordenadosPorVotos" :key="n.nome" class="border-t">
                    <td class="px-4 py-3 text-sm text-gray-700">{{ idx + 1 }}</td>
                    <td class="px-4 py-3 text-sm text-gray-800">{{ n.nome }}</td>
                    <td class="px-4 py-3 text-sm text-gray-800">{{ n.totalVotos }}</td>
                    <td class="px-4 py-3 text-sm text-gray-800">{{ formatPercent(n.percent) }}</td>
                  </tr>
                </tbody>
              </table>
            </div>

            <!-- Mobile cards -->
            <div class="md:hidden mt-4 space-y-3">
              <div v-for="(n, idx) in ordenadosPorVotos" :key="n.nome" class="p-3 border rounded-lg flex items-center justify-between">
                <div>
                  <div class="text-sm font-semibold text-gray-800">{{ idx + 1 }}. {{ n.nome }}</div>
                  <div class="text-xs text-gray-500 mt-1">Votos: <span class="text-gray-800">{{ n.totalVotos }}</span></div>
                </div>
                <div class="text-right">
                  <div class="text-sm font-bold text-azul">{{ formatPercent(n.percent) }}</div>
                </div>
              </div>
            </div>

          </div>
        </div>

        <!-- Footer actions / notes -->
        <div class="mt-6 flex flex-col md:flex-row items-start md:items-center justify-between gap-3">
          <div class="text-sm text-gray-500">
            <p>Distribuição de votos baseada em votos válidos.</p>
            <p class="mt-1">Se existirem votos inválidos, aparecem contabilizados ao lado.</p>
          </div>

          <div class="flex gap-2">
            <button @click="toggleVotacao" class="bg-white border px-3 py-2 rounded-md text-sm hover:shadow-sm">
              {{ votacaoAberta ? 'Encerrar votação' : 'Abrir votação' }}
            </button>

          </div>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import SideBar from './SideBar.vue'
import HeaderADM from './HeaderADM.vue'
import { http } from '@/Request/api'

/* ---------- Dados exemplo (substituir por API se necessário) ---------- */
const nomeados = ref([])

const totalInvalidVotes = ref(15) // exemplo
const votacaoAberta = ref(true)

/* ---------- Cores para slices ---------- */
const sliceColors = [
  '#2563EB', '#10B981', '#F59E0B', '#EF4444', '#6B21A8', '#0EA5E9', '#F97316'
]

/* ---------- Totais e percentagens ---------- */
// helper to read the vote count supporting both new `totalVotos` and legacy `votos`
const getVoteCount = (n) => Number(n?.totalVotos ?? n?.votos ?? 0)

const totalValidVotes = computed(() => {
  return nomeados.value.reduce((s, n) => s + getVoteCount(n), 0)
})

const ordenadosPorVotos = computed(() => {
  const total = totalValidVotes.value || 1
  return [...nomeados.value]
    .map(n => ({ ...n, _voteCount: getVoteCount(n) }))
    .sort((a, b) => b._voteCount - a._voteCount)
    .map(n => ({ ...n, percent: (n._voteCount / total) * 100, totalVotos: n._voteCount }))
})

async function GetListaMaisVotados(){
  try{
    let resp = await http.get("/candidatos/ListaDosMaisVotados")
    nomeados.value = resp.data
  }
  catch(ex){
    console.log(ex)
  }
}

onMounted(async ()=>{
  await GetListaMaisVotados()
})

/* ---------- SVG Pie slices (path strings) ---------- */
const svgSize = 200
const pieSlices = computed(() => {
  const total = totalValidVotes.value || 1
  let startAngle = -90
  return ordenadosPorVotos.value.map((n, i) => {
    const votes = Number(n.totalVotos ?? n._voteCount ?? 0)
    const sliceAngle = (votes / total) * 360
    const endAngle = startAngle + sliceAngle
    const path = describeArcPath(100, 100, 90, startAngle, endAngle)
    const color = sliceColors[i % sliceColors.length]
    startAngle = endAngle
    return { path, color, value: votes, label: n.nome, percent: n.percent }
  })
})

/* ---------- Helpers para SVG arc (polar to cartesian + path) ---------- */
function polarToCartesian(cx, cy, r, angleDeg) {
  const angleRad = (angleDeg - 90) * Math.PI / 180.0
  return {
    x: cx + (r * Math.cos(angleRad)),
    y: cy + (r * Math.sin(angleRad))
  }
}
function describeArcPath(cx, cy, r, startAngle, endAngle) {
  const start = polarToCartesian(cx, cy, r, endAngle)
  const end = polarToCartesian(cx, cy, r, startAngle)
  const largeArcFlag = endAngle - startAngle <= 180 ? '0' : '1'
  return [`M ${cx} ${cy}`, `L ${start.x} ${start.y}`, `A ${r} ${r} 0 ${largeArcFlag} 0 ${end.x} ${end.y}`, 'Z'].join(' ')
}

/* ---------- Utilitários ---------- */
const formatPercent = (p) => `${(Math.round(p * 10) / 10).toFixed(1)}%`
const voteStatusClass = computed(() => votacaoAberta.value ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800')

/* ---------- Ações: export CSV / export PDF (print-friendly) ---------- */
function exportCSV() {
  // CSV with headers: Nome,Votos,Percentagem
  const rows = [['Nome', 'totalVotos', 'Percentagem']]
  ordenadosPorVotos.value.forEach(n => rows.push([n.nome, Number(n.totalVotos ?? n._voteCount ?? 0), formatPercent(n.percent)]))
  // invalid votes row
  rows.push(['Votos inválidos', totalInvalidVotes.value, ''])
  const csv = rows.map(r => r.map(cell => `"${String(cell).replace(/"/g, '""')}"`).join(',')).join('\n')
  const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' })
  const url = URL.createObjectURL(blob)
  const a = document.createElement('a')
  a.href = url
  a.download = `totalVotos${new Date().toISOString().slice(0,10)}.csv`
  a.click()
  URL.revokeObjectURL(url)
}

function exportPDF() {
  // Simples: abrir nova janela com versão imprimível (user can save as PDF)
  const printWindow = window.open('', '_blank')
  const style = `
    <style>
      body{font-family: Inter, system-ui, -apple-system, "Segoe UI", Roboto, "Helvetica Neue", Arial;}
      table{border-collapse:collapse;width:100%}
      th,td{border:1px solid #ddd;padding:8px;text-align:left}
      th{background:#f3f4f6;font-weight:600}
    </style>
  `
  let html = `<html><head><title>Votos - ${new Date().toLocaleDateString()}</title>${style}</head><body>`
  html += `<h2>Votação do Público — Favorito do Público</h2>`
  html += `<p>Total votos válidos: ${totalValidVotes.value} — Votos inválidos: ${totalInvalidVotes.value}</p>`
  html += `<table><thead><tr><th>#</th><th>Nome</th><th>Votos</th><th>Percentagem</th></tr></thead><tbody>`
  ordenadosPorVotos.value.forEach((n, i) => {
    const votes = Number(n.totalVotos ?? n._voteCount ?? 0)
    html += `<tr><td>${i+1}</td><td>${n.nome}</td><td>${votes}</td><td>${formatPercent(n.percent)}</td></tr>`
  })
  html += `</tbody></table></body></html>`
  printWindow.document.write(html)
  printWindow.document.close()
  printWindow.focus()
  // user can print/save as PDF
}

/* ---------- Controls: toggle voting / reset ---------- */
function toggleVotacao() {
  votacaoAberta.value = !votacaoAberta.value
}
// function resetVotos() {
//   if (!confirm('Resetar todos os votos para 0?')) return
//   nomeados.value = nomeados.value.map(n => ({ ...n, totalVotos: 0 }))
//   totalInvalidVotes.value = 0
// }
</script>

<style scoped>
/* small helper for truncation */
.line-clamp-3 {
  display: -webkit-box;
  line-clamp: 3;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
</style>
