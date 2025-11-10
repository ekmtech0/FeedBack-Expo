<template>
  <div class="flex min-h-screen bg-gray-50">
    <SideBar />

    <main class="flex-1 md:ml-64">
      <HeaderAdm />

      <div class="max-w-7xl mx-auto px-4 py-6">
        <!-- Título -->
        <h2 class="font-bold text-xl text-azul mb-6 flex items-center gap-2">

          Dashboard Geral
        </h2>

        <!-- Cards principais -->
        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 mb-10">
          <div
            class="bg-white p-6 rounded-2xl shadow flex items-center justify-between transition hover:shadow-lg hover:scale-[1.02]"
          >
            <div>
              <h4 class="text-gray-500 font-medium">Total de Visitantes</h4>
              <p class="text-3xl font-bold text-azul mt-1">{{ estatisticas.visitantes }}</p>
            </div>
            <div class="text-4xl"><svg xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-azul" width="512" height="512" viewBox="0 0 512 512"><circle cx="152" cy="184"  r="72" fill="currentColor"/><path fill="currentColor" d="M234 296c-28.16-14.3-59.24-20-82-20c-44.58 0-136 27.34-136 82v42h150v-16.07c0-19 8-38.05 22-53.93c11.17-12.68 26.81-24.45 46-34"/><path fill="currentColor" d="M340 288c-52.07 0-156 32.16-156 96v48h312v-48c0-63.84-103.93-96-156-96"/><circle cx="340" cy="168" r="88" fill="currentColor" /></svg></div>
          </div>

          <div
            class="bg-white p-6 rounded-2xl shadow flex items-center justify-between transition hover:shadow-lg hover:scale-[1.02]"
          >
            <div>
              <h4 class="text-gray-500 font-medium">Total de Feedbacks</h4>
              <p class="text-3xl font-bold text-azul mt-1">{{ estatisticas.feedbacks }}</p>
            </div>
            <div class="text-4xl"><svg xmlns="http://www.w3.org/2000/svg" class="text-azul"  width="24" height="24" viewBox="0 0 24 24"><path fill="currentColor" d="M18 3a4 4 0 0 1 4 4v8a4 4 0 0 1-4 4h-4.724l-4.762 2.857a1 1 0 0 1-1.508-.743L7 21v-2H6a4 4 0 0 1-3.995-3.8L2 15V7a4 4 0 0 1 4-4zm-4 9H8a1 1 0 0 0 0 2h6a1 1 0 0 0 0-2m2-4H8a1 1 0 1 0 0 2h8a1 1 0 0 0 0-2"/></svg></div>
          </div>

          <div
            class="bg-white p-6 rounded-2xl shadow flex items-center justify-between transition hover:shadow-lg hover:scale-[1.02]"
          >
            <div>
              <h4 class="text-gray-500 font-medium">Satisfação Média</h4>
              <p class="text-3xl font-bold text-azul mt-1">{{ estatisticas.satisfacao }}%</p>
            </div>
            <div class="text-4xl"><svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12 text-azul"  width="512" height="512" viewBox="0 0 512 512"><path fill="currentColor" fill-rule="evenodd" d="M196.423 321.225c4.837.623 9.353.907 13.576.907c43.958 0 56.135-31.174 60.99-43.604l.006-.016c9.45-24.254 17.298-68.048 20.253-84.868c3.136-18.695-1.032-35.718-11.723-47.94c-9.771-11.165-24.392-17.715-41.188-17.715h-47.966s4.23-16.741 7.415-29.147c10.518-40.972-12.854-55.658-46.137-53.904l-48.566 90.79l-60.417 33.863v124.806zm36.052 45.263c-4.631-5.294-8.038-11.49-10.157-18.331c24.081-1.754 41.46-11.362 53.539-23.708c12.439-12.712 18.11-27.249 20.705-33.902l.161-.414c10.384-26.649 18.618-72.398 21.679-89.844l.05-.172l.212-1.261q.603-3.599.935-7.187l149.736 26.125v124.807l-60.417 33.863l-48.567 90.79c-33.283 1.754-56.654-12.932-46.137-53.904a9808 9808 0 0 0 7.416-29.147h-47.966c-16.796 0-31.418-6.55-41.189-17.715" clip-rule="evenodd"/></svg></div>
          </div>
        </div>

        <!-- Gráficos -->
        <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
          <div class="bg-white rounded-2xl shadow p-4 flex flex-col">
            <h3 class="font-semibold text-gray-700 mb-3 text-center">Participação por Área</h3>
            <div class="flex-1">
              <canvas id="chartArea" class="!h-[260px]"></canvas>
            </div>
          </div>

          <div class="bg-white rounded-2xl shadow p-4 flex flex-col">
            <h3 class="font-semibold text-gray-700 mb-3 text-center">Satisfação Geral</h3>
            <div class="flex-1 flex items-center justify-center">
              <canvas id="chartDonut" class="!h-[220px]"></canvas>
            </div>
          </div>

          <div class="bg-white rounded-2xl shadow p-4 flex flex-col">
            <h3 class="font-semibold text-gray-700 mb-3 text-center">Feedbacks por hora</h3>
            <div class="flex-1">
              <canvas id="chartLinha" class="!h-[260px]"></canvas>
            </div>
          </div>
        </div>

        <!-- Ranking + Comentários -->
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-6 mt-10">
          <div class="bg-white rounded-2xl shadow p-5">
            <h3 class="font-semibold text-gray-700 mb-4">🏆 Ranking das Áreas</h3>
            <ul class="divide-y">
              <li
                v-for="(r, i) in rankingAreas"
                :key="i"
                class="flex justify-between items-center py-2 text-gray-700"
              >
                <span class="font-medium">{{ i + 1 }}. {{ r.areaName }}</span>
                <span class="font-semibold text-azul">{{ r.avgRating }}/5</span>
              </li>
            </ul>
          </div>

          <div class="bg-white rounded-2xl shadow p-5">
            <h3 class="font-semibold text-gray-700 mb-4 flex"><svg xmlns="http://www.w3.org/2000/svg" class="text-azul "  width="24" height="24" viewBox="0 0 24 24"><path fill="currentColor" d="M18 3a4 4 0 0 1 4 4v8a4 4 0 0 1-4 4h-4.724l-4.762 2.857a1 1 0 0 1-1.508-.743L7 21v-2H6a4 4 0 0 1-3.995-3.8L2 15V7a4 4 0 0 1 4-4zm-4 9H8a1 1 0 0 0 0 2h6a1 1 0 0 0 0-2m2-4H8a1 1 0 1 0 0 2h8a1 1 0 0 0 0-2"/></svg>  Comentários Recentes</h3>
            <div v-for="(fb, i) in comentariosRecentes" :key="i" class="border-b py-2 last:border-none">
              <p class="font-semibold text-gray-900">{{ fb.name }}</p>
              <p class="text-sm text-gray-600 italic mt-1">"{{ fb.comentario }}"</p>
              <p class="text-xs text-gray-400 mt-1">{{ fb.data }}</p>
            </div>
          </div>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import Chart from 'chart.js/auto'
