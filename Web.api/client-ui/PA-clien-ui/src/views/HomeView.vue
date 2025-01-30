<template>
  <v-card dir="rtl">
    <v-card-title class="d-flex flex-row ga-8 justify-center mt-4">
      <h1 style="color: #e03131">{{ balance.expense }}</h1>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        height="48px"
        viewBox="0 -960 960 960"
        width="48px"
        fill="#e03131"
        class="mt-1"
      >
        <path
          d="M280-436h400v-94H280v94ZM480.14-55Q392-55 314.51-88.08q-77.48-33.09-135.41-91.02-57.93-57.93-91.02-135.27Q55-391.72 55-479.86 55-569 88.08-646.49q33.09-77.48 90.86-134.97 57.77-57.48 135.19-91.01Q391.56-906 479.78-906q89.22 0 166.83 33.45 77.6 33.46 135.01 90.81t90.89 134.87Q906-569.34 906-480q0 88.28-33.53 165.75t-91.01 135.28q-57.49 57.8-134.83 90.89Q569.28-55 480.14-55Zm-.14-94q138 0 234.5-96.37T811-480q0-138-96.5-234.5t-235-96.5q-137.5 0-234 96.5t-96.5 235q0 137.5 96.37 234T480-149Zm0-331Z"
        />
      </svg>

      <v-divider inset vertical :thickness="3"></v-divider>
      <h1 style="color: #37b24d">{{ balance.income }}</h1>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        height="48px"
        viewBox="0 -960 960 960"
        width="48px"
        fill="#37b24d"
        class="mt-1"
      >
        <path
          d="M447-274h72v-166h167v-72H519v-174h-72v174H274v72h173v166Zm33.4 219q-88.87 0-166.12-33.08-77.25-33.09-135.18-91.02-57.93-57.93-91.02-135.12Q55-391.41 55-480.36q0-88.96 33.08-166.29 33.09-77.32 90.86-134.81 57.77-57.48 135.03-91.01Q391.24-906 480.28-906t166.49 33.45q77.44 33.46 134.85 90.81t90.89 134.87Q906-569.34 906-480.27q0 89.01-33.53 166.25t-91.01 134.86q-57.49 57.62-134.83 90.89Q569.28-55 480.4-55Zm.1-94q137.5 0 234-96.37T811-480.5q0-137.5-96.31-234T479.5-811q-137.5 0-234 96.31T149-479.5q0 137.5 96.37 234T480.5-149Zm-.5-331Z"
        />
      </svg>
    </v-card-title>

    <v-card-text class="mt-4">
      <v-row>
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
          ></v-select></v-col
        ><v-col cols="12" md="2" sm="3"
          ><v-select
            label="مرتب سازی"
            density="compact"
            :items="sorts"
            variant="outlined"
            v-model="sort"
            item-title="title"
            item-value="key"
            bg-color="light-green-lighten-5"
          ></v-select></v-col
        ><v-spacer></v-spacer
        ><v-col cols="auto">
          <create-transaction
            :cards="cards.slice(1)"
            @reload-data="handleEmits"
            :isCreateMode="true"
          ></create-transaction></v-col
      ></v-row>
      <v-tabs v-model="tab" bg-color="teal-darken-3">
        <v-tab :value="0">همه</v-tab>
        <v-tab :value="1">درآمد</v-tab>
        <v-tab :value="2">هزینه</v-tab>
      </v-tabs>
      <v-table v-if="items.length > 0">
        <thead>
          <tr>
            <th class="text-right">دسته بندی</th>
            <th class="text-center">مبلغ</th>
            <th class="text-center">کارت</th>
            <th class="text-center">تاریخ</th>
            <th class="text-center">نوع</th>
            <th class="text-center"></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" :key="item.transactionId" class="table-item">
            <td class="text-right">{{ item.category.title }}</td>
            <td class="text-center">ت {{ item.amount }}</td>
            <td class="text-center" dir="ltr">
              {{ cardNumberFormat(item.creditCard.number) }}
            </td>
            <td class="text-center">{{ item.creationDate }}</td>
            <td class="text-center">
              {{ item.transactionTypeId == 1 ? 'درآمد' : 'هزینه' }}
            </td>
            <td class="text-center">
              <create-transaction
                :is-create-mode="false"
                @reload-data="handleEmits"
                :transaction-id="item.transactionId"
              ></create-transaction>
              <delete-transaction
                :transaction-id="item.transactionId"
                @reload-data="handleEmits"
              ></delete-transaction>
              <description :description="item.description"></description>
            </td>
          </tr>
        </tbody>
      </v-table>

      <not-found v-else></not-found>
    </v-card-text>
  </v-card>
