<script setup lang="ts">
import { useAuthStore } from '@/store/auth';
import { storeToRefs } from 'pinia';

const authStore = useAuthStore();
const { loading } = storeToRefs(authStore);
const { loginWithGoogle } = authStore;

async function onLoginGoogle() {
  await loginWithGoogle();
}
</script>

<template>
  <div class="login flex flex-column">
    <div class="flex-grow-1"></div>
    <div class="flex-grow-0">
      <ProgressSpinner
        v-show="loading"
        stroke-width="0.4rem"
        class="login__progress"
      />
      <Button
        v-show="!loading"
        label="Login with Google"
        severity="danger"
        icon="pi pi-google"
        @click="onLoginGoogle"
        size="large"
        class="login__btn-google"
      />
    </div>
    <div class="flex-grow-1"></div>
  </div>
</template>

<style lang="scss">
.login {
  height: 100vh;
  &__btn-google {
    margin-bottom: 0.5rem;
    width: 243px;
    height: 40px;
  }
  &__progress {
    height: 3rem;
  }
}
</style>
