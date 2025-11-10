<template>
  <div class="flex min-h-screen bg-gray-50">
    <SideBar />

    <main class="flex-1 md:ml-64">
      <HeaderAdm />

      <div class="max-w-7xl mx-auto px-4 py-6">
        <!-- Título -->
        <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-3 mt-6">
          <h2 class="font-bold text-xl text-azul flex items-center gap-2">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-azul" viewBox="0 0 24 24" fill="currentColor">
              <path d="M2 21h20V3H2v18zm2-2V5h16v14H4z" />
              <path d="M7 9h10v2H7zm0 4h7v2H7z" />
            </svg>
            Gestão de Feedbacks
          </h2>
        </div>

        <!-- Filtros -->
        <div class="mt-6 flex flex-col sm:flex-row gap-3">
          <div class="flex-1">
            <input
              v-model="busca"
              type="text"
              placeholder="Buscar por nome..."
              class="w-full border rounded-lg px-4 py-2 focus:outline-none focus:ring-azul focus:ring-1 text-sm"
            />
          </div>

          <div>
            <select
              v-model="filtroArea"
              class="border rounded-lg px-4 py-2 text-sm focus:outline-none focus:ring-azul focus:ring-1"
            >
              <option value="">Todas as Áreas</option>
              <option v-for="area in areasUnicas" :key="area">{{ area }}</option>
            </select>
          </div>
        </div>

        <!-- Tabela Desktop -->
        <div class="mt-6 bg-white rounded-xl shadow overflow-hidden">
          <div class="overflow-x-auto">
            <table class="min-w-full table-auto hidden md:table">
              <thead class="bg-gray-100">
                <tr>
                  <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Área</th>
                  <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Nome</th>
                  <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Avaliação</th>
                  <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Comentário</th>
                  <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Data</th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="fb in feedbacksFiltrados"
                  :key="fb.id"
                  class="border-t hover:bg-gray-50 transition"
                >
                  <td class="px-6 py-4 text-gray-800 font-medium">{{ fb.area }}</td>
                  <td class="px-6 py-4 text-gray-700">{{ fb.nome }}</td>
                  <td class="px-6 py-4">
                    <span class="bg-azul/10 text-azul font-semibold px-3 py-1 rounded-lg text-sm">
                      {{ fb.avaliacao }}/5
                    </span>
                  </td>
                  <td class="px-6 py-4 text-gray-600 italic">{{ fb.comentario }}</td>
                  <td class="px-6 py-4 text-gray-500 text-sm">{{ fb.data }}</td>
                </tr>
              </tbody>
            </table>
          </div>

          <!-- Cards Mobile -->
          <div class="md:hidden divide-y bg-white">
            <div
              v-for="fb in feedbacksFiltrados"
              :key="fb.id"
              class="p-4 hover:bg-gray-50"
            >
              <p class="font-semibold text-gray-900">{{ fb.nome }}</p>
              <p class="text-gray-700 text-sm mt-1">Área: {{ fb.area }}</p>
              <p class="text-gray-600 text-sm mt-1 italic">"{{ fb.comentario }}"</p>
              <div class="flex justify-between items-center mt-2">
                <span class="text-azul text-sm font-semibold">★ {{ fb.avaliacao }}/5</span>
                <span class="text-gray-400 text-xs">{{ fb.data }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import SideBar from './SideBar.vue'
import HeaderAdm from './HeaderAdm.vue'
import api from '@/request/api'

const feedbacks = ref([])

onMounted(async () => {
  try {
    const resp = await api.get("/feedbacks")
    feedbacks.value = resp.data.map(f => ({
      id: f.id,
      nome: f.name,              // backend: name → frontend: nome
      area: f.area,
      avaliacao: f.rating,       // backend: rating → frontend: avaliacao
      comentario: f.comentario,
      data: f.data ?? new Date().toISOString().split("T")[0] // fallback se não vier data
    }))
  } catch (err) {
    console.error("Erro ao carregar feedbacks", err)
  }
})

const busca = ref('')
const filtroArea = ref('')

// lista única de áreas
const areasUnicas = computed(() => [...new Set(feedbacks.value.map(f => f.area))])

// filtro por nome e área
const feedbacksFiltrados = computed(() => {
  return feedbacks.value.filter(fb => {
    const buscaNome = fb.nome.toLowerCase().includes(busca.value.toLowerCase())
    const filtroPorArea = filtroArea.value ? fb.area === filtroArea.value : true
    return buscaNome && filtroPorArea
  })
})
</script>

<style scoped>
select,
input {
  @apply bg-white;
}
</style>