</template>

<script setup>
import axios from '@/services/axios'
import { reactive, ref, watch } from 'vue'
import CreateTransaction from './transaction/Create.vue'
import DeleteTransaction from './transaction/Delete.vue'
import UseAuthenticationStore from '@/stores/UseAuthenticationStore'
import NotFound from '@/components/NotFound.vue'
import { onBeforeUnmount } from 'vue'
import Description from './transaction/Description.vue'
import cardNumberFormat from '@/Common/CardNumberFormat'
import UseTermStore from '@/stores/UseTermStore'
import UseLoadingStore from '@/stores/UseLoadingStore'

const authStore = UseAuthenticationStore()
const termStore = UseTermStore()
const loadingStore = UseLoadingStore()

const items = ref([])
const balance = reactive({ expense: 0, income: 0 })
const cards = ref([{ number: 'همه کارت ها', creditCardId: '' }])
const tab = ref(0)
const card = ref(cards.value[0])
const terms = termStore.terms
const sorts = ref([
  { title: 'تاریخ ایجاد', key: 'date' },
  { title: 'مبلغ', key: 'amount' },
])
const sort = ref(sorts.value[0])
const term = ref(terms[0])
let sequenceIndex = 0
const sequenceLength = 10
let pendingRequest = false

const initTransactions = async () => {
  loadingStore.loading = true
  pendingRequest = true
  await axios
    .get('api/transaction/list', {
      params: {
        transactionTypeId: tab.value,
        creditCardId: card.value,
        accountId: authStore.accountId,
        term: term.value,
        sort: sort.value,
        index: sequenceIndex,

        sequenceLength: sequenceLength,
      },
    })
    .then((response) => {
      Object.assign(balance, response.data.balance)
      items.value.push(...response.data.transactions)
      if (response.data.transactions.length) {
        sequenceIndex += response.data.transactions.length
      }
    })
    .finally(() => {
      loadingStore.loading = false
      pendingRequest = false
    })
}

const initCards = async () => {
  cards.value = []
  await axios
    .get('api/credit-card/list', {
      params: { accountId: authStore.accountId },
    })
    .then((response) => {
      cards.value = response.data
    })
    .finally(() => {
      cards.value.unshift({ number: 'همه کارت ها', creditCardId: '' })
    })
}

const resetTransactions = () => {
  sequenceIndex = 0
  items.value = []
}

const handleEmits = () => {
  resetTransactions()
  initTransactions()
  initCards()
}

const handleScrollEnd = () => {
  const { offsetHeight, scrollTop } = document.documentElement
  const { innerHeight } = window
  if (Math.round(scrollTop) + innerHeight === offsetHeight && !pendingRequest) {
    initTransactions()
    document.documentElement.scrollTop -= 10
  }
}

watch(tab, () => {
  resetTransactions()
  initTransactions()
})
watch(card, () => {
  resetTransactions()
  initTransactions()
})
watch(term, () => {
  resetTransactions()
  initTransactions()
})
watch(sort, () => {
  resetTransactions(), initTransactions()
})

window.addEventListener('scroll', handleScrollEnd)

onBeforeUnmount(() => {
  window.removeEventListener('scroll', handleScrollEnd)
})

initCards()
initTransactions()
</script>
