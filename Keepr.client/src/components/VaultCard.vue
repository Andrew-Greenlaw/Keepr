<template>
  <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
    <div class="vault-card selectable shadow" :style="{ backgroundImage: `url(${vault.img})` }">
      <div class="background d-flex align-items-end h-100">
        <div class="d-flex flex-column justify-content-between h-100 w-100">
          <i class="mdi mdi-lock text-end p-2 fs-3 text-light" :style="vault.isPrivate ? '' : 'visibility: hidden'"></i>
          <h3 class="text-light p-1 text-shadow">{{ vault.name }}</h3>
        </div>
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
  transition: transform .2s;
}

.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px rgb(83, 83, 83);
  font-weight: bold;
  letter-spacing: 0.08rem
}

.vault-card:hover {
  transform: scale(1.05)
}

.background {
  background: rgb(71, 76, 97);
  background: linear-gradient(0deg, rgba(71, 76, 97, 0.6362920168067228) 10%, rgba(0, 0, 0, 0.2665441176470589) 30%, rgba(71, 76, 97, 0.0844712885154062) 47%);
}
</style>