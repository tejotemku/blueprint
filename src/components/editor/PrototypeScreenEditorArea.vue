<template>
  <div class="editor-area">
    <div 
      v-for="el, index in getCurrentScreenElementsData" 
      :key="el.id" 
      :style="'position: absolute; zIndex: ' + index + '; top: ' + el.top + 'px; left: ' + el.left + 'px'" v-html="el.html" 
    />
    <ProjectElement>
      <div :style="'background-color: yellow; width: 100px; height: 30px;'">
        test
      </div>
    </ProjectElement>
  </div>
</template>

<script>
import ProjectElement from "../../components/editor/ProjectElement.vue"
import { mapGetters } from "vuex";

export default {
  name: "PrototypeScreenEditorArea",
  components: {
    ProjectElement
  },
  computed: {
    ...mapGetters(['getCurrentScreenElementsData'])
  }
}
</script>
<style scoped>
.editor-area {
  position: relative;
  width: 800px;
  height: 600px;
  margin: 0 auto;
  background-color: rgb(194, 209, 237);
}
</style>