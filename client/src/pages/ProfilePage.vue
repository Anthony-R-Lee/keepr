<script setup>
import { AppState } from '@/AppState';
import KeepCard from '@/components/KeepCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { profilesService } from '@/services/ProfilesService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()

const account = computed(() => AppState.account)

const keeps = computed(() => AppState.profileKeeps)

const profile = computed(() => AppState.activeProfile)

const vaults = computed(() => AppState.profileVaults)

onMounted(() => {
  getProfileById()
  getUsersVault()
  getUsersKeep()
})

watch(route, () => {
  getUsersVault()
})

async function getUsersVault() {
  try {
    const profileId = route.params.profileId
    await profilesService.GetUsersVault(profileId)
  }
  catch (error) {
    Pop.meow(error);
    logger.error("GETTING USERS VAULT", error)
  }
}

async function getUsersKeep() {
  try {
    const profileId = route.params.profileId
    await profilesService.GetUsersKeep(profileId)
  }
  catch (error) {
    Pop.meow(error);
    logger.error("GETTING USERS KEEP", error)
  }
}

async function getProfileById() {
  try {
    const profileId = route.params.profileId
    await profilesService.getProfileById(profileId)
  }
  catch (error) {
    Pop.meow(error);
    logger.error("GETTING PROFILE BY ID", error)
  }
}
</script>


<template>
  <div class="about text-center">
    <div v-if="account">
      <div>

        <img class="rounded cover-img" :src="profile.coverImg" alt="" />
        <img class="rounded profile-img" :src="profile.picture" alt="" />
        <h1> {{ profile.name }}</h1>
        <p>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
      </div>
      <div>

        <div class="col-md-7 justify-content-center text-start d-flex py-3">
          <h1>Vaults</h1>
        </div>
        <div class="masonry mx-5 px-5">
          <div v-for="vault in vaults" :key="vault.id">
            <VaultCard :vault="vault" />
          </div>
        </div>
        <div class="col-md-7 justify-content-center text-start d-flex py-3">
          <h1>Keeps</h1>
        </div>
        <div class="masonry mx-5 px-5">
          <div v-for="keep in keeps" :key="keep.id">
            <KeepCard :keep="keep" />
          </div>
        </div>
      </div>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.masonry {
  column-count: 4;
}

.profile-img {
  max-width: 100px;
  max-height: 5em;
  border-radius: 50%;
}

.cover-img {
  max-width: 100px;
}
</style>