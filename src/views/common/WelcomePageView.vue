<template>
  <div style="display:flex; flexDirection:column;">
    <h1 style="text-align: center"> Welcome to <br /> Blueprint</h1>
    <v-btn
      color="success"
      class="mr-4"
      @click="goToLoginPage()"
      style="margin-block: 10px"
    >
      Log in
    </v-btn>
    <v-btn
      color="cyan"
      class="mr-4"
      @click="goToRegisterPage()"
      style="margin-block: 10px; color: white"
    >
      Register
    </v-btn>
    <div style="display:flex; justify-content:space-around">
      <v-btn
        color="amber"
        class="mr-4"
        @click="goToGuestPage()"
        style="margin-block: 10px; color: white"
      >
        Open Guest Mode
      </v-btn>
      <v-btn
        color="red accent-3"
        class="mr-4"
        @click="goToAboutGuestPage()"
        style="margin-block: 10px; color: white"
      >
        <font-awesome-icon icon="circle-info" style="fontSize: 1.5rem"/>
      </v-btn>
    </div>
  </div>
</template>

<script>
import router from '../../router'

export default {
  name: 'WelcomePageView',
  data() {
    return {
    };
  },
  methods: {
    goToLoginPage() {
      router.push('/login');
    },
    goToRegisterPage() {
      router.push('/register');
    },
    goToGuestPage() {
      router.push('/guest');
    },
    goToAboutGuestPage() {
      alert('clicked "about guest" button');
      // create a popup that explains what a guest mode is
    }
  }
}
</script>

<style scoped>

</style>
