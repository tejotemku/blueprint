<template>
  <div
    @mousedown.stop.prevent="startDragging"
    :style="{
        position: 'absolute',
        top: parseInt(this.top) + 'px',
        left: parseInt(this.left) + 'px',
        // top: '0px',
        // left: '0px',
        zIndex: 9999 - this.zIndex,
        opacity: this.itemOpacity,
        transform: `scale(${this.scale})`
    }"
    @click.prevent.stop="setAsSelected"
    class="prevent-select"
    ref="dragableElement"
  >
    <slot  />
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "DragableElement",
  props: {
    top: {
      type: Number,
      default: 0,
    },
    left: {
      type: Number,
      default: 0,
    },
    zIndex: {
      type: Number,
      default: 0,
    },
    elementId: {
      type: String,
      default: ''
    }
  },
  computed: {
    ...mapGetters({
      scale: 'getScreenScale'
    }),
  },
  data() {
    return {
      itemOpacity: 1,
      pos1: 0,
      pos2: 0,
      pos3: 0,
      pos4: 0,
    }
  },
  watch: {
    scale(new_val, old_val) {
      if (!old_val) return;
      const element = this.$refs.dragableElement;
      this.$refs.dragableElement.style.top = element.offsetTop * new_val/old_val + "px";
      this.$refs.dragableElement.style.left = element.offsetLeft * new_val/old_val + "px";
      this.maintainBoundries();
    }
  },
  methods: {
    startDragging(e) {
      this.itemOpacity = 0.65;
      e = e || window.event;
      e.preventDefault();
      this.pos3 = e.clientX;
      this.pos4 = e.clientY;
      document.onmouseup = this.closeDragElement;
      document.onmousemove = this.elementDrag;
    },
    elementDrag(e) {
      e = e || window.event;
      e.preventDefault();
      const element = this.$refs.dragableElement;
      this.pos1 = this.pos3 - e.clientX;
      this.pos2 = this.pos4 - e.clientY;
      this.pos3 = e.clientX;
      this.pos4 = e.clientY;
      const prototypeScreenEditorArea = document.getElementById('prototype-screen-editor-area');
      const widthConstraint = prototypeScreenEditorArea.offsetWidth;
      const heightConstraint = prototypeScreenEditorArea.offsetHeight;
      // console.log(element.offsetTop, this.pos2);
      let top = parseInt(this.between(element.offsetTop - this.pos2, 0, heightConstraint - element.offsetHeight));
      let left = parseInt(this.between(element.offsetLeft - this.pos1, 0, widthConstraint - element.offsetWidth));
      console.log(
        'width constraint', widthConstraint,
        '\nwidthConstraint - element.offsetWidth', widthConstraint - element.offsetWidth,
        '\nwidthConstraint - (element.offsetWidth * this.scale)', widthConstraint - (element.offsetWidth * this.scale),
        '\nelement.offsetWidth', element.offsetWidth,
        '\nelement offset left', element.offsetLeft,
        '\nelement offset left - pos', element.offsetLeft - this.pos1,
        '\nscale', this.scale
      );
      let newElementData = {
        top: top,
        left: left,
        elementId: this.elementId
      }
      this.$store.dispatch('actionChangeElementPositionOnScreen', newElementData);
      this.$refs.dragableElement.style.top = top + "px";
      this.$refs.dragableElement.style.left = left + "px";
    },
    between(x, min_v, max_v) {
      x = Math.min(x, max_v);
      x = Math.max(x, min_v);
      return x;
    },
    closeDragElement() {
      this.itemOpacity = 1;
      document.onmouseup = null;
      document.onmousemove = null;
    },
    setAsSelected() {
      this.$store.dispatch("actionSetSelectedElementId", this.elementId);
    },
    maintainBoundries(shouldRescale) {
      const element = this.$refs.dragableElement;
      const prototypeScreenEditorArea = document.getElementById('prototype-screen-editor-area');
      const widthConstraint = prototypeScreenEditorArea.offsetWidth;
      const heightConstraint = prototypeScreenEditorArea.offsetHeight;
      let top = this.between(element.offsetTop, 0, heightConstraint - element.offsetHeight);
      let left = this.between(element.offsetLeft, 0, widthConstraint - element.offsetWidth);
      let newElementData = {
        top: top,
        left: left,
        elementId: this.elementId
      }
      this.$store.dispatch('actionChangeElementPositionOnScreen', newElementData);
      this.$refs.dragableElement.style.top = top * (!shouldRescale || this.scale) + "px";
      this.$refs.dragableElement.style.left = left * (!shouldRescale || this.scale) + "px";
    },
  },
  mounted() {
    this.maintainBoundries();
  }
}

</script>

<style scoped>
.prevent-select {
  -webkit-user-select: none; /* Safari */
  -ms-user-select: none; /* IE 10 and IE 11 */
  user-select: none; /* Standard syntax */
}
</style>
