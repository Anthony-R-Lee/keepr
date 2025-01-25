<script setup>
import { Keep } from '@/models/Keep';
import ModalWrapper from './ModalWrapper.vue';
import Pop from '@/utils/Pop';
import { logger } from '@/utils/Logger';
import { keepsService } from '@/services/KeepsService';
import { Modal } from 'bootstrap';
import { computed } from 'vue';
import { AppState } from '@/AppState';
import KeepDetailModal from './KeepDetailModal.vue';
import ProfilePage from '@/pages/ProfilePage.vue';

const activeKeep = computed(() => AppState.activeKeep)

const account = computed(() => AppState.account)



const props = defineProps({
  keep: { type: Keep, required: true }
})

async function getKeepById(keepId) {
  try {
    await keepsService.getKeepById(keepId)
    Modal.getInstance('#keepDetailModal').show()
  }
  catch (error) {
    Pop.meow(error);
    logger.error("GETTING KEEP BY ID", error)
  }
}
</script>


<template>
  <div v-if="keep" @click="getKeepById(keep.id)" data-bs-toggle="modal" data-bs-target="#keepDetailModal" role="button"
    class="card shadow-lg d-flex box-shadow" :style="{ backgroundImage: `url(${keep?.img})` }">
    <div v-if="account?.id == props.keep.creatorId" class="delete-btn">
      <button class="btn btn-danger"><i class="mdi mdi-close"></i></button>
    </div>
    <div class="d-flex justify-content-between">
      <div class="title text-light text-capitalize d-flex align-items-end">
        <b>
          {{ keep?.name }}
        </b>
      </div>
      <RouterLink to="/profile" class="">
        <img :src="keep.creator.picture" class="profile-img" alt="">
      </RouterLink>
    </div>
  </div>

  <ModalWrapper modalId="keepDetailModal" modalTitle="Keep Detail">

    <KeepDetailModal />
  </ModalWrapper>
</template>


<style lang="scss" scoped>
.card {
  min-height: 10em;
  background-position: center;
  background-size: cover;
  margin: 1.75em;
  border: none;
}

.delete-btn {
  position: absolute;
  top: 0;
  right: 0;
  margin: -0.5em;
  border-radius: 100em;
  max-height: 10px;
}

.profile-img {
  position: absolute;
  bottom: 0;
  right: 0;
  margin: 0.5em;
  border-radius: 50%;
  max-height: 3em;
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
}
</style>