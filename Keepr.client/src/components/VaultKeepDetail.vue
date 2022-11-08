<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-6 p-0 d-flex align-items-center justify-content-center bg-black keep-img">
        <img class="img-fluid keep-img" :src="keep.img" :alt="keep.name" :title="keep.name">
      </div>
      <div class="col-md-6 py-4">
        <div class="row flex-column justify-content-between h-100">
          <div class="col-12">
            <div class="row">
              <div class="col-12 text-end pe-4"><button type="button" class="btn-close" data-bs-dismiss="modal"
                  aria-label="Close"></button></div>
              <div class="col-12 d-flex justify-content-center gap-3">
                <div class="d-flex align-items-center"><i class="mdi mdi-eye fs-3 px-2"></i> {{ keep.views }}</div>
                <div class="d-flex align-items-center"><i class="mdi mdi-alpha-k-box-outline fs-3 px-2"></i>
                  {{ keep.kept }}
                </div>
              </div>
            </div>
          </div>
          <div class="col-12">
            <h2 class="text-center">{{ keep.name }}</h2>
            <div class="p-3 px-5">
              <p>{{ keep.description }}</p>
            </div>
          </div>
          <div class="col-12">
            <div class="row justify-content-between">
              <div class="col-6 d-flex align-items-center">
                <button v-if="account.id == vault.creatorId">Remove Keep</button>
              </div>
              <div class="col-6 d-flex align-items-center justify-content-end">
                <div class="d-flex align-items-center text-end p-2 rounded selectable" data-bs-dismiss="modal"
                  @click="GoToProfile(keep.creatorId)">
                  <img class="img-fluid profile-img" :src="keep.creator?.picture" alt="Creator Profile Picture"
                    :title="keep.creator?.name">
                  <h5 class="px-2">{{ keep.creator?.name }}</h5>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from '@vue/reactivity';
import { useRouter } from 'vue-router';
import { AppState } from '../AppState.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const router = useRouter()
    const editable = ref({})
    return {
      editable,
      keep: computed(() => AppState.keep),
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vault),
      async GoToProfile(id) {
        router.push({ name: 'Profile', params: { id: id } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.profile-img {
  height: 3rem;
  width: 3rem;
  border-radius: 50%;
}

.keep-img {
  border-bottom-left-radius: .375rem;
  border-top-left-radius: .375rem;
}

@media screen and (max-width: 1200px) {
  .profile-img {
    height: 2rem;
    width: 2rem;
    border-radius: 50%;
  }

  h5 {
    font-size: x-small;
  }
}

@media screen {
  .profile-img {
    height: 3rem;
    width: 3rem;
    border-radius: 50%;
  }

  h5 {
    font-size: x-small;
  }
}
</style>