import SideBar from './SideBar.vue'
import HeaderAdm from './HeaderAdm.vue'
import api from '@/request/api'
import * as signalR from '@microsoft/signalr'

const estatisticas = ref({
  visitantes: 0,
  feedbacks: 0,
  satisfacao: 0
})

const rankingAreas = ref([])
const comentariosRecentes = ref([])

let chartArea, chartDonut, chartLinha
let connection

function formatNumber(n) {
  return Number(n).toFixed(2)
}

onMounted(async () => {
  // Inicializa gráficos
  chartArea = new Chart(document.getElementById('chartArea'), {
    type: 'bar',
    data: {
      labels: [],
      datasets: [{
        label: 'Participantes',
        data: [],
        backgroundColor: '#009FE3'
      }]
    },
    options: { responsive: true, maintainAspectRatio: false }
  })

  chartDonut = new Chart(document.getElementById('chartDonut'), {
    type: 'doughnut',
    data: {
      labels: ['Satisfeitos', 'Neutros', 'Insatisfeitos'],
      datasets: [{
        data: [],
        backgroundColor: ['#009FE3', '#FACC15', '#F87171']
      }]
    },
    options: { responsive: true, maintainAspectRatio: false, cutout: '70%' }
  })

  chartLinha = new Chart(document.getElementById('chartLinha'), {
    type: 'line',
    data: {
      labels: [],
      datasets: [{
        label: 'Feedbacks',
        data: [],
        borderColor: '#009FE3',
        borderWidth: 2,
        fill: false,
        tension: 0.4
      }]
    },
    options: { responsive: true, maintainAspectRatio: false }
  })

  // Carrega dados iniciais via API
  try {
    const [respRanking, respUltimos, respTotal, respPercentuais] = await Promise.all([
      api.get("feedbacks/ranking"),
      api.get("feedbacks/ultimos3"),
      api.get("feedbacks/total"),
      api.get("feedbacks/percentuais")
    ])

    estatisticas.value.feedbacks = formatNumber(respTotal.data)
    estatisticas.value.satisfacao = formatNumber(respPercentuais.data.geral)
    rankingAreas.value = respRanking.data
    comentariosRecentes.value = respUltimos.data

    // Atualiza gráficos iniciais
    chartArea.data.labels = rankingAreas.value.map(a => a.areaName)
    chartArea.data.datasets[0].data = rankingAreas.value.map(a => a.participantes ?? 0)
    chartArea.update()

    chartDonut.data.datasets[0].data = [
      respPercentuais.data.satisfeitos,
      respPercentuais.data.neutros,
      respPercentuais.data.insatisfeitos
    ]
    chartDonut.update()

    chartLinha.data.labels = respUltimos.data.map(fb => fb.data)
    chartLinha.data.datasets[0].data = respUltimos.data.map((_, i) => i + 1)
    chartLinha.update()
  } catch (err) {
    console.error(err)
  }

  // 🔥 Conexão SignalR
  connection = new signalR.HubConnectionBuilder()
    .withUrl("/feedbackHub") // ajusta porta se necessário
    .withAutomaticReconnect()
    .build()

  // Eventos recebidos
  connection.on("ReceiveTotal", total => {
    estatisticas.value.feedbacks = formatNumber(total)
  })

  connection.on("ReceiveMedia", media => {
    estatisticas.value.satisfacao = formatNumber(media)
  })

  connection.on("ReceivePercentuais", (percentuais, geral) => {
    estatisticas.value.satisfacao = formatNumber(geral)
    chartDonut.data.datasets[0].data = [
      percentuais.satisfeitos,
      percentuais.neutros,
      percentuais.insatisfeitos
    ]
    chartDonut.update()
  })

  connection.on("ReceiveRanking", ranking => {
    rankingAreas.value = ranking
    chartArea.data.labels = ranking.map(r => r.areaName)
    chartArea.data.datasets[0].data = ranking.map(r => r.participantes ?? 0)
    chartArea.update()
  })

  connection.on("ReceiveUltimos3", ultimos => {
    comentariosRecentes.value = ultimos
    chartLinha.data.labels = ultimos.map(fb => fb.data)
    chartLinha.data.datasets[0].data = ultimos.map((_, i) => i + 1)
    chartLinha.update()
  })

  try {
    await connection.start()
    console.log("SignalR conectado ao Dashboard")
  } catch (err) {
    console.error("Erro ao conectar SignalR", err)
  }
})

onUnmounted(async () => {
  if (connection) {
    await connection.stop()
    console.log("SignalR desconectado do Dashboard")
  }
})
</script>


<style scoped>
canvas {
  width: 100% !important;
}
</style>
