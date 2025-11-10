<template>
  <nav
    class="fixed top-0 left-0 w-full z-50  bg-azul shadow-lg transition-all duration-500"
  >
    <div
      class="max-w-7xl mx-auto px-6 py-3 flex items-center justify-between text-white"
    >
      <!-- Logo -->
      <div class="flex items-center space-x-3">
        <img
          :src="LogoBranco"
          alt="Expo Jovem Logo"
          class="h-14 drop-shadow-[0_0_10px_rgba(255,255,255,0.4)]"
        />
        <span class="text-xl font-bold tracking-wide hidden sm:block"
          >2025</span
        >
      </div>

      <!-- Menu desktop -->
      <ul
        class="hidden md:flex items-center space-x-8 font-medium text-sm uppercase"
      >
        <li>
          <a
            href="#inicio"
            class="hover:text-[#00C8FF] transition-all duration-300"
            >Início</a
          >
        </li>
        <li>
          <a
            href="#areas"
            class="hover:text-[#00C8FF] transition-all duration-300"
            >Áreas</a
          >
        </li>
        <li>
          <a
            href="#feedback"
            class="hover:text-[#00C8FF] transition-all duration-300"
            >Feedback</a
          >
        </li>
        <li>
          <a
            href="#sobre"
            class="hover:text-[#00C8FF] transition-all duration-300"
            >Sobre</a
          >
        </li>
      </ul>

      <!-- Botão de menu (mobile) -->
      <button
        @click="menuOpen = !menuOpen"
        class="md:hidden focus:outline-none text-white hover:text-[#00C8FF] transition-all duration-300"
      >
        <svg
          v-if="!menuOpen"
          xmlns="http://www.w3.org/2000/svg"
          class="h-7 w-7"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M4 6h16M4 12h16M4 18h16" />
        </svg>
        <svg
          v-else
          xmlns="http://www.w3.org/2000/svg"
          class="h-7 w-7"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M6 18L18 6M6 6l12 12" />
        </svg>
      </button>
    </div>

    <!-- Menu mobile -->
    <transition name="slide">
      <div
        v-if="menuOpen"
        class="md:hidden bg-white/10 backdrop-blur-xl border-t border-white/20 text-white text-center py-6 space-y-4"
      >
        <a href="#inicio" class="block hover:text-[#00C8FF]">Início</a>
        <a href="#areas" class="block hover:text-[#00C8FF]">Áreas</a>
        <a href="#feedback" class="block hover:text-[#00C8FF]">Feedback</a>
        <a href="#sobre" class="block hover:text-[#00C8FF]">Sobre</a>
      </div>
    </transition>
  </nav>
</template>

<script setup>
import { ref } from "vue";
import LogoBranco from '@/assets/img/ej branco.png'
const menuOpen = ref(false);
</script>

<style scoped>
.slide-enter-active,
.slide-leave-active {
  transition: all 0.4s ease;
}
.slide-enter-from {
  opacity: 0;
  transform: translateY(-10px);
}
.slide-leave-to {
  opacity: 0;
  transform: translateY(-10px);
}
</style>




<!-- 
(b) Dashboard Administrativo

Menu lateral com ícones:

Dashboard

Áreas

Feedbacks

Estatísticas

Configurações

Conteúdo principal:

Cards com números principais:

Total de visitantes

Total de feedbacks

Média geral de satisfação

Gráficos (linhas, donuts e barras)

Ranking das áreas com melhores notas

Comentários recentes (últimos feedbacks)

(c) Gestão de Áreas

Tabela com:

Nome da área

QR Code gerado automaticamente

Número de feedbacks

Botões: “Editar”, “Apagar”, “Ver QR Code”

Botão “Adicionar nova área”

Opção de gerar QR Code em imagem ou link

(d) Gestão de Feedbacks

Tabela com todos os feedbacks recebidos:

Colunas: Área, Nome, Avaliação, Comentário, Data

Filtro por área, busca por nome

Opção “Exportar feedbacks” (CSV/PDF)

(e) Página de Estatísticas Detalhadas

Gráficos por área (linhas, barras, donuts)

Filtro por período (hoje / semana / evento completo)

Percentual de satisfação geral

Curva de participação (número de feedbacks por hora) -->



