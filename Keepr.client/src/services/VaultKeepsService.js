import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async createVaultKeep(data) {
    console.log(data)
    await api.post(`api/vaultKeeps`, data)
    AppState.keep.kept++
  }
  async deleteVaultKeep(id) {
    const res = await api.delete(`api/vaultKeeps/${id}`)
    console.log(res.data)
    AppState.keepsInVault = AppState.keepsInVault.filter(k => k.vaultKeepId != id)
  }
}
export const vaultKeepsService = new VaultKeepsService()