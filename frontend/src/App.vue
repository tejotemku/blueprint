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
.rectangle {
  width: 80px;
  height: 50px;
  font-size: 0.8rem;
  text-align: center;
  text-overflow: ellipsis;
}

.rectangle-full {
  background-color: brown;
}
.rectangle-hollow {
  border: black 1px solid;
}
.rectangle-full-rounded {
  background-color: rgb(5, 230, 5);
  border-radius: 10%;
}
.rectangle-hollow-rounded {
  border: red 1px solid;
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
  background: rgb(125, 25, 164);
}
.circle-hollow {
  border-radius: 50%;
  border: rgb(25, 162, 164) 1px solid;
}
</style>
