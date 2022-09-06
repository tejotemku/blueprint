<template>
  <div 
    id="prototype-screen-editor-area"
    @drop.prevent="droppedItem" 
    @dragenter.prevent
    @dragover.prevent
    >
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
import { handleElement } from '../../common/prototypeElementsHandlers.js';

export default {
  name: "PrototypeScreenEditorArea",
  components: {
    DragableElement
  },
  computed: {
    ...mapGetters(['getCurrentScreenElementsData'])
  },
  methods: {
    droppedItem(e) {
      console.log(e);
      const sourceItem = this.$store.getters['getDraggedItem'];
      if (sourceItem) {
        // new object is created to avoid deep copies
        let item = {
          ...sourceItem,
          id: Date.now(),
          top: e.y - e.target.offsetTop,
          left: e.x - e.target.offsetLeft,
        }
        item = handleElement(item);
        this.$store.dispatch('actionAddElementToCurrentScreenElements', item)
        this.$store.dispatch('actionResetDraggedItem');
      }
    },
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