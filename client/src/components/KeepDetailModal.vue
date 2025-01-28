<script setup>
import { Keep } from '@/models/Keep';
import KeepCard from './KeepCard.vue';
import { computed } from 'vue';
import { AppState } from '@/AppState';
import ModalWrapper from './ModalWrapper.vue';
import { Vault } from '@/models/Vault';

const activeKeep = computed(() => AppState.activeKeep)

defineProps({
  keep: { type: Keep },
  vault: { type: Vault }
})
</script>


<template>
  <div class="modal-dialog modal-xl bg-body " id="keepDetailModal" modalId="keepDetailModal">
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
        <div class="text-dark d-flex justify-content-center mx-3 my-2 px-5">
          {{ activeKeep.description }}
        </div>
        <div class="d-flex justify-content-between align-items-end px-5">
          <div class="d-flex align-items-end">
            <div class="d-flex align-items-end">
              <div class="dropdown">
                <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                  VAULTS
                </button>
                <ul class="dropdown-menu">
                  <li><a class="dropdown-item" href="#">Action</a></li>
                  <li><a class="dropdown-item" href="#">Another action</a></li>
                  <li><a class="dropdown-item" href="#">Something else here</a></li>
                </ul>
              </div>
              <div>
                <button class="btn btn-secondary text-light">save</button>
              </div>
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
  bottom: 0;
  right: 0;
}

.profile-info img {
  border-radius: 50%;
  max-height: 2em;
  margin-right: 5px;
  margin: 4px;
}
</style>