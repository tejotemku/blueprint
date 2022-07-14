<template>
  <v-form
    ref="form"
    v-model="valid"
    @submit="(e) => {e.preventDefault(); createNewProject();}"
  >
    <h1>Prepare a new project</h1>
    <v-text-field
      v-model="projectName"
      :rules="projectNameRules"
      label="Project Name"
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
      @click="forgotPassword"
    >
      Cancel
    </v-btn>
  </v-form>
</template>

<script>
import router from '../../router'

export default {
  name: 'CreateProject',
  data() {
    return {
      projectName: null,
      projectDescription: null,
      projectWidth: null,
      projectHeight: null,
      projectNameRules: [
        v => !!v || 'Project Name is required',
      ],
      valid: false,
    }
  },
  methods: {
    createNewProject() {
      // TODO: this is a mock
      let projectId = 0;
      router.push(`project/${projectId}`);
    }
  }
}
</script>

<style scoped>
.login-box {
  padding-block: 2vw;
  padding-inline: 2.5vw;
  font-size: 2vw;
}
</style>