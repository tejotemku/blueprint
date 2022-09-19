import Router from 'vue-router'
// common pages
import WelcomePageView from '../views/common/WelcomePageView.vue'
import LoginView from '../views/common/LoginView.vue'
import RegisterView from '../views/common/RegisterView.vue'
// loggedIn pages../views/user/HomepageView.vue
import HomepageView from '../views/user/HomepageView.vue'
import CreateProject from '../views/user/CreateProject.vue'
import ProjectEditor from '../views/common/ProjectEditor.vue'
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
      path: '/create-project',
      name: 'CreateProject',
      component: CreateProject

    },
    {
      path: '/project/:id',
      name: 'ProjectEditor',
      component: ProjectEditor
    },
    {
      path: '*',
      name: 'PageNotFound',
      component: PageNotFound
    },
  ]
})