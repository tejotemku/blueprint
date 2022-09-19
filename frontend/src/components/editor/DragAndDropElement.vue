<template>
  <div
    draggable
    @dragstart="dragStart"
    @dragend="dragEnd"
    class="drag-n-drop"
  >
    <slot />
  </div>
</template>

<script>
export default {
  name: 'DragAndDropElement',
  props: {
    elementInfo: {
      type: Object,
      default: () => {},
    }
  },
  methods: {
    dragStart() {
      this.$store.dispatch('actionSetDraggedItem', this.elementInfo);
    },
    dragEnd() {
      this.$store.dispatch('actionResetDraggedItem');
    },

  }

}
</script>

<style scoped>
.drag-n-drop {
  margin: 5px;
}
</style>