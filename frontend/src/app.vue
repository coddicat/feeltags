<script setup lang="ts">
import { useAppStore } from '@/store/app';
import { useToast } from 'primevue/usetoast';
import { useConfirm } from 'primevue/useconfirm';
import { onBeforeUnmount, onMounted, ref } from 'vue';
import Header from '@/components/main-menu.vue';
import { storeToRefs } from 'pinia';
import { getLocation } from '@/exts';

const appStore = useAppStore();
const { init } = appStore;
const { globalLoading } = storeToRefs(appStore);
init(useToast(), useConfirm());

const windowWidth = ref(0);
const windowHeight = ref(0);

const resizeObserver = new ResizeObserver(entries => {
  for (const entry of entries) {
    if (entry.target === document.body) {
      handlerWindowResize();
    }
  }
});

onMounted(async () => {
  resizeObserver.observe(document.body);
  getLocation();
});

onBeforeUnmount(() => {
  resizeObserver.disconnect();
});

function handlerWindowResize() {
  windowWidth.value = document.body.clientWidth;
  windowHeight.value = document.body.clientHeight;
}
</script>

<template>
  <div class="app">
    <Header />
    <RouterView #="{ Component }">
      <transition mode="out-in" name="fade">
        <component :is="Component" :key="$route.fullPath" />
      </transition>
    </RouterView>
    <Toast position="bottom-center" />
    <ConfirmDialog class="mx-4" :draggable="false" />
    <div v-if="globalLoading > 0" class="app__global-loading">
      <ProgressSpinner strokeWidth="4" />
    </div>
  </div>
</template>

<style lang="scss">
.app {
  height: 100vh;
  background-color: #fffff9;
  border-left: 1px solid #f9f9f9;
  border-right: 1px solid #f9f9f9;

  &__global-loading {
    display: flex;
    justify-content: center;
    align-items: center;
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 9999;
    background-color: rgba(60, 60, 60, 0.6);
  }
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}
</style>
