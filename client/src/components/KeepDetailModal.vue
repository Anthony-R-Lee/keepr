<script setup>
import { computed, ref } from 'vue';
import { AppState } from '@/AppState';
import Pop from '@/utils/Pop';
import { logger } from '@/utils/Logger';
import { keepsService } from '@/services/KeepsService';

const activeKeep = computed(() => AppState.activeKeep)

const account = computed(() => AppState.account)

const vaultKeeps = computed(() => AppState.vaultKeeps)

const myVaults = computed(() => AppState.myVaults)

// const route = useRoute()

const editableData = ref({
  vaultId: '',
  keepId: ''
})

async function createVaultKeep() {
  editableData.value.keepId = activeKeep.value.id
  try {
    await keepsService.createVaultKeep(editableData.value)
    editableData.value = {
      vaultId: '',
      keepId: ''
    }
  }
  catch (error) {
    Pop.meow(error);
    logger.error("CREATING VAULTKEEP", error)
  }
}
</script>


<template>
  <div class="bg-body" id="keepDetailModal" modalId="keepDetailModal">
    <div class="d-flex justify-content-end">
      <button class="btn-close p-0" type="button" data-bs-dismiss="modal" aria-label="Close"></button>
    </div>
    <div v-if="activeKeep" class="d-flex justify-content-between modal-xl">
      <div class="col-md-6 col-12">
        <img class="keep-detail-img" :src="activeKeep.img" alt="Keep Image">
      </div>
      <div class="col-md-6 col-12">
        <div class="d-flex justify-content-center fs-5">
          <div class="px-3">
            <i class="mdi mdi-eye-outline p-1"></i>
            <span>
              {{ activeKeep.views }}
            </span>
          </div>
          <div class="px-3">
            <i class="mdi mdi-alpha-k-box-outline p-1"></i>
            <span>0</span>
          </div>
        </div>
        <div class="px-4 pt-5 mt-5">
          <div class="d-flex justify-content-center align-items-center">
            <h3 class="recipe-detail-header" :title="`View ${activeKeep.name} in Detail`">{{ activeKeep.name }}</h3>

          </div>
        </div>
        <div class="text-dark d-flex justify-content-center mx-3 mb-5 px-5">
          {{ activeKeep.description }}
        </div>
        <div class="d-flex justify-content-between align-items-end px-5">
          <form v-if="account" class="d-flex align-items-end dropdown-input" @submit.prevent="createVaultKeep()">

            <select v-model="editableData.vaultId" class="form-select vault-select mx-2" aria-label="Default" required>
              <option value="" disabled selected>VAULTS</option>
              <option v-for="vault in myVaults" :key="vault.id" :value="vault.id">
                {{ vault.name }}
              </option>
            </select>
            <div>
              <button class="btn btn-secondary text-light" :title="`Save ${activeKeep?.name}`"
                type="submit">save</button>
            </div>
          </form>
          <RouterLink class="profile-info d-flex align-items-center pe-3 text-dark" :title="activeKeep.creator.name"
            :to="{ name: 'Profile', params: { profileId: activeKeep.creatorId } }">
            <div>
              <img :src="activeKeep.creator.picture" alt="">
            </div>
            <div>
              <b>{{ activeKeep.creator.name }}</b>
            </div>
          </RouterLink>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.vault-select {
  cursor: pointer;
}

.keep-detail-img {
  object-fit: cover;
  object-position: center;
  width: 100%;
  aspect-ratio: 1/1;
  min-height: 30dvh;
  padding: 0;
  margin: 0;
}

.profile-info {
  position: absolute;
  bottom: 1em;
  right: 0;
}

.dropdown-input {
  position: absolute;
  bottom: 1em;
  left: 50%;
  border-color: black;
}

.profile-info img {
  border-radius: 50%;
  max-height: 2em;
  margin-right: 5px;
  margin: 4px;
}
</style>