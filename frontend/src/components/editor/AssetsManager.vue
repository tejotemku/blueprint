<template>
  <div class="assets-box-container">
    <v-subheader> 
      IMAGES
      <v-btn icon color="#00f" @click="openNewAssetTool">
        <font-awesome-icon icon="plus-square" style="fontSize: 1rem"/>
      </v-btn>
    </v-subheader>
    <div class="assets-box">
      <DragAndDropElement 
        v-for="asset in projectAssets" 
        :key="asset"
        :elementInfo="{
          type: 'Image',
          description: 'Image',
          properties: {
            src: asset,
            width: 0,
            height: 0,
            imageLimitMode: 'scale',
            redirect: null,
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
import { mapGetters } from "vuex";

export default {
  name: 'AssetsManager',
  components: {
    DragAndDropElement
  },
  computed: {
    ...mapGetters({
      projectAssets: 'getProjectAssets'
    }),
  },
  methods: {
    openNewAssetTool() {
      this.$emit('newAssetTool:show');
    }
  }
}
</script>

<style scoped>
.assets-box {
  display: flex;
  flex-wrap: wrap;
}
.assets-box-container {
  position: fixed;
  z-index: 3000;
  bottom: 0px;
  left: 15%;
  width: 70%;
  height: 20%;
  border: solid #dee2e6 1px;
  background-color: white;
  overflow-y: auto;
  border: solid #dee2e6 1px;
  background-color: white;
}
</style>