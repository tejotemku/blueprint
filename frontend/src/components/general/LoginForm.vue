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
import router from '@/router';
import { mapGetters } from "vuex";

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
      snackbar: false,
      snackbarMessage: '',
      snackbarHideTimer: null
    }
  },
  computed: {
    ...mapGetters({
      token: 'getToken'
    }),
  },
  methods: {
    acceptLogin() {
      router.push({ name: 'Homepage' });
    },
    rejectLogin() {
      this.password=null;
    },
    goToFrontpage() {
      router.push({ name: 'WelcomePage' });
    },
    async checkLogin() {
      try {
        let payload = {
          username: this.username,
          password: this.password,
        }
        await this.$store.dispatch('actionLogin', payload);
        this.acceptLogin();
      }
      catch(e) {
        console.log(e.response.status)
        switch(e.response.status) {
          case 401:
            this.openSnackbar("Login failed, check username and password");
            this.rejectLogin();
            break;
          case 500:
            this.openSnackbar("Internal Server Error");
            break;
          default:
            this.rejectLogin();
            break;
        }
      }
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
</style>