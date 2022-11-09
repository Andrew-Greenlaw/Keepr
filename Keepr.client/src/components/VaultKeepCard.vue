<template>
  <div class="keep-card shadow">
    <img class="photo img-fluid selectable" :src="keep.img" alt="keep image" data-bs-toggle="modal"
      data-bs-target="#keepModal" @click="GetKeepById(keep.id)" aria-label="Open Keep Detail">
    <div class="text">
      <div>
        <h3 class="text-shadow">{{ keep.name }}</h3>
      </div>
    </div>
    <div class="button-delete selectable bg-danger d-flex justify-content-center align-items-center"
      @click="RemoveKeepFromVault(keep.vaultKeepId)" v-if="account.id == vault.creatorId"
      aria-label="Remove Keep From Vault" title="Remove Keep"><i class="mdi mdi-window-close"></i>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { KeepInVault } from '../models/KeepInVault.js';
import { Vault } from '../models/Vault.js';
import { keepsService } from '../services/KeepsService.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';
import Pop from '../utils/Pop.js';

export default {
  props: {
    keep: {
      type: KeepInVault,
      required: true
    },
    vault: {
      type: Vault,
      required: true
    }
  },
  setup() {
    return {
      account: computed(() => AppState.account),
      async GetKeepById(id) {
        try {
          await keepsService.GetKeepById(id)
        } catch (error) {
          Pop.error("[GetByKeepId]", error)
        }
      },
      async RemoveKeepFromVault(id) {
        try {
          const yes = await Pop.confirm("Remove this From Vault?")
          if (!yes) { return }
          await vaultKeepsService.deleteVaultKeep(id)
        } catch (error) {
          Pop.error('[RemoveKeepFromVault]', error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.keep-card {
  margin-bottom: 1.5rem;
  position: relative;
  transition: transform .2s;
}

.keep-card:hover {
  transform: scale(1.05)
}

.button-delete {
  height: 1.2rem;
  width: 1.2rem;
  border-radius: 50%;
  position: absolute;
  top: 0;
  right: 0;
  transform: translate(.5rem, -.5rem);
  transition: transform .2s;
}

.button-delete:hover {
  transform: scale(1.3) translate(.5rem, -.5rem);
}

.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px rgb(83, 83, 83);
  font-weight: bold;
  letter-spacing: 0.08rem
}

.text {
  position: absolute;
  bottom: 1rem;
  left: .5rem;
  color: white;
  width: auto;
}
</style>