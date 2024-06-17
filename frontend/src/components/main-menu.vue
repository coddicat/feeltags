<script setup lang="ts">
import { useAuthStore } from '@/store/auth';
import { storeToRefs } from 'pinia';
import Menu from 'primevue/menu';
import type { MenuItem } from 'primevue/menuitem';
import { ref } from 'vue';

const authStore = useAuthStore();
const { currentAccount } = storeToRefs(authStore);
const { logout } = authStore;

const menu = ref<Menu>();
const items = ref<MenuItem[]>([
  {
    label: 'Sign out',
    icon: 'pi pi-sign-out',
    command: logout
  },
  {
    separator: true
  },
  {
    label: 'Permanently Delete Account',
    icon: 'pi pi-trash'
  }
]);

const toggle = (event: Event) => {
  menu.value?.toggle(event);
};
</script>
<template>
  <Menubar class="main-menu border-noround">
    <template #start>
      <Avatar image="logo.png" shape="circle" />
      <span class="main-menu__title">FeelTags</span>
    </template>
    <template #end>
      <Button
        rounded
        text
        class="p-1"
        severity="info"
        size="small"
        @click="toggle"
      >
        <div class="flex flex-column align-items-start mx-3">
          <span class="main-menu__avatat-info">
            {{ currentAccount?.name }}
          </span>
          <span class="main-menu__avatat-info">
            {{ currentAccount?.email }}
          </span>
        </div>

        <Avatar :image="currentAccount?.picture" size="normal" shape="circle" />
      </Button>
      <Menu ref="menu" class="main-avatar-menu" :model="items" popup />
    </template>
  </Menubar>
</template>

<style lang="scss">
.main-avatar-menu {
  .p-submenu-header {
    display: none;
  }
}

.main-menu {
  height: 4rem;
  background-color: #fff;

  .p-menubar-start {
    display: flex;
    align-items: center;
    gap: 0.5rem;
  }

  &__title {
    font-family: monospace;
    font-size: 1.5rem;
    color: #330;
    font-weight: 600;
    pointer-events: none;
  }

  &__avatat-info {
    max-width: 12rem;
    white-space: nowrap;
    overflow-x: hidden;
    text-overflow: ellipsis;
  }
}
</style>
