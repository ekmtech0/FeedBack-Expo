<script setup>
import { ref } from "vue";
import { useRoute } from "vue-router";
import NavBar from "./Componentes/NavBar.vue";
import SlideArea from "./SlideArea.vue";
import ModaslFeedback from "./Componentes/ModaslFeedback.vue";
import api from "@/request/api"; // importa o axios configurado
import { onMounted } from "vue";
import QrcodeArea from "./Componentes/QrcodeArea.vue";

import FingerprintJS from '@fingerprintjs/fingerprintjs'

const route = useRoute();
const browserId = ref("")
const url = ref("");
onMounted(async () => {
  url.value = window.location.href
  const fp = await FingerprintJS.load()
  const result = await fp.get()
  browserId.value = result.visitorId   // string única para o navegador
})


// id da área vem dos params
const areaId = Number(route.params.id)
// nome da área vem da query (se quiser mostrar no título)
const areaName = route.query.nome

const nome = ref("")
const comentario = ref("")
const avaliacao = ref(0)
const erros = ref({})
const Aparecer = ref(false)

function validar() {
  erros.value = {}
  if (!nome.value) erros.value.nome = "Por favor, insira o seu nome."
  if (!comentario.value) erros.value.comentario = "O comentário é obrigatório."
  if (!avaliacao.value) erros.value.avaliacao = "Selecione uma avaliação."
  return Object.keys(erros.value).length === 0
}

async function enviarFeedback() {
  if (validar()) {
    try {
      const feedback = {
        name: nome.value,
        browserId: browserId.value, // gera um id único do navegador
        comentario: comentario.value,
        rating: avaliacao.value,
        areaId: areaId
      }

      await api.post("/feedbacks", feedback)

      // limpa o formulário
      nome.value = ""
      comentario.value = ""
      avaliacao.value = 0
      Aparecer.value = true
    } catch (err) {
      console.error("Erro ao enviar feedback", err)
      alert("Já não pode mais comentar nesta area")
    }
  }
}
</script>

<template>
  <section class="min-h-screen w-full bg-gradient-to-b from-[#009FE3]/10 via-white to-[#009FE3]/20 flex flex-col">
    <NavBar />
    <SlideArea />

    <div class="flex-grow flex items-center justify-center px-4 sm:px-8 py-10">
      <div class="w-full max-w-3xl bg-white/70 backdrop-blur-md border border-[#009FE3]/20 rounded-2xl shadow-lg p-6 sm:p-10 space-y-8">
        <!-- Nome da área dinâmico -->
        <h2 class="text-2xl sm:text-3xl font-extrabold text-[#004E7D]">
          {{ areaName }}
        </h2>

        <!-- Formulário -->
        <form @submit.prevent="enviarFeedback" class="space-y-6">
          <!-- Nome -->
          <div>
            <label for="nome" class="text-sm font-bold text-[#009FE3] block mb-1">Nome</label>
            <input v-model.trim="nome" type="text" id="nome" placeholder="O seu nome completo"
              class="border border-[#009FE3]/30 focus:border-[#009FE3] w-full rounded-lg p-3 text-sm sm:text-base transition-all focus:ring-2 focus:ring-[#009FE3]/30 outline-none" />
            <p v-if="erros.nome" class="text-red-500 text-xs mt-1">{{ erros.nome }}</p>
          </div>

          <!-- Comentário -->
          <div>
            <label for="comentario" class="text-sm font-bold text-[#009FE3] block mb-1">Comentário</label>
            <textarea v-model.trim="comentario" id="comentario" placeholder="Deixe o seu comentário aqui..."
              class="border border-[#009FE3]/30 focus:border-[#009FE3] w-full rounded-lg p-3 text-sm sm:text-base transition-all focus:ring-2 focus:ring-[#009FE3]/30 outline-none resize-none h-32"></textarea>
            <p v-if="erros.comentario" class="text-red-500 text-xs mt-1">{{ erros.comentario }}</p>
          </div>

          <!-- Avaliação -->
          <div class="space-y-2 text-center">
            <label class="text-sm font-bold text-[#009FE3]">Avaliação</label>
            <div class="flex justify-center gap-2">
              <svg v-for="estrela in 5" :key="estrela" @click="avaliacao = estrela" xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 20 20" fill="currentColor"
                :class="['w-8 h-8 cursor-pointer transition-all duration-300',
                  avaliacao >= estrela ? 'text-[#FFD700] scale-110 drop-shadow-md' : 'text-gray-300 hover:text-[#FFD700]']">
                <path
                  d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.286 3.967a1 1 0 00.95.69h4.175c.969 0 1.371 1.24.588 1.81l-3.382 2.46a1 1 0 00-.364 1.118l1.287 3.966c.3.921-.755 1.688-1.54 1.118l-3.382-2.46a1 1 0 00-1.175 0l-3.382 2.46c-.785.57-1.84-.197-1.54-1.118l1.287-3.966a1 1 0 00-.364-1.118L2.05 9.394c-.783-.57-.38-1.81.588-1.81h4.175a1 1 0 00.95-.69l1.286-3.967z" />
              </svg>
            </div>
            <p v-if="erros.avaliacao" class="text-red-500 text-xs mt-1">{{ erros.avaliacao }}</p>
          </div>

          <button type="submit"
            class="w-full bg-[#009FE3] hover:bg-[#007CC0] text-white font-semibold py-3 rounded-lg transition-all duration-300 shadow hover:shadow-lg text-sm sm:text-base">
            Enviar Feedback
          </button>
        </form>
      </div>
    </div>

    <QrcodeArea :link="url" :area="areaName"/>
    <ModaslFeedback v-if="Aparecer" />
  </section>
</template>
