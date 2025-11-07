<template>
  <section
    class="min-h-screen w-full bg-gradient-to-b from-[#009FE3]/10 via-white to-[#009FE3]/20 flex flex-col"
  >
    <NavBar />
    <SlideArea />

    <!-- Conteúdo principal -->
    <div class="flex-grow flex items-center justify-center px-4 sm:px-8 py-10">
      <div
        class="w-full max-w-3xl bg-white/70 backdrop-blur-md border border-[#009FE3]/20 rounded-2xl shadow-lg p-8 sm:p-10"
      >
        <!-- Cabeçalho -->
        <div class="text-center mb-8">
          <h2 class="text-2xl sm:text-3xl font-extrabold text-[#004E7D] mb-2">
            Área da Saúde
          </h2>
          <p class="text-gray-600 text-sm sm:text-base">
            Escolhe a subárea que desejas visitar ou avaliar:
          </p>
        </div>

        <!-- Subáreas -->
        <div class="grid grid-cols-1 sm:grid-cols-2 gap-6">
          <div
            v-for="sub in subareas"
            :key="sub.nome"
            @click="irParaSubarea(sub.link, sub.nome)"
            class="cursor-pointer p-6 bg-white/90 border border-[#009FE3]/30 rounded-xl shadow hover:shadow-lg hover:bg-[#009FE3]/10 transition-all text-center group"
          >
            <h3
              class="text-lg sm:text-xl font-bold text-[#004E7D] group-hover:text-[#009FE3]"
            >
              {{ sub.nome }}
            </h3>
            <p class="text-gray-500 text-sm mt-1">Expositores: {{ sub.expositor }}</p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import NavBar from "./Componentes/NavBar.vue";
import SlideArea from "./SlideArea.vue";

const router = useRouter();

const subareas = ref([
  { nome: "Odontologia", expositor: 2, link: "Odontologia" },
  { nome: "Enfermagem", expositor: 3, link: "Enfermagem" },
  { nome: "Análises Clínicas", expositor: 1, link: "Analises" },
]);

function irParaSubarea(path, nome) {
  router.push({ path, query: { area: nome } });
}
</script>
