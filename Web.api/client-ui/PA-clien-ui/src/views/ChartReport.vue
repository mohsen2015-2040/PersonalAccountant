<template>
  <v-card class="ma-5" dir="rtl">
    <v-card-title
      ><v-card-title class="d-flex flex-row ga-4">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          height="48px"
          viewBox="0 -960 960 960"
          width="48px"
          fill="#2b8a3e"
        >
          <path
            d="M510-510h309q-11-122-98.5-211.5T510-819v309Zm-60 369v-678q-131 11-220.5 109T140-480q0 132 89.5 230T450-141Zm60 0q123-8 210.5-97.5T819-450H510v309Zm-30-339Zm0 400q-83 0-156-31.5T197-197q-54-54-85.5-127T80-480q0-83 31.5-156T197-763q54-54 127-85.5T480-880q83 0 156 31.5T763-763q54 54 85.5 127T880-480q0 83-31.5 156T763-197q-54 54-127 85.5T480-80Z"
          />
        </svg>
        <h2 style="color: #495057">گزارش گیری</h2></v-card-title
      >
      <v-row
        ><v-col cols="12" sm="12" md="6"><v-divider></v-divider></v-col></v-row
    ></v-card-title>
    <v-card-text
      ><v-row class="mt-3">
        <v-col cols="12" md="2" sm="3">
          <v-select
            label="کارت"
            density="compact"
            :items="cards"
            variant="outlined"
            v-model="card"
            item-title="number"
            item-value="creditCardId"
            bg-color="light-green-lighten-5"
          ></v-select></v-col
        ><v-col cols="12" md="2" sm="3"
          ><v-select
            label="زمان"
            density="compact"
            :items="terms"
            variant="outlined"
            v-model="term"
            item-title="title"
            item-value="key"
            bg-color="light-green-lighten-5"
          ></v-select></v-col></v-row
      ><v-row v-if="transactions.length"
        ><v-col cols="12">
          <Pie
            :options="options"
            :data="balanceChartData"
            v-if="chartDataLoaded && balanceChartData.datasets[0].data.length" />

          <not-found :width="300" v-else /></v-col
        ><v-divider></v-divider
        ><v-col cols="12" md="6" sm="6">
          <Pie
            :options="options"
            :data="incomeCategoriesChartData"
            v-if="chartDataLoaded && incomeCategoriesChartData.datasets[0].data.length" /><not-found
            :width="300"
            v-else /></v-col
        ><v-divider vertical></v-divider
        ><v-col cols="12" md="6" sm="6"
          ><Pie
            :options="options"
            :data="expenseCategoriesChartData"
            v-if="chartDataLoaded && expenseCategoriesChartData.datasets[0].data.length"
          /><not-found :width="300" v-else /> </v-col></v-row
      ><not-found v-else /></v-card-text
  ></v-card>
</template>

<script setup>
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  PieController,
  Legend,
  BarElement,
  ArcElement,
  CategoryScale,
  LinearScale,
} from 'chart.js'
import NotFound from '@/components/NotFound.vue'
import { Pie } from 'vue-chartjs'
import { options } from '@/Common/PieChartConfig'
import { reactive, watch } from 'vue'
import { ref } from 'vue'
import getRandomColor from '@/Common/RandomColor'
import axios from '@/services/axios'
import UseAuthenticationStore from '@/stores/UseAuthenticationStore'
import UseTermStore from '@/stores/UseTermStore'
import UseLoadingStore from '@/stores/UseLoadingStore'

ChartJS.register(
  Title,
  Tooltip,
  Legend,
  ArcElement,
  PieController,
  BarElement,
  CategoryScale,
  LinearScale,
)

const loadingStore = UseLoadingStore()
const authStore = UseAuthenticationStore()
const termStore = UseTermStore()

const cards = ref([{ number: 'همه کارت ها', creditCardId: '' }])
const card = ref(cards.value[0])
let chartDataLoaded = ref(false)
const terms = termStore.terms
const term = ref(terms[0])
const balance = { income: 0, expense: 0 }
let incomeCategoryAmounts = {}
let incomeCategoryColors = {}
let expenseCategoryAmounts = {}
let expenseCategoryColors = {}
let transactions = ref([])
let incomeTransactions = []
let expenseTransactions = []

const balanceChartData = reactive({
  labels: ['هزینه', 'درآمد'],
  datasets: [
    {
      backgroundColor: ['#e03131', '#37b24d'],
      data: [],
    },
  ],
})
const incomeCategoriesChartData = reactive({
  labels: [],
  datasets: [
    {
      backgroundColor: [],
      data: [],
    },
  ],
})
const expenseCategoriesChartData = reactive({
  labels: [],
  datasets: [
    {
      backgroundColor: [],
      data: [],
    },
  ],
})

/*const getRandomColor = () => {
  const randomColor = Math.floor(Math.random() * 16777215).toString(16)
  return '#' + randomColor
}*/

const initTransactions = async () => {
  chartDataLoaded.value = false
  loadingStore.loading = true
  await axios
    .get('api/transaction/list', {
      params: {
        creditCardId: card.value,
        accountId: authStore.accountId,
        term: term.value,
      },
    })
    .then((response) => {
      Object.assign(balance, response.data.balance)
      transactions.value = response.data.transactions
    })
    .finally(() => {
      loadingStore.loading = false
    })
}

const initCards = async () => {
  await axios
    .get('api/credit-card/list', {
      params: { accountId: authStore.accountId },
    })
    .then((response) => {
      cards.value.push(...response.data)
    })
}

const initCharts = () => {
  calculateCategories()
  balanceChartData.datasets[0].data = [balance.expense, balance.income]
  incomeCategoriesChartData.labels = [...Object.keys(incomeCategoryAmounts)]
  incomeCategoriesChartData.datasets[0].backgroundColor = [...Object.values(incomeCategoryColors)]
  incomeCategoriesChartData.datasets[0].data = [...Object.values(incomeCategoryAmounts)]
  expenseCategoriesChartData.labels = [...Object.keys(expenseCategoryAmounts)]
  expenseCategoriesChartData.datasets[0].backgroundColor = [...Object.values(expenseCategoryColors)]
  expenseCategoriesChartData.datasets[0].data = [...Object.values(expenseCategoryAmounts)]
}

const filterTransactions = () => {
  incomeTransactions = []
  expenseTransactions = []

  for (const element of transactions.value) {
    if (element.transactionTypeId == 1) {
      incomeTransactions.push(element)
    } else {
      expenseTransactions.push(element)
    }
  }
}

const calculateCategories = () => {
  filterTransactions()

  incomeCategoryAmounts = {}
  incomeCategoryColors = {}
  expenseCategoryAmounts = {}
  expenseCategoryColors = {}

  for (const element of incomeTransactions) {
    incomeCategoryAmounts[`${element.category.title}`] = 0
  }

  for (const element of expenseTransactions) {
    expenseCategoryAmounts[`${element.category.title}`] = 0
  }

  for (const element of incomeTransactions) {
    incomeCategoryAmounts[`${element.category.title}`] += element.amount
    incomeCategoryColors[`${element.category.title}`] = getRandomColor()
  }
  for (const element of expenseTransactions) {
    expenseCategoryAmounts[`${element.category.title}`] += element.amount
    expenseCategoryColors[`${element.category.title}`] = getRandomColor()
  }
}

watch(transactions, () => {
  initCharts()
  chartDataLoaded.value = true
})

watch(card, initTransactions)
watch(term, initTransactions)

initTransactions()
initCards()
</script>
