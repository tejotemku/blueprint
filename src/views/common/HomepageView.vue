<template>
  <div>

    <v-row>
      <ProjectPreview v-for="item in GetUserProjects()" :key="item['id']" :projectName="item['name']"/>
      <v-card class="add-project-box">
        <font-awesome-icon icon="plus-square"/>
      </v-card>  
    </v-row>
  </div>
</template>

<script>
// import router from '../../router';
import ProjectPreview from '../../components/ProjectPreview.vue'

export default {
  name: 'HomepageView',
  components: {
    ProjectPreview,
  },
  methods: {
    GetUserProjects() {
      //TODO: this is a mock,
      return [
        {"id": 1, "name": "test1"},
        {"id": 2, "name": "test2"},
        {"id": 3, "name": "test3"},
        {"id": 4, "name": "test4"},
        {"id": 5, "name": "test5"},
        {"id": 6, "name": "test6"},
        {"id": 7, "name": "test7"},
        {"id": 8, "name": "test8"},
        {"id": 9, "name": "test9"},
        {"id": 10, "name": "test10"},
        {"id": 11, "name": "test11"},
        {"id": 12, "name": "test12"},
        {"id": 13, "name": "test13"},
      ]
    }
  },
  beforeMount() {
   // TODO: check if logged in and reddirect if necessary
  },
}
</script>

<style>
.add-project-box {
  padding: 1rem;
  margin: 0 auto;
}
</style>
