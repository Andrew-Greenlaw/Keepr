import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Keep } from '../models/Keep.js'
import { Vault } from '../models/Vault.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
      console.log(AppState.account)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async GetVaults() {
    const res = await api.get('/account/vaults')
    AppState.vaults = res.data.map(v => new Vault(v))
    // console.log(res.data)
    // console.log(AppState.vaults)
  }
  async GetKeeps() {
    const account = await AppState.account
    const res = await api.get(`/api/profiles/${account.id}/keeps`)
    console.log(res.data)
    AppState.profileKeeps = res.data.map(k => new Keep(k))
    console.log('My Keeps', AppState.profileKeeps)
  }
}

export const accountService = new AccountService()
