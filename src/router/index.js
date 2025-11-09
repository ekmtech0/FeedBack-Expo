import { createRouter, createWebHistory } from 'vue-router'


 export const routes = [
    {
      path: '/',
      name: '/BoasVindas',
      component:() => import('@/Views/Boas-Vndas.vue'),
   },
   {
     path: '/MainHome',
     name: 'MainHome',
     component: () => import('@/Views/Main-Home.vue')
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
     path: '/SlideArea',
     name: 'SlideArea',
     component: () => import('@/Views/SlideArea.vue')
   },
   {
     path: '/FormArea',
     name: 'Formarea',
     component: () => import('@/Views/Componentes/FormArea.vue')
   },
   
   {
     path: '/Dashboard',
     name: 'Dashboard',
     component: () => import('@/Views/ADM/DashboardADM.vue')
   },
   {
     path: '/SideBar',
     name: 'Sidebar',
     component: ()=> import('@/Views/ADM/SideBar.vue')
   },
   {
     path: '/Area',
     name: 'Area',
     component: ()=> import('@/Views/ADM/AreaAdm.vue')
   },
   {
     path: '/feedback',
     name: 'feedback',
     component: ()=> import('@/Views/ADM/FeedbackAdm.vue')
     
   },
   {
     path: '/estatistica',
     name: 'estatistica',
     component: ()=> import('@/Views/ADM/EstatisticaAdm.vue')
   },
   {
     path: '/headerAdm',
     name: 'header',
     commponent: () => import('@/Views/ADM/HeaderAdm.vue')
   },
   {
     path: '/Expositor',
     name: 'Expositor',
     component: () => import('@/Views/ADM/ExpositorAdm.vue')
   },
    {
    path: "/area/:id", // <- Rota dinâmica
    name: "AreaFeedback",
    component:() => import ('@/Views/AreaFeedback.vue'),
    props: true,
  },
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
