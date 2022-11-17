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
    <v-snackbar
      v-model="snackbar"
    >
      {{snackbarMessage}}
      <template v-slot:action="{ attrs }">
        <v-btn
          color="red"
          text
          v-bind="attrs"
          @click="closeSnackbar"
        >
          Close
        </v-btn>
      </template>
    </v-snackbar>
  </v-form>
</template>

<script>
import router from '@/router'

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
      snackbar: false,
      snackbarMessage: '',
      snackbarHideTimer: null
    }
  },
  methods: {
    async checkRegister() {
      try {
        let payload = {
          'username': this.username,
          'email': this.email,
          'password': this.password
        }
        await this.$store.dispatch('actionRegister', payload);
        router.push({ name: 'Homepage'});
      }
      catch(e) {
        switch(e.response.status) {
          case 409:
            this.openSnackbar("Register failed, user by this username or email exists");
            break;
          case 500:
            this.openSnackbar("Internal Server Error");
            break;
          default:
          console.log(e);
        }
      }
    },
    goToFrontpage () {
      router.push({ name: 'WelcomePage'});
    },
    openSnackbar(snackbarMessage) {
      this.snackbarMessage = snackbarMessage;
      this.snackbarHideTimer = setTimeout(() => this.closeSnackbar(), 5000);
      this.snackbar = true;
    },
    closeSnackbar() {
      this.snackbar = false;
      this.snackbarHideTimer = false;
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