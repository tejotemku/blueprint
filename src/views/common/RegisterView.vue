<template>
    <RegisterForm/>
</template>

<script>
import RegisterForm from '../../components/RegisterForm.vue'
// import router from '../../router';


export default {
  name: 'RegisterView',
  components: {
  RegisterForm
  },
  beforeMount() {
   // TODO: check if logged in and reddirect if necessary
  },
}
</script>

<style>
</style>
