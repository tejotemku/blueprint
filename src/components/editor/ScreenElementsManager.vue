<template>
  <v-list>
    <v-subheader> LAYER VIEW </v-subheader>
    <v-list-item-group
        v-model="selectedItem"
        color="primary"
    >
      <draggable
        v-model="screenElements"
        ghost-class="ghost"
      >
        <v-list-item v-for="element in screenElements" :key="element.id" draggable>
          <v-list-item-content>
            <v-list-item-title> {{ element.description }} </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </draggable>
    </v-list-item-group>
  </v-list>
</template>

<script>
import draggable from 'vuedraggable'

export default {
  components: {
    draggable
  },
  name: 'ScreenElementsManager',
  data() {
    return {
      screenElements: [],
      selectedItem: null,
    }
  },
  methods: {
    sortElementsByDepth() {
      this.screenElements.sort((a,b) => a.depth - b.depth);
    },
    getCurrentScreenElements() {
      const screenElements = this.$store.getters['getCurrentScreenElementsData'];
      this.screenElements = screenElements;

    }
  },
  beforeMount() {
    this.getCurrentScreenElements();
    this.sortElementsByDepth();
  },
}
</script>
<style scoped>
.element-box {
  border: solid black 1px;
}

.element-list {
  overflow-y: overlay;
}

.ghost {
  opacity: 0.5;
}
</style>