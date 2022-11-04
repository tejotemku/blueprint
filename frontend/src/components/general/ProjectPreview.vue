<template>
  <v-card class="project-preview-box" @click="goToProject">
    <v-card-title class="project-preview-box-title-name">
      {{this.projectData.name}}
    </v-card-title>
    <v-card-text>
      {{this.projectData.description}}
    </v-card-text>
  </v-card>
  
</template>

<script>
import router from '@/router'
export default {
  name: 'ProjectPreview',
  props: {
    projectData: {
      type: Object,
      default: () => {}
    } 
  },
  methods: {
    goToProject() {
      router.push(`/project/${this.projectData.id}`)
    }
  }
}
</script>

<style scoped>
.project-preview-box {
  margin: 1rem;
  display: flex;
  flex-direction: column;
  min-width: 400px;
  width: calc(70vw/3)
}
.project-preview-box-title-name {
  word-break: normal;
}
</style>