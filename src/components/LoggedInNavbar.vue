<template>
  <div class="navbar">
    <div class="navbar-username">{{getUserName()}}</div>
    <div class="spacer"/>
    <v-btn
      color="success"
      @click="createNewProject"
      class="mx-2"
    >
      New Project
    </v-btn>
    <v-btn
      color="warning"
      @click="logout"
      class="mx-2"
    >
      Logout
    </v-btn>
  </div>
</template>

<script>
import router from '../router'

export default {
  name: "LoggedInNavbar",
  methods: {
    logout() {
      //TODO: delete user token
      router.push(`/`);
    },
    createNewProject() {
      router.push(`/create-project`);
    },
    getUserName() {
      //TODO: this is a mock
      return "MockUsername"
    },
  }
}
</script>

<style scoped>
.navbar {
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