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

    <v-card dir="rtl">
      <v-card-title>آیا اطمینان دارید؟</v-card-title>
      <v-divider></v-divider>
      <v-card-text
        ><p>با حذف این کارت تراکنش های مربوط به آن نیز حذف خواهد شد.</p>
        <p>آیا اطمینان دارید؟</p></v-card-text
      >
      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn text="لغو" variant="text" @click.prevent="dialog = false"></v-btn>

        <v-btn
          text="حذف"
          variant="tonal"
          color="error"
          @click.prevent="deleteItem(props.creditCardId)"
        ></v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { ref } from 'vue'
import axios from '@/services/axios'
import UseLoadingStore from '@/stores/UseLoadingStore'

const loadingStore = UseLoadingStore()

const dialog = ref(false)
const props = defineProps(['creditCardId'])
const emits = defineEmits(['reload-data'])

const deleteItem = async (id) => {
  loadingStore.loading = true
  await axios
    .post('api/credit-card/delete', { creditCardId: id })
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
