<template>
  <div class="container-fluid vault-page">
    <div class="row flex-column align-items-center justify-content-center">
      <div
        class="col-3 cover-img rounded shadow d-flex flex-column justify-content-end align-items-center mt-5 text-light"
        :style="{ backgroundImage: `url(${vault.img})` }" title="vault image">
        <h1>{{ vault.name }}</h1>
        <h4>By: {{ vault.creator?.name }}</h4>
      </div>
      <div class="edit-button col-md-3 text-end" :style="account.id == vault.creatorId ? '' : 'visibility: hidden'">
        <div class="dropdown ">
          <button class="btn dropdown-toggle lighten-30 text-uppercase selectable rounded" type="button"
            data-bs-toggle="dropdown" aria-expanded="false">
            ...
          </button>
          <ul class="dropdown-menu">
            <li>
              <div class="dropdown-item" data-bs-toggle="modal" data-bs-target="#editVaultModal">Edit Vault</div>
            </li>
            <li>
              <div class="dropdown-item" @click="deleteVault(vault.id)">Delete</div>
            </li>
          </ul>
        </div>
      </div>
      <div class="col-6 d-flex flex-column align-items-center justify-content-center mb-4">
        <div>
          <h3>{{ keepsInVault.length }} Keeps</h3>
        </div>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="masonry-with-columns">
          <VaultKeepCard v-for="k in keepsInVault" :key="k.id" :keep="k" :vault="vault" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import Pop from '../utils/Pop.js'
import { vaultsService } from '../services/VaultsService.js'
import { useRoute, useRouter } from 'vue-router'
import { onMounted, watchEffect } from 'vue'
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import { router } from '../router.js'

export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    async function GetVaultById() {
      try {
        await vaultsService.GetVaultById(route.params.id)
      } catch (error) {
        Pop.error('[GetVaultById]', error)
      }
    }
    async function GetVaultKeeps() {
      try {
        await vaultsService.GetKeepsInVault(route.params.id)
      } catch (error) {
        Pop.error('[GetVaultKeeps]', error)
      }
    }
    onMounted(() => {
      GetVaultById()
      GetVaultKeeps()
    })

    return {
      vault: computed(() => AppState.vault),
      keepsInVault: computed(() => AppState.keepsInVault),
      account: computed(() => AppState.account),
      async deleteVault(id) {
        try {
          const yes = await Pop.confirm("are you sure you want to delete this vault?")
          if (!yes) { return }
          await vaultsService.deleteVault(id)
          router.push({ name: 'Account' })
        } catch (error) {
          Pop.error('[DeleteVault]', error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.cover-img {
  height: 15rem;
  background-position: center;
  background-size: cover;
}

.masonry-with-columns {
  columns: 4;
  column-gap: 2rem;
}

@media screen and (max-width: 600px) {
  .masonry-with-columns {
    columns: 2;
    column-gap: 2rem;
  }
}
</style>