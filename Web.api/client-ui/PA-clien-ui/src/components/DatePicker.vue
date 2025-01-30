<template>
  <div>
    <Vue3PersianDatetimePicker
      :displayFormat="displayFormat"
      :format="format"
      :auto-submit="true"
      color="#01579B"
      :locale="locale"
      @close="showDatePickerPopup = false"
      :editable="true"
      :show="showDatePickerPopup"
      :type="type"
      :element="elementName"
      v-bind="$attrs"
      v-model="value"
      append-to="body"
    />

    <v-text-field
      :id="elementName"
      maxlength="10"
      density="comfortable"
      variant="outlined"
      @click="showDatePickerPopup = true"
      @click:prepend-inner="showDatePickerPopup = true"
      prepend-inner-icon="mdi-calendar-multiple"
      hide-details="auto"
      v-model="value"
      v-bind="$attrs"
    />
  </div>
</template>

<script setup>
import Vue3PersianDatetimePicker from 'vue3-persian-datetime-picker'
import { ref, computed } from 'vue'

const props = defineProps({
  modelValue: String,
  type: String,
  locale: {
    type: String,
    default: 'fa',
  },
})

const elementName = Date.now().toString()

const showDatePickerPopup = ref(false)

const emit = defineEmits(['update:modelValue'])

const value = computed({
  get() {
    return props.modelValue
  },
  set(value) {
    emit('update:modelValue', value)
  },
})

const format = computed(() => {
  return props.type === 'datetime'
    ? 'jYYYY/jMM/jDD HH:mm'
    : props.type === 'time'
      ? 'HH:mm'
      : 'jYYYY/jMM/jDD'
})
const displayFormat = computed(() => {
  return props.type === 'datetime'
    ? 'dddd jDD jMMMM jYYYY HH:mm'
    : props.type === 'time'
      ? 'HH:mm'
      : 'dddd jDD jMMMM jYYYY'
})
</script>
