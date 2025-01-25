<script setup>
import { Keep } from '@/models/Keep';
import KeepCard from './KeepCard.vue';
import { computed } from 'vue';
import { AppState } from '@/AppState';

const activeKeep = computed(() => AppState.activeKeep)

defineProps({
  keep: { type: Keep, required: true }
})
</script>


<template>
  <div>
    <ModalWrapper class="modal-xl" id="keepDetailModal" modalId="keepDetailModal" modalTitle="Keep Detail">
      <div v-if="activeKeep" class="d-flex justify-content-between">
        <div class="col-6">
          <img class="recipe-detail-img" :src="activeKeep.img" alt="">
        </div>
        <div class="col-6">
          <div class="px-4">
            <div class="d-flex justify-content-between align-items-center">
              <h3 class="recipe-detail-header">{{ activeKeep.name }}</h3>

            </div>
            <p>by: <span class="text-capitalize">dkjfkdjf</span></p>
          </div>
          <div class="text-dark d-flex justify-content-center bg-body-secondary col-lg-2 text-capitalize category">
            {{ activeKeep.description }}
          </div>
        </div>
      </div>
    </ModalWrapper>
  </div>
</template>


<style lang="scss" scoped></style>