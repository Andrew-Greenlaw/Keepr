<template>
  <div class="container-fluid vault-page">
    <div class="row flex-column align-items-center justify-content-center">
      <div
        class="col-lg-4 col-md-6 col-11 cover-img rounded shadow d-flex flex-column justify-content-end align-items-center mt-5 text-light"
        :style="{ backgroundImage: `url(${vault.img})` }" title="vault image">
        <h1 class="text-shadow">{{ vault.name }}</h1>
        <h4 class="text-shadow">By: {{ vault.creator?.name }}</h4>
      </div>
      <div class="edit-button col-md-3 text-end" :style="account.id == vault.creatorId ? '' : 'visibility: hidden'">
        <div class="dropdown">
          <button class="btn dropdown-toggle lighten-30 text-uppercase selectable rounded" type="button"
            data-bs-toggle="dropdown" aria-expanded="false" title="Options">
            ...
          </button>
          <ul class="dropdown-menu">
            <li>
              <div class="dropdown-item selectable" @click="deleteVault(vault.id)" aria-label="Delete Vault">Delete
              </div>
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
          <VaultKeepCard v-for="k in keepsInVault" :key="k.vaultKeepId" :keep="k" :vault="vault" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import Pop from '../utils/Pop.js'
import { vaultsService } from '../services/VaultsService.js'
import { useRoute, useRouter } from 'vue-router'
import { onMounted, onUnmounted, watchEffect } from 'vue'
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import { onAuthLoaded } from '@bcwdev/auth0provider-client'
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    async function GetVaultById() {
      try {
        await vaultsService.GetVaultById(route.params.id)
      } catch (error) {
        router.push({ name: 'Home' })
        // Pop.error('[GetVaultById]', error)
        Pop.toast('this is private or doesnt exist!')

      }
    }
    async function GetVaultKeeps() {
      try {
        await vaultsService.GetKeepsInVault(route.params.id)
      } catch (error) {
        router.push({ name: 'Home' })
        // Pop.error('[GetVaultKeeps]', error)
        Pop.toast('this is private or doesnt exist!')

      }
    }
    onAuthLoaded(() => {
      GetVaultById()
      GetVaultKeeps()
    })

    onMounted(() => {
    })

    // watchEffect(() => {
    //   if (AppState.vault?.isPrivate && AppState.account?.id == AppState.vault?.creatorId) {
    //     router.push({ name: 'Home' })
    // Pop.toast('this is private or doesnt exist!')
    //     AppState.vault = null
    //   }
    // })

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

.keep-card:hover {
  transform: scale(1.05)
}

.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px rgb(83, 83, 83);
  font-weight: bold;
  letter-spacing: 0.08rem
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