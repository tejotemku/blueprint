<template>
  <div class="editor-area">
    <drag-it-dude v-for="el in screenElements" :key="el.id" :style="'zIndex: ' + el.depth + '; top: ' + el.top + 'px; left: ' + el.left + 'px'" v-html="el.html" />
  </div>
</template>

<script>
import DragItDude from 'vue-drag-it-dude';

export default {
  name: "PrototypeScreenEditorArea",
  components: {
    DragItDude
  },
  data() {
    return {
      screenElements: [],
    }
  },
  methods: {
    getCurrentScreenElements() {
      const screenElements = this.$store.getters['getCurrentScreenElementsData'];
      this.screenElements = screenElements;
    }
  },
  beforeMount() {
    this.getCurrentScreenElements();
  },
}
</script>
<style scoped>
.editor-area {
  position: relative;
  width: 1600px;
  height: 800px;
  margin: 0 auto;
  background-color: rgb(194, 209, 237);
}
</style>