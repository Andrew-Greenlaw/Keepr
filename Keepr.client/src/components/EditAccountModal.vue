<template>
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <form @submit="handleSubmit()">
        <div class="modal-header">
          <h5 class="modal-title">Add Your Vault</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="mb-3">
            <input type="text" class="form-control" id="name" v-model="editable.name" placeholder="Name">
          </div>
          <div class="mb-3">
            <label for="picture" class="form-label">Picture</label>
            <input type="url" class="form-control" id="picture" v-model="editable.picture"
              placeholder="https://picture.com.png">
          </div>
          <div class="mb-3">
            <label for="coverImg" class="form-label">CoverImg</label>
            <input type="url" class="form-control" id="coverImg" v-model="editable.coverImg"
              placeholder="https://coverImg.com.png">
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" aria-label="Close">Close</button>
            <button type="submit" class="btn btn-primary" aria-label="Save Changes">Save Changes</button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>


<script>
import { ref, watchEffect } from 'vue';
import { AppState } from '../AppState.js';
import { accountService } from '../services/AccountService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...AppState.account }
    })
    return {
      editable,
      async handleSubmit() {
        try {
          logger.log(editable)
          await accountService.editAccount(editable.value)
        } catch (error) {
          Pop.error('[editAccount]')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>