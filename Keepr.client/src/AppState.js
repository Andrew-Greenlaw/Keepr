import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
  /** @type {import('./models/Keep.js').Keep} */
  keep: {},
  /** @type {import('./models/Vault.js').Vault[]} */
  vaults: [],
  /** @type {import('./models/Profile.js').Profile} */
  profile: {},
  /** @type {import('./models/Keep.js').Keep[]} */
  profileKeeps: [],
  /** @type {import('./models/Vault.js').Vault[]} */
  profileVaults: [],
  /** @type {import('./models/vault.js').vault} */
  vault: {},
  /** @type {import('./models/KeepInVault.js').KeepInVault[]} */
  keepsInVault: [],
  /** @type {import('./models/VaultKeep.js').VaultKeep[]} */
  vaultKeeps: []
})
