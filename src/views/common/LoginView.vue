<template>
    <LoginForm/>
</template>

<script>
import LoginForm from '../../components/elementary/LoginForm.vue'
// import router from '../../router';


export default {
  name: 'LoginView',
  components: {
  LoginForm
  },
  beforeMount() {
   // TODO: check if logged in and reddirect if necessary
  },
}
</script>

<style>
</style>
