<script setup>
import KeepCreateForm from './KeepCreateForm.vue';



defineProps({
  modalId: { type: String, required: true },
  modalTitle: { type: String },
})
</script>


<template>
  <div class="modal fade" :id="modalId" tabindex="-1" :aria-labelledby="modalId + 'Label'" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-body">
          <slot></slot>
        </div>
        <!-- <div class="modal-header">
          <h1 class="modal-title" :id="modalId + 'Label'">{{ modalTitle }}</h1>
        </div> -->
        <!-- <button class="btn-close" type="button" data-bs-dismiss="modal" aria-label="Close"></button> -->
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>