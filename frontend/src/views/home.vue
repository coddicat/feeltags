<script setup lang="ts">
import { useAuthStore } from '@/store/auth';
import { storeToRefs } from 'pinia';
function onAvatar() {
  //
}
const authStore = useAuthStore();
const { currentAccount } = storeToRefs(authStore);
</script>

<template>
  <div class="home">
    <div class="home__title">
      <span>FeelTags</span>
    </div>

    <Button rounded @click="onAvatar" text size="large" class="p-2">
      <Avatar :image="currentAccount?.picture" size="large" shape="circle" />
    </Button>

    <div>{{ currentAccount }}</div>
  </div>
</template>

<style lang="scss">
.home {
  &__title {
    font-family: monospace;
    font-size: 3rem;
    color: #330;
    font-weight: 600;
    text-shadow: 0.3rem 0.3rem #fcc;
    pointer-events: none;
    opacity: 1;
    // transition: all 1s ease-in-out;
    margin-bottom: 18rem;
    text-align: center;
  }
}
</style>
