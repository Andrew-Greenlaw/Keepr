<template>
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <form @submit="handleSubmit()">
        <div class="modal-header">
          <h5 class="modal-title">Add Your Keep</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="mb-3">
            <input type="text" class="form-control" id="name" v-model="editable.name" placeholder="Name" maxlength="20"
              required aria-label="Name Input">
          </div>
          <div class="mb-3">
            <textarea type="text" class="form-control" id="description" v-model="editable.description"
              placeholder="Description" rows="3" required aria-label="Description Input" maxlength="100"></textarea>
          </div>
          <div class="mb-3">
            <label for="img" class="form-label ps-2">Image</label>
            <input type="url" class="form-control" id="img" v-model="editable.img" placeholder="https://Img.com.png"
              required>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" aria-label="Close">Close</button>
          <button type="submit" class="btn btn-primary" aria-label="Create">Create</button>
        </div>
      </form>
    </div>
  </div>
</template>


<script>
import { ref } from 'vue'
import { AppState } from '../AppState.js'
import { AuthService } from '../services/AuthService.js'
import { keepsService } from '../services/KeepsService.js'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async handleSubmit() {
        try {
          if (!AppState.account.id) {
            return AuthService.loginWithRedirect()
          }
          await keepsService.createKeep(editable.value)
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