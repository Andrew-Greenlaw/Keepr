<template>
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <form @submit.prevent="handleSubmit()">
        <div class="modal-header">
          <h5 class="modal-title">Add Your Vault</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="mb-3">
            <input type="text" class="form-control" id="vaultName" v-model="editable.name" placeholder="Name"
              maxlength="20" required aria-label="Name Input">
          </div>
          <div class="mb-3">
            <label for="vaultImg" class="form-label ps-2">Image</label>
            <input type="url" class="form-control" id="vaultImg" v-model="editable.img"
              placeholder="https://Img.com.png" required>
          </div>
          <div class="mb-3 form-check">
            <input type="checkbox" class="form-check-input" id="privateCheckBox" v-model="editable.isPrivate">
            <label class="form-check-label" for="privateCheckBox">Private?</label>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" aria-label="Close">Close</button>
          <button type="submit" class="btn btn-primary" data-bs-dismiss="modal" aria-label="Create">Create</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState.js'
import { AuthService } from '../services/AuthService.js'
import { vaultsService } from '../services/VaultsService.js'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const router = useRouter()
    const editable = ref({ description: "Not Implemented" })
    return {
      editable,
      async handleSubmit() {
        try {
          if (!AppState.account.id) {
            return AuthService.loginWithRedirect()
          }
          await vaultsService.createVault(editable.value)
          router.push({ name: 'Account' })
        } catch (error) {
          Pop.error(error, "[Handling Submit]")
        }
      }
    }
  }
}
</script>
<style lang="scss" scoped>

</style>