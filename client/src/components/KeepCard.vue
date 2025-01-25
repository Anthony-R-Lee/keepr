<script setup>
import { Keep } from '@/models/Keep';



defineProps({
  keep: { type: Keep, required: true }
})
</script>


<template>
  <div>
    <img :src="keep.img" alt="">
  </div>

</template>


<style lang="scss" scoped></style>