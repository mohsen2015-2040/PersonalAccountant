import './assets/base.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { VNumberInput, VDateInput } from 'vuetify/lib/labs/components.mjs'

const vuetify = createVuetify({
  components: {
    ...components,
    VDateInput,
    VNumberInput,
  },
  directives,
})

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use()
app.use(vuetify)

app.mount('#app')
