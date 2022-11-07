import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Profile } from "../models/Profile.js"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"

class ProfilesService {
  async GetProfile(id) {
    const res = await api.get("api/profiles/" + id)
    // console.log(res.data)
    AppState.profile = new Profile(res.data)
    // console.log("the Profile", AppState.profile)
  }
  async GetProfileKeeps(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    // console.log('profilekeeps data', res.data)
    AppState.profileKeeps = res.data.map(k => new Keep(k))
    // console.log('profile keeps', AppState.profileKeeps)
  }
  async GetProfileVault(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    // console.log('profile vaults data', res.data)
    AppState.profileVaults = res.data.map(k => new Vault(k))
    // console.log("profile vaults", AppState.profileVaults)
  }
}
export const profilesService = new ProfilesService()