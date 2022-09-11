<template>
  <div class="screens-box-container">
    <v-subheader class="row-space-between"> 
      SCREENS 
      <v-btn icon color="#00f" @click="openNewScreenCreationTool">
        <font-awesome-icon icon="plus-square" style="fontSize: 1rem"/>
      </v-btn>
    </v-subheader>
    <div class="screens-box">
      <div 
        v-for="screen, screenId of screens" 
        :key="screenId"
        @click="changeCurrentScreen(screenId)"
        class="screen-item row-space-between"
      >
        {{ screen.name }}
      <v-btn icon @click="openScreenEditingTool(screenId, screen.name)">
        <font-awesome-icon icon="pen-to-square" style="fontSize: 1rem"/>
      </v-btn>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
  
export default {
  name: 'ScreenManager',
  computed: {
    ...mapGetters({
      screens: 'getScreensInfo'
    }),
  },
  methods: {
    changeCurrentScreen(id) {
      this.$store.dispatch('actionSetCurrentScreen', id)
    },
    openNewScreenCreationTool() {
      this.$emit('newScreenCreation:show');
    },
    openScreenEditingTool(id, screenName) {
      this.$emit('screenEditingTool:show', id, screenName);
    }
  }
}
</script>


<style scoped>
.screens-box {
  display: flex;
  flex-direction: column;
  padding-inline: 10px;
}

.screens-box-container {
  position: fixed;
  z-index: 3000;
  bottom: 0px;
  right: 0px;
  width: 15%;
  height: calc(40vh - 30px);
  border: solid #dee2e6 1px;
  background-color: white;
  overflow-y: auto;
  border: solid #dee2e6 1px;
  background-color: white;
}

.screen-item {
  border-block-end: #bdc0c4 1px solid;
}
</style>