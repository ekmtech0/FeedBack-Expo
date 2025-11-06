<template>

    
    <section>
        <img
  :src="imagens[indexAtual]"
  alt="Slide de Imagens"
  class=""
/>

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
import { ref,  onMounted, onUnmounted  } from 'vue'
const imagens = [
  
    '/public/img/saude.png',
    '/public/img/informatica.png',
    '/public/img/mecanica.png',
    '/public/img/pastelaria.png',
     
  
  
]

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