<template>
  <div id="prototype-screen-editor-area">
    <DragableElement
      v-for="el, index in getCurrentScreenElementsData" 
      :key="el.id"
      :top="el.top"
      :left="el.left"
      :zIndex="index"
      v-html="el.html"
    >
    </DragableElement>
  </div>
</template>

<script>
import DragableElement from "./DragableElement.vue"
import { mapGetters } from "vuex";

export default {
  name: "PrototypeScreenEditorArea",
  components: {
    DragableElement
  },
  computed: {
    ...mapGetters(['getCurrentScreenElementsData'])
  }
}
</script>
<style scoped>
#prototype-screen-editor-area {
  position: relative;
  width: 1000px;
  height: 570px;
  margin: 0 auto;
  background-color: rgb(194, 209, 237);
}
</style>