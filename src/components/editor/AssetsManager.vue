<template>
  <div class="assets-box-container">
    <v-subheader> ASSETS </v-subheader>
    <div class="assets-box">
      <DragAndDropElement 
        v-for="asset in assets" :key="asset"
        :elementInfo="{
          type: 'Image',
          description: 'Asset Image',
          properties: {
            src: asset,
          }
        }" 
      >
        <v-img
          contain
          :max-height="50"
          :max-width="80"
          :src="asset"
        />
      </DragAndDropElement>
    </div>
  </div>
</template>

<script>
import DragAndDropElement from './DragAndDropElement.vue'

export default {
  name: 'AssetsManager',
  components: {
    DragAndDropElement
  },
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