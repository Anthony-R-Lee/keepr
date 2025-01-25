import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getKeeps(){
    const response = await api.get('api/keeps')
    logger.log("GOT KEEPS ", response.data)
  }
}

export const keepsService = new KeepsService()