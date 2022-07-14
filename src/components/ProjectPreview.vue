<template>
  <v-card class="project-preview-box">
    <div style="width: 40px; height: 40px; backgroundColor:red">
      <!-- TODO: create preview here -->
    </div>
    <v-card-title style="">
      {{this.projectName}}
    </v-card-title>
  </v-card>
  
</template>

<script>
export default {
  name: 'ProjectPreview',
  props: ["projectName"],
  data() {
    return {

    }
  },
  methods: {
  }
}
</script>

<style scoped>
.project-preview-box {
  padding: 1rem;
  margin: 1rem;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  min-width: 150px;
  width: calc(90vh/6)
}
</style>