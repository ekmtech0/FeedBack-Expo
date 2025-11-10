<template>
  <div class="flex min-h-screen bg-gray-50">
    <SideBar/>
    <main class="flex-1 md:ml-64">
      <HeaderAdm/>

      <div class="max-w-7xl mx-auto px-4 py-6">
        <!-- Título + Botão -->
        <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-3 mt-6">
          <h2 class="font-bold text-xl text-azul flex items-center gap-2">
            Gestão de Áreas
          </h2>
          <div class="flex items-center gap-3">
            <button
              @click="abrirModal"
              class="bg-azul text-white px-4 py-2 rounded-xl shadow hover:bg-blue-800 transition text-sm sm:text-base w-full"
            >
              + Adicionar Área
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
                    <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Área</th>
                    <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Expositor</th>
                    <th class="px-6 py-3 text-left text-xs font-semibold text-gray-600 uppercase">Feedback</th>
                    <th class="px-6 py-3 text-right text-xs font-semibold text-gray-600 uppercase">Ações</th>
                  </tr>
                </thead>
                <tbody>
                  <tr
                    v-for="(area, i) in areas"
                    :key="area.id"
                    class="border-t hover:bg-gray-50 transition cursor-pointer"
                    @click="abrirDetalhes(area)"
                  >
                    <td class="px-6 py-4 font-medium text-gray-800">{{ area.name }}</td>
                    <td class="px-6 py-4 text-gray-700">{{ area.expositantes }}</td>
                    <td class="px-6 py-4 text-gray-600">{{ area.feedbacks }}</td>
                    <td class="px-6 py-4 text-right space-x-3">
                      <button
                        @click.stop="editarArea(area)"
                        class="text-yellow-600 hover:text-yellow-700 font-medium text-sm"
                      >
                        Editar
                      </button>
                      <button
                        @click.stop="eliminarArea(area.id, i)"
                        class="text-red-600 hover:text-red-700 font-medium text-sm"
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
                v-for="(area, i) in areas"
                :key="area.id"
                class="p-4 hover:bg-gray-50 cursor-pointer"
                @click="abrirDetalhes(area)"
              >
                <div class="flex justify-between items-start">
                  <div>
                    <p class="font-semibold text-gray-900">{{ area.name }}</p>
                    <p class="text-gray-600 text-sm mt-1">Expositor: {{ area.expositantes }}</p>
                    <p class="text-gray-500 text-sm mt-1 italic">Feedbacks: {{ area.feedbacks }}</p>
                  </div>
                  <div class="flex gap-2">
                    <button @click.stop="editarArea(area)" class="text-yellow-600 text-sm">Editar</button>
                    <button @click.stop="eliminarArea(area.id, i)" class="text-red-600 text-sm">Eliminar</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Modal -->
      <div v-if="mostrarModal" class="fixed inset-0 z-50 flex items-center justify-center bg-black/50 px-4">
        <div class="bg-white rounded-xl shadow-lg p-6 w-full max-w-lg mx-auto">
          <h3 class="text-lg font-bold text-azul mb-4">
            {{ editando ? 'Editar Área' : 'Adicionar Nova Área' }}
          </h3>
          <div class="flex flex-col gap-3">
            <label class="text-sm font-medium text-gray-700">Nome da Área</label>
            <input
              v-model="form.name"
              type="text"
              placeholder="Ex.: Informática"
              class="border rounded-lg px-4 py-2 focus:outline-none focus:ring-azul focus:ring-1"
            />
          </div>
          <div class="flex justify-end mt-5 gap-3">
            <button @click="fecharModal" class="px-4 py-2 bg-gray-200 rounded-lg hover:bg-gray-300 text-sm">
              Cancelar
            </button>
            <button @click="salvarArea" class="px-4 py-2 bg-azul text-white rounded-lg hover:bg-blue-800 text-sm">
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

const areas = ref([])
const mostrarModal = ref(false)
const editando = ref(false)
const form = ref({ id: null, name: '' })
const editIndex = ref(null)

const getAreas = async () => {
  try {
    const resp = await api.get("/areas")
    areas.value = resp.data
  } catch (err) {
    console.error(err)
  }
}

onMounted(getAreas)

const abrirModal = () => {
  editando.value = false
  form.value = { id: null, name: '' }
  mostrarModal.value = true
}

const editarArea = (area) => {
  editando.value = true
  form.value = { ...area }
  mostrarModal.value = true
}

const salvarArea = async () => {
  try {
    if (editando.value) {
      // PUT precisa do id na URL e o body com os dados
      await api.put("/areas/" + form.value.id +"/"+form.value.name)
      const idx = areas.value.findIndex(a => a.id === form.value.id)
      if (idx !== -1) areas.value[idx] = { ...form.value }
    } else {
      // POST só precisa do nome
      const resp = await api.post("areas/"+form.value.name )
      areas.value.push(resp.data)
    }
    mostrarModal.value = false
  } catch (err) {
    console.error("Erro ao salvar área", err)
  }
}


const eliminarArea = async (id, index) => {
  if (confirm(`Tens certeza que queres eliminar a área "${areas.value[index].name}"?`)) {
    try {
      await api.delete("/areas/" + id)
      areas.value.splice(index, 1)
    } catch (err) {
      console.error("Erro ao eliminar área", err)
    }
  }
}

const fecharModal = () => mostrarModal.value = false
const abrirDetalhes = (area) => console.log("Detalhes da área:", area)
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
