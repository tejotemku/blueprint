<template>
  <div class="library-box-container">
    <v-subheader> COMPONENTS </v-subheader>
    <!-- button -->
    <v-subheader @click="showButtons=!showButtons" class="row-space-between"> 
      <div> Buttons </div> 
      <font-awesome-icon :icon="showButtons? 'angle-double-up' : 'angle-double-down'" style="fontSize: 1rem;"/> 
    </v-subheader>
    <div class="library-box-segment" v-if="showButtons">
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
            backgroundColor: null,
            borderColor: null,
            textColor: null,
          }
        }"
        v-html="btn.previewHtml"
      />
    </div>
    <div class="stepper" v-else />


    <!-- input fields -->
    <v-subheader @click="showInputFields=!showInputFields" class="row-space-between"> 
      <div> Input Fields </div> 
      <font-awesome-icon :icon="showInputFields? 'angle-double-up' : 'angle-double-down'" style="fontSize: 1rem;"/> 
    </v-subheader>
    <div class="library-box-segment" v-if="showInputFields">
      <DragAndDropElement 
        v-for="inputField, index in inputFields"
        :key="index"
        :elementInfo="{
          type: 'InputField',
          description: inputField.description,
          properties: {
            class: inputField.class,
            text: inputField.text,
            inputType: inputField.inputType,
            textColor: inputField.textColor,
            width: inputField.defaultWidth,
            height: inputField.defaultHeight,
            redirect: null,
            ...('isChecked' in inputField) && {checked: inputField.isChecked},
          }
        }"
        v-html="inputField.previewHtml"
      />
    </div>
    <div class="stepper" v-else />


    <!-- text fields -->
    <v-subheader @click="showTextFields=!showTextFields" class="row-space-between"> 
      <div> Text Fields </div> 
      <font-awesome-icon :icon="showTextFields? 'angle-double-up' : 'angle-double-down'" style="fontSize: 1rem;"/> 
    </v-subheader>
    <div class="library-box-segment" v-if="showTextFields">
      <DragAndDropElement 
        v-for="textField, index in textFields"
        :key="index"
        :elementInfo="{
          type: 'TextField',
          description: 'Text Field',
          properties: {
            textColor: textField.textColor,
            width: 200,
            height: 150,
            redirect: null,
            text: 'Text Field Example Text'
          }
        }"
        v-html="textField.previewHtml"
      />
    </div>
    <div class="stepper" v-else />


    <!-- shapes -->
    <v-subheader @click="showShapes=!showShapes" class="row-space-between"> 
      <div> Shapes </div> 
      <font-awesome-icon :icon="showShapes? 'angle-double-up' : 'angle-double-down'" style="fontSize: 1rem;"/> 
    </v-subheader>
    <div class="library-box-segment" v-if="showShapes">
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
    <div class="stepper" v-else />


  </div>
</template>

<script>
import DragAndDropElement from './DragAndDropElement.vue'
import { buttonComponents } from '@/prototypeComponentsLibrary/buttons'
import { shapesComponents } from '@/prototypeComponentsLibrary/shapes'
import { inputFieldsComponents } from '@/prototypeComponentsLibrary/inputFields'
import { textFieldsComponents } from '@/prototypeComponentsLibrary/textFields'




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
      textFields: textFieldsComponents(),
      showButtons: true,
      showInputFields: true,
      showTextFields: true,
      showShapes: true,
    }
  }
}
</script>

<style scoped>
.library-box-segment {
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
  overflow: auto;
}

.stepper {
  height: 1px;
  width: 96%;
  margin-left: 2%;
  border-bottom: 1px solid #dee2e6;
}
</style>