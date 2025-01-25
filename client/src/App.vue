<script setup>
import Navbar from './components/Navbar.vue';
import { AppState } from './AppState.js';
import ModalWrapper from './components/ModalWrapper.vue';

</script>

<template>
  <header>
    <Navbar />
  </header>
  <main class="position-relative">
    <router-view />
  </main>
  <footer>
    <!-- Made with 💖 by CodeWorks -->
  </footer>
  <ModalWrapper />
</template>

<style lang="scss">
@import '@/assets/scss/main.scss';
</style>
