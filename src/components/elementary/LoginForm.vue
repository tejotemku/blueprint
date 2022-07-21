<template>
  <v-form
    ref="form"
    v-model="valid"
    @submit="(e) => {e.preventDefault(); checkLogin();}"
  >
    <h1>Hi, log in here</h1>
    <v-text-field
      v-model="email"
      :rules="emailRules"
      label="E-mail"
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
    <v-btn
      color="error"
      class="mr-4"
      @click="forgotPassword"
    >
      Forgot Password
    </v-btn>
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
      email: null,
      password: null,
      emailRules: [
        v => !!v || 'Email is required',
        v => /.+@.+\..+/.test(v) || 'Incorrect email adress',
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
        'email': this.email,
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
      console.log('Logged in correctly :D');
      router.push(`/home`);
      // TODO: set user token
    },
    rejectLogin() {
      this.email=null;
      this.password=null;
      console.log('Logged in incorrectly :X');
      alert("Email or password incorrect.");
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
      return payload.email == "test@example.com", payload.password == "Rumcajs$4"
    }
  }
}
</script>

<style scoped>
</style>