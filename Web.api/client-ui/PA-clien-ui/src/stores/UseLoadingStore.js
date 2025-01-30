import { defineStore } from 'pinia'
import { ref } from 'vue'

export default defineStore('loading', () => {
  let loading = ref(false)

  return { loading }
})
