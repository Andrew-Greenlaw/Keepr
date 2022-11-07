import { AppState } from '../AppState'
import { Keep } from '../models/Keep.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
class KeepsService {
  async GetAllKeeps() {
    const res = await api.get("api/keeps")
    // console.log(res.data)
    AppState.keeps = res.data.map(k => new Keep(k))
    // console.log(AppState.keeps)
  }
  async GetKeepById(id) {
    AppState.keep = {}
    const res = await api.get("api/keeps/" + id)
    // console.log(res.data)
    AppState.keep = new Keep(res.data)
    // console.log("active keep", AppState.keep)
  }
}
export const keepsService = new KeepsService()