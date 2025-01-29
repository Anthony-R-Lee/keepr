<script setup>
import { AppState } from '@/AppState';
import KeepCard from '@/components/KeepCard.vue';
import VaultKeepCard from '@/components/VaultKeepCard.vue';
import { vaultsService } from '@/services/VaultsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute()
const router = useRouter()

const vaults = computed(() => AppState.activeVault)
const keeps = computed(() => AppState.keeps)
const account = computed(() => AppState.account)
const vaultKeeps = computed(() => AppState.vaultKeeps)


watch(route, () => {
  getVaultById()
  getVaultKeeps()
}, { immediate: true })

async function getVaultById() {
  try {
    const vaultId = route.params.vaultId
    await vaultsService.getVaultById(vaultId)
  }
  catch (error) {
    Pop.meow(error);
    logger.error("GETTING VAULT BY ID", error)
    router.push({ name: 'Home' })
  }
}

async function getVaultKeeps() {
  try {
    const vaultId = route.params.vaultId
    await vaultsService.getVaultKeeps(vaultId)
  }
  catch (error) {
    Pop.meow(error);
    logger.error("GETTING VAULT KEEPS", error)
  }
}

async function deleteVault() {
  try {
    const confirm = await Pop.confirm(`Do you really want to delete ${vaults.value.name}?`)
    if (!confirm) return
    const vaultId = route.params.vaultId
    await vaultsService.deleteVault(vaultId)
    router.push({ name: 'Home' })
  }
  catch (error) {
    Pop.meow(error);
    logger.error("DELETING VAULT", error)
  }
}
</script>


<template>
  <!-- FIXME I cannot access my own private vaults (look at the backend) -->
  <!-- FIXME test getting keeps in private vault and deleting private vault keeps and deleting private vault -->
  <div class="container-fluid">
    <div v-if="vaults" class="">
      <div class="d-flex justify-content-center">
        <div class="card mt-4 shadow-lg box-shadow col-md-12" :style="{ backgroundImage: `url(${vaults?.img})` }">
          <div class=" bg-img">
            <div v-if="account?.id == vaults.creatorId" class="delete-btn">
              <button @click="deleteVault()" :title="`Delete ${vaults.name}`" class="btn btn-danger"><i
                  class="mdi mdi-close"></i></button>
            </div>
          </div>
          <div class="align-self-end text-shadow pt-5 mt-5">
            <div class="title text-light justify-content-center text-capitalize d-flex align-self-end">
              <b>
                {{ vaults.name }}
              </b>
            </div>
            <div class="creator-name text-light justify-content-center text-capitalize d-flex align-self-end">
              <b>
                by
                {{ vaults.creator.name }}
              </b>
            </div>
          </div>
        </div>
      </div>
      <div class=" d-flex justify-content-center">
        <div v-if="vaultKeeps.length != 1" class="keep-count">
          <b>{{ vaultKeeps.length }} Keeps</b>
        </div>
        <div v-else class="keep-count">
          <b>{{ vaultKeeps.length }} Keep</b>
        </div>
      </div>
      <div>
        <div class="masonry mt-5 justify-content-center">
          <div v-for="vaultKeep in vaultKeeps" :key="vaultKeep.id">
            <VaultKeepCard :vaultKeep="vaultKeep" />
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
.card {
  min-height: 15em;
  background-position: center;
  background-size: cover;
  // margin: 1.75em;
  border: none;
  width: 30dvw;
  display: inline-block;

  .bg-img {
    width: 100%;
  }

  .delete-btn {
    position: absolute;
    top: 0;
    right: 0;
    margin: -0.5em;
    border-radius: 100em;
    max-height: 10px;
    min-width: 1em;
    opacity: 0;
  }

  :hover .delete-btn {
    opacity: 1;
    transition: all .2s ease;
  }

}

.title {
  margin: 0.5em;
  margin-bottom: 1em;
  // padding: 0.5em;
  padding-left: 0.5em;
  padding-right: 0.5em;
  padding-bottom: -0.5em;
  border-radius: 5px;
  font-size: 2em;
  text-shadow: 1px 1px #000000;
  width: 90%;
}

.creator-name {
  margin: 0.5em;
  // padding: 0.5em;
  padding-left: 0.5em;
  padding-right: 0.5em;
  border-radius: 5px;
  font-size: 1em;
  text-shadow: 1px 1px #000000;
  width: 90%;
}

.keep-count {
  background-color: #DED6E9;
  font-size: 1.25em;
  padding: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 15px;
  border-radius: 10px;
}

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
</style>