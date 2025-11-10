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
                        :class="corFoto(expositor.nome)"
                      >
                        {{ iniciais(expositor.nome) }}
                      </div>
                    </td>
                    <td class="px-6 py-4 font-medium text-gray-800">{{ expositor.nome }}</td>
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
                    :class="corFoto(expositor.nome)"
                  >
                    {{ iniciais(expositor.nome) }}
                  </div>
                  <div>
                    <p class="font-semibold text-gray-900">{{ expositor.nome }}</p>
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
              v-model="form.nome"
              type="text"
              placeholder="Expositor"
              class="border rounded-lg px-4 py-2 focus:outline-none focus:ring-azul focus:ring-1"
            />

            <label class="text-sm font-medium text-gray-700 mt-2">Área de Exposição</label>
            <select
              v-model="form.area"
              class="border rounded-lg px-4 py-2 focus:outline-none focus:ring-azul focus:ring-1"
            >
              <option value="">Selecionar área</option>
              <option v-for="area in areas" :key="area">{{ area }}</option>
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
import { ref } from 'vue'
import SideBar from './SideBar.vue'
import HeaderAdm from './HeaderAdm.vue'


const expositores = ref([
  { nome: 'Victor Makuka', area: 'Informática' },
  { nome: 'Edvaldo Mendes', area: 'Saúde' },
  { nome: 'Abílio Junior', area: 'Mecânica' },
  { nome: 'Aricleni dos Santos', area: 'Pastelaria' }
])

const areas = ['Informática', 'Saúde', 'Mecânica', 'Pastelaria', 'Moda', 'Educação']

// Estado modal
const mostrarModal = ref(false)
const editando = ref(false)
const form = ref({ nome: '', area: '' })
const editIndex = ref(null)

// Funções
const abrirModal = () => {
  editando.value = false
  form.value = { nome: '', area: '' }
  mostrarModal.value = true
}

const editarExpositor = (index) => {
  editando.value = true
  editIndex.value = index
  form.value = { ...expositores.value[index] }
  mostrarModal.value = true
}

const salvarExpositor = () => {
  if (!form.value.nome || !form.value.area) return
  if (editando.value) expositores.value[editIndex.value] = { ...form.value }
  else expositores.value.push({ ...form.value })
  mostrarModal.value = false
}

const eliminarExpositor = (index) => {
  if (confirm(`Tens certeza que queres eliminar o expositor "${expositores.value[index].nome}"?`)) {
    expositores.value.splice(index, 1)
  }
}

const fecharModal = () => (mostrarModal.value = false)

// Iniciais do nome
const iniciais = nome => nome.split(' ').map(p => p[0]).join('').toUpperCase().slice(0, 2)
const corFoto = nome => {
  const cores = ['bg-blue-500', 'bg-green-500', 'bg-pink-500', 'bg-yellow-500', 'bg-purple-500']
  return cores[nome.length % cores.length]
}
</script>

<style scoped>
/* a base já é Tailwind */
</style>
