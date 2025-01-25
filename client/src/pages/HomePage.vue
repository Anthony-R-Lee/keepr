<script setup>
import { keepsService } from '@/services/KeepsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { onMounted } from 'vue';

onMounted(() => {
  getKeeps()
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
  <div>
  </div>
</template>

<style scoped lang="scss"></style>
