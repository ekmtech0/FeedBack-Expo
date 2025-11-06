import { createRouter, createWebHistory } from 'vue-router'


 export const routes = [
    {
      path: '/',
      name: '/Main-Home',
      component:() => import('@/Views/Main-Home.vue'),
   },
   {
     path: '/NavBar',
     name: 'NavBar',
     component: ()=> import('@/Views/Componentes/NavBar.vue')
    },
   {
     path: '/BoasVindas',
     name: '/boasVindas',
     component: () => import('@/Views/Boas-Vndas.vue')
   },
   {
     path: '/InformaticaArea',
     name: 'InformaticaArea',
     component: () => import('@/Views/InformaticaArea.vue')
   },
   {
     path: '/EletricidadeArea',
     name: 'EletricidadeArea',
     component: ()=> import('@/Views/EletricidadeArea.vue')
   },
   {
     path: '/FrioArea',
     name: 'FrioArea',
     component: () => import('@/Views/FrioAre.vue')
   },
   {
     path: '/GestaoArea',
     name: '/GestaoArea',
     component: ()=> import('@/Views/GestaoArea.vue')
   },
   {
     path: '/MecanicaArea',
     name: '/MecanicaArea',
     component: ()=> import('@/Views/MecanicaArea.vue')
   },
   {
     path: '/SaudeArea',
     name: 'SaudeArea',
     component: ()=> import('@/Views/SaudeArea.vue')
   },

   {
     path: '/PastelariaArea',
     name: 'PastelariaArea',
     component: ()=> import('@/Views/PastelariaArea.vue')
   },
   {
     path: '/SlideArea',
     name: 'SlideArea',
     component: () => import('@/Views/SlideArea.vue')
   },
   {
     path: '/FormArea',
     name: 'Formarea',
     component: () => import('@/Views/Componentes/FormArea.vue')
   }
  ]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
  scrollBehavior(to) {
    if (to.hash) {
      return { el: to.hash, behavior: 'smooth' }
    }
    return { top: 0 }
  },
})
export default router
