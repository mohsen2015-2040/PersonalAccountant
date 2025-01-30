<template>
  <v-card dir="rtl" class="ma-5">
    <v-card-title class="d-flex flex-row ga-4">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        height="64px"
        viewBox="0 -960 960 960"
        width="64px"
        fill="#2b8a3e"
      >
        <path
          d="M880-740v520q0 24-18 42t-42 18H140q-24 0-42-18t-18-42v-520q0-24 18-42t42-18h680q24 0 42 18t18 42ZM140-631h680v-109H140v109Zm0 129v282h680v-282H140Zm0 282v-520 520Z"
        />
      </svg>
      <h1 style="color: #424242">مدیریت کارت ها</h1></v-card-title
    >
    <v-row
      ><v-col cols="12" sm="12" md="6"><v-divider></v-divider></v-col
    ></v-row>

    <v-card-text class="mt-4">
      <v-row>
        <v-col cols="auto"
          ><v-btn
            @click.prevent="modalsStore.createCardVisible = true"
            color="warning"
            text="کارت جدید +"
            variant="flat"
          ></v-btn>
          <createCard @reload-data="initItems"></createCard></v-col
      ></v-row>

      <v-row justify="center">
        <v-col cols="9">
          <v-table v-if="items.length > 0">
            <thead>
              <tr>
                <th class="text-center">شماره کارت</th>
                <th class="text-left"></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in items" :key="item.transactionId" class="table-item">
                <td class="text-center" dir="ltr">{{ cardNumberFormat(item.number) }}</td>

                <td class="text-left">
                  <deleteCard
                    :creditCardId="item.creditCardId"
                    @reload-data="initItems"
                  ></deleteCard>
                </td>
              </tr>
            </tbody>
          </v-table>
          <not-found v-else></not-found></v-col
      ></v-row> </v-card-text
  ></v-card>
</template>

<script setup>
import createCard from '@/views/CreditCard/Create.vue'
import deleteCard from '@/views/CreditCard/Delete.vue'
import axios from '@/services/axios'
import { ref } from 'vue'
import NotFound from '@/components/NotFound.vue'
import UseLoadingStore from '@/stores/UseLoadingStore'
import UseModalsStore from '@/stores/UseModalsStore'
import cardNumberFormat from '@/Common/CardNumberFormat'
import UseAuthenticationStore from '@/stores/UseAuthenticationStore'

const loadingStore = UseLoadingStore()
const modalsStore = UseModalsStore()
const authStore = UseAuthenticationStore()

const items = ref([])

const initItems = async () => {
  loadingStore.loading = true
  await axios
    .get('api/credit-card/list', {
      params: { accountId: authStore.accountId },
    })
    .then((response) => {
      items.value = response.data
    })
    .finally(() => {
      loadingStore.loading = false
    })
}

initItems()
</script>
