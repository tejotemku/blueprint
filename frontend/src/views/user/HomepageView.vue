<template>
  <div>
    <LoggedInNavbar>
      <v-btn
          color="success"
          @click="createNewProject"
          class="me-2"
        >
          New Project
        </v-btn>
    </LoggedInNavbar>
    <div class="homepage-content" >
      <div class="my-2 ml-15 homepage-tools-row row-space-between">
        <v-text-field  
          v-if="projectsFetched && !noProjects"
          v-model="projectSearchBarValue"
          label="Search by project name..."
          class="project-search-bar"
          outlined
          dense
          hide-details
        />        
      </div>
      <v-skeleton-loader
        :loading="!projectsFetched"
        :boilerplate="true"
        transition="scale-transition"
        type="table-tbody"
        :tile="false"
        class="mx-auto"
      >
        <v-row class="projects-row" v-if="!noProjects && !noFilteredProjects">
          <ProjectPreview v-for="item in filteredUsersProjects" :key="item.projectId" :projectData="item"/>
        </v-row>
        <h1 
          class="no-projects-info"
          v-else
        >
          {{ 
            noFilteredProjects ? 
            'No projects under that name' : 
            'You have no projects. To create a project use the "New Project" project.'}}
          
      </h1>
      </v-skeleton-loader>
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
      usersProjects: [],
      projectsFetched: false
    }
  },
  computed: {
    ...mapGetters({
      token: 'getToken',
      username: 'getUsername'
    }),
    filteredUsersProjects() {
      let filteredProjects = [ ...this.usersProjects ].filter(
        x => x.name.toLowerCase().includes(this.projectSearchBarValue.trim())
      );
      return filteredProjects;
    },
    noProjects() {
      return this.usersProjects.length == 0;
    },
    noFilteredProjects() {
      return this.filteredUsersProjects.length == 0;
    },
  },
  methods: {
    async getUserProjects() {
      this.projectsFetched = false;
      await api.getUsersProjectsInfo(this.token, this.username).then(
        response => {
          this.usersProjects = response.data;
          this.projectsFetched = true;
        }
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
.no-projects-info {
  width: 100%;
  height: 60vh;
  margin: 0 auto;
  padding-top: 15%;
  text-align: center;
}

</style>
