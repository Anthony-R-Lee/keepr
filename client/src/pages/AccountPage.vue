<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';

const account = computed(() => AppState.account)

const keeps = computed(() => AppState.keeps)
const vaults = computed(() => AppState.vaults)

</script>

<template>
  <div class="about text-center">
    <div v-if="account">
      <div>

        <img class="rounded cover-img" :src="account.coverImg" alt="" />
        <img class="rounded profile-img" :src="account.picture" alt="" />
        <h1> {{ account.name }}</h1>
        <p>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
      </div>
      <div>

        <div class="col-md-7 justify-content-center text-start d-flex">
          <h1>Vaults</h1>
        </div>
        <div class="col-md-7 justify-content-center text-start d-flex">
          <h1>Keeps</h1>
        </div>
      </div>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
</template>

<style scoped lang="scss">
.profile-img {
  max-width: 100px;
  max-height: 5em;
  border-radius: 50%;
}

.cover-img {
  max-width: 100px;
}
</style>
