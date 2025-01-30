import UseAuthenticationStore from '@/stores/UseAuthenticationStore'
import axios from '@/services/axios'

export default function checkAuth(next) {
  const store = UseAuthenticationStore()

  if (store.accountId == null) {
    axios.get('api/account/check-access').then((response) => {
      store.accountId = response.data
      next()
    })
  } else {
    next()
  }
}
