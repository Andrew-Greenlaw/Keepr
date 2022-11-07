<template>
  <div class="keep-card selectable" data-bs-toggle="modal" data-bs-target="#keepModal" @click="GetKeepById(keep.id)">
    <img class="photo img-fluid" :src="keep.img" alt="keep image">
    <div class="text">
      <div>
        <h3>{{ keep.name }}</h3>
      </div>
    </div>
    <img class="profile-img" :src="keep.creator?.picture" :alt="keep.creator?.name" :title="keep.creator?.name">
  </div>
</template>


<script>
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';

export default {
  props: {
    keep: {
      type: Keep,
      required: true
    }
  },
  setup() {
    return {
      async GetKeepById(id) {
        try {
          await keepsService.GetKeepById(id)
        } catch (error) {
          Pop.error("[GetByKeepId]", error)
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

}

.text {
  position: absolute;
  bottom: 1rem;
  left: .5rem;
  color: white;
  width: auto;
}


.profile-img {
  position: absolute;
  bottom: 1rem;
  right: .5rem;
  height: 3rem;
  width: 3rem;
  border-radius: 50%;
}
</style>