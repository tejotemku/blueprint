<template>
  <v-list class="screen-element-box-container">
    <v-subheader> LAYER VIEW </v-subheader>
    <v-list-item-group
        v-model="selectedItem"
        color="primary"
    >
      <draggable
        v-model="screenElements"
        ghost-class="ghost"
        @change="heirarchyChanged"
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
import { mapGetters } from "vuex";
import draggable from 'vuedraggable'

export default {
  components: {
    draggable
  },
  name: 'ScreenElementsManager',
  computed: {
    ...mapGetters({
      currentScreenElements: 'getCurrentScreenElementsData'
    }),
  },
  watch: {
    currentScreenElements: function(val) {
      this.screenElements = val;
    }
  },
  data() {
    return {
      screenElements: [],
      selectedItem: null,
    }
  },
  methods: {
    heirarchyChanged() {
      this.$store.dispatch("actionSetCurrentScreenElements", this.screenElements);
    }
  },
  beforeMount() {
    this.screenElements = this.currentScreenElements;
  },
}
</script>
<style scoped>
.screen-element-box-container {
  position: fixed;
  z-index: 3000;
  top: 75px;
  right: 0px;
  width: 15%;
  height: calc(60vh - 45px);
  border: solid #dee2e6 1px;
  background-color: white;
  overflow-y: auto;
}

.ghost {
  opacity: 0.5;
}
</style>