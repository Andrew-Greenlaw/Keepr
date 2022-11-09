<template>
  <div class="profile-page container-fluid">
    <div class="row justify-content-center">
      <div class="col-md-7 cover-img rounded shadow d-flex justify-content-center align-items-end mt-5"
        :style="{ backgroundImage: `url(${account.coverImg})` }" title="account cover image">
        <img class="img-fluid shadow" :src="account.picture" alt="Account Picture" :title="account.name">
      </div>
      <div class="edit-button col-md-7 text-end">
        <div class="dropdown ">
          <button class="btn dropdown-toggle lighten-30 text-uppercase selectable rounded" type="button"
            data-bs-toggle="dropdown" aria-expanded="false">
            ...
          </button>
          <ul class="dropdown-menu">
            <li>
              <div class="dropdown-item" data-bs-toggle="modal" data-bs-target="#editAccountModal">Edit Account</div>
            </li>
          </ul>
        </div>
      </div>
      <div class="col-md-6 d-flex flex-column align-items-center justify-content-center">
        <h1>{{ account.name }}</h1>
        <div>
          <h4>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</h4>
        </div>
      </div>
    </div>
    <div class="row justify-content-center mt-4">
      <div class="col-md-8">
        <h2>Vaults</h2>
        <div class="row">
          <div class="col-6 col-md-3 mb-3" v-for="v in vaults" :key="v.id">
            <VaultCard :vault="v" />
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-center mt-4">
      <div class="col-md-8">
        <h2>Keeps</h2>
        <div class="masonry-with-columns">
          <ProfileKeepCard v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState.js'
import { accountService } from '../services/AccountService.js'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    async function GetVaults() {
      try {
        await accountService.GetVaults()
      } catch (error) {
        Pop.error('[GetVaults]', error)
      }
    }
    async function GetMyKeeps() {
      try {
        await accountService.GetKeeps()
      } catch (error) {
        Pop.error('[GetMyKeeps]', error)
      }
    }
    onMounted(() => {
      GetVaults()
    })
    watchEffect(() => GetMyKeeps(AppState.account))
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.profileKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.cover-img {
  height: 25rem;
  background-position: center;
  background-size: cover;

  img {
    height: 10rem;
    width: 10rem;
    border-radius: 50%;
    transform: translateY(5rem);
    object-fit: cover;
  }
}

.edit-button {
  margin-bottom: 5rem;
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