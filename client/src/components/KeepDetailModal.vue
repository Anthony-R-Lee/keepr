<script setup>
import { Keep } from '@/models/Keep';
import KeepCard from './KeepCard.vue';
import { computed, ref } from 'vue';
import { AppState } from '@/AppState';
import ModalWrapper from './ModalWrapper.vue';
import { Vault } from '@/models/Vault';
import Pop from '@/utils/Pop';
import { logger } from '@/utils/Logger';
import { useRoute } from 'vue-router';
import { keepsService } from '@/services/KeepsService';

const activeKeep = computed(() => AppState.activeKeep)

const vaultKeep = computed(() => AppState.vaultKeep)

const route = useRoute()

// const editableVaultKeepData = ref({
//   keepId: '',
//   vaultId: ''
// })

const props = defineProps({
  keep: { type: Keep },
  vaults: { type: Vault }
})

async function createVaultKeep() {
  try {
    const vaultData = { vaultId: route.params.vaultId }
    await keepsService.createVaultKeep(vaultData)
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
            <h3 class="recipe-detail-header">{{ activeKeep.name }}</h3>

          </div>
        </div>
        <div class="text-dark d-flex justify-content-center mx-3 mb-5 px-5">
          {{ activeKeep.description }}
        </div>
        <div class="d-flex justify-content-between align-items-end px-5">
          <div class="d-flex align-items-end dropdown-input">
            Vaults Raw: {{ vaults }}
            <select class="form-select vault-select mx-2" aria-label="Default">
              <option selected>VAULTS</option>
              <option v-for="vault in vaults" :key="vault.id">{{ props.vaults.name }}</option>
            </select>
            <div :disabled="vaults?.isPrivate">
              <button @click="createVaultKeep()" class="btn btn-secondary text-light">save</button>
            </div>
          </div>
          <RouterLink class="profile-info d-flex align-items-center pe-2 text-dark " role="button"
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
  padding-left: 3em;
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