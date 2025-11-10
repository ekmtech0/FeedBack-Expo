<template>
  <div class="flex min-h-screen bg-gray-50">
    <SideBar />
    <main class="flex-1 md:ml-64">
      <HeaderAdm />

      <div class="max-w-7xl mx-auto px-4 py-6">
        <!-- Título + Botão -->
        <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-3 mt-6">
          <h2 class="font-bold text-xl text-azul flex items-center gap-2">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-azul" viewBox="0 0 24 24" fill="currentColor">
              <path d="M12 12c2.21 0 4-1.79 4-4S14.21 4 12 4 8 5.79 8 8s1.79 4 4 4zM4 20c0-2.67 5.33-4 8-4s8 1.33 8 4v1H4v-1z"/>
            </svg>
            Gestão de Expositores
          </h2>

          <div class="flex items-center gap-3">
            <button
              @click="abrirModal"
              class="bg-azul text-white px-4 py-2 rounded-xl shadow hover:bg-blue-800 transition text-sm sm:text-base w-full"
              aria-label="Adicionar expositor"
            >
              + Adicionar Expositor
            </button>
          </div>
        </div>

        <!-- Conteúdo principal -->
        <div class="mt-6 space-y-6">
          <div class="bg-white rounded-xl shadow overflow-hidden">
            <div class="overflow-x-auto">
              <table class="min-w-full table-auto hidden md:table">
                <thead class="bg-gray-100">
                  <tr>
                    <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Foto</th>
                    <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Nome</th>
                    <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Área</th>
                    <th class="px-6 py-3 text-right text-xs font-semibold text-gray-600 uppercase">Ações</th>
                  </tr>
                </thead>
                <tbody>
                  <tr
                    v-for="(expositor, index) in expositores"
                    :key="index"
                    class="border-t hover:bg-gray-50 transition"
                  >
                    <td class="px-6 py-4">
                      <div
                        class="w-10 h-10 rounded-full flex items-center justify-center font-semibold text-white"
                        :class="corFoto(expositor.name)"
                      >
                        {{ iniciais(expositor.name) }}
                      </div>
                    </td>
                    <td class="px-6 py-4 font-medium text-gray-800">{{ expositor.name }}</td>
                    <td class="px-6 py-4 text-gray-600">{{ expositor.area }}</td>
                    <td class="px-6 py-4 text-right space-x-3">
                      <button
                        @click.stop="editarExpositor(index)"
                        class="text-yellow-600 hover:text-yellow-700 font-medium text-sm"
                        aria-label="Editar expositor"
                      >
                        Editar
                      </button>
                      <button
                        @click.stop="eliminarExpositor(index)"
                        class="text-red-600 hover:text-red-700 font-medium text-sm"
                        aria-label="Eliminar expositor"
                      >
                        Eliminar
                      </button>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>

            <!-- Cards Mobile -->
            <div class="md:hidden divide-y bg-white">
              <div
                v-for="(expositor, index) in expositores"
                :key="index"
                class="p-4 hover:bg-gray-50 flex justify-between items-start"
              >
                <div class="flex items-center gap-3">
                  <div
                    class="w-10 h-10 rounded-full flex items-center justify-center font-semibold text-white text-sm"
                    :class="corFoto(expositor.name)"
                  >
                    {{ iniciais(expositor.name) }}
                  </div>
                  <div>
                    <p class="font-semibold text-gray-900">{{ expositor.name }}</p>
                    <p class="text-gray-600 text-sm mt-1">Área: {{ expositor.area }}</p>
                  </div>
                </div>
                <div class="flex flex-col items-end gap-2">
                  <button @click.stop="editarExpositor(index)" class="text-yellow-600 text-sm">Editar</button>
                  <button @click.stop="eliminarExpositor(index)" class="text-red-600 text-sm">Eliminar</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Modal (Adicionar / Editar) -->
      <div
        v-if="mostrarModal"
        class="fixed inset-0 z-50 flex items-center justify-center bg-black/50 px-4"
        role="dialog"
        aria-modal="true"
      >
        <div class="bg-white rounded-xl shadow-lg p-6 w-full max-w-lg mx-auto">
          <h3 class="text-lg font-bold text-azul mb-4">
            {{ editando ? 'Editar Expositor' : 'Adicionar Novo Expositor' }}
          </h3>

          <div class="flex flex-col gap-3">
            <label class="text-sm font-medium text-gray-700">Nome do Expositor</label>
            <input
              v-model="form.name"
              type="text"
              placeholder="Expositor"
              class="border rounded-lg px-4 py-2 focus:outline-none focus:ring-azul focus:ring-1"
            />

            <label class="text-sm font-medium text-gray-700 mt-2">Área de Exposição</label>
            <select
              v-model="form.areaId"
              class="border rounded-lg px-4 py-2 focus:outline-none focus:ring-azul focus:ring-1"
            >
              <option value="">Selecionar área</option>
              <option v-for="area in areas" :key="area.id" :value="area.id">{{ area.name }}</option>
            </select>
          </div>

          <div class="flex justify-end mt-5 gap-3">
            <button @click="fecharModal" class="px-4 py-2 bg-gray-200 rounded-lg hover:bg-gray-300 text-sm">
              Cancelar
            </button>
            <button
              @click="salvarExpositor"
              class="px-4 py-2 bg-azul text-white rounded-lg hover:bg-blue-800 text-sm"
            >
              {{ editando ? 'Guardar' : 'Salvar' }}
            </button>
          </div>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import SideBar from './SideBar.vue'
