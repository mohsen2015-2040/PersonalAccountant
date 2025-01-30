<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <v-overlay v-model="overlay" class="align-center justify-center">
    <HalfCircleSpinner :animation-duration="700" :size="props.size" color="#FFEA00"
  /></v-overlay>
</template>

<script setup>
import { HalfCircleSpinner } from 'epic-spinners'
import { ref } from 'vue'

const props = defineProps(['size'])
const overlay = ref(true)
</script>
