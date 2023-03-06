<template>
  <div 
    id="prototype-screen-editor-area"
    ref="prototype-screen-editor-area"
    @drop.prevent="droppedItem" 
    @dragenter.prevent
    @dragover.prevent
    @click="resetSelectedItem"
    :style="{
      width: `${this.projectWidth}px`,
      height: `${this.projectHeight}px`,
      transform: `scale(${this.projectScale})`,
      transformOrigin: 'top center',
  }"
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
      ref="dragableElement"
    />
  </div>
</template>
<script>
import DragableElement from "./DragableElement.vue";
import { mapGetters } from "vuex";
import { generateElementHtml } from '@/utils/prototypeElementsHandlers.js';

export default {
  name: "PrototypeScreenEditorArea",
  components: {
    DragableElement
  },
  props: {
    maxWidthPercentage: {
      type: Number,
      default: 0.60
    },
    maxHeightPercentage: {
      type: Number,
      default: 0.60
    },
  },
  computed: {
    ...mapGetters({
      currentScreenElements: 'getCurrentScreenElementsData',
      selectedItem: 'getSelectedItemId',
      projectHeight: 'getProjectHeight',
      projectWidth: 'getProjectWidth',
      projectScale: 'getScreenScale'
    }),
    processedElements: function () {
      const elements = this.currentScreenElements;
      let processedElements = [];
      elements.forEach( el => {
        const processedElement = {
          id: el.id,
          top: el.top,
          left: el.left,
          html: generateElementHtml(el)
        }
        processedElements.push(processedElement);
      })
      return processedElements;
    },
  },
  watch: {
    maxHeightPercentage() {
      this.updateProjectEditorAreaSize();
    }
  },
  methods: {
    updateProjectScale(scale) {
      this.$store.dispatch("actionSetScreenScale", scale);
    },
    updateProjectEditorAreaSize() {
      const prototypeHeight = this.projectHeight;
      const prototypeWidth = this.projectWidth;
      let prototypeRatio = prototypeWidth / prototypeHeight;

      const maxAreaHeight = window.innerHeight * this.maxHeightPercentage;
      const maxAreaWidth = window.innerWidth * this.maxHeightPercentage;
      let areaRatio = maxAreaWidth / maxAreaHeight;

      let biggerWidthDiscrapency = prototypeRatio >= areaRatio;
      let ratio = prototypeRatio / areaRatio;
      let newAreaHeight = maxAreaHeight / (ratio * biggerWidthDiscrapency + 1 * !biggerWidthDiscrapency);

      this.updateProjectScale(newAreaHeight / prototypeHeight);
    },
    droppedItem(e) {
      const sourceItem = this.$store.getters.getDraggedItem;
      if (sourceItem) {
        const prototypeScreenEditorArea = this.$refs['prototype-screen-editor-area'];
        const widthConstraint = prototypeScreenEditorArea.offsetWidth;
        const heightConstraint = prototypeScreenEditorArea.offsetHeight;
        const offsetTop = prototypeScreenEditorArea.offsetTop;
        const offsetLeft = prototypeScreenEditorArea.offsetLeft;
        // new object is created to avoid deep copies
        let item = {
          ...sourceItem,
          id: Date.now() + '',
          top: this.between(e.y - offsetTop, 0, heightConstraint),
          left: this.between(e.x - offsetLeft, 0, widthConstraint),
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
  },
  mounted() {
    this.updateProjectEditorAreaSize();
    this.updateProjectScale(this.projectScale);
    window.addEventListener('resize', () =>  this.updateProjectEditorAreaSize());
  }
}
</script>
<style scoped>
#prototype-screen-editor-area {
  position: relative;
  top: 0px;
  margin: 0 auto;
  background-color: rgb(255, 255, 255);
  outline: solid #c0c5ca 3px;
  z-index: 2500;
  overflow: hidden;
}
.selected-item {
  outline:  3px dashed rgb(48, 137, 201);
}
</style>