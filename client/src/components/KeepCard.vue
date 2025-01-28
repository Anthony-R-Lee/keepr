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
import { useRoute } from 'vue-router';
import { profilesService } from '@/services/ProfilesService';

const activeKeep = computed(() => AppState.activeKeep)

const account = computed(() => AppState.account)

const route = useRoute()

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

async function deleteKeep(keepId) {
  try {
    const confirm = await Pop.confirm(`Do you really want to delete ${props.keep.name}?`)

    if (!confirm) return

    await keepsService.deleteKeep(keepId)
  }
  catch (error) {
    Pop.meow(error);
    logger.error("DELETING KEEP", error);

  }
}
</script>


<template>
  <div v-if="keep" class="card shadow-lg box-shadow" :style="{ backgroundImage: `url(${keep?.img})` }">
    <div>

      <div v-if="account?.id == keep.creatorId" class="delete-btn">
        <button @click="deleteKeep(props.keep.id)" class="btn btn-danger"><i class="mdi mdi-close"></i></button>
      </div>
      <div class="justify-content-between bg-img">
        <div @click="getKeepById(props.keep.id)" data-bs-toggle="modal" data-bs-target="#keepDetailModal"
          class="title text-light text-capitalize d-flex align-items-end">
          <b>
            {{ props.keep?.name }}
          </b>
        </div>
        <RouterLink :to="{ name: 'Profile', params: { profileId: keep.creatorId } }" class="" role="button">
          <img :src="keep.creator.picture" :title="keep.creator.name" class="profile-img" alt="">
        </RouterLink>
      </div>
    </div>
  </div>
  <ModalWrapper class="modal-xl" id="keepDetailModal" modalId="keepDetailModal">

    <KeepDetailModal />
  </ModalWrapper>
</template>


<style lang="scss" scoped>
.card {
  min-height: 10em;
  background-position: center;
  background-size: cover;
  // margin: 1.75em;
  border: none;
  width: 100%;
  position: relative;
  display: inline-block;

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
}

.delete-btn {
  position: absolute;
  top: 0;
  right: 0;
  margin: -0.5em;
  border-radius: 100em;
  max-height: 10px;
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