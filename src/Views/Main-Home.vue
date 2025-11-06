<template>
  <section class="min-h-screen w-full bg-gradient-to-b from-[#009FE3]/20 via-white to-[#009FE3]/10">
    <!-- Navbar (podes importar quando quiser) -->
     
    <NavBar />
     <SlideArea class="mt-8"/>
    <!-- Conteúdo principal -->
    <div
      class="max-w-6xl mx-auto flex flex-col items-center justify-center px-4 sm:px-6 md:px-8 lg:py-16 py-10"
    >
      <!-- Título -->
      <div
        class="shadow-md bg-white/70 backdrop-blur-sm border border-[#009FE3]/20 rounded-2xl px-6 sm:px-8 py-6 mb-10 text-center w-full max-w-3xl"
      >
        <span
          class="text-xl sm:text-2xl lg:text-3xl font-extrabold text-[#004E7D] drop-shadow-sm block"
        >
          As Áreas da Exposição
        </span>
        <p class="text-gray-600 mt-2 text-sm sm:text-base leading-relaxed">
          Escolhe uma área para explorar os expositores e deixar o teu feedback.
        </p>
      </div>

      <!-- Cards das áreas -->
      <div
        class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 sm:gap-8 w-full"
      >
        <div
          v-for="(area, index) in Areas"
          :key="index"
          @click="irPara(area.link)"
          class="group cursor-pointer relative bg-white/90 hover:bg-[#009FE3]/10 border border-[#009FE3]/20 shadow-md hover:shadow-lg rounded-xl transition-all duration-500 p-6 sm:p-8 flex flex-col justify-between"
        >
          <!-- Nome da área -->
          <div class="text-center">
            <h2
              class="text-lg sm:text-xl lg:text-2xl font-bold text-[#004E7D] group-hover:text-[#009FE3] transition-all"
            >
              {{ area.nome }}
            </h2>
            <p class="text-sm text-gray-500 mt-1">
              Expositores: {{ area.expositor }}
            </p>
          </div>

          <!-- Linha decorativa animada -->
          <div
            class="h-1 w-0 bg-[#009FE3] mt-4 rounded-full group-hover:w-full transition-all duration-500"
          ></div>

          <!-- Ícone ilustrativo -->
          <div
            class="absolute top-2 right-2 sm:right-3 bg-[#009FE3] text-white p-2 rounded-full shadow-md group-hover:scale-110 transition-transform"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              class="h-5 w-5 sm:h-6 sm:w-6"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
              stroke-width="2"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                d="M9 5l7 7-7 7"
              />
            </svg>
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
import { defineEmits } from 'vue';

const emit = defineEmits(['area-enviada']);


const router = useRouter();

const Areas = ref([
  { nome: "Informática", expositor: "2", link: "/InformaticaArea" },
  { nome: "Eletricidade", expositor: "1", link: "/EletricidadeArea" },
  { nome: "Frio e Climatização", expositor: "1", link: "/FrioArea" },
  { nome: "Gestão Empresarial", expositor: "2", link: "/GestaoArea" },
  { nome: "Mecânica", expositor: "1", link: "/MecanicaArea" },
  { nome: "Saúde", expositor: "8", link: "/SaudeArea" },
  { nome: "Pastelaria", expositor: "2", link: "/PastelariaArea" },
]);

function irPara(path) {
    router.push(path);

    function EnviarNome(nome) {
         emit('area-enviada', nome);
    }

}
</script>
