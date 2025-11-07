<template>
    <section>


    <picture>
      <!-- Imagem para telas grandes -->
      <source media="(min-width: 1024px)" :srcset="imagensDesktop[indexAtual]" />

      <!-- Imagem para telas pequenas -->
      <img
        :src="imagens[indexAtual]"
        alt="Slide"
        class="w-full h-full object-cover transition-all duration-700"
      />
    </picture>

<!--  INDICADORES (bolinhas) -->
<div class="absolute mt-[-24px] left-1/2 transform -translate-x-1/2 flex gap-3 z-10">
  <span
    v-for="(img, i) in imagens"
    :key="i"
    @click="indexAtual = i"
    :class="[
      'w-2 h-2 rounded-full cursor-pointer transition-all duration-300',
      indexAtual === i ? 'bg-white scale-125' : 'bg-gray-300 hover:bg-white'
    ]"
  ></span>
</div>

    </section>
</template>


<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import saude from '@/assets/img/saude.png'
import informatica from '@/assets/img/informatica.png'
import mecanica from '@/assets/img/mecanica.png'
import pastelaria from '@/assets/img/pastelaria.png'
import eletricidade from '@/assets/img/eletricidade.png'
import frio from '@/assets/img/frio.png'
import gestao from '@/assets/img/gestao.png'
import Dsaude from '@/assets/img/Denfermagem.png'
import Dinfo from '@/assets/img/Dinfo.png'
import DEetri from '@/assets/img/Deletri.png'
import Dfrio from '@/assets/img/Dfrio.png'
import Dmeca from '@/assets/img/Dmeca.png'
import Dpaste from '@/assets/img/Dpaste.png'
import Dgestao from '@/assets/img/Dgestao.png'



const imagens = [saude, informatica, eletricidade, pastelaria, mecanica, gestao, frio];
const imagensDesktop = [Dsaude, Dinfo, DEetri, Dpaste, Dmeca,Dgestao, Dfrio] 

const indexAtual = ref(0)
let intervalo = null

// Trocar de imagem automaticamente a cada 5s
onMounted(() => {
  intervalo = setInterval(() => {
    indexAtual.value = (indexAtual.value + 1) % imagens.length
  }, 4000)
})

onUnmounted(() => {
  clearInterval(intervalo)
})

</script>