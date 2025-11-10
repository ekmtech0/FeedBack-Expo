<template>
  <div class="flex min-h-screen bg-gray-50">
    <SideBar />
    <main class="flex-1 md:ml-64">
      <HeaderAdm />

      <div class="max-w-7xl mx-auto px-4 py-6 space-y-8">
        <!-- Título + Filtro -->
        <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4 mt-6">
          <h2 class="font-bold text-xl text-azul flex items-center gap-2">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-azul" fill="currentColor" viewBox="0 0 24 24">
              <path d="M3 3v18h18V3H3zm2 2h14v14H5V5z"/>
            </svg>
            Estatísticas Detalhadas
          </h2>

       
        </div>

        <!-- Gráficos principais -->
        <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
          <!-- Curva de Participação -->
          <div class="bg-white rounded-xl shadow p-4 col-span-2">
            <h3 class="font-semibold text-gray-700 mb-4">Curva de Participação</h3>
            <Line :data="dadosLinha" :options="opcoesLinha" />
          </div>

          <!-- Donut de Satisfação -->
          <div class="bg-white rounded-xl shadow p-4">
            <h3 class="font-semibold text-gray-700 mb-4">Satisfação Geral</h3>
            <Doughnut :data="dadosDonut" :options="opcoesDonut" />
            <p class="text-center mt-4 text-gray-600">
              <span class="text-3xl font-bold text-azul">{{ satisfacaoGeral }}%</span>
              <br />
              de satisfação média
            </p>
          </div>
        </div>

        <!-- Gráfico de Barras + Ranking -->
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-6">
          <!-- Barras -->
          <div class="bg-white rounded-xl shadow p-4">
            <h3 class="font-semibold text-gray-700 mb-4">Média de Satisfação por Área</h3>
            <Bar :data="dadosBarras" :options="opcoesBarras" />
          </div>

          <!-- Ranking -->
          <div class="bg-white rounded-xl shadow p-4">
            <h3 class="font-semibold text-gray-700 mb-4">Ranking das Áreas</h3>
            <ul class="divide-y">
              <li
                v-for="(area, i) in ranking"
                :key="i"
                class="flex justify-between items-center py-3"
              >
                <span class="font-medium text-gray-700">{{ i + 1 }}. {{ area.nome }}</span>
                <span class="bg-azul/10 text-azul px-3 py-1 rounded-lg text-sm font-semibold">
                  {{ area.media }}/5
                </span>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup>

import { Line, Bar, Doughnut } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  LineElement,
  BarElement,
  CategoryScale,
  LinearScale,
  ArcElement,
  PointElement
} from 'chart.js'

import SideBar from './SideBar.vue'
import HeaderAdm from './HeaderAdm.vue'

ChartJS.register(
  Title,
  Tooltip,
  Legend,
  LineElement,
  BarElement,
  CategoryScale,
  LinearScale,
  ArcElement,
  PointElement
)


// Dados: feedbacks por hora
const dadosLinha = {
  labels: ['12h', '13h', '14h', '15h', '16', '17h', '18h'],
  datasets: [
    {
      label: 'Feedbacks recebidos',
      data: [5, 12, 22, 30, 25, 18, 10],
      borderColor: '#009FE3',
      tension: 0.4,
      fill: false
    }
  ]
}

const opcoesLinha = {
  responsive: true,
  plugins: { legend: { display: true } }
}

// Donut
const satisfacaoGeral = 87
const dadosDonut = {
  labels: ['Satisfeitos', 'Neutros', 'Insatisfeitos'],
  datasets: [
    {
      data: [87, 8, 5],
      backgroundColor: ['#009FE3', '#FFCE56', '#FF6384'],
      hoverOffset: 6
    }
  ]
}

const opcoesDonut = {
  responsive: true,
  plugins: { legend: { position: 'bottom' } }
}

// Barras por área
const dadosBarras = {
  labels: ['Informática', 'Saúde', 'Mecânica', 'Pastelaria', 'Gestão Emp.','Eletricidade'],
  datasets: [
    {
      label: 'Média de satisfação',
      data: [4.8, 3.6, 4.2, 4.9, 3.8, 3,9],
      backgroundColor: '#009FE3'
    }
  ]
}

const opcoesBarras = {
  responsive: true,
  plugins: { legend: { display: false } },
  scales: { y: { beginAtZero: true, max: 5 } }
}

// Ranking
const ranking = [
  { nome: 'Pastelaria', media: 4.9 },
  { nome: 'Informática', media: 4.8 },
  { nome: 'Mecânica', media: 4.2 },
  { nome: 'Saúde', media: 3.6 },
  { nome: 'Gestão Empresarial', media: 3.8 }
]
</script>

<style scoped>
select {
  @apply bg-white;
}
</style>
