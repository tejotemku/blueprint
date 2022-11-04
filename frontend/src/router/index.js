import Router from 'vue-router'
import store from '@/store'
// common pages
import WelcomePageView from '@/views/common/WelcomePageView.vue'
import LoginView from '@/views/common/LoginView.vue'
import RegisterView from '@/views/common/RegisterView.vue'
// loggedIn pages@/views/user/HomepageView.vue
import HomepageView from '@/views/user/HomepageView.vue'
import CreateProject from '@/views/user/CreateProject.vue'
import ProjectEditor from '@/views/common/ProjectEditor.vue'
// error pages
import PageNotFound from '@/views/error/err404page.vue'
export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'WelcomePage',
      component: WelcomePageView,
      beforeEnter: async (to, from, next) => {
        await store.dispatch("actionCheckLoggedIn");
        if (store.getters["getToken"]) next({ name: "Homepage" });
        else next();
      }
    },
    {
      path: '/guest',
      name: 'Guest',
      component: ProjectEditor,
      beforeEnter: async (to, from, next) => {
        await store.dispatch("actionCheckLoggedIn");
        if (store.getters["getToken"]) next({ name: "Homepage" });
        else next();
      }

    },
    {
      path: '/login',
      name: 'Login',
      component: LoginView,
      beforeEnter: async (to, from, next) => {
        await store.dispatch("actionCheckLoggedIn");
        if (store.getters["getToken"]) next({ name: "Homepage" });
        else next();
      }
    },
    {
      path: '/register',
      name: 'Register',
      component: RegisterView,
      beforeEnter: async (to, from, next) => {
        await store.dispatch("actionCheckLoggedIn");
        if (store.getters["getToken"]) next({ name: "Homepage" });
        else next();
      }
    },
    {
      path: '/home',
      name: 'Homepage',
      component: HomepageView,
      beforeEnter: async (to, from, next) => {
        await store.dispatch("actionCheckLoggedIn");
        if (store.getters["getToken"]) next();
        else next({ name: "Login" });
      }
    },
    {
      path: '/create-project',
      name: 'UserCreateProject',
      component: CreateProject,
      beforeEnter: async (to, from, next) => {
        await store.dispatch("actionCheckLoggedIn");
        if (store.getters["getToken"]) next();
        else next({ name: "Login" });
      }
    },
    {
      path: '/project/:id',
      name: 'UserProjectEditor',
      component: ProjectEditor,
      beforeEnter: async (to, from, next) => {
        await store.dispatch("actionCheckLoggedIn");
        if (store.getters["getToken"]) next();
        else next({ name: "Login" });
      }
    },
    {
      path: '*',
      name: 'PageNotFound',
      component: PageNotFound
    },
  ]
})