import { defineStore } from 'pinia';
import type { ConfirmationOptions } from 'primevue/confirmationoptions';
import type { ToastServiceMethods } from 'primevue/toastservice';
import { computed, ref } from 'vue';

export type ConfirmObject = {
  require: (option: ConfirmationOptions) => void;
  close: () => void;
};

export const useAppStore = defineStore('app', () => {
  const toast = ref<ToastServiceMethods>();
  const confirm = ref<ConfirmObject>();
  const globalLoading = ref(0);

  function init(_toast: ToastServiceMethods, _confirm: ConfirmObject) {
    toast.value = _toast;
    confirm.value = _confirm;
  }

  function toastFailure(summary: string, detail: string) {
    toast.value?.add({
      summary: summary,
      detail: detail,
      severity: 'error',
      life: 5000,
      closable: false
    });
  }
  function toastSuccess() {
    toast.value?.add({
      summary: 'Success',
      severity: 'success',
      life: 2000,
      closable: false
    });
  }
  function toastError(summary: string) {
    toast.value?.add({
      summary: summary,
      severity: 'error',
      closable: true
    });
  }

  const isMobile = computed(() =>
    /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(
      navigator.userAgent
    )
  );
  const isDev = computed(() => import.meta.env.MODE === 'development');
  const version = computed(() => import.meta.env.VITE_APP_VERSION);

  return {
    globalLoading,
    isDev,
    version,
    isMobile,
    init,
    toastError,
    toastFailure,
    toastSuccess
  };
});
