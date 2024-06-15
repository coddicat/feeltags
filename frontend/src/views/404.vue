<script setup lang="ts">
import { useRouter } from 'vue-router';
const { replace } = useRouter();
function onNavigateHome() {
  return replace({ name: 'home' });
}
</script>
<template>
  <div class="flex flex-column">
    <div class="flex-grow-1"></div>
    <div class="flex flex-column flex-grow-0">
      <h2>Oops! Looks like you're exploring uncharted territory.</h2>
      <Button link label="Navigate Home" @click="onNavigateHome" />
    </div>
    <div class="flex-grow-1"></div>
  </div>
</template>
