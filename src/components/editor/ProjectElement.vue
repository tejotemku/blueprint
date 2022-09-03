<template>
  <div
    @mousedown="startDragging" 
    @mousemove="dragging" 
    @mouseup="stopDragging"
    :style="ghostItemStyle"
  >
    <slot  />
  </div>
</template>

<script>
export default {
  name: "ProjectElement",
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
      ghostItemStyle: {
        position: 'absolute',
        top: this.top,
        left: this.left,
        zIndex: this.zIndex,
        opacity: 1
      }
    }
  },
  methods: {
    startDragging() {
      console.log("Started dragging!");
      this.ghostItemStyle["opacity"] = 0.5;
    },
    stopDragging() {
      console.log("Stopped dragging!");
      this.ghostItemStyle["opacity"] = 1;
    },
    dragging() {
      console.log("Dragging!");
    }
  },
}

</script>

<style scoped>

</style>
