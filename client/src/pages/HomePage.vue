<script setup>
import { AppState } from '@/AppState';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted } from 'vue';

const keeps = computed(() => AppState.keeps)

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
  <div class="container">
    <section class="row">
      <div class="">
        <div v-for="keep in keeps" :key="keep.id">
          <KeepCard :keep="keep" />
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped lang="scss"></style>
