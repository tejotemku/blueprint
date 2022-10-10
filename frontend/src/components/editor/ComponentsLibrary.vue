<template>
  <div class="library-box-container">
    <v-subheader> COMPONENTS </v-subheader>
    <!-- button -->
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
            text: 'button',
            width: null,
            height: null,
            redirect: null,
          }
        }"
        v-html="btn.previewHtml"
      />
    </div>
    <!-- input fields -->
    <v-subheader> Input Fields </v-subheader>
    <div class="library-box">
      <DragAndDropElement 
        v-for="inputField, index in inputFields"
        :key="index"
        :elementInfo="{
          type: 'InputField',
          description: 'InputField',
          properties: {
            class: inputField.class,
            placeholder: 'input field',
            inputType: inputField.inputType,
            textColor: inputField.textColor,
            width: null,
            height: null,
            redirect: null,
          }
        }"
        v-html="inputField.previewHtml"
      />
    </div>
    <!-- shapes -->
    <v-subheader> Shapes </v-subheader>
    <div class="library-box">
      <DragAndDropElement 
        v-for="shape, index in shapes"
        :key="index"
        :elementInfo="{
          type: 'Shape',
          description: 'Shape',
          properties: {
            class: shape.class,
            width: shape.width,
            height: shape.height,
            redirect: null,
          }
        }"
        v-html="shape.previewHtml"
      />
    </div>
  </div>
</template>

<script>
import DragAndDropElement from './DragAndDropElement.vue'
import { buttonComponents } from '../../prototypeComponentsLibrary/buttons'
import { shapesComponents } from '../../prototypeComponentsLibrary/shapes'
import { inputFieldsComponents } from '../../prototypeComponentsLibrary/inputFields'




export default {
  name: 'ComponentsLibrary',
  components: {
    DragAndDropElement
  },
  data() {
    return {
      buttons: buttonComponents(),
      inputFields: inputFieldsComponents(),
      shapes: shapesComponents(),
    }
  }
}
</script>

<style scoped>
.library-box {
  display: flex;
  flex-wrap: wrap;
}
.library-box-container {
  position: fixed;
  z-index: 3000;
  top: 75px;
  left: 0px;
  width: 15%;
  height: calc(100vh - 75px);
  border: solid #dee2e6 1px;
  background-color: white;
}
</style>