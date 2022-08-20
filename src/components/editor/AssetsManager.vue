<template>
  <div class="assets-box-container">
    <v-subheader> ASSETS </v-subheader>
    <div class="assets-box">
      <div 
        v-for="asset in assets" :key="asset">
        <v-img
          contain
          :max-height="50"
          :max-width="80"
          :src="asset"
        />
      </div>
    </div>
  </div>
</template>

<script>

export default {
  name: 'AssetsManager',
  data() {
    return {
      assets: [],
      selectedItem: null,
    }
  },
  methods:  {
    getAssets() {
      this.assets = this.$store.getters["getProjectAssets"];
    },
  },
  beforeMount() {
    this.getAssets();
  },

}
</script>

<style scoped>
.assets-box {
  display: flex;
  flex-wrap: wrap;
}
.assets-box-container {
  border: solid black 1px;
  background-color: white;
}
</style>