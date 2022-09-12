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
            <v-list-item-title class="screen-item row-space-between list-item-laylout"> 
              {{ element.description }}
              <v-btn icon @click="openElementEditingTool(element.id, element.properties, element.description)">
                <font-awesome-icon icon="pen-to-square" style="fontSize: 1rem"/>
              </v-btn>
            </v-list-item-title>
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
    ...mapGetters([
      'getCurrentScreenElementsData',
    ]),
  },
  watch: {
    getCurrentScreenElementsData: function(val) {
      this.screenElements = val;
    },
    getDefaultScreenId: function(val) {
      this.deaultScreenId = val;
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
    },
    openElementEditingTool(id, properties, description) {
      this.$emit('elementEditingTool:show', id, properties, description)
    },
  },
  beforeMount() {
    this.screenElements = this.getCurrentScreenElementsData;
    this.deaultScreenId = this.getDefaultScreenId;
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

.list-item-laylout {
  align-items: center;
}
</style>