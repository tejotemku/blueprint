<template>
    <LoginForm/>
</template>

<script>
import LoginForm from '../../components/general/LoginForm.vue'


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
