<template>
  <div style="display:flex; flexDirection:column;">
    <h1 style="text-align: center"> Welcome to <br /> Blueprint</h1>
    <v-btn
      color="success"
      class="mb-2 white-text"
      @click="goToLoginPage()"
    >
      Log in
    </v-btn>
    <v-btn
      color="cyan"
      class="mb-4 white-text"
      @click="goToRegisterPage()"
    >
      Register
    </v-btn>
    <div style="display:flex; justify-content:space-around">
      <v-btn
        color="amber"
        class="mr-4 white-text"
        @click="goToGuestPage()"
      >
        Open Guest Mode
      </v-btn>
      <v-dialog width="500">
        <template v-slot:activator="{ on }">
          <v-btn color="red accent-3 white-text" v-on="on">
            <font-awesome-icon icon="circle-info" style="fontSize: 1.5rem"/>
          </v-btn>
        </template>
        <v-card>
          <v-card-title>
            Guest Mode
          </v-card-title>
          <v-card-text>
            Guest mode allows not logged-in users to create a prototype and download it. 
            In this mode all editor tools are avaliable
            <br/>
            <small class="red-text">
              The guest project is not saved in cloud, but is deleted after session ends.   
            </small>
          </v-card-text>
        </v-card>
      </v-dialog>
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
