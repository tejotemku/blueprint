<template>
  <v-form
    ref="form"
    v-model="valid"
    @submit="(e) => {e.preventDefault(); createNewProject();}"
    class="create-project-box"
  >
    <h1>Create a prototype project</h1>
    <v-text-field
      v-model="projectName"
      :rules="projectNameRules"
      label="Project name"
      outlined
      dense
    />
    <div class="row-center">
      <v-switch
        v-model="customDimensions"
      />
      {{customDimensions? 'Custom Dimensions' :  'Standard Dimensions'}}
    </div>
    <template v-if="customDimensions">
      <v-text-field
        v-model="customWidth"
        :rules="widthRules"
        label="Width (pixels)"
        class="mb-2"
        outlined
        dense
      />
      <v-text-field
        v-model="customHeight"
        :rules="heightRules"
        label="Height (pixels)"
        class="mb-2"
        outlined
        dense
      />
    </template>
    <v-select
      v-else
      :items="dimensions"
      item-value="first"
      label="Dimensions"
      v-model="chosenStandardDimensions"
      outlined
    >
    Aplication Window Dimensions
    </v-select>
    <v-btn
      :disabled="!valid"
      color="success"
      class="mr-4"
      type="submit"
    >
      Create Project
    </v-btn>
    <v-btn
      color="error"
      class="mr-4"
      @click="cancelGuestMode"
    >
      Cancel
    </v-btn>
  </v-form>
</template>

<script>
import router from '@/router';
import { windowDimensions, defaultWindowDimensions } from '@/common/prototypeDimensions.js';

export default {
  name: 'GuestCreateProjectForm',
  data() {
    return {
      customDimensions: false,
      projectName: null,
      projectWidth: null,
      projectHeight: null,
      customWidth: '1920',
      customHeight: '1080',
      widthRules: [
        v => !!v || 'Width is required',
        v => /^\+?\d+$/.test(v) || 'Value must be an integer.'
      ],
      heightRules: [
        v => !!v || 'Height is required',
        v => /^\+?\d+$/.test(v) || 'Value must be an integer.'
      ],
      projectNameRules: [
        v => !!v || 'Project name is required',
      ],
      valid: false,
      dimensions: windowDimensions,
      chosenStandardDimensions: defaultWindowDimensions,
    }
  },
  methods: {
    cancelGuestMode() {
      router.push({ name: "WelcomePage" });
    },
    createNewProject() {
      let projectFile = {
        'title': this.projectName,
        'screens': {
          "0": {
            name: 'My First Screen',
            elements: [ ]
          }
        },
        'assets': [ ],
        'currentScreenId': "0",
        'defaultScreenId': "0",
      }
      if (this.customDimensions) {
        projectFile.width = this.customWidth;
        projectFile.height = this.customHeight;
      } else {
        let dividerIndex = this.chosenStandardDimensions.indexOf('x');
        let aspectRatioStartIndex = this.chosenStandardDimensions.indexOf('(');
        projectFile.width = this.chosenStandardDimensions.slice(0, dividerIndex);
        projectFile.height = this.chosenStandardDimensions.slice(dividerIndex + 1, aspectRatioStartIndex);
      }
      this.$store.dispatch('actionSetProjectData', projectFile);
      this.$emit('createdProject');
    },
  }
}
</script>

<style scoped>
.create-project-box {
  background-color: rgb(254, 254, 254);
  padding-inline: 30px;
  padding-block: 15px;
  border-radius: 10px;
  z-index: 4000;
}
</style>