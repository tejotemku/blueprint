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
.row-space-between {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.row-center {
  display: flex;
  flex-direction: row;
  align-items: center;
}
.rectangle {
  width: 80px;
  height: 50px;
  font-size: 0.8rem;
  text-align: center;
  text-overflow: ellipsis;
}
.rectangle-full {
  background-color: #000;
}
.rectangle-hollow {
  border: #000 1px solid;
}
.rectangle-full-rounded {
  background-color: #000;
  border-radius: 10%;
}
.rectangle-hollow-rounded {
  border: #000 1px solid;
  border-radius: 10%;
}
.circle {
  width: 80px;
  height: 80px;
  font-size: 0.8rem;
  text-align: center;
  text-overflow: ellipsis;
}
.circle-full {
  border-radius: 50%;
  background: #000;
}
.circle-hollow {
  border-radius: 50%;
  border: #000 1px solid;
}
.header-big {
  font-size: larger;
}
.header-xx-large {
  font-size: xx-large;
}
.header-small {
  font-size: small;
}
.header-tiny {
  font-size: xx-small;
}
</style>
