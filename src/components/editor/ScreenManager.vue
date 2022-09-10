<template>
  <div class="screens-box-container">
    <v-subheader> Screens </v-subheader>
    <div class="screens-box">
    </div>
  </div>
</template>

<script>
  import { mapGetters } from "vuex";
  
export default {
  name: 'ScreenManager',
  computed: {
    ...mapGetters({
      projectAssets: 'getProjectAssets'
    }),
  },
}
</script>


<style scoped>
  .screens-box {
    display: flex;
    flex-direction: column;
  }
  .screens-box-container {
    border: solid black 1px;
    background-color: white;
  }
  </style>