import Home from '@/views/home.vue';
import { createRouter, createWebHistory } from 'vue-router';
import View404 from '@/views/404.vue';
import { useAuthStore } from '@/store/auth';

const routes = [
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/login',
    name: 'login',
    meta: {
      allowAnonymous: true
    },
    component: () => import('@/views/login.vue')
  },

  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: View404,
    meta: {
      allowAnonymous: true
    }
  }
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
});

router.beforeEach(async to => {
  if (to.meta.allowAnonymous) {
    return true;
  }

  const authStore = useAuthStore();
  const { check } = authStore;
  const authenticated = await check();
  if (authenticated) {
    return true;
  }

  return {
    name: 'login',
    replace: true
  };
});

export default router;
