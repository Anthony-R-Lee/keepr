<script setup>
import KeepCreateForm from './KeepCreateForm.vue';



defineProps({
  modalId: { type: String, required: true },
  modalTitle: { type: String, required: true },
})
</script>


<template>
  <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title" :id="modalId + 'Label'">{{ modalTitle }}</h1>
          <button class="btn-close" type="button" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <KeepCreateForm />
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>