import HeaderAdm from './HeaderAdm.vue'
import api from '@/request/api'

const expositores = ref([])
const areas = ref([])

// carregar expositores + áreas
onMounted(async () => {
  try {
    const [respAreas, respExpositores] = await Promise.all([
      api.get('areas'),
      api.get('expositantes')
    ])
    areas.value = respAreas.data
    expositores.value = respExpositores.data.map(ex => ({
      ...ex,
      area: areas.value.find(a => a.id === ex.areaId)?.name || 'Área desconhecida'
    }))
  } catch (err) {
    console.error('Erro ao carregar dados:', err)
  }
})

// modal + formulário
const mostrarModal = ref(false)
const editando = ref(false)
const editIndex = ref(null)
const form = ref({ name: '', areaId: '' })

// abrir / editar
const abrirModal = () => {
  editando.value = false
  form.value = { name: '', areaId: '' }
  mostrarModal.value = true
}

const editarExpositor = (index) => {
  editando.value = true
  editIndex.value = index
  form.value = { name: expositores.value[index].name, areaId: expositores.value[index].areaId }
  mostrarModal.value = true
}

// salvar expositor
const salvarExpositor = async () => {
  if (!form.value.name || !form.value.areaId) return

  try {
    const { data } = await api.post('expositantes', form.value)
    expositores.value.push({
      ...data,
      area: areas.value.find(a => a.id === data.areaId)?.name || 'Área desconhecida'
    })
    mostrarModal.value = false
  } catch (error) {
    console.error('Erro ao salvar expositor:', error)
  }
}

// eliminar
const eliminarExpositor = (index) => {
  expositores.value.splice(index, 1)
}

const fecharModal = () => (mostrarModal.value = false)

// helpers
const iniciais = (nome) => {
  if (!nome) return '?'
  return nome.split(' ').map(p => p[0]).join('').toUpperCase().slice(0, 2)
}

const corFoto = (nome) => {
  const cores = ['bg-blue-500', 'bg-green-500', 'bg-pink-500', 'bg-yellow-500', 'bg-purple-500']
  if (!nome) return 'bg-gray-400'
  return cores[nome.length % cores.length]
}
</script>

<style scoped>
/* já estás a usar Tailwind */
</style>
