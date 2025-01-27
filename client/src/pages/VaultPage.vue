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

watch(route, () => {
  getVaultById()
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
</script>


<template>
  <div v-if="vaults">
    <div>

      {{ vaults?.name }}
    </div>
  </div>
</template>


<style lang="scss" scoped></style>