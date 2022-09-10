<template>
  <div>
    <LoggedInNavbarVue />
    <div class="project-view">
      <MinimiseComponentBtn buttonFloat="right" :right="true" :bottom="false">
        <ScreenElementsManager />
      </MinimiseComponentBtn>
      
      <MinimiseComponentBtn  buttonFloat="right" :right="true" :bottom="true">
        <AssetsManager />
      </MinimiseComponentBtn>
      
      <MinimiseComponentBtn  buttonFloat="left" :right="false" :bottom="true">

      </MinimiseComponentBtn>
      <PrototypeScreenEditorArea  />
    </div>
    <!-- Project Editor site - project id {{this.$route.params.id}} -->
    <!-- TODO: Library -->
  </div>
</template>

<script>
import LoggedInNavbarVue from '../../components/general/LoggedInNavbar.vue'
import ScreenElementsManager from '../../components/editor/ScreenElementsManager.vue'
import AssetsManager from '../../components/editor/AssetsManager.vue'
import MinimiseComponentBtn from '../../components/editor/MinimiseComponentBtn.vue'
import PrototypeScreenEditorArea from '../../components/editor/PrototypeScreenEditorArea.vue'
import { projectData } from '../../mocks/projectDataMock.js'


export default {
  name: "ProjectEditor",
  components: {
    LoggedInNavbarVue,
    ScreenElementsManager,
    MinimiseComponentBtn,
    PrototypeScreenEditorArea,
    AssetsManager,
  },
  data() {
    return {
      projectData: {},
    }
  },
  beforeMount() {
    this.getProjectData();
  },
  methods: {
    fetchData() {
      // let projectId = this.$route.params.id;
      // TODO: this is a mock
      return projectData();
    },
    getProjectData() {
      try {
        const projectData = this.fetchData();
        this.$store.dispatch("actionSetProjectData", projectData);
        this.projectData = projectData;
      }
      catch(err) {
        console.log(err);
      }
    }
  }
}
</script>

<style scoped>
.project-view {
  margin-block-start: 75px;
}
</style>
