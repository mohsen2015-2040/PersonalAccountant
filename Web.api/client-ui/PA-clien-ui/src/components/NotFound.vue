<template>
  <div>
    <v-img src="/no-result-search-icon.png" :width="width" class="ma-auto"></v-img>
  </div>
</template>

<script setup>
const props = defineProps(['width'])

const width = props.width != null ? props.width : 500
</script>
