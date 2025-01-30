<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <v-dialog v-model="dialog" width="auto">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        icon="mdi-text-box-outline"
        size="small"
        color="info"
        v-bind="activatorProps"
        class="mr-1"
        :disabled="props.description == null"
      ></v-btn>
    </template>

    <v-card max-width="400" dir="rtl" prepend-icon="mdi-text-box-outline" title="توضیحات">
      <v-divider></v-divider>
      <v-card-text>
        {{ props.description }}
      </v-card-text>
      <v-card-actions>
        <v-btn class="ms-auto" text="باشه" @click.prevent="dialog = false"></v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { ref } from 'vue'

const dialog = ref(false)
const props = defineProps(['description'])
</script>
