<template>
  <v-form
    ref="form"
    v-model="valid"
    @submit="(e) => {e.preventDefault(); checkLogin();}"
  >
    <h1>Hi, log in here</h1>
    <v-text-field
      v-model="username"
      :rules="usernameRules"
      label="Username"
      class="mb-2"
      outlined
      dense
    />
    <v-text-field
      v-model="password"
      :rules="passwordRules"
      type="password"
      label="Password"
      class="mb-2"
      outlined
      dense
    />
    <v-btn
      :disabled="!valid"
      color="success"
      class="mr-4"
      type="submit"
    >
      Log In
    </v-btn>
    <!-- <v-btn
      color="error"
      class="mr-4"
      @click="forgotPassword"
    >
      Forgot Password
    </v-btn> -->
    <v-btn
      color="warning"
      @click="goToFrontpage"
    >
      Return to Main Page
    </v-btn>
  </v-form>
</template>

<script>
import router from '../../router'

export default {
  name: 'LoginForm',
  data() {
    return {
      username: null,
      password: null,
      usernameRules: [
        v => !!v || 'Username is required',
      ],
      passwordRules: [
        v => !!v || 'Password is required',
      ],
      valid: false,
    }
  },
  methods: {
    async checkLogin() {
      let payload = {
        'username': this.username,
        'password': this.password,
      };

      if (this.verifyCredentials(payload)) {
        this.acceptLogin();
      }
      else { 
        this.rejectLogin()
      }
    },
    acceptLogin() {
      console.log('Logged in correctly');
      router.push(`/home`);
      // TODO: set user token
    },
    rejectLogin() {
      this.username=null;
      this.password=null;
      console.log('Logged in incorrectly');
      alert("Username or password incorrect.");
    },
    goToFrontpage() {
      router.push("/");
    },
    forgotPassword() {
      //TODO: resetting password
      alert("This function is not implemented yet.");
    },
    verifyCredentials(payload) {
      // TODO: logging in, currently this is a mock
      return payload.username == "SuperKowal", payload.password == "Rumcajs$4"
    }
  }
}
</script>

<style scoped>
</style>