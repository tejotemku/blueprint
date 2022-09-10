<template>
  <div>
    <LoggedInNavbarVue>
      <v-btn
        color="secondary"
        @click="generatePrototypeActions"
        class="mx-2"
      >
      Generate Prototype
      </v-btn>
    </LoggedInNavbarVue>
    <div class="project-view">
      <MinimiseComponentBtn buttonFloat="right" :right="true" :bottom="false">
        <ScreenElementsManager />
      </MinimiseComponentBtn>
      
      <MinimiseComponentBtn  buttonFloat="right" :right="true" :bottom="true">
        <AssetsManager />
      </MinimiseComponentBtn>
      
      <MinimiseComponentBtn  buttonFloat="left" :right="false" :bottom="true">
        <ScreenManager />
      </MinimiseComponentBtn>
      
      <MinimiseComponentBtn  buttonFloat="left" :right="false" :bottom="false">
        <ComponentsLibrary />
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
import ScreenManager from '../../components/editor/ScreenManager.vue'
import ComponentsLibrary from '../../components/editor/ComponentsLibrary.vue'
import { projectData } from '../../mocks/projectDataMock.js'
import { generatePrototype } from '../../common/generatePrototype.js'


export default {
  name: "ProjectEditor",
  components: {
    LoggedInNavbarVue,
    ScreenElementsManager,
    MinimiseComponentBtn,
    PrototypeScreenEditorArea,
    AssetsManager,
    ScreenManager,
    ComponentsLibrary,
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
    },
    generatePrototypeActions() {
      generatePrototype();
    }
  }
}
</script>

<style scoped>
.project-view {
  margin-block-start: 75px;
}
</style>
