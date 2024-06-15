import 'primevue/resources/themes/aura-light-teal/theme.css';
// import 'primevue/resources/themes/aura-dark-teal/theme.css';

import 'primeicons/primeicons.css';
import 'primeflex/primeflex.css';
import PrimeVue from 'primevue/config';
import InputText from 'primevue/inputtext';
import InputMask from 'primevue/inputmask';
import Badge from 'primevue/badge';
import Button from 'primevue/button';
import Divider from 'primevue/divider';
import ProgressSpinner from 'primevue/progressspinner';
import ToastService from 'primevue/toastservice';
import ConfirmationService from 'primevue/confirmationservice';
import Toast from 'primevue/toast';
import Sidebar from 'primevue/sidebar';
import Tooltip from 'primevue/tooltip';
import Fieldset from 'primevue/fieldset';
import Dialog from 'primevue/dialog';
import Menu from 'primevue/menu';
import ConfirmDialog from 'primevue/confirmdialog';
import Card from 'primevue/card';
import Panel from 'primevue/panel';
import Avatar from 'primevue/avatar';
import Ripple from 'primevue/ripple';

import type { App } from 'vue';

export function usePrimeVue(app: App) {
  app.component('ProgressSpinner', ProgressSpinner);
  app.component('Button', Button);
  app.component('InputMask', InputMask);
  app.component('InputText', InputText);
  app.component('Badge', Badge);
  app.component('Toast', Toast);
  app.component('Divider', Divider);
  app.component('Sidebar', Sidebar);
  app.component('Fieldset', Fieldset);
  app.component('Dialog', Dialog);
  app.component('Menu', Menu);
  app.component('ConfirmDialog', ConfirmDialog);
  app.component('Card', Card);
  app.component('Panel', Panel);
  app.component('Avatar', Avatar);

  app.directive('tooltip', Tooltip);
  app.directive('ripple', Ripple);

  app.use(ConfirmationService);
  app.use(ToastService);
  app.use(PrimeVue);
}
