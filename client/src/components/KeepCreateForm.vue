<script setup>
import { Account } from '@/models/Account';
import { keepsService } from '@/services/KeepsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { ref } from 'vue';
import KeepDetailModal from './KeepDetailModal.vue';

defineProps({
  account: { type: Account, required: true }
})

const editableData = ref({
  name: '',
  description: '',
  img: ''
})

async function createKeep() {
  try {
    await keepsService.createKeep(editableData.value)
    editableData.value = {
      name: '',
      description: '',
      img: ''
    }
  } catch (error) {
    Pop.meow(error);
    logger.error("CREATING KEEP", error)
  }
}

</script>


<template>
  <form @submit.prevent="createKeep()" class="form">
    <button class="btn-close py-2" type="button" data-bs-dismiss="modal" aria-label="Close"></button>
    <div class="d-flex justify-content-between gap-2">
      <div class="form-floating mb-4 col-6">

        <input v-model="editableData.name" type="text" class="form-control" id="name" placeholder="Name..." required
          minlength="3" maxlength="255">
        <label for="name">Name</label>
      </div>
      <div class="form-floating mb-4 col-6">
        <input v-model="editableData.description" type="text" class="form-control" id="description"
          placeholder="Description..." required minlength="3" maxlength="255">
        <label for="description">Description</label>
      </div>
    </div>
    <div class="d-flex gap-2">
      <div class="form-floating col-10">
        <input v-model="editableData.img" type="url" class="form-control" id="img" placeholder="Image Url..." required
          maxlength="1000">
        <label for="img">Image</label>
      </div>
      <div class="text-end col-2">
        <button class="btn btn-warning" type="submit">Create</button>
      </div>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>