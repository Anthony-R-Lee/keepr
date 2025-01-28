<script setup>
import { AppState } from '@/AppState';
import VaultCard from '@/components/VaultCard.vue';
import { vaultsService } from '@/services/VaultsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()

const vaults = computed(() => AppState.activeVault)

const account = computed(() => AppState.account)

onMounted(() => {
  getVaultById()
  getVaultKeeps()
})

async function getVaultById() {
  try {
    const vaultId = route.params.vaultId
    await vaultsService.getVaultById(vaultId)
  }
  catch (error) {
    Pop.meow(error);
    logger.error("GETTING VAULT BY ID", error)
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
</script>


<template>
  <div class="d-flex justify-content-center">
    <div v-if="vaults">
      <div class="card mt-4 shadow-lg box-shadow" :style="{ backgroundImage: `url(${vaults?.img})` }">
        <div class="justify-content-between bg-img">
          <div class="title text-light justify-content-center text-capitalize d-flex align-items-end">
            <b>
              {{ vaults.name }}
            </b>
          </div>
          <div class="creator-name text-light justify-content-center text-capitalize d-flex align-items-end">
            <b>
              by
              {{ vaults.creator.name }}
            </b>
          </div>
        </div>
      </div>
      <div class=" d-flex justify-content-center">
        <div class="keep-count">
          <b>13 Keeps</b>
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
  position: relative;
  display: inline-block;

  .bg-img {
    width: 100%;
  }
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

.title {
  margin: 0.5em;
  margin-bottom: 1em;
  padding: 0.5em;
  padding-left: 0.5em;
  padding-right: 0.5em;
  border-radius: 5px;
  font-size: 2em;
  text-shadow: 1px 1px rgba(0, 0, 0, 0.738);
  position: absolute;
  bottom: 0;
  width: 90%;
}

.creator-name {
  margin: 0.5em;
  padding: 0.5em;
  padding-left: 0.5em;
  padding-right: 0.5em;
  border-radius: 5px;
  font-size: 1em;
  text-shadow: 1px 1px rgba(0, 0, 0, 0.738);
  position: absolute;
  bottom: 0;
  width: 90%;
}
</style>