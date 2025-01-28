import { Vault } from '@/models/Vault.js'
import { AppState } from '../AppState.js'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'

class AccountService {
  async GetMyVault() {
    AppState.profileVaults = []
    const response = await api.get("account/vaults")
    logger.log("GOT MY VAULTS", response.data)
    AppState.profileVaults = response.data.map(pojo => new Vault(pojo))
  }
  async editAccount(accountData) {
    const response = await api.put('account', accountData)
    logger.log("EDITED ACCOUNT", response.data);
    AppState.account = new Account(response.data)
  }   async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
