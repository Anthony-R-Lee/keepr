<script setup>
import { computed, ref, watch } from 'vue';
import { AppState } from '../AppState.js';
import EditAccountModal from '@/components/EditAccountModal.vue';
import { accountService } from '@/services/AccountService.js';
import { Modal } from 'bootstrap';
import Pop from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';

const account = computed(() => AppState.account)

const keeps = computed(() => AppState.keeps)
const vaults = computed(() => AppState.vaults)

const accountData = ref({
  name: '',
  picture: '',
  coverImg: ''
})

watch(account, () => {
  accountData.value.name = account.value?.name
  accountData.value.picture = account.value?.picture
  accountData.value.coverImg = account.value?.coverImg

}, { immediate: true })

async function editAccount() {
  try {
    await accountService.editAccount(accountData.value)
    Modal.getInstance('#editAccountModal').show()
  }
  catch (error) {
    Pop.meow(error);
    logger.error("EDITING ACCOUNT", error)
  }
}
</script>

<template>
  <div class="about text-center">
    <div v-if="account">
      <div>
        <div class="d-flex justify-content-between">
          <div>
            <img class="rounded cover-img" :src="account.coverImg" alt="" />
          </div>
          <div class="btn-group">
            <button class="btn fs-3" type="button" data-bs-toggle="dropdown" data-bs-auto-close="true"
              aria-expanded="false">
              <i class="mdi mdi-dots-horizontal"></i>
            </button>
            <ul class="dropdown-menu">
              <li @click="editAccount()"><a class="dropdown-item" href="#" data-bs-toggle="modal"
                  data-bs-target="#editAccountModal">Edit
                  Account</a></li>
            </ul>
          </div>
        </div>
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
