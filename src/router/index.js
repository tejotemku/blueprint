import Router from 'vue-router'
import WelcomePage from '../views/Welcome.vue'
export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'WelcomePage',
      component: WelcomePage
    },
  ]
})