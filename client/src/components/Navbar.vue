<script setup>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref(loadState('theme') || 'light')

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}

</script>

<template>
  <nav class="navbar navbar-expand-sm navbar-warning bg-page px-3 border-bottom border-warning">
    <div class="col-md-2 d-flex align-items-center">
      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <button class="btn bg-warning home-btn">
            <b>Home</b>
          </button>
        </div>
      </router-link>
      <div class="dropdown">
        <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
          <b>Create</b>
        </button>
        <ul class="dropdown-menu bg-primary">
          <li><a class="dropdown-item border-bottom border-dark" href="#">New Keep</a></li>
          <li><a class="dropdown-item" href="#">New Vault</a></li>
        </ul>
      </div>
    </div>
    <div class="d-flex justify-content-center align-items-center col-md-8 title-box">
      <p>the</p>
      <p>keepr</p>
      <p>co.</p>
    </div>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse col-md-2" id="navbarText">
      <ul class="navbar-nav me-auto">
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <!-- <div>
        <button class="btn text-light" @click="toggleTheme"
          :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
          <Icon :name="theme == 'light' ? 'weather-sunny' : 'weather-night'" />
        </button>
      </div> -->
      <Login />
    </div>
  </nav>
  <!-- <hr class="text-shadow m-1"> -->
</template>

<style scoped>
.home-btn {
  border-radius: 10px;
}

.title-box {
  border: black;
  border-radius: 10px;
  border-width: 1px;
}

/* a:hover {
  text-decoration: none;
} */

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}
</style>
