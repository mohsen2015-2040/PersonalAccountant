<template>
  <v-app-bar color="green-lighten-1" prominent>
    <v-toolbar-title class="text-center"
      ><h2>
        <router-link to="/" style="text-decoration: none; color: #fafafa">حسابی</router-link>
      </h2></v-toolbar-title
    >

    <v-app-bar-nav-icon
      variant="tonal"
      @click.stop="drawer = !drawer"
      v-if="authStore.accountId != null"
    ></v-app-bar-nav-icon>
  </v-app-bar>
  <v-navigation-drawer
    v-model="drawer"
    :location="$vuetify.display.mobile ? 'up' : 'right'"
    temporary
    v-if="authStore.accountId != null"
    dir="rtl"
  >
    <v-list-item link height="70" append-icon="mdi-chart-pie"
      ><router-link to="/chart-report" style="text-decoration: none; color: #495057"
        >گزارش گیری</router-link
      ></v-list-item
    >

    <v-list-item link height="70" append-icon="mdi-credit-card-multiple-outline"
      ><router-link to="/credit-cards" style="text-decoration: none; color: #495057"
        >مدیریت کارت ها</router-link
      ></v-list-item
    >

    <v-list-item link height="70" append-icon="mdi-key-outline"
      ><router-link to="/account/change-password" style="text-decoration: none; color: #495057"
        >تغییر رمز عبور</router-link
      ></v-list-item
    >

    <v-list-item
      title="خروج از حساب"
      height="70"
      style="color: #495057"
      append-icon="mdi-logout-variant"
      @click.prevent="logout"
    ></v-list-item>
  </v-navigation-drawer>
</template>

<script setup>
import { ref } from 'vue'
import { RouterLink } from 'vue-router'
import UseAuthenticationStore from '@/stores/UseAuthenticationStore'
import { useRouter } from 'vue-router'

let drawer = ref(false)
const authStore = UseAuthenticationStore()
const router = useRouter()

const logout = () => {
  localStorage.removeItem('token')
  authStore.accountId = null
  router.push({ name: 'login' })
}
</script>
