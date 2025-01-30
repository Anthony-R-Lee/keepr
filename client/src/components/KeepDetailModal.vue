<script setup>
import { computed, ref } from 'vue';
import { AppState } from '@/AppState';
import Pop from '@/utils/Pop';
import { logger } from '@/utils/Logger';
import { keepsService } from '@/services/KeepsService';
import { Modal } from 'bootstrap';
import { Keep } from '@/models/Keep';

const activeKeep = computed(() => AppState.activeKeep)

const account = computed(() => AppState.account)

const vaultKeeps = computed(() => AppState.vaultKeeps)

const myVaults = computed(() => AppState.myVaults)

const props = defineProps({
  keep: { type: Keep }
})

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
    Modal.getInstance('#keepDetailModal').hide()
    activeKeep.value.kept++
  }
  catch (error) {
    Pop.meow(error);
    logger.error("CREATING VAULTKEEP", error)
  }
}
</script>


<template>
  <!-- FIXME kept count is not working -->
  <div class="bg-body" id="keepDetailModal" modalId="keepDetailModal">
    <div class="d-flex justify-content-end">
      <button class="btn-close mb-1" type="button" data-bs-dismiss="modal" aria-label="Close"></button>
    </div>
    <div v-if="activeKeep" class="modal-xl">
      <div class="d-flex flex-wrap justify-content-between">

        <div class="col-lg-6 col-12 ms-auto">
          <img class="keep-detail-img" :src="activeKeep.img" alt="Keep Image">
        </div>
        <div class="col-lg-6 col-12 ms-auto">
          <div class="d-flex justify-content-center fs-5 pt-3">
            <div class="px-3">
              <i :title="`Views for ${activeKeep.name}`" class="mdi mdi-eye-outline p-1"></i>
              <span>
                {{ activeKeep.views }}
              </span>
            </div>
            <div class="px-3">
              <i :title="`Keep count for ${activeKeep.name}`" class="mdi mdi-alpha-k-box-outline p-1"></i>
              <!-- <span>{{ props.keep.kept }}</span> -->
            </div>
          </div>
          <div class="px-2 pt-3 mt-3 px-lg-4 pt-lg-5 mt-lg-4">
            <div class="d-flex justify-content-center align-items-center">
              <h3 class="recipe-detail-header" :title="`View ${activeKeep.name} in Detail`">{{ activeKeep.name }}</h3>

            </div>
          </div>
          <div class="text-dark d-flex justify-content-center mx-3 mb-5 px-md-5">
            {{ activeKeep.description }}
          </div>
          <div class="px-5">
            <form v-if="account" class="d-flex align-items-end dropdown-input" @submit.prevent="createVaultKeep()">

              <select v-model="editableData.vaultId"
                class="form-select d-flex justify-content-start vault-select mx-1 mx-md-2" aria-label="Default"
                required>
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



@media screen and (max-width: 992px) {
  .dropdown-input {
    position: relative;
    bottom: 0;
    left: 0;
    border-color: black;
    margin-bottom: 1em;
  }

  .profile-info {
    position: relative;
    bottom: 0;
    right: 0;
  }

  .profile-info img {
    border-radius: 50%;
    max-height: 2em;
    margin-right: 5px;
    margin: 4px;
  }
}

@media screen and (min-width: 992px) {
  .dropdown-input {
    position: absolute;
    bottom: 1em;
    left: 50%;
    border-color: black;
  }

  .profile-info {
    position: absolute;
    bottom: 1em;
    right: 0;
  }

  .profile-info img {
    border-radius: 50%;
    max-height: 2em;
    margin-right: 5px;
    margin: 4px;
  }
}
</style>