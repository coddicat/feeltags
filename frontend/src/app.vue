<script setup lang="ts">
import { useAppStore } from '@/store/app';
import { useToast } from 'primevue/usetoast';
import { useConfirm } from 'primevue/useconfirm';
import { onBeforeUnmount, onMounted, ref } from 'vue';

const { init } = useAppStore();
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
  <RouterView #="{ Component }">
    <transition mode="out-in" name="fade">
      <component :is="Component" :key="$route.fullPath" />
    </transition>
  </RouterView>
  <Toast position="bottom-center" />
  <ConfirmDialog class="mx-4" :draggable="false" />
</template>

<style lang="scss">
.app {
  //
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
