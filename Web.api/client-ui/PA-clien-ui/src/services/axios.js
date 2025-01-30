import axios from 'axios'
import UseSnackbarStore from '@/stores/UseSnackbarStore'
import router from '../router'

const instance = axios.create({
  baseURL: 'https://localhost:7271/',
  headers: {
    post: {
      'Content-Type': 'application/x-www-form-urlencoded',
    },
  },
})

instance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token')
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  },
)
instance.interceptors.response.use(
  (response) => {
    if (response.config.method === 'post') {
      const snackbarStore = UseSnackbarStore()
      snackbarStore.visible = true
      snackbarStore.message = 'با موفقیت انجام شد'
      snackbarStore.color = 'success'
    }
    return response
  },
  (error) => {
    const response = error.response
    const snackbarStore = UseSnackbarStore()

    switch (response.status) {
      case 401:
        router.push({ name: 'login' })
        break
      case 400:
        snackbarStore.visible = true
        snackbarStore.message = response.data
        snackbarStore.color = 'error'
        break
      default:
        snackbarStore.visible = true
        snackbarStore.message =
          'متاسفانه اکنون امکان پاسخگویی به درخواست شما وجود ندارد. لطفا بعدا تلاش کنید'
        snackbarStore.color = 'warning'
    }
    return Promise.reject(error)
  },
)

export default instance
