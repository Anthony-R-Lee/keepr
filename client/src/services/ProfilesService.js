import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { Profile } from "@/models/Profile.js"
import { Vault } from "@/models/Vault.js"
import { Keep } from "@/models/Keep.js"

class ProfilesService{
  async getProfileById(profileId) {
    const response = await api.get(`api/profiles/${profileId}`)
    logger.log('GOT PROFILE BY ID', response.data)
    const profile = new Profile(response.data)
    AppState.activeProfile = profile
  }
  async GetUsersVault(profileId) {
    AppState.profileVaults = []
    const response = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log("GOT USER VAULTS", response.data)
    AppState.profileVaults = response.data.map(pojo => new Vault(pojo))
  }
  
  async GetUsersKeep(profileId) {
    AppState.profileKeeps = []
    const response = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log("GOT USER KEEPS", response.data)
      AppState.profileKeeps = response.data.map(pojo => new Keep(pojo) )
  }
}

export const profilesService = new ProfilesService()