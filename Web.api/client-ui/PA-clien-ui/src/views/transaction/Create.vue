<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <v-dialog max-width="500" v-model="dialog">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        v-if="props.isCreateMode"
        v-bind="activatorProps"
        color="warning"
        text="تراکنش +"
        variant="flat"
      ></v-btn>
      <v-btn v-else icon="mdi-pencil" size="small" color="success" v-bind="activatorProps"></v-btn>
    </template>

    <v-card
      prepend-icon="mdi-paper-roll-outline"
      :title="`${props.isCreateMode ? 'افزودن' : 'ویرایش'} تراکنش`"
      dir="rtl"
    >
      <v-divider></v-divider>
      <v-card-text>
        <v-row dense>
          <v-col cols="12" md="12" sm="12">
            <v-number-input
              :reverse="false"
              controlVariant="default"
              label="*مبلغ"
              :hideInput="false"
              :inset="false"
              variant="outlined"
              v-model="model.amount"
              min="100"
              bg-color="light-green-lighten-5"
            ></v-number-input>
          </v-col>

          <v-col cols="12" md="6" sm="6">
            <v-select
              label="*دسته بندی"
              density="compact"
              :items="categories"
              variant="outlined"
              v-model="model.category"
              item-title="title"
              item-value="categoryId"
              bg-color="light-green-lighten-5"
              dir="rtl"
              :rules="[required]"
            ></v-select>
          </v-col>
          <v-col cols="12" md="6" sm="6">
            <v-select
              label="*کارت"
              :items="cards"
              density="compact"
              variant="outlined"
              v-model="model.creditCard"
              item-title="number"
              item-value="creditCardId"
              bg-color="light-green-lighten-5"
              :rules="[required]"
            ></v-select>
          </v-col>
          <v-col cols="6">
            <v-btn-toggle
              v-model="model.transactionTypeId"
              color="light-green-darken-2"
              rounded="1"
              group
              mandatory
              density="default"
              border="md"
            >
              <v-btn :value="2">هزینه</v-btn>
              <v-btn :value="1"> درآمد </v-btn>
            </v-btn-toggle>
          </v-col>
          <v-col cols="6"
            ><date-picker v-model="model.creationDate" :rules="[required]"></date-picker
          ></v-col>
          <v-col cols="12" class="mt-5">
            <v-text-field
              label="توضیحات"
              bg-color="light-green-lighten-5"
              variant="outlined"
              :rules="[txtFieldMaxLength]"
              dir="rtl"
              v-model="model.description"
            ></v-text-field>
          </v-col>
        </v-row>
        <create-category
          @reload-data="initCategories"
          :transactionTypeId="model.transactionTypeId"
        />
        <createcard @reload-data="initCards" />
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn text="لغو" variant="text" @click.prevent="dialog = false"></v-btn>

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
import axios from '@/services/axios'
import { reactive, ref, watch } from 'vue'
import UseModalsStore from '@/stores/UseModalsStore'
import UseAuthenticationStore from '@/stores/UseAuthenticationStore'
import createCategory from '@/views/Category/Create.vue'
import createcard from '@/views/CreditCard/Create.vue'
import { required } from '@/Common/Validation'
import { txtFieldMaxLength } from '@/Common/Validation'
import DatePicker from '@/components/DatePicker.vue'
import UseLoadingStore from '@/stores/UseLoadingStore'

const modalsStore = UseModalsStore()
const authStore = UseAuthenticationStore()
const loadingStore = UseLoadingStore()

const dialog = ref(false)
const categories = ref([])
const cards = ref([])
const props = defineProps(['isCreateMode', 'transactionId'])
const emits = defineEmits(['reload-data'])

const model = reactive({
  transactionTypeId: 2,
  amount: 100,
  creationDate: null,
  description: '',
  category: null,
  creditCard: null,
})

const initCategories = async () => {
  await axios
    .get('api/category/list', {
      params: { accountId: authStore.accountId, transactionTypeId: model.transactionTypeId },
    })
    .then((response) => {
      categories.value = response.data
    })
    .finally(() => {
      categories.value.push({ categoryId: 'add', title: '+ افزودن دسته' })
    })
}
const initCards = async () => {
  await axios
    .get('api/credit-card/list', {
      params: { accountId: authStore.accountId },
    })
    .then((response) => {
      cards.value = response.data
    })
    .finally(() => {
      cards.value.push({ creditCardId: 'add', number: '+ افزودن کارت' })
    })
}

const initModel = async (id) => {
  if (id == null) {
    return
  }
  loadingStore.loading = true
  await axios
    .get('api/transaction/edit', {
      params: { transactionId: id },
    })
    .then((response) => {
      Object.assign(model, response.data)
    })
    .finally(() => {
      loadingStore.loading = false
    })
}

const submit = async () => {
  console.log(model.description)
  if (
    required(model.category) != true ||
    required(model.creationDate) != true ||
    required(model.creditCard) != true ||
    txtFieldMaxLength(model.description) != true
  ) {
    console.log('not valid')
    return
  }
  const { category, creditCard, ...restModel } = model
  loadingStore.loading = true
  if (props.transactionId != null) {
    await axios
      .post('api/transaction/edit', {
        transactionId: props.transactionId,
        creditCardId: creditCard,
        categoryId: category,
        ...restModel,
      })
      .then(() => {
        dialog.value = false
        emits('reload-data')
      })
      .finally(() => {
        loadingStore.loading = false
      })
  } else {
    await axios
      .post('api/transaction/create', {
        creditCardId: creditCard,
        categoryId: category,
        ...restModel,
      })
      .then(() => {
        dialog.value = false
        emits('reload-data')
      })
      .finally(() => {
        loadingStore.loading = false
      })
  }
}

watch(
  () => model.category,
  (newVal) => {
    if (newVal == 'add') {
      console.log(newVal)
      modalsStore.createCategoryVisible = true
    }
  },
  { deep: true },
)

watch(
  () => model.creditCard,
  (newVal) => {
    if (newVal == 'add') {
      modalsStore.createCardVisible = true
    }
  },
  { deep: true },
)

watch(
  () => modalsStore.createCategoryVisible,
  () => {
    model.category = null
  },
)
watch(
  () => modalsStore.createCardVisible,
  () => {
    model.creditCard = null
  },
)

watch(
  () => model.transactionTypeId,
  () => {
    model.category = null
    initCategories()
  },
  { deep: true },
)
watch(dialog, async () => {
  if (!dialog.value) {
    model.transactionTypeId = 2
    model.amount = 100
    model.description = ''
    model.creationDate = null
    model.category = null
    model.creditCard = null
  } else {
    initCategories()
    initCards()
    initModel(props.transactionId)
  }
})
</script>
