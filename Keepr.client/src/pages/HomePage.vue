<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 mt-4">
        <div class="masonry-with-columns">
          <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js'
import Pop from '../utils/Pop.js';

export default {
  setup() {
    async function GetAllKeeps() {
      try {
        await keepsService.GetAllKeeps()
      } catch (error) {
        Pop.error("[GetAllKeeps]", error)
      }
    }
    onMounted(() => {
      GetAllKeeps()
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
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
