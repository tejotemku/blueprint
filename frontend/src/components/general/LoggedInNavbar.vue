<template>
  <div class="navbar">
    <div class="navbar-username" @click="goToHomepage">{{getUserName()}}</div>
    <div class="spacer" />
     <slot />
    <v-btn
      v-if="token"
      color="warning"
      @click="logout"
      class="mx-2"
    >
      Logout
    </v-btn>
  </div>
</template>

<script>
import router from '@/router';
import { mapGetters } from "vuex";

export default {
  name: "LoggedInNavbar",
  computed: {
    ...mapGetters({
      username: 'getUsername',
      token: 'getToken'
    }),
  },
  methods: {
    logout() {
      this.$store.dispatch("actionLogOut");
      router.push({ name: 'WelcomePage' });
    },
    getUserName() {
      return this.username || "Blueprint - Guest Mode"
    },
    goToHomepage() {
      router.push({ name: this.username? 'Homepage' : 'WelcomePage' });
    }
  }
}
</script>

<style scoped>
.navbar {
  z-index: 3001;
  height: 75px;
  width: 100%;
  position: fixed;
  top: 0;
  left: 0;
  background-color: gainsboro;
  display: flex;
  align-items: center;
  padding-inline: 30px;
}

.navbar-username {
  font-weight: bold;
  font-size: 1.5rem;
  color: darkslategrey;
}

.spacer {
  flex: 1000;
}

</style>