import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import UseAuthenticationStore from '@/stores/UseAuthenticationStore'
import checkAuth from '@/Middleware/CheckAuth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      beforeEnter: (to, from, next) => {
        checkAuth(next)
        document.title = 'حسابی | تراکنش ها'
      },
    },
    {
      path: '/credit-cards',
      name: 'creditCards',
      component: () => import('../views/CreditCard/HomeView.vue'),
      beforeEnter: (to, from, next) => {
        checkAuth(next)
        document.title = 'حسابی | مدیریت کارت ها'
      },
    },
    {
      path: '/account/login',
      name: 'login',
      component: () => import('../views/Account/Login.vue'),

      beforeEnter: (to, from, next) => {
        const store = UseAuthenticationStore()
        if (store.accountId != null) {
          next({ name: 'home' })
        } else {
          next()
          document.title = 'حسابی | ورود'
        }
      },
    },
    {
      path: '/account/create',
      name: 'accountCreate',
      component: () => import('../views/Account/Create.vue'),
      beforeEnter: (to, from, next) => {
        const store = UseAuthenticationStore()
        if (store.accountId != null) {
          next({ name: 'home' })
        } else {
          next()
          document.title = 'حسابی | ثبت نام'
        }
      },
    },
    {
      path: '/account/change-password',
      name: 'changePassword',
      component: () => import('../views/Account/ChangePassword.vue'),
      beforeEnter: (to, from, next) => {
        checkAuth(next)
        document.title = 'حسابی | تغییر رمز'
      },
    },
    {
      path: '/chart-report',
      name: 'chartReport',
      component: () => import('../views/ChartReport.vue'),
      beforeEnter: (to, from, next) => {
        checkAuth(next)
        document.title = 'حسابی | گزارش گیری'
      },
    },
  ],
})
export default router
