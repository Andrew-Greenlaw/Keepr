<template>
  <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
    <div class="vault-card selectable" :style="{ backgroundImage: `url(${vault.img})` }">
      <div class="background d-flex align-items-end h-100">
        <h3 class="text-light">{{ vault.name }}</h3>
      </div>
    </div>
  </router-link>
</template>


<script>
import { Vault } from '../models/Vault.js';

export default {
  props: {
    vault: {
      type: Vault,
      required: true
    }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.vault-card {
  height: 10rem;
  background-size: cover;
  background-position: center;
}

.background {
  background: rgb(71, 76, 97);
  background: linear-gradient(0deg, rgba(71, 76, 97, 0.6362920168067228) 10%, rgba(0, 0, 0, 0.2665441176470589) 30%, rgba(71, 76, 97, 0.0844712885154062) 47%);
}
</style>