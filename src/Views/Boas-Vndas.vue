<template>
  <section
    class="flex items-center justify-center min-h-screen bg-gradient-to-br from-[#009FE3] via-sky-400 to-blue-900 relative overflow-hidden"
  >
    <!-- Luz de fundo animada -->
    <div class="absolute inset-0">
      <div
        class="absolute w-80 h-80 bg-white/20 rounded-full blur-3xl top-10 left-20 animate-pulse"
      ></div>
      <div
        class="absolute w-96 h-96 bg-[#009FE3]/30 rounded-full blur-3xl bottom-10 right-20 animate-pulse delay-300"
      ></div>
    </div>

    <div
      class="relative z-10 text-center flex flex-col items-center p-8 bg-white/10 backdrop-blur-xl rounded-3xl shadow-2xl max-w-md mx-auto border border-white/20"
    >
      <p
        class="text-white font-semibold text-sm lg:text-base uppercase tracking-widest mb-4 animate-fadeIn"
      >
        Seja bem-vindo!
      </p>

      <img
        src="/public/img/ej branco.png"
        alt="Expo Jovem Logo"
        class="lg:h-[400px] h-[250px] object-contain drop-shadow-[0_0_20px_rgba(255,255,255,0.3)] animate-float"
      />

      <a
        href="#"
        class="mt-[-30px] bg-white/90 text-[#009FE3] font-semibold rounded-2xl px-8 py-3 text-sm lg:text-lg shadow-lg hover:scale-105 hover:bg-white transition-all duration-300 ease-in-out"
      >
        Explorar Áreas
      </a>
    </div>
  </section>
</template>

<script setup>
// Nenhuma lógica ainda, apenas layout
</script>

<style scoped>
@keyframes float {
  0%, 100% {
    transform: translateY(0);
  }
  50% {
    transform: translateY(-8px);
  }
}
.animate-float {
  animation: float 4s ease-in-out infinite;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
.animate-fadeIn {
  animation: fadeIn 1s ease-in-out forwards;
}
</style>
