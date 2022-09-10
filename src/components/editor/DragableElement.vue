<template>
  <div
    @mousedown.prevent="startDragging"
    :style="{
        position: 'absolute',
        top: this.top + 'px',
        left: this.left + 'px',
        zIndex: 2999 - this.zIndex,
        opacity: this.itemOpacity
    }"
    class="prevent-select"
    ref="dragableElement"
  >
    <slot  />
  </div>
</template>

<script>
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
      const element = this.$refs['dragableElement'];
      this.pos1 = this.pos3 - e.clientX;
      this.pos2 = this.pos4 - e.clientY;
      this.pos3 = e.clientX;
      this.pos4 = e.clientY;
      const prototypeScreenEditorArea = document.getElementById('prototype-screen-editor-area');
      const widthConstraint = prototypeScreenEditorArea.offsetWidth;
      const heightConstraint = prototypeScreenEditorArea.offsetHeight;
      this.$refs['dragableElement'].style.top = this.between(element.offsetTop - this.pos2, 0, heightConstraint - element.offsetHeight) + "px";
      this.$refs['dragableElement'].style.left = this.between(element.offsetLeft - this.pos1, 0, widthConstraint - element.offsetWidth) + "px";
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
    }
  },
}

</script>

<style scoped>
.prevent-select {
  -webkit-user-select: none; /* Safari */
  -ms-user-select: none; /* IE 10 and IE 11 */
  user-select: none; /* Standard syntax */
}
</style>
