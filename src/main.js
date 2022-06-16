import Vue from 'vue';
import App from './App.vue';
import axios from 'axios';
import Vuetify from '@/plugins/vuetify';
import store from './store';

// Vue router
import VueRouter from 'vue-router';
import router from "./router";

// Vue ont awesome
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { faCircleInfo, faPlusSquare } from '@fortawesome/free-solid-svg-icons';
import '@babel/polyfill'

library.add(faCircleInfo, faPlusSquare
  )
Vue.component('font-awesome-icon', FontAwesomeIcon)

Vue.config.productionTip = false
Vue.use(VueRouter);
axios.defaults.baseURL = 'http://' + window.location.hostname + ':5000/';  // the future backend port

export const vue = new Vue({
  render: h => h(App),
  store,
  router: router,
  vuetify: Vuetify
}).$mount('#app')
