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
          d="M280-240q-100 0-170-70T40-480q0-100 70-170t170-70q74 0 128 36t82 95.33h430v217.34h-97.33V-240h-204v-131.33H490Q462-312 408-276t-128 36Zm0-66.67q69.33 0 113-44.83t51.67-86.5H688v131.33h68V-438h97.33v-84H444.67q-8-41.67-51.67-86.5t-113-44.83q-72 0-122.67 50.66Q106.67-552 106.67-480t50.66 122.67Q208-306.67 280-306.67ZM280-408q30.33 0 51.17-20.83Q352-449.67 352-480q0-30.33-20.83-51.17Q310.33-552 280-552q-30.33 0-51.17 20.83Q208-510.33 208-480q0 30.33 20.83 51.17Q249.67-408 280-408Zm0-72Z"
        /></svg
      >تغییر رمز عبور</v-card-title
    ><v-divider class="mb-6"></v-divider>
    <v-form fast-fail @submit.prevent="submit">
      <v-text-field
        :append-inner-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
        :type="show1 ? 'text' : 'password'"
        variant="outlined"
        class="input-group--focused mb-2"
        label="*رمز عبور فعلی"
        v-model="model.password"
        density="compact"
        @click:append-inner="show1 = !show1"
        bg-color="grey-lighten-5"
        :rules="[required]"
      ></v-text-field>

      <v-text-field
        :append-inner-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
        :type="show2 ? 'text' : 'password'"
        variant="outlined"
        class="input-group--focused mb-2"
        label="*رمز عبور جدید"
        v-model="model.newPassword"
        density="compact"
        @click:append-inner="show2 = !show2"
        bg-color="grey-lighten-5"
        :rules="[passwordLength, txtFieldMaxLength]"
      ></v-text-field>
      <v-text-field
        :append-inner-icon="show3 ? 'mdi-eye' : 'mdi-eye-off'"
        :type="show3 ? 'text' : 'password'"
        variant="outlined"
        class="input-group--focused mb-5"
        label="*تکرار رمز عبور جدید"
        v-model="model.newConfirmPassword"
        density="compact"
        @click:append-inner="show3 = !show3"
        bg-color="grey-lighten-5"
        :rules="[confirmPassword(model.newConfirmPassword, model.newPassword)]"
      ></v-text-field>
      <v-btn class="mt-2" type="submit" block color="warning">تایید</v-btn>
    </v-form>
  </v-card>
</template>

<script setup>
import { reactive } from 'vue'
import axios from '@/services/axios'
import UseAuthenticationStore from '@/stores/UseAuthenticationStore'
import { useRouter } from 'vue-router'
import { confirmPassword, required, passwordLength, txtFieldMaxLength } from '@/Common/Validation'
import UseLoadingStore from '@/stores/UseLoadingStore'
import { ref } from 'vue'

const authStore = UseAuthenticationStore()
const loadingStore = UseLoadingStore()
const router = useRouter()

const model = reactive({
  password: '',
  newPassword: '',
  newConfirmPassword: '',
})
const show1 = ref(false)
const show2 = ref(false)
const show3 = ref(false)

const submit = async () => {
  if (
    confirmPassword(model.newConfirmPassword, model.newPassword) != true ||
    required(model.password) != true ||
    passwordLength(model.newPassword) != true ||
    txtFieldMaxLength(model.newPassword) != true
  ) {
    return
  }
  loadingStore.loading = true
  axios
    .post('api/account/change-password', {
      accountId: authStore.accountId,
      ...model,
    })
    .then(() => {
      router.push({ name: 'home' })
    })
    .finally(() => {
      loadingStore.loading = false
    })
}
</script>
