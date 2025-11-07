<template>
  <!-- Fundo semitransparente -->
  <!-- Fundo semitransparente -->
<div
  v-if="showModal"
  class="fixed inset-0 z-50 flex items-center justify-center bg-black/50  transition-all duration-300"
>
  <!-- Modal -->
  <div
    class="bg-white rounded-2xl shadow-2xl w-[90%] max-w-md text-center p-8 animate-fadeIn relative"
  >
    <!-- Ícone de sucesso -->
    <div class="flex justify-center mb-4">
      <div class="w-16 h-16 rounded-full bg-[#009FE3]/10 flex items-center justify-center">
        <svg xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-[#009FE3]" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M5 13l4 4L19 7" />
        </svg>
      </div>
    </div>

    <!-- Mensagens -->
    <h2 class="text-xl font-semibold text-gray-800 mb-2">
      Obrigado pelo teu feedback na área de <span class="text-[#009FE3]">{{ area }}</span>!
    </h2>
    <p class="text-gray-600 mb-6 text-sm">
      O teu feedback ajuda a <strong>Expo Jovem 2025</strong> a crescer!
    </p>

    <!-- Botão -->
    <button
      @click="irPara('/MainHome')"
      class="bg-[#009FE3] hover:bg-[#0084c4] transition-colors text-white px-6 py-2 rounded-full font-medium shadow-md"
    >
      Voltar à página inicial
    </button>
  </div>
</div>

</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useRoute } from "vue-router";
const route = useRoute();
const router = useRouter();
const showModal = ref(true); 

function irPara(path) {
    router.push(path);
}
const area = ref(route.query.area);
</script>

<style scoped>
@keyframes fadeIn {
  from {
    opacity: 0;
    transform: scale(0.95);
  }
  to {
    opacity: 1;
    transform: scale(1);
  }
}
.animate-fadeIn {
  animation: fadeIn 0.3s ease-out;
}
</style>
