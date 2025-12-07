<template>
  <button class="regular-btn">
    <p>{{ text }}</p>
  </button>
</template>

<script setup>
import { defineProps } from "vue";

defineProps({
  text: {
    type: String,
    default: "text"
  }
});
</script>

<style scoped>
.regular-btn {
  max-width: max-content;
  min-width: min-content;
  text-align: center;
  padding: 5px 15px;
  background-color: #3A5A40;
  border-radius: 10px;
  border: none;
  cursor: pointer;
}

.regular-btn > p {
  color: #FAF7F2 !important;
}

.regular-btn:hover {
  background-color: #6D8B74;
}
</style>