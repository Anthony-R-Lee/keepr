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
}

export const keepsService = new KeepsService()