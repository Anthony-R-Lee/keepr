<script setup>
import { computed, onMounted, ref, watch } from 'vue';
import { AppState } from '../AppState.js';
import EditAccountModal from '@/components/EditAccountModal.vue';
import { accountService } from '@/services/AccountService.js';
import { Modal } from 'bootstrap';
import Pop from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { useRoute } from 'vue-router';
import VaultCard from '@/components/VaultCard.vue';

const route = useRoute()

const account = computed(() => AppState.account)

const keeps = computed(() => AppState.profileKeeps)

const vaults = computed(() => AppState.profileVaults)

const accountData = ref({
  name: '',
  picture: '',
  coverImg: ''
})

onMounted(() => {
  getMyVault()
})

watch(account, () => {
  accountData.value.name = account.value?.name
  accountData.value.picture = account.value?.picture
  accountData.value.coverImg = account.value?.coverImg

}, { immediate: true })

async function getMyVault() {
  try {
    // const profileId = route.params.profileId
    await accountService.GetMyVault()
  }
  catch (error) {
    Pop.meow(error);
    logger.error("GETTING MY VAULTS", error)
  }
}

// async function getUsersKeep() {
//   try {
//     const profileId = route.params.profileId
//     await profilesService.GetUsersKeep(profileId)
//   }
//   catch (error) {
//     Pop.meow(error);
//     logger.error("GETTING USERS KEEP", error)
//   }
// }
</script>

<template>
  <div class="about text-center">
    <div v-if="account">
      <div>
        <div class="d-flex justify-content-between">
          <div class="col-12 pt-4">
            <img class="rounded cover-img" :src="account.coverImg" alt="" />
          </div>
          <div class="btn-group">
            <button class="btn fs-3" type="button" data-bs-toggle="dropdown" data-bs-auto-close="true"
              aria-expanded="false">
              <i class="mdi mdi-dots-horizontal"></i>
            </button>
            <ul class="dropdown-menu">
              <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#editAccountModal">Edit
                  Account</a></li>
            </ul>
          </div>
        </div>
        <img class="rounded profile-img" :src="account.picture" alt="" />
        <h1 class="mt-5"> {{ account.name }}</h1>
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
.masonry {
  column-count: 4;
}

.profile-img {
  max-width: 100px;
  max-height: 5em;
  border-radius: 50%;
  margin: -5em;
}

.cover-img {
  max-width: 40%;
  min-height: 10em;
  background-position: center;
  background-size: contain;
}
</style>
