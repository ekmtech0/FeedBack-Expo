<template>
  <div>
    <!-- 🔹 Botão hamburguer (só aparece no mobile) -->
    <button
      @click="isOpen = !isOpen"
      class="md:hidden fixed top-4 left-4 z-50 p-3 bg-azul text-white rounded-lg shadow-md transition-transform active:scale-95"
      aria-label="Abrir Menu"
    >
      <i class="fas fa-bars text-lg"></i>
    </button>

    <!-- 🔹 Overlay (só aparece quando sidebar está aberta no mobile) -->
    <div
      v-if="isOpen"
      class="fixed inset-0 bg-black/50 z-40 md:hidden"
      @click="isOpen = false"
    ></div>

    <!-- 🔹 Sidebar -->
    <aside
      :class="[
        'fixed left-0 top-0 h-screen w-64 bg-azul text-white shadow-xl flex flex-col justify-between transition-transform duration-500 z-50 md:translate-x-0',
        isOpen ? 'translate-x-0' : '-translate-x-full'
      ]"
    >
      <!-- Topo: Logo -->
      <div>
        <div class="flex items-center justify-center py-6 px-4">
          <img
            :src="LogoBranco"
            alt="Expo Jovem Logo"
            class="h-12 drop-shadow-[0_0_10px_rgba(255,255,255,0.4)]"
          />
          <span class="ml-2 text-lg font-semibold tracking-wide hidden sm:block">Admin</span>
        </div>

        <div class="border-b border-white/20"></div>

        <!-- Menu -->
        <nav class="mt-4 overflow-y-auto">
          <ul class="space-y-1">
            <li>
              <a
                @click="navigate('/Dashboard')"
                class="flex items-center px-6 py-3 hover:bg-white/10 transition-all duration-300 group cursor-pointer"
              >
                <i class="fas fa-home mr-3 text-[#00C8FF] group-hover:scale-110 transition-transform"></i>
                <span class="text-base">Dashboard</span>
              </a>
            </li>

            <li>
              <a
                @click="toggleSubmenu"
                class="flex items-center justify-between px-6 py-3 hover:bg-white/10 transition-all duration-300 group cursor-pointer"
              >
                <div class="flex items-center">
                  <i class="fas fa-layer-group mr-3 text-[#00C8FF] group-hover:scale-110 transition-transform"></i>
                  <span class="text-base">Áreas</span>
                </div>
                <i :class="['fas', aparecer ? 'fa-chevron-up' : 'fa-chevron-down', 'text-xs opacity-70']"></i>
              </a>

              <!-- Submenu -->
              <transition name="slide-fade">
                <div v-if="aparecer" class="ml-10 space-y-1">
                  <a
                    @click="navigate('/Area')"
                    class="flex items-center px-4 py-2 text-sm hover:bg-white/10 transition rounded cursor-pointer"
                  >
                    <span>Ver todas</span>
                  </a>
                  <a
                    @click="navigate('/Expositor')"
                    class="flex items-center px-4 py-2 text-sm hover:bg-white/10 transition rounded cursor-pointer"
                  >
                    <span>Expositores</span>
                  </a>
                </div>
              </transition>
            </li>

            <li>
              <a
                @click="navigate('/feedback')"
                class="flex items-center px-6 py-3 hover:bg-white/10 transition-all duration-300 group cursor-pointer"
              >
                <i class="fas fa-comment-dots mr-3 text-[#00C8FF] group-hover:scale-110 transition-transform"></i>
                <span class="text-base">Feedbacks</span>
              </a>
            </li>

            <li>
              <a
                @click="navigate('/estatistica')"
                class="flex items-center px-6 py-3 hover:bg-white/10 transition-all duration-300 group cursor-pointer"
              >
                <i class="fas fa-chart-line mr-3 text-[#00C8FF] group-hover:scale-110 transition-transform"></i>
                <span class="text-base">Estatísticas</span>
              </a>
            </li>

            <li>
              <a
                href="#configuracoes"
                class="flex items-center px-6 py-3 hover:bg-white/10 transition-all duration-300 group cursor-pointer"
              >
                <i class="fas fa-cog mr-3 text-[#00C8FF] group-hover:spin transition-transform"></i>
                <span class="text-base">Configurações</span>
              </a>
            </li>
          </ul>
        </nav>
      </div>

      <!-- Rodapé -->
      <div class="border-t border-white/20 py-4 text-center text-sm opacity-80">
        © 2025 Expo Jovem
      </div>
    </aside>
  </div>
</template>

<script setup>
import LogoBranco from '@/assets/img/ej branco.png'
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const isOpen = ref(false)
const aparecer = ref(false)

function navigate(path) {
  router.push(path)
  isOpen.value = false // fecha sidebar no mobile
}

function toggleSubmenu() {
  aparecer.value = !aparecer.value
}
</script>

<style scoped>
/* 🔹 Scrollbar personalizado */
aside::-webkit-scrollbar {
  width: 6px;
}
aside::-webkit-scrollbar-thumb {
  background: rgba(255, 255, 255, 0.3);
  border-radius: 3px;
}

/* 🔹 Transição do submenu */
.slide-fade-enter-active,
.slide-fade-leave-active {
  transition: all 0.3s ease;
}
.slide-fade-enter-from,
.slide-fade-leave-to {
  transform: translateY(-5px);
  opacity: 0;
}
</style>
