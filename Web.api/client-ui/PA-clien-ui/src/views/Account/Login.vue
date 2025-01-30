<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <v-card
    variant="elevated"
    class="mx-auto mt-12 pa-10"
    max-width="500"
    dir="rtl"
    color="light-green-lighten-5"
  >
    <v-card-title class="d-flex flex-row ga-2"
      ><svg
        xmlns="http://www.w3.org/2000/svg"
        height="32px"
        viewBox="0 -960 960 960"
        width="32px"
        fill="#2b8a3e"
      >
        <path
          d="M480.67-120v-66.67h292.66v-586.66H480.67V-840h292.66q27 0 46.84 19.83Q840-800.33 840-773.33v586.66q0 27-19.83 46.84Q800.33-120 773.33-120H480.67Zm-63.34-176.67-47-48 102-102H120v-66.66h351l-102-102 47-48 184 184-182.67 182.66Z"
        /></svg
      >ورود به حساب</v-card-title
    ><v-divider class="mb-6"></v-divider>
    <v-form fast-fail @submit.prevent="submit">
      <v-text-field
        variant="outlined"
        v-model="model.username"
        label="*نام کاربری"
        density="compact"
        bg-color="grey-lighten-5"
        :rules="[required, txtFieldMaxLength]"
      ></v-text-field>

      <v-text-field
        :append-inner-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
        :type="show ? 'text' : 'password'"
        variant="outlined"
        class="input-group--focused mb-5"
        label="*رمز عبور"
        name="password"
        v-model="model.password"
        density="compact"
        @click:append-inner="show = !show"
        :rules="[required]"
        bg-color="grey-lighten-5"
      ></v-text-field>
      <small>حساب کاربری ندارید؟ <router-link to="/account/create">ثبت نام</router-link></small>

      <v-btn class="mt-2" type="submit" elevation="0" block color="warning">تایید</v-btn>
    </v-form>
  </v-card>
</template>

<script setup>
import { reactive, ref } from 'vue'
import axios from '@/services/axios'
import { useRouter } from 'vue-router'
import UseAuthenticationStore from '@/stores/UseAuthenticationStore'
import UseLoadingStore from '@/stores/UseLoadingStore'
import { required, txtFieldMaxLength } from '@/Common/Validation'

const loadingStore = UseLoadingStore()
const router = useRouter()

const show = ref(false)

const authenticationStore = UseAuthenticationStore()

const model = reactive({
  username: '',
  password: '',
})

const submit = async () => {
  if (
    required(model.username) != true ||
    required(model.password) != true ||
    txtFieldMaxLength(model.username) != true
  ) {
    return
  }
  loadingStore.loading = true
  await axios
    .post('api/account/login', {
      ...model,
    })
    .then((response) => {
      localStorage.setItem('token', response.data.token)
      authenticationStore.accountId = response.data.account.accountId
      router.push({ name: 'home' })
    })
    .finally(() => {
      loadingStore.loading = false
    })
}
</script>
