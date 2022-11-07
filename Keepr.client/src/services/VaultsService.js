import { AppState } from "../AppState.js"
import { KeepInVault } from "../models/KeepInVault.js"
import { Vault } from "../models/Vault.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { api } from "./AxiosService.js"

class VaultsService {
  async GetVaultById(id) {
    const res = await api.get(`api/vaults/${id}`)
    AppState.vault = new Vault(res.data)
    console.log(res.data)
    console.log('ActiveVault', AppState.vault)
  }
  async GetKeepsInVault(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    console.log(res.data)
    AppState.vaultKeeps = res.data.map(k => new KeepInVault(k))
    console.log('VaultKeeps', AppState.vaultKeeps)
  }
}
export const vaultsService = new VaultsService()