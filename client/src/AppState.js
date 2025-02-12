import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,
  /** @type {import('./models/Keep.js').Keep[]} user info from the database*/
  keeps: [],
  /** @type {import('./models/Keep.js').Keep} user info from the database*/
  activeKeep: null,
  /** @type {import('./models/Vault.js').Vault[]} user info from the database*/
  vaults: [],
  /** @type {import('./models/Vault.js').Vault[]} user info from the database*/
  profileVaults: [],
  /** @type {import('./models/Vault.js').Vault[]} user info from the database*/
  myVaults: [],
  /** @type {import('./models/Keep.js').Keep[]} user info from the database*/
  profileKeeps: [],
  /** @type {import('./models/Vault.js').Vault} user info from the database*/
  activeVault: null,
  /** @type {import('./models/Profile.js').Profile} user info from the database*/
  activeProfile: null,
  /** @type {import('./models/VaultKeep.js').VaultKeep} user info from the database*/
  vaultKeep: null,
  /** @type {import('./models/VaultKeep.js').VaultKeep[]} user info from the database*/
  vaultKeeps: [null],
  /** @type {import('./models/VaultKept.js').VaultKept[]} user info from the database*/
  vaultKepts: [null],
})

