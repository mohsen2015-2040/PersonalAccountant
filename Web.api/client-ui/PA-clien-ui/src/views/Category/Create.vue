<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <v-dialog max-width="300" v-model="modalStore.createCategoryVisible">
    <v-card dir="rtl">
      <v-card-title>
        + افزودن دسته {{ props.transactionTypeId == 1 ? 'درآمد' : 'هزینه' }}
      </v-card-title>
      <v-divider></v-divider>
      <v-card-text>
        <v-row>
          <v-col cols="12">
            <v-text-field
              label="*عنوان دسته"
              variant="outlined"
              v-model="model.title"
              bg-color="light-green-lighten-5"
              :rules="[required, txtFieldMaxLength]"
            ></v-text-field>
          </v-col>
        </v-row>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-btn
          text="لغو"
          variant="text"
          @click.prevent="modalStore.createCategoryVisible = false"
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
import { onBeforeUnmount, reactive } from 'vue'
import axios from '@/services/axios'
import AuthenticationStore from '@/stores/UseAuthenticationStore'
import { required, txtFieldMaxLength } from '@/Common/Validation'
import UseModalsStore from '@/stores/UseModalsStore'
import UseLoadingStore from '@/stores/UseLoadingStore'

const loadingStore = UseLoadingStore()

const modalStore = UseModalsStore()
const emits = defineEmits(['reload-data'])
const props = defineProps(['transactionTypeId'])

const model = reactive({
  title: '',
})

const submit = async () => {
  if (required(model.title) != true || txtFieldMaxLength(model.title) != true) {
    return
  }
  loadingStore.loading = true
  await axios
    .post('api/category/create', {
      accountId: AuthenticationStore().accountId,
      transactionTypeId: props.transactionTypeId,
      ...model,
    })
    .then(() => {
      modalStore.createCategoryVisible = false
      emits('reload-data')
    })
    .finally(() => {
      loadingStore.loading = false
    })
}

onBeforeUnmount(() => {
  modalStore.categoryCreateVisible = false
})
</script>
