<template>
  <div class="assets-box-container">
    <v-subheader> ASSETS </v-subheader>
    <div class="assets-box">
      <DragAndDropElement 
        v-for="asset in projectAssets" 
        :key="asset"
        :elementInfo="{
          type: 'Image',
          description: 'Image',
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
}
</script>

<style scoped>
.assets-box {
  display: flex;
  flex-wrap: wrap;
}
.assets-box-container {
  border: solid #dee2e6 1px;
  background-color: white;
}
</style>