<template>
  <nav class="navbar navbar-light bg-light px-3 shadow">
    <div class="d-flex">
      <router-link :to="{ name: 'Home' }" class="btn lighten-30 selectable text-uppercase">
        Home
      </router-link>
      <div class="dropdown selectable rounded">
        <button class="btn dropdown-toggle lighten-30 text-uppercase" type="button" data-bs-toggle="dropdown"
          aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu">
          <li>
            <div class="dropdown-item" data-bs-toggle="modal" data-bs-target="#newKeepModal">New Keep</div>
          </li>
          <li>
            <div class="dropdown-item" data-bs-toggle="modal" data-bs-target="#newVaultModal">New Vault</div>
          </li>
        </ul>
      </div>
      <!-- LOGIN COMPONENT HERE -->
    </div>
    <router-link :to="{ name: 'Home' }">
      <div class="d-flex align-items-center">
        <img alt="logo" src="Keepr logo.png" height="45" />
      </div>
    </router-link>
    <Login />
  </nav>
</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.navbar {
  --bs-navbar-padding-y: none;
}

/* .navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
} */

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
