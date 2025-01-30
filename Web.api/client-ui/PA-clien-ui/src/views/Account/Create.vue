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
          d="M726.67-400v-126.67H600v-66.66h126.67V-720h66.66v126.67H920v66.66H793.33V-400h-66.66ZM360-480.67q-66 0-109.67-43.66Q206.67-568 206.67-634t43.66-109.67Q294-787.33 360-787.33t109.67 43.66Q513.33-700 513.33-634t-43.66 109.67Q426-480.67 360-480.67ZM40-160v-100q0-34.67 17.5-63.17T106.67-366q70.66-32.33 130.89-46.5 60.22-14.17 122.33-14.17T482-412.5q60 14.17 130.67 46.5 31.66 15 49.5 43.17Q680-294.67 680-260v100H40Zm66.67-66.67h506.66V-260q0-14.33-7.83-27t-20.83-19q-65.34-31-116.34-42.5T360-360q-57.33 0-108.67 11.5Q200-337 134.67-306q-13 6.33-20.5 19t-7.5 27v33.33ZM360-547.33q37 0 61.83-24.84Q446.67-597 446.67-634t-24.84-61.83Q397-720.67 360-720.67t-61.83 24.84Q273.33-671 273.33-634t24.84 61.83Q323-547.33 360-547.33Zm0-86.67Zm0 407.33Z"
        /></svg
      >ثبت نام</v-card-title
    >
    <v-divider class="mb-6"></v-divider>
    <v-form fast-fail @submit.prevent="submit">
      <v-text-field
        variant="outlined"
        v-model="model.username"
        label="*نام کاربری"
        class="mb-2"
        density="compact"
        bg-color="grey-lighten-5"
        :rules="[required, txtFieldMaxLength]"
      ></v-text-field>

      <v-text-field
        :append-inner-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
        :type="show1 ? 'text' : 'password'"
        variant="outlined"
        class="input-group--focused mb-2"
        label="*رمز عبور"
        name="password"
        v-model="model.password"
        density="compact"
        @click:append-inner="show1 = !show1"
        bg-color="grey-lighten-5"
        :rules="[required, passwordLength, txtFieldMaxLength]"
      ></v-text-field>
      <v-text-field
        :append-inner-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
        :type="show2 ? 'text' : 'password'"
        variant="outlined"
        class="input-group--focused mb-5"
        label="*تکرار رمز عبور"
        name="confirmPassword"
        v-model="model.confirmPassword"
        density="compact"
        @click:append-inner="show2 = !show2"
        bg-color="grey-lighten-5"
        :rules="[confirmPassword(model.confirmPassword, model.password)]"
      ></v-text-field>

      <small>حساب کاربری دارید؟ <router-link to="/account/login">ورود به حساب</router-link></small>
      <v-btn class="mt-2" type="submit" block color="warning">تایید</v-btn>
    </v-form>
  </v-card>
</template>

<script setup>
import { reactive, ref } from 'vue'
import axios from '@/services/axios'
import { useRouter } from 'vue-router'
import UseAuthenticationStore from '@/stores/UseAuthenticationStore'
import { confirmPassword, required, passwordLength, txtFieldMaxLength } from '@/Common/Validation'
import UseLoadingStore from '@/stores/UseLoadingStore'

const loadingStore = UseLoadingStore()

const show1 = ref(false)
const show2 = ref(false)
const router = useRouter()
const authenticationStore = UseAuthenticationStore()

const model = reactive({
  username: '',
  password: '',
  confirmPassword: '',
})

const submit = async () => {
  if (
    required(model.username) != true ||
    required(model.password) != true ||
    passwordLength(model.password) != true ||
    confirmPassword(model.confirmPassword, model.password) != true ||
    txtFieldMaxLength(model.username) != true ||
    txtFieldMaxLength(model.password) != true
  ) {
    return
  }
  loadingStore.loading = true
  await axios
    .post('api/account/create', {
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
