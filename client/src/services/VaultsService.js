import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"

class VaultsService{
  async getVaults() {
      AppState.vaults = []
      const response = await api.get('api/vaults')
      logger.log("GOT VAULTS ", response.data)
      AppState.vaults = response.data.map(pojo => new Vault(pojo))
  }
  async createVault(vaultData) {
    const response = await api.post('api/vaults', vaultData)
      logger.log("CREATED VAULT", response.data)
      const keep = new Vault(response.data)
      AppState.vaults.unshift(keep)
    }

    async getVaultById(vaultId) {
      const response = await api.get(`api/vaults/${vaultId}`)
      logger.log("GOT VAULT BY ID", response.data)
    }

}

export const vaultsService = new VaultsService()