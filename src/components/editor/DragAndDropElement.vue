<template>
  <div
    draggable
    @dragstart="dragStart"
    @dragend="dragEnd"
  >
    <slot />
  </div>
</template>

<script>
export default {
  name: 'DragAndDropElement',
  props: ['elementInfo'],
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

</style>