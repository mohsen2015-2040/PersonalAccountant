import { defineStore } from 'pinia'
import { ref } from 'vue'

export default defineStore('snackbar-store', () => {
  let visible = ref(false)
  let message = ref('')
  let color = ref('')

  return { visible, message, color }
})
