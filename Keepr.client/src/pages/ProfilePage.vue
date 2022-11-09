<template>
  <div class="profile-page container-fluid">
    <div class="row justify-content-center">
      <div class="col-md-7 cover-img rounded shadow d-flex justify-content-center align-items-end mt-5"
        :style="{ backgroundImage: `url(${profile.coverImg})` }" title="profile cover image">
        <img class="img-fluid shadow" :src="profile.picture" alt="Profile Picture" :title="profile.name">
      </div>
      <div class="col-md-6 d-flex flex-column align-items-center justify-content-center">
        <h1>{{ profile.name }}</h1>
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
import { onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState.js'
import { profilesService } from '../services/ProfilesService.js'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const route = useRoute()
    async function GetProfile() {
      try {
        await profilesService.GetProfile(route.params.id)
      } catch (error) {
        Pop.error('[GetProfile]', error)
      }
    }
    async function GetProfileKeeps() {
      try {
        await profilesService.GetProfileKeeps(route.params.id)
      } catch (error) {
        Pop.error('[GetProfileKeeps]', error)
      }
    }
    async function GetProfileVaults() {
      try {
        await profilesService.GetProfileVault(route.params.id)
      } catch (error) {
        Pop.error('[GetProfileVaults]', error)
      }
    }

    onMounted(() => {
      GetProfile()
      GetProfileKeeps()
      GetProfileVaults()
    })
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults)
    }
  }
}
</script>


<style lang="scss" scoped>
.cover-img {
  height: 25rem;
  background-position: center;
  background-size: cover;
  margin-bottom: 8rem;

  img {
    height: 10rem;
    width: 10rem;
    border-radius: 50%;
    transform: translateY(5rem);
  }
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