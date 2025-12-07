<template>
  <button class="circle">
    {{ msg }}
  </button>
</template>

<script setup>
import { defineProps } from "vue";

defineProps({
  msg: {
    type: String,
    default: "+"
  }
});
</script>

<style scoped>
.circle {
  height: 35px;
  width: 35px;
  background: transparent;
  border-radius: 50%;
  border: 1px solid #1B1A17;
  color: #1B1A17;
  cursor: pointer;
  font-size: 20px;

  display: flex;
  justify-content: center;
  align-items: center;
}

.circle:hover {
  border: 1px solid #6D8B74;
  color: #6D8B74;
}
</style>
