<template>
  <div class="screens-box-container">
    <v-subheader> SCREENS </v-subheader>
    <div class="screens-box">
      <div 
        v-for="screenName, screenId of screens" 
        :key="screenId"
        @click="changeCurrentScreen(screenId)"
      >
        {{screenName}}
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
    }
  }
}
</script>


<style scoped>
  .screens-box {
    display: flex;
    flex-direction: column;
    padding-inline-start: 10px;
  }
  .screens-box-container {
    border: solid #dee2e6 1px;
    background-color: white;
  }
  </style>