import { defineStore, storeToRefs } from 'pinia';
import { useAppStore } from './app';
import {
  verifyGoogle,
  logout as logoutApi,
  check as checkApi,
  type Account
} from '@/api/login';
import router from '@/router/index';
import { signInWithPopup, type User } from 'firebase/auth';
import { googleAuth, googleProvider } from '@/firebase';
import { ref } from 'vue';

export const useAuthStore = defineStore('auth', () => {
  const homeStore = useAppStore();
  const { toastError } = homeStore;
  const { globalLoading } = storeToRefs(homeStore);
  const loading = ref(false);
  const currentAccount = ref<Account>();

  async function onGoogleAuth(user: User) {
    try {
      loading.value = true;
      const response = await verifyGoogle(user);
      if (response) {
        currentAccount.value = response;
        router.replace({ name: 'home' });
      }
    } catch (ex) {
      toastError(`${ex}`);
    } finally {
      loading.value = false;
    }
  }

  async function loginWithGoogle() {
    try {
      loading.value = true;
      const googleuathResult = await signInWithPopup(
        googleAuth,
        googleProvider
      );
      if (googleuathResult?.user) {
        await onGoogleAuth(googleuathResult.user);
      }
    } catch (ex) {
      toastError(`${ex}`);
    } finally {
      loading.value = false;
    }
  }

  async function logout() {
    try {
      globalLoading.value++;
      await logoutApi();
    } catch (ex) {
      toastError(`${ex}`);
    } finally {
      globalLoading.value--;
      currentAccount.value = undefined;
      router.replace({ name: 'login' });
    }
  }

  async function check(): Promise<boolean> {
    try {
      if (currentAccount.value) {
        return true;
      }

      globalLoading.value++;
      currentAccount.value = await checkApi();
    } catch (error) {
      toastError(`${error}`);
      currentAccount.value = undefined;
    } finally {
      globalLoading.value--;
    }

    return !!currentAccount.value;
  }

  return { loading, currentAccount, loginWithGoogle, logout, check };
});
