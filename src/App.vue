<template>
  <v-app id="app">
    <v-main>
      <v-layout align-center justify-center fill-height>
        <router-view />
      </v-layout>
    </v-main>
    <!-- create snackbar here later -->
  </v-app> 
</template>

<script>

export default {
  name: 'App',
  components: {
  }
}
</script>

<style>
.app {
  width: 100vw;
  height: 100vh;
}
</style>
