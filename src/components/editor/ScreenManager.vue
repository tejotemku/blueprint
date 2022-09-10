<template>
  <div class="screens-box-container">
    <v-subheader class="subheader"> 
      SCREENS 
      <v-btn icon color="#00f" @click="openNewScreenCreationModal">
        <font-awesome-icon icon="plus-square" style="fontSize: 1rem"/>
      </v-btn>
    </v-subheader>
    <div class="screens-box">
      <div 
        v-for="screenName, screenId of screens" 
        :key="screenId"
        @click="changeCurrentScreen(screenId)"
        class="screen-item"
      >
        {{ screenName }}
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
      screens: 'getMinimalScreensInfo'
    }),
  },
  methods: {
    changeCurrentScreen(id) {
      this.$store.dispatch('actionSetCurrentScreen', id)
    },
    openNewScreenCreationModal() {
      this.$emit('newScreenCreation:show');
    },
  }
}
</script>


<style scoped>
.screens-box {
  display: flex;
  flex-direction: column;
  padding-inline: 10px;
}

.subheader {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
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