import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Keep } from "@/models/Keep.js";

class KeepsService {
async getKeeps(){
  const response = await api.get('api/keeps')
  logger.log("GOT KEEPS ", response.data)
  AppState.keeps = response.data.map(pojo => new Keep(pojo))
  
}
async createKeep(keepData) {
  const response = await api.post('api/keeps', keepData)
  logger.log("CREATED KEEP", response.data)
  const keep = new Keep(response.data)
  AppState.keeps.unshift(keep)
}

async getKeepById(keepId) {
  AppState.activeKeep = null
  const response = await api.get(`api/keeps/${keepId}`)
  logger.log("GOT KEEP BY ID", response.data)
  const keep = new Keep(response.data)
  AppState.activeKeep = keep
}
  async deleteKeep(keepId) {
    const response = await api(`api/keeps/${keepId}`)
    logger.log("DELETED KEEP", response.data)
    const keepIndex = AppState.keeps.findIndex(keep => keep.id == keepId)
    AppState.keeps.splice(keepIndex, 1)
  }
  
}

export const keepsService = new KeepsService()