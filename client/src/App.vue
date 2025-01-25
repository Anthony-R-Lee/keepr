<script setup>
import Navbar from './components/Navbar.vue';
import { AppState } from './AppState.js';
import ModalWrapper from './components/ModalWrapper.vue';
import KeepCreateForm from './components/KeepCreateForm.vue';
import KeepDetailModal from './components/KeepDetailModal.vue';
import EditAccountModal from './components/EditAccountModal.vue';

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
  <ModalWrapper modalId="keepCreateModal">
    <KeepCreateForm />
    <KeepDetailModal />
    <EditAccountModal />
  </ModalWrapper>
</template>

<style lang="scss">
@import '@/assets/scss/main.scss';
</style>
