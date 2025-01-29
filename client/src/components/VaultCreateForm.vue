<script setup>
import { Account } from '@/models/Account';
import ModalWrapper from './ModalWrapper.vue';
import { ref } from 'vue';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { vaultsService } from '@/services/VaultsService';
import { Modal } from 'bootstrap';


defineProps({
  account: { type: Account }
})

const editableData = ref({
  name: '',
  isPrivate: false,
  description: '',
  img: ''
})

async function createVault() {
  try {
    await vaultsService.createVault(editableData.value)
    editableData.value = {
      name: '',
      isPrivate: false,
      description: '',
      img: ''
    }
    Modal.getInstance('#vaultCreateModal').hide()

  } catch (error) {
    Pop.meow(error);
    logger.error("CREATING VAULT", error)
  }
}


</script>


<template>
  <form @submit.prevent="createVault()" class="form">
    <div class="d-flex justify-content-end">
      <button class="btn-close py-2" type="button" data-bs-dismiss="modal" aria-label="Close"></button>
    </div>
    <div class="">
      <div class="form-floating mb-4 col-12">
        <input v-model="editableData.name" type="text" class="form-control" id="name" placeholder="Name..." required
          minlength="3" maxlength="255">
        <label for="name">Name</label>
      </div>
      <div class="form-floating mb-4 col-12">
        <input v-model="editableData.description" type="text" class="form-control" id="description"
          placeholder="Description..." required minlength="3" maxlength="255">
        <label for="name">Description</label>
      </div>
      <div class="form-floating col-12">
        <input v-model="editableData.img" type="url" class="form-control" id="img" placeholder="Image Url..." required
          maxlength="1000">
        <label for="img">Image</label>
      </div>
      <!-- <div class="form-check form-floating mb-4 col-12 p-3 d-flex justify-content-between">
        <input v-model="editableData.isPrivate" class="form-check-input form-control" type="checkbox" value=""
          id="isPrivate" checked>
        <label class="form-check-label" for="isPrivate">
          <p>
            Make Vault Private?
          </p>
        </label>
      </div> -->
    </div>
    <div class="d-flex justify-content-end pt-3">
      <div class="text-end col-3">
        <button class="btn btn-warning" type="submit">Create Vault</button>
      </div>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>