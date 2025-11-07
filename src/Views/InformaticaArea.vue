<template>
  <section
    class="min-h-screen w-full bg-gradient-to-b from-[#009FE3]/10 via-white to-[#009FE3]/20 flex flex-col"
  >
    <NavBar />
    <SlideArea />

    <!-- Conteúdo principal -->
    <div class="flex-grow flex items-center justify-center px-4 sm:px-8 py-10">
      <div
        class="w-full max-w-3xl bg-white/70 backdrop-blur-md border border-[#009FE3]/20 rounded-2xl shadow-lg p-6 sm:p-10 space-y-8"
      >
        <!-- Cabeçalho -->
        <div class="text-center space-y-2">
          <h2 class="text-2xl sm:text-3xl font-extrabold text-[#004E7D]">
            Informática 💻
          </h2>
          <p class="text-gray-500 text-sm sm:text-base">
            Deixe o seu parecer e avaliação abaixo.
          </p>
        </div>

        <!-- Formulário -->
        <form @submit.prevent="enviarFeedback" class="space-y-6">
          <!-- Nome -->
          <div>
            <label
              for="nome"
              class="text-sm font-bold text-[#009FE3] block mb-1"
              >Nome</label
            >
            <input
              v-model.trim="nome"
              type="text"
              id="nome"
              placeholder="O seu nome completo"
              class="border border-[#009FE3]/30 focus:border-[#009FE3] w-full rounded-lg p-3 text-sm sm:text-base transition-all focus:ring-2 focus:ring-[#009FE3]/30 outline-none"
            />
            <p
              v-if="erros.nome"
              class="text-red-500 text-xs sm:text-sm mt-1 font-medium"
            >
              {{ erros.nome }}
            </p>
          </div>

          <!-- Comentário -->
          <div>
            <label
              for="comentario"
              class="text-sm font-bold text-[#009FE3] block mb-1"
              >Comentário</label
            >
            <textarea
              v-model.trim="comentario"
              id="comentario"
              placeholder="Deixe o seu comentário aqui..."
              class="border border-[#009FE3]/30 focus:border-[#009FE3] w-full rounded-lg p-3 sm:p-4 text-sm sm:text-base transition-all focus:ring-2 focus:ring-[#009FE3]/30 outline-none resize-none h-32"
            ></textarea>
            <p
              v-if="erros.comentario"
              class="text-red-500 text-xs sm:text-sm mt-1 font-medium"
            >
              {{ erros.comentario }}
            </p>
          </div>

          <!-- Avaliação por estrelas -->
          <div class="space-y-2">
            <label class="text-sm font-bold text-[#009FE3] block">
              Avaliação
            </label>
            <div class="flex items-center gap-2 justify-center">
              <svg
                v-for="estrela in 5"
                :key="estrela"
                @click="avaliacao = estrela"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 20 20"
                fill="currentColor"
                :class="[ 
                  'w-8 h-8 sm:w-10 sm:h-10 cursor-pointer transition-all duration-300',
                  avaliacao >= estrela
                    ? 'text-[#FFD700] scale-110 drop-shadow-md'
                    : 'text-gray-300 hover:text-[#FFD700]'
                ]"
              >
                <path
                  d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.286 3.967a1 1 0 00.95.69h4.175c.969 0 1.371 1.24.588 1.81l-3.382 2.46a1 1 0 00-.364 1.118l1.287 3.966c.3.921-.755 1.688-1.54 1.118l-3.382-2.46a1 1 0 00-1.175 0l-3.382 2.46c-.785.57-1.84-.197-1.54-1.118l1.287-3.966a1 1 0 00-.364-1.118L2.05 9.394c-.783-.57-.38-1.81.588-1.81h4.175a1 1 0 00.95-.69l1.286-3.967z"
                />
              </svg>
            </div>
            <p
              v-if="erros.avaliacao"
              class="text-red-500 text-xs sm:text-sm mt-1 text-center font-medium"
            >
              {{ erros.avaliacao }}
            </p>
          </div>

          <!-- Botão -->
          <div>
            <button
              type="submit"
              class="w-full bg-[#009FE3] hover:bg-[#007CC0] text-white font-semibold py-3 rounded-lg transition-all duration-300 shadow hover:shadow-lg text-sm sm:text-base"
            >
              Enviar Feedback
            </button>
          </div>
        </form>
      </div>
    </div>
    <QrCodeArea
      area="Informática"
      link="https://ekmtech0.github.io/FeedBack-Expo/"
    />
  </section>
  <ModaslFeedback v-if="Aparecer"/>
</template>

<script setup>
import { ref } from "vue";
import NavBar from "./Componentes/NavBar.vue";
import SlideArea from "./SlideArea.vue";
import ModaslFeedback from "./Componentes/ModaslFeedback.vue";
import QrCodeArea from "./Componentes/QrcodeArea.vue";

const nome = ref("");
const comentario = ref("");
const avaliacao = ref(0);
const erros = ref({ nome: "", comentario: "", avaliacao: "" });
const Aparecer = ref(false)

function validarCampos() {
  let valido = true;
  erros.value = { nome: "", comentario: "", avaliacao: "" };

  if (!nome.value) {
    erros.value.nome = "Por favor, insira o seu nome.";
    valido = false;
  }
  if (!comentario.value) {
    erros.value.comentario = "O campo de comentário não pode estar vazio.";
    valido = false;
  }
  if (avaliacao.value === 0) {
    erros.value.avaliacao = "Selecione uma avaliação com estrelas.";
    valido = false;
  }

  return valido;
}

function enviarFeedback() {
  if (validarCampos()) {
    nome.value = "";
    comentario.value = "";
      avaliacao.value = 0;
    Aparecer.value = true
  }
}

</script>
