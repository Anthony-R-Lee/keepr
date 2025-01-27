import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"

class VaultsService{
  async createVault(vaultData) {
      const response = await api.post('api/vaults', vaultData)
      logger.log("CREATED VAULT", response.data)
      const keep = new Vault(response.data)
      AppState.vaults.unshift(keep)
  }

}

export const vaultsService = new VaultsService()