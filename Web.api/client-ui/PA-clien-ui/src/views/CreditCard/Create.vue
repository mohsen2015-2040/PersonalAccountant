<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <v-dialog max-width="300" v-model="modalsStore.createCardVisible">
    <v-card prepend-icon="mdi-credit-card-plus-outline" title="افزودن کارت" dir="rtl">
      <v-divider></v-divider>
      <v-card-text>
        <v-row>
          <v-col cols="12" md="12" sm="12">
            <v-text-field
              label="*شماره کارت"
              variant="outlined"
              v-model="model.number"
              bg-color="light-green-lighten-5"
              :rules="[required, cardNumberLength]"
            ></v-text-field>
          </v-col>
        </v-row>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-btn
          text="لغو"
          variant="text"
          @click.prevent="modalsStore.createCardVisible = false"
        ></v-btn>
        <v-btn
          text="ذخیره"
          variant="tonal"
          @click.prevent="submit"
          style="background-color: #dcedc8"
        ></v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { reactive, watch } from 'vue'
import axios from '@/services/axios'
import AuthenticationStore from '@/stores/UseAuthenticationStore'
import { cardNumberLength, required } from '@/Common/Validation'
import UseLoadingStore from '@/stores/UseLoadingStore'
import UseModalsStore from '@/stores/UseModalsStore'

const loadingStore = UseLoadingStore()
const modalsStore = UseModalsStore()

const emits = defineEmits('reload-data')

const model = reactive({
  number: '',
})

const submit = async () => {
  if (required(model.number) != true || cardNumberLength(model.number) != true) {
    return
  }
  loadingStore.loading = true
  await axios
    .post('api/credit-card/create', {
      accountId: AuthenticationStore().accountId,
      ...model,
    })
    .then(() => {
      modalsStore.createCardVisible = false
      emits('reload-data')
    })
    .finally(() => {
      loadingStore.loading = false
    })
}

watch(
  () => modalsStore.createCardVisible,
  (value) => {
    if (!value) {
      model.number = ''
    }
  },
)
</script>
