<template>
  <v-form
    ref="form"
    v-model="valid"
    @submit="(e) => {e.preventDefault(); createNewProject();}"
  >
    <h1>Create a new prototype project</h1>
    <v-text-field
      v-model="projectName"
      :rules="projectNameRules"
      label="Project name"
      outlined
      dense
    />
    <v-switch
      v-model="customDimensions"
      :label="customDimensions? 'Custom Dimensions' :  'Standard Dimensions'"
      class="mt-0"
    >
    </v-switch>
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
    <v-textarea 
      label="Project description"
      class="mb-2"
      v-model="projectDescription"
      outlined
      dense
    />
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
      @click="returnToHomepage"
    >
      Cancel
    </v-btn>
  </v-form>
</template>

<script>
import router from '../../router'

export default {
  name: 'CreateProjectForm',
  data() {
    return {
      customDimensions: false,
      projectName: null,
      projectDescription: null,
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
        v => !!v || 'Project Name is required',
      ],
      valid: false,
      dimensions: [
        '1920x1080(16:9)',
      ],
      chosenStandardDimensions: '1920x1080(16:9)',
    }
  },
  methods: {
    createNewProject() {
      let payload = {
        'name': this.projectName,
        'description': this.projectDescription || ''
      }
      if (this.customDimensions) {
        payload['width'] = this.customWidth;
        payload['height'] = this.customHeight;
      } else {
        let dividerIndex = this.chosenStandardDimensions.indexOf('x');
        let aspectRatioStartIndex = this.chosenStandardDimensions.indexOf('(');
        payload['width'] = this.chosenStandardDimensions.slice(0, dividerIndex);
        payload['height'] = this.chosenStandardDimensions.slice(dividerIndex + 1, aspectRatioStartIndex);
      }
      console.log(payload);
      // TODO: this is a mock
      let projectId = 0;
      router.push(`/project/${projectId}`);
    },
    returnToHomepage() {
      router.push('/home')
    }
  }
}
</script>

<style scoped>
.create-project-box {
  padding-block: 2vw;
  padding-inline: 2.5vw;
  font-size: 2vw;
}
</style>