import Router from 'vue-router'
// main pages
import WelcomePageView from '../views/common/WelcomePageView.vue'
import LoginView from '../views/common/LoginView.vue'
import RegisterView from '../views/common/RegisterView.vue'
import HomepageView from '../views/common/HomepageView.vue'
// error pages
import PageNotFound from '../views/error/err404page.vue'
export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'WelcomePage',
      component: WelcomePageView
    },
    {
      path: '/login',
      name: 'LoginView',
      component: LoginView
    },
    {
      path: '/register',
      name: 'RegisterView',
      component: RegisterView
    },
    {
      path: '/home',
      name: 'HomepageView',
      component: HomepageView

    },
    {
      path: '*',
      name: 'PageNotFound',
      component: PageNotFound
    },
  ]
})