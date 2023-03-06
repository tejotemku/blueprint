<template>
  <div style="display:flex; flexDirection:column;">
    <h1 style="text-align: center"> Welcome to <br /> Blueprint</h1>
    <div style="display:flex; justify-content:space-around">
      <v-btn
        color="amber"
        class="mr-4 white-text"
        @click="goToGuestPage()"
      >
        Create a prototype
      </v-btn>
    </div>
  </div>
</template>

<script>
import router from '@/router'

export default {
  name: 'WelcomePageView',
  data() {
    return {
    };
  },
  methods: {
    goToLoginPage() {
      router.push({ name: 'Login'});
    },
    goToRegisterPage() {
      router.push({ name: 'Register'});
    },
    goToGuestPage() {
      router.push({ name: 'Guest'});
    },
  }
}
</script>

<style scoped>
.white-text {
  color: white;
}
.red-text {
  color: red;
}

.dialog-box {
  margin: 0 auto;
  width: fit-content;
  padding: 2rem;
}
</style>
