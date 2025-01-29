import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"
import { VaultKeep } from "@/models/VaultKeep.js"

class VaultsService{
  async deleteVault(vaultId) {
    const response = await api.delete(`api/vaults/${vaultId}`)
    logger.log("DELETED VAULT", response.data)
  }
  async getVaultKeeps(vaultId) {
    AppState.vaultKeeps = []
    const response = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log("GOT VAULTKEEPS", response.data)
    AppState.vaultKeeps = response.data.map(pojo =>new VaultKeep(pojo))
  }
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
      AppState.activeVault = null
      const response = await api.get(`api/vaults/${vaultId}`)
      logger.log("GOT VAULT BY ID", response.data)
      const vault = new Vault(response.data)
      AppState.activeVault = vault
    }

}

export const vaultsService = new VaultsService()