<template>
  <v-form
    ref="form"
    v-model="valid"
    @submit="(e) => {e.preventDefault(); checkRegister();}"
  >
    <h1>Hi, register a new Blueprint account</h1>
    <v-text-field
      v-model="email"
      :rules="emailRules"
      label="E-mail"
      class="mb-2"
      outlined
      dense
    />
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
    <v-text-field
      v-model="passwordConfirm"
      :rules="passwordConfirmRules"
      type="password"
      label="Confirm password"
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
      Register
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
  name: 'RegisterForm',
  data() {
    return {
      email: null,
      username: null,
      password: null,
      passwordConfirm: null,
      emailRules: [
        v => !!v || 'Email is required',
        v => /.+@.+\..+/.test(v) || 'Incorrect email adress'
      ],
      usernameRules: [
        v => !!v || 'Username is required'
      ],
      passwordRules: [
        v => !!v || 'Password is required',
        v => /^(?=.*\d)(?=.*[A-Z])(?=.*[a-z]).{8,32}/.test(v) || 'Password must be 8-32 characters long, contain lowercase and uppercase letter, number'
      ],
      passwordConfirmRules: [
        v => !!v && this.password == this.passwordConfirm || 'Passwords must match'
      ],
      valid: false,
    }
  },
  methods: {
    async checkRegister() {
      let payload = {
        'email': this.email,
        'username': this.username,
        'password': this.password,
      };
      if (!this.checkCredentialAvaliability(payload)) {
        alert("Error: cannot register");
        return;
        }
      // TODO: registering in, currently this is a mock
      router.push("/home");
      // TODO: delete after payload has diffrent job
      console.log(payload);
    },
    goToFrontpage () {
      router.push("/");
    },
    checkCredentialAvaliability(payload) {
      // TODO: check if email or username is used
      // MOCK
      return payload.username != "mock_error";
    }
  }
}
</script>

<style scoped>
.register-box {
  padding-block: 2vw;
  padding-inline: 2.5vw;
  font-size: 2vw;
}
</style>