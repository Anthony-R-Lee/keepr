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
import KeepCard from '@/components/KeepCard.vue';

const route = useRoute()

const account = computed(() => AppState.account)

const keeps = computed(() => AppState.profileKeeps)

const vaults = computed(() => AppState.myVaults)

const accountData = ref({
  name: '',
  picture: '',
  coverImg: ''
})

onMounted(() => {
  getMyVault()
  getMyKeep()

})

watch(account, () => {
  getMyKeep()
})

watch(account, () => {
  accountData.value.name = account.value?.name
  accountData.value.picture = account.value?.picture
  accountData.value.coverImg = account.value?.coverImg

}, { immediate: true })

async function getMyVault() {
  try {
    // const profileId = route.params.profileId
    await accountService.getMyVault()
  }
  catch (error) {
    Pop.meow(error);
    logger.error("GETTING MY VAULTS", error)
  }
}

async function getMyKeep() {
  try {
    const accountId = account.value?.id
    await accountService.getMyKeep(accountId)
  }
  catch (error) {
    Pop.meow(error);
    logger.error("GETTING MY KEEPS", error)
  }
}
</script>

<template>
  <div class="about text-center">
    <div v-if="account">
      <div>
        <div class="d-flex justify-content-between">
          <div class="col-12 pt-4">
            <img class="rounded cover-img" :src="account.coverImg" alt="" />
          </div>
          <div class="btn-group edit-dropdown">
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

<style scoped lang="scss">
@media screen and (min-width: 768px) {
  .masonry {
    column-count: 4;
  }
}

@media screen and (max-width: 768px) {
  .masonry {
    column-count: 2;
  }
}

.edit-dropdown {
  position: absolute;
  right: 0;
  top: 0;
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
