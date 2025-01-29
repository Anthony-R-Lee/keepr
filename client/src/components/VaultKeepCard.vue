<script setup>
import { AppState } from '@/AppState';
import { Keep } from '@/models/Keep';
import { VaultKeep } from '@/models/VaultKeep';
import { VaultKept } from '@/models/VaultKept';
import { vaultsService } from '@/services/VaultsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed } from 'vue';

const account = computed(() => AppState.account)

const props = defineProps({
  vaultKeep: { type: VaultKept }
})

async function removeVaultKeep(vaultKeepId) {
  try {
    const confirm = await Pop.confirm(`Do you really want to remove ${props.vaultKeep.name}?`)
    if (!confirm) return
    await vaultsService.removeVaultKeep(vaultKeepId)
  }
  catch (error) {
    Pop.meow(error);
    logger.error("REMOVING VAULT KEEP", error)
  }
}
</script>


<template>
  <div v-if="vaultKeep" class="ms-4 my-1 card shadow-lg box-shadow"
    :style="{ backgroundImage: `url(${vaultKeep?.img})` }">
    <div>
      <div v-if="account?.id == vaultKeep.creatorId" class="delete-btn">
        <button @click="removeVaultKeep(props.vaultKeep.vaultKeepId)" :title="`Remove Vault Keep${vaultKeep.name}`"
          class="btn btn-danger"><i class="mdi mdi-close"></i></button>
      </div>
      <div class="justify-content-between bg-img">
        <div class="title text-light text-capitalize d-flex align-items-end">
          <b>
            {{ vaultKeep.name }}
          </b>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.card {
  min-height: 20em;
  background-position: center;
  background-size: cover;
  border: none;
  // margin: 5em;
  max-width: 20dvw;

  .bg-img {
    width: 100%;
  }

  .profile-img {
    position: absolute;
    bottom: 0;
    right: 0;
    margin: 0.5em;
    border-radius: 50%;
    max-height: 3em;
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
  padding: 0.5em;
  padding-left: 0.5em;
  padding-right: 0.5em;
  border-radius: 5px;
  text-shadow: 1px 1px rgba(0, 0, 0, 0.738);
  position: absolute;
  bottom: 0;
  width: 90%;
  cursor: pointer;
}
</style>