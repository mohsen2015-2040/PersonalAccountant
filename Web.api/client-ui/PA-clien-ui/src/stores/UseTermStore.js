import { defineStore } from 'pinia'
import { ref } from 'vue'

export default defineStore('term-store', () => {
  const terms = ref([
    { key: 'day', title: 'یک روز گذشته' },
    { key: 'week', title: 'هفته جاری' },
    { key: 'month', title: 'ماه جاری' },
    { key: 'threeMonth', title: 'سه ماه گذشته' },
    { key: 'sixMonth', title: 'شش ماه گذشته' },
    { key: 'year', title: 'سال جاری' },
  ])

  return { terms }
})
