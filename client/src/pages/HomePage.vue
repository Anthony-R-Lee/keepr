<script setup>
import { AppState } from '@/AppState';
import KeepCard from '@/components/KeepCard.vue';
import KeepDetailModal from '@/components/KeepDetailModal.vue';
import { accountService } from '@/services/AccountService';
import { keepsService } from '@/services/KeepsService';
import { vaultsService } from '@/services/VaultsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';

const keeps = computed(() => AppState.keeps)

const account = computed(() => AppState.account)

const activeKeep = computed(() => AppState.activeKeep)

const vaults = computed(() => AppState.myVaults)

const route = useRoute()

onMounted(() => {
  getKeeps()
  // getMyVaults()
})

async function getKeeps() {
  try {
    await keepsService.getKeeps()
  }
  catch (error) {
    Pop.meow(error);
    logger.error("GETTING KEEPS", error)
  }
}
</script>

<template>
  <div class="container">
    <div class="masonry mt-5">
      <div v-for="keep in keeps" :key="keep.id">
        <KeepCard :keep="keep" />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
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
