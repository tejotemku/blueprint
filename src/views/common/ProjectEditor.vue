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
        <ScreenElementsManager />
        <AssetsManager />
        <ScreenManager @newScreenCreation:show="isOn_NewScreenCreation=true"/>
        <ComponentsLibrary />
      <PrototypeScreenEditorArea  />
    </div>
    <Modal 
      :isOn="isOn_NewScreenCreation"
      @modal:hide="isOn_NewScreenCreation=false"
    >
      <NewScreenCreation />
    </Modal>
    <!-- Project Editor site - project id {{this.$route.params.id}} -->
    <!-- TODO: Library -->
  </div>
</template>

<script>
import LoggedInNavbarVue from '../../components/general/LoggedInNavbar.vue'
import ScreenElementsManager from '../../components/editor/ScreenElementsManager.vue'
import AssetsManager from '../../components/editor/AssetsManager.vue'
import PrototypeScreenEditorArea from '../../components/editor/PrototypeScreenEditorArea.vue'
import ScreenManager from '../../components/editor/ScreenManager.vue'
import ComponentsLibrary from '../../components/editor/ComponentsLibrary.vue'
import Modal from '../../components/general/Modal.vue'
import NewScreenCreation from '../../components/editor/NewScreenCreation.vue'
import { projectData } from '../../mocks/projectDataMock.js'
import { generatePrototype } from '../../common/generatePrototype.js'



export default {
  name: "ProjectEditor",
  components: {
    LoggedInNavbarVue,
    ScreenElementsManager,
    PrototypeScreenEditorArea,
    AssetsManager,
    ScreenManager,
    ComponentsLibrary,
    Modal,
    NewScreenCreation
  },
  data() {
    return {
      projectData: {},
      isOn_NewScreenCreation: false
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
