import { ref } from 'vue'
import { defineStore } from 'pinia'

export default defineStore('authentication-store', () => {
  //const doubleCount = computed(() => count.value * 2)
  /*function increment() {
    count.value++
  }*/
  //const authenticated = ref(false)
  const accountId = ref(null)
  const accessChecked = ref(false)

  return { accessChecked, accountId }
})
