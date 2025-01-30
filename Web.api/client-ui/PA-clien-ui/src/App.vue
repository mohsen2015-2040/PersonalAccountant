<template>
  <v-app>
    <AppBar></AppBar>

    <v-main>
      <RouterView></RouterView>
      <Loading v-if="loadingStore.loading" :size="60"></Loading>

      <v-snackbar
        class="mb-4"
        v-model="snackbarStore.visible"
        :color="snackbarStore.color"
        timeout="4000"
      >
        {{ snackbarStore.message }}
      </v-snackbar>
    </v-main>
  </v-app>
</template>

<script setup>
import AppBar from './components/AppBar.vue'
import { RouterView } from 'vue-router'
import UseSnackbarStore from './stores/UseSnackbarStore'
import UseLoadingStore from './stores/UseLoadingStore'
import Loading from './components/Loading.vue'

const snackbarStore = UseSnackbarStore()
const loadingStore = UseLoadingStore()
</script>
