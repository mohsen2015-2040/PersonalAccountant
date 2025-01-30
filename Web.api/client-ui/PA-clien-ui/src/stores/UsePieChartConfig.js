import { defineStore } from 'pinia'

export default defineStore('pie-chart-config', () => {
  const options = { responsive: true, maintainAspectRatio: false }

  return { options }
})
