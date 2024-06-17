import 'primevue/resources/themes/aura-light-teal/theme.css';
// import 'primevue/resources/themes/aura-dark-teal/theme.css';

import 'primeicons/primeicons.css';
import 'primeflex/primeflex.css';
import PrimeVue from 'primevue/config';
import Button from 'primevue/button';
import ProgressSpinner from 'primevue/progressspinner';
import ToastService from 'primevue/toastservice';
import ConfirmationService from 'primevue/confirmationservice';
import Toast from 'primevue/toast';
import Fieldset from 'primevue/fieldset';
import Menu from 'primevue/menu';
import ConfirmDialog from 'primevue/confirmdialog';
import Avatar from 'primevue/avatar';
import Menubar from 'primevue/menubar';
import Skeleton from 'primevue/skeleton';

// import Ripple from 'primevue/ripple';
// import Card from 'primevue/card';
// import Panel from 'primevue/panel';
// import InputText from 'primevue/inputtext';
// import InputMask from 'primevue/inputmask';
// import Badge from 'primevue/badge';
// import Divider from 'primevue/divider';
// import Sidebar from 'primevue/sidebar';
// import Tooltip from 'primevue/tooltip';
// import Dialog from 'primevue/dialog';

import type { App } from 'vue';

export function usePrimeVue(app: App) {
  app.component('ProgressSpinner', ProgressSpinner);
  app.component('Button', Button);
  app.component('Toast', Toast);
  app.component('Skeleton', Skeleton);
  app.component('Fieldset', Fieldset);
  app.component('Menu', Menu);
  app.component('ConfirmDialog', ConfirmDialog);
  app.component('Avatar', Avatar);
  app.component('Menubar', Menubar);

  // app.component('InputMask', InputMask);
  // app.component('InputText', InputText);
  // app.component('Badge', Badge);
  // app.component('Divider', Divider);
  // app.component('Sidebar', Sidebar);
  // app.component('Dialog', Dialog);
  // app.component('Card', Card);
  // app.component('Panel', Panel);
  // app.directive('tooltip', Tooltip);
  // app.directive('ripple', Ripple);

  app.use(ConfirmationService);
  app.use(ToastService);
  app.use(PrimeVue);
}
