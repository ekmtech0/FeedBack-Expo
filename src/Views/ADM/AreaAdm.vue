<template>
  <div class="flex min-h-screen bg-gray-50">
    
      <SideBar/>
    <main class="flex-1 md:ml-64"> <!-- empurra no desktop, full width no mobile -->
      <!-- Header -->
     <HeaderAdm/>

      <div class="max-w-7xl mx-auto px-4 py-6">
        <!-- Título + Botão -->
        <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-3 mt-6">
          <h2 class="font-bold text-xl text-azul flex items-center gap-2">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-azul" viewBox="0 0 32 32" fill="currentColor">
              <path d="M19 24h4v4h-4zm7 0h4v4h-4zm-7-7h4v4h-4zm7 0h4v4h-4z"/>
              <path d="M17 24H4V10h24v5h2v-5a2 2 0 0 0-2-2h-6V4a2 2 0 0 0-2-2h-8a2 2 0 0 0-2 2v4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h13ZM12 4h8v4h-8Z"/>
            </svg>
            Gestão de Áreas
          </h2>

          <div class="flex items-center gap-3">
            <button
              @click="abrirModal"
              class="bg-azul text-white px-4 py-2 rounded-xl shadow hover:bg-blue-800 transition text-sm sm:text-base w-full"
              aria-label="Adicionar área"
            >
              + Adicionar Área
            </button>
          </div>
        </div>

        <!-- Conteúdo principal -->
        <div class="mt-6 space-y-6">
          <!-- Tabela desktop (scroll horizontal on small screens) -->
          <div class="bg-white rounded-xl shadow overflow-hidden">
            <div class="overflow-x-auto">
              <table class="min-w-full table-auto hidden md:table">
                <thead class="bg-gray-100">
                  <tr>
                    <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Área</th>
                    <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Expositor</th>
                    <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Feedback</th>
                    <th class="px-6 py-3 text-right text-xs font-semibold text-gray-600 uppercase">Ações</th>
                  </tr>
                </thead>
                <tbody>
                  <tr
                    v-for="(area, index) in areas"
                    :key="index"
                    class="border-t hover:bg-gray-50 transition cursor-pointer"
                    @click="abrirDetalhes(area)"
                  >
                    <td class="px-6 py-4 font-medium text-gray-800">{{ area.nome }}</td>
                    <td class="px-6 py-4 text-gray-700">{{ area.expositor }}</td>
                    <td class="px-6 py-4 text-gray-600">{{ area.feedback }}</td>
                    <td class="px-6 py-4 text-right space-x-3">
                      <button
                        @click.stop="editarArea(index)"
                        class="text-yellow-600 hover:text-yellow-700 font-medium text-sm"
                        aria-label="Editar área"
                      >
                        Editar
                      </button>
                      <button
                        @click.stop="eliminarArea(index)"
                        class="text-red-600 hover:text-red-700 font-medium text-sm"
                        aria-label="Eliminar área"
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
                v-for="(area, index) in areas"
                :key="index"
                class="p-4 hover:bg-gray-50 cursor-pointer"
                @click="abrirDetalhes(area)"
              >
                <div class="flex justify-between items-start">
                  <div>
                    <p class="font-semibold text-gray-900">{{ area.nome }}</p>
                    <p class="text-gray-600 text-sm mt-1">Expositor: {{ area.expositor }}</p>
                    <p class="text-gray-500 text-sm mt-1 italic">Feedbacks: {{ area.feedback }}</p>
                  </div>

                  <div class="flex flex-col items-end gap-2">
                    <div class="flex gap-2">
                      <button @click.stop="editarArea(index)" class="text-yellow-600 text-sm">Editar</button>
                      <button @click.stop="eliminarArea(index)" class="text-red-600 text-sm">Eliminar</button>
                    </div>
                  </div>
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
            {{ editando ? 'Editar Área' : 'Adicionar Nova Área' }}
          </h3>

          <div class="flex flex-col gap-3">
            <label class="text-sm font-medium text-gray-700">Nome da Área</label>
            <input
              v-model="form.nome"
              type="text"
              placeholder="Ex.: Informática"
              class="border rounded-lg px-4 py-2 focus:outline-none focus:ring-azul focus:ring-1"
            />

            
          </div>

          <div class="flex justify-end mt-5 gap-3">
            <button @click="fecharModal" class="px-4 py-2 bg-gray-200 rounded-lg hover:bg-gray-300 text-sm">
              Cancelar
            </button>
            <button
              @click="salvarArea"
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

// Dados simulados
const areas = ref([
  { nome: 'Informática', expositor: '2', feedback: '4' },
  { nome: 'Saúde', expositor: '12', feedback: '8' },
  { nome: 'Mecânica', expositor: '5', feedback: '8' },
  { nome: 'Pastelaria', expositor: '12', feedback: '12' }
])

// Estado modal
const mostrarModal = ref(false)
const editando = ref(false)
const form = ref({ nome: '', expositor: '', feedback: '' })
const editIndex = ref(null)

// Funções
const abrirModal = () => {
  editando.value = false
  form.value = { nome: '', expositor: '', feedback: '' }
  mostrarModal.value = true
}

const editarArea = (index) => {
  editando.value = true
  editIndex.value = index
  form.value = { ...areas.value[index] }
  mostrarModal.value = true
}

const salvarArea = () => {
  if (!form.value.nome || !form.value.expositor) return
  if (editando.value) areas.value[editIndex.value] = { ...form.value }
  else areas.value.push({ ...form.value })
  mostrarModal.value = false
}

const eliminarArea = (index) => {
  if (confirm(`Tens certeza que queres eliminar a área "${areas.value[index].nome}"?`)) {
    areas.value.splice(index, 1)
  }
}


const fecharModal = () => mostrarModal.value = false
</script>

<style scoped>
/* se usas Tailwind, a maior parte do styling está nas classes utilitárias */
/* pequenas melhorias opcionais */
@media (min-width: 768px) {
  main { /* empurra conteúdo quando sidebar ocupa 16rem (64) */
    /* md:ml-64 aplicado nas classes do template */
  }
}
</style>
