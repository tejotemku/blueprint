<template>
  <div 
    id="prototype-screen-editor-area"
    @drop.prevent="droppedItem" 
    @dragenter.prevent
    @dragover.prevent
    @click="resetSelectedItem"
    >
    <DragableElement
      v-for="el, index in processedElements" 
      :key="el.id"
      :top="el.top"
      :left="el.left"
      :zIndex="index"
      v-html="el.html"
      :elementId="el.id"
      :class="isSelectedItem(el.id)"
    />
  </div>
</template>
<script>
import DragableElement from "./DragableElement.vue"
import { mapGetters } from "vuex";
import { generateElementHtml } from '../../common/prototypeElementsHandlers.js';

export default {
  name: "PrototypeScreenEditorArea",
  components: {
    DragableElement
  },
  computed: {
    ...mapGetters({
      currentScreenElements: 'getCurrentScreenElementsData',
      selectedItem: 'getSelectedItemId'
    }),
    processedElements: function () {
      const elements = this.currentScreenElements;
      let processedElements = [];
      elements.forEach( el => {
        const processedElement = {
          id: el.id,
          top: el.top,
          left: el.left,
          html: generateElementHtml(el, true)
        }
        processedElements.push(processedElement);
      })
      return processedElements;
    } 
  },
  methods: {
    droppedItem(e) {
      const sourceItem = this.$store.getters.getDraggedItem;
      if (sourceItem) {
        // new object is created to avoid deep copies
        const prototypeScreenEditorArea = document.getElementById('prototype-screen-editor-area');
        const widthConstraint = prototypeScreenEditorArea.offsetWidth;
        const heightConstraint = prototypeScreenEditorArea.offsetHeight;
        let item = {
          ...sourceItem,
          id: Date.now() + '',
          top: this.between(e.y - e.target.offsetTop, 0, heightConstraint),
          left: this.between(e.x - e.target.offsetLeft, 0, widthConstraint),
          redirect: null,
        }
        this.$store.dispatch('actionAddElementToCurrentScreen', item);
        this.$store.dispatch('actionResetDraggedItem');
      }
    },
    between(x, min_v, max_v) {
      x = Math.min(x, max_v);
      x = Math.max(x, min_v);
      return x;
    },
    resetSelectedItem() {
        this.$store.dispatch('actionResetSelectedElementId');
    },
    isSelectedItem(id) {
      return id == this.selectedItem ? 'selected-item ' : ''; 
    }
  }
}
</script>
<style scoped>
#prototype-screen-editor-area {
  position: relative;
  width: 65vw;
  height: 65vh;
  margin-inline: 0 auto;
  background-color: rgb(238, 240, 225);
  z-index: 2500;
}
.selected-item {
  outline:  3px dashed rgb(48, 137, 201);
  
}
</style>