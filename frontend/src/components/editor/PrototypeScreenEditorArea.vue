<template>
  <div 
    id="prototype-screen-editor-area"
    ref="prototype-screen-editor-area"
    @drop.prevent="droppedItem" 
    @dragenter.prevent
    @dragover.prevent
    @click="resetSelectedItem"
    >
    <span class="size-label">{{projectHeight}}x{{projectWidth}} - {{parseInt(projectScale*100)}}% scale</span>
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
import { generateElementHtml } from '@/common/prototypeElementsHandlers.js';

export default {
  name: "PrototypeScreenEditorArea",
  components: {
    DragableElement
  },
  props: {
    maxWidthPercentage: {
      type: Number,
      default: 0.65
    },
    maxHeightPercentage: {
      type: Number,
      default: 0.65
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

      const area = this.$refs['prototype-screen-editor-area'];
      let biggerWidthDiscrapency = prototypeRatio >= areaRatio;
      let scale = prototypeRatio / areaRatio;
      let newAreaWidth = maxAreaWidth * (scale * !biggerWidthDiscrapency + 1 * biggerWidthDiscrapency);
      let newAreaHeight = maxAreaHeight / (scale * biggerWidthDiscrapency + 1 * !biggerWidthDiscrapency);
      // console.log(
      //   'areaRatio', areaRatio, 
      //   '\nprototypeRatio', prototypeRatio, 
      //   '\nnewAreaWidth', newAreaWidth, 
      //   '\nnewAreaHeight', newAreaHeight, 
      //   '\nscale', scale, 
      //   '\nbigger width', biggerWidthDiscrapency, 
      //   '\nwidth scaler', (scale * !biggerWidthDiscrapency + 1 * biggerWidthDiscrapency), 
      //   '\nold width', maxAreaWidth, 
      //   '\nold height', maxAreaHeight,
      //   '\nscale', newAreaHeight / prototypeHeight,
      //   '\nscale', newAreaWidth / prototypeWidth
      // );
      area.style.width = newAreaWidth + 'px';
      area.style.height = newAreaHeight  + 'px';
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
  width: 64vw;
  height: 64vh;
  margin-inline: 0 auto;
  background-color: rgb(255, 255, 255);
  outline: solid #c0c5ca 3px;
  z-index: 2500;
  overflow: visible;
}
.selected-item {
  outline:  3px dashed rgb(48, 137, 201);
}

.size-label {
  position: absolute;
  left: -3px;
  top: -20px;
  line-height: 14px;
  font-size: 12px;
  color: rgb(93, 93, 93);
  background-color: rgba(48, 137, 201, 0.5);
  padding: 3px;
  border-radius: 1px;
}
</style>