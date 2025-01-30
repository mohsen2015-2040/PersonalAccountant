import { defineStore } from 'pinia'
import { ref } from 'vue'

export default defineStore('modals-store', () => {
  let createCategoryVisible = ref(false)
  let createCardVisible = ref(false)

  return { createCategoryVisible, createCardVisible }
})
