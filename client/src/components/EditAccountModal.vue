<script setup>
import { AppState } from '@/AppState';
import { accountService } from '@/services/AccountService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { computed, ref, watch } from 'vue';


const account = computed(() => AppState.account)


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
  <ModalWrapper id="editAccountModal" modalId="editAccountModal" class="modal-xl">
    <div class="modal-dialog modal-xl bg-body">
      <div v-if="accountData">
        <form @submit.prevent="editAccount()" class="row text-start">
          <div class="mb-3 col-4">
            <label for="account-name">Name</label>
            <input v-model="accountData.name" type="text" id="account-name" name="account-name" class="form-control"
              maxlength="100" required>
          </div>
          <div class="mb-3 col-4">
            <label for="account-coverImg">Cover Image</label>
            <input v-model="accountData.coverImg" type="text" id="account-coverImg" name="account-coverImg"
              class="form-control" maxlength="500">
          </div>
          <div class="mb-3 col-4">
            <label for="account-picture">Profile Picture</label>
            <input v-model="accountData.picture" type="text" id="account-picture" name="account-picture"
              class="form-control" maxlength="500">
          </div>
        </form>
      </div>
    </div>
  </ModalWrapper>
</template>


<style lang="scss" scoped></style>