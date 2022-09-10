<template>
  <div class="library-box-container">
    <v-subheader> COMPONENTS </v-subheader>
    <v-subheader> Buttons </v-subheader>
    <div class="library-box">
      <DragAndDropElement 
        v-for="btn, index in buttons"
        :key="index"
        :elementInfo="{
          type: 'Button',
          description: 'Button',
          properties: {
            class: btn.class,
            text: 'button'
          }
        }"
        v-html="btn.previewHtml"
      />
    </div>
  </div>
</template>

<script>
import DragAndDropElement from './DragAndDropElement.vue'
import { buttonComponents } from '../../prototypeComponentsLibrary/buttons'

export default {
  name: 'ComponentsLibrary',
  components: {
    DragAndDropElement
  },
  data() {
    return {
      buttons: buttonComponents(),
      inputFields: [],
      shapes: [],
    }
  },
  beforeMount() {
    this.buttons = buttonComponents()
    console.log(this.buttons);
  } 
}
</script>

<style scoped>
.library-box {
  display: flex;
  flex-wrap: wrap;
}
.library-box-container {
  border: solid #dee2e6 1px;
  background-color: white;
}
</style>