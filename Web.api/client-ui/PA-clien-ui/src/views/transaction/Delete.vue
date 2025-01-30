<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <v-dialog max-width="500" v-model="dialog">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        icon="mdi-trash-can-outline"
        size="small"
        color="error"
        v-bind="activatorProps"
        class="mr-1"
      ></v-btn>
    </template>

    <v-card title="آیا اطمینان دارید؟" text="آیا از حذف این تراکنش مطمئن هستید؟" dir="rtl">
      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn text="لغو" variant="text" @click.prevent="dialog = false"></v-btn>

        <v-btn
          text="حذف"
          variant="tonal"
          color="error"
          @click.prevent="deleteItem(props.transactionId)"
        ></v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import axios from '@/services/axios'
import { ref } from 'vue'
import UseLoadingStore from '@/stores/UseLoadingStore'

const loadingStore = UseLoadingStore()

const dialog = ref(false)

const props = defineProps(['transactionId'])
const emits = defineEmits(['reload-data'])

const deleteItem = async (id) => {
  loadingStore.loading = true
  await axios
    .post('api/transaction/delete', { transactionId: id })
    .then((response) => {
      if (response.status === 200) {
        dialog.value = false
        emits('reload-data')
      }
    })
    .finally(() => {
      loadingStore.loading = false
    })
}
</script>
