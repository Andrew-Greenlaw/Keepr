<template>
  <div class="modal-dialog modal-dialog-centered modal-xl">
    <div class="modal-content">
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
                    <div class="d-flex align-items-center"><i class="mdi mdi-eye fs-3 px-2" title="View Count"></i> {{
                        keep.views
                    }}</div>
                    <div class="d-flex align-items-center"><i class="mdi mdi-alpha-k-box-outline fs-3 px-2"
                        title="Kept Count"></i>
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
                  <div class="col-xl-6 d-flex align-items-center justify-content-center">
                    <form @submit.prevent="handleSubmit()" v-if="account.id">
                      <div class="d-flex align-items-center">
                        <div class="input-group px-3">
                          <label for="vaults" class="input-group-text">Vaults</label>
                          <select class="form-select" v-model="editable.vaultId" id="vaults">
                            <option :value="v.id" v-for="v in vaults">{{ v.name }}</option>
                            <option selected>Select a Vault</option>
                          </select>
                        </div>
                        <button type="submit" class="btn btn-dark" aria-label="Add to a vault">Save</button>
                      </div>
                    </form>
                  </div>
                  <div class="col-xl-6 d-flex align-items-center justify-content-center mt-1">
                    <div class="d-flex align-items-center text-end p-2 rounded selectable" data-bs-dismiss="modal"
                      @click="GoToProfile(keep.creatorId)" aria-label="Go to Profile Page">
                      <img class="img-fluid profile-img" :src="keep.creator?.picture" alt="Creator Profile Picture"
                        :title="keep.creator?.name">
                      <h5 class="px-2 creator-name">{{ keep.creator?.name }}</h5>
                    </div>
                  </div>
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
      async handleSubmit() {
        try {
          editable.value.keepId = this.keep.id
          await vaultKeepsService.createVaultKeep(editable.value)
          Pop.toast("you added this to your vault")
        } catch (error) {
          Pop.error('[handleSubmit]', error)
        }
      },
      keep: computed(() => AppState.keep),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
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
  object-fit: cover;
}

.creator-name {
  font-size: 1.3vw;
}

.modal-content {
  border: none;
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
    object-fit: cover;
  }


}

@media screen {
  .profile-img {
    height: 3rem;
    width: 3rem;
    border-radius: 50%;
    object-fit: cover;
  }

  .creator-name {
    font-size: medium;
  }
}
</style>