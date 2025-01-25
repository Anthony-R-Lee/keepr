<script setup>
import { AppState } from '@/AppState';
import { accountService } from '@/services/AccountService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { computed, ref, watch } from 'vue';


const account = computed(() => AppState.account)


const accountData = ref({
  name: '',
  picture: '',
  coverImg: ''
})
</script>


<template>
  <ModalWrapper id="editAccountModal" modalId="editAccountModal" class="modal-xl">
    <div class="modal-dialog modal-xl bg-body">
      <div v-if="accountData">
        <form action=""></form>
      </div>
    </div>
  </ModalWrapper>
</template>


<style lang="scss" scoped></style>