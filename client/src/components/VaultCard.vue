<script setup>
import { AppState } from '@/AppState';
import { Vault } from '@/models/Vault';
import { vaultsService } from '@/services/VaultsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()

const profileVaults = computed(() => AppState.profileVaults)

const account = computed(() => AppState.account)

defineProps({
  vault: { type: Vault, required: true }
})

</script>


<template>
  <div v-if="vault" class="card shadow-lg box-shadow" :style="{ backgroundImage: `url(${vault?.img})` }">
    <RouterLink :to="{ name: 'Vault', params: { vaultId: vault.id } }">

      <div>
        <div class="justify-content-between bg-img">
          <div class="title text-light text-capitalize d-flex align-items-end">
            <b>
              {{ vault.name }}
            </b>
          </div>
        </div>
      </div>
    </RouterLink>
  </div>
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