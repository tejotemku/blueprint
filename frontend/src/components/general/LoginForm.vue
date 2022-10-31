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
    }
  },
  computed: {
    ...mapGetters({
      token: 'getToken'
    }),
  },
  methods: {
    acceptLogin() {
      router.push(`/home`);
    },
    rejectLogin() {
      this.username=null;
      this.password=null;
      //TODO: change to snackbar
      // alert("Username or password incorrect.");
    },
    goToFrontpage() {
      router.push("/");
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
        console.log(e);
        this.rejectLogin();
      }
    }
  }
}
</script>

<style scoped>
</style>