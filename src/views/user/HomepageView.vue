<template>
  <div>
    <LoggedInNavbar />
    <div class="homepage-content">
      <div class="my-2 homepage-tools-row">
        <v-text-field  
          v-model="projectSearchBarValue"
          label="Search by project name..."
          class="project-search-bar"
          outlined
          dense
          hide-details
        />        
        
        <v-btn
          color="success"
          @click="createNewProject"
          class="me-2"
        >
          New Project
        </v-btn>
      </div>
      <v-row class="projects-row">
        <ProjectPreview v-for="item in getUserProjects().filter(v => filterProjectsByName(v))" :key="item['id']" :projectData="item"/>
      </v-row>
    </div>
  </div>
</template>

<script>
import router from '../../router';
import ProjectPreview from '../../components/general/ProjectPreview.vue'
import LoggedInNavbar from '../../components/general/LoggedInNavbar.vue'

export default {
  name: 'HomepageView',
  components: {
    ProjectPreview,
    LoggedInNavbar,
  },
  data() {
    return {
      projectSearchBarValue: ''
    }
  },
  methods: {
    getUserProjects() {
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
    },
    createNewProject() {
      router.push(`/create-project`);
    },
    filterProjectsByName(value) {
      return value['name'].indexOf(this.projectSearchBarValue) != -1
    }
  },
  beforeMount() {
   // TODO: check if logged in and reddirect if necessary
  },
}
</script>

<style>
.projects-row {
  padding-inline: 5vh;
}
.homepage-content {
  height: 90vh;
  width: 100vw;
  padding: 50px;
}
.project-search-bar {
  max-width: 400px;
}
.homepage-tools-row {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  
}

</style>
