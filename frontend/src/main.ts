import './assets/main.scss';
import { usePrimeVue } from './prime-vue';
import { createApp } from 'vue';
import { createPinia } from 'pinia';
import App from './app.vue';
import router from './router';
import './firebase';

const app = createApp(App);
usePrimeVue(app);
app.use(createPinia());
app.use(router);

app.mount('#app');
