<template>
  <div>
    <LoggedInNavbar />
    <div class="homepage-content">
      <div class="my-2 homepage-tools-row row-space-between">
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
        <ProjectPreview v-for="item in usersProjects" :key="item.projectId" :projectData="item"/>
      </v-row>
    </div>
  </div>
</template>

<script>
import router from '@/router';
import ProjectPreview from '@/components/general/ProjectPreview.vue';
import LoggedInNavbar from '@/components/general/LoggedInNavbar.vue';
import { api } from "@/api"
import { mapGetters } from "vuex";


export default {
  name: 'HomepageView',
  components: {
    ProjectPreview,
    LoggedInNavbar,
  },
  data() {
    return {
      projectSearchBarValue: '',
      usersProjects: []
    }
  },
  computed: {
    ...mapGetters({
      token: 'getToken',
      username: 'getUsername'
    }),
  },
  methods: {
    async getUserProjects() {
      await api.getUsersProjectsInfo(this.token, this.username).then(
        response => this.usersProjects = response.data
      );
    },
    createNewProject() {
      router.push(`/create-project`);
    },
  },
  beforeMount() {
    this.getUserProjects();
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
  align-items: center;
}

</style>
