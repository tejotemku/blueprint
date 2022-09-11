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
        <ScreenManager 
          @newScreenCreation:show="showNewScreenCreator" 
          @screenEditingTool:show="showScreenEditingTool"
        />
        <ComponentsLibrary />
      <PrototypeScreenEditorArea  />
    </div>
    <Modal 
      :isOn="isOn_NewScreenCreation"
      @modal:hide="hideModal"
    >
      <ManageScreenTool 
        @closeTool="hideModal" 
        :editingMode="screnManagerToolEditingMode"
        :screenInfo="screenToEditData"
      />
    </Modal>
    <!-- Project Editor site - project id {{this.$route.params.id}} -->
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
import ManageScreenTool from '../../components/editor/ManageScreenTool.vue'
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
    ManageScreenTool
  },
  data() {
    return {
      projectData: {},
      isOn_NewScreenCreation: false,
      screnManagerToolEditingMode: false,
      screenToEditData: {}
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
    },
    hideModal() {
      this.isOn_NewScreenCreation = false;
    },
    showNewScreenCreator() {
      this.screnManagerToolEditingMode = false;
      this.screenToEditData = {};
      this.isOn_NewScreenCreation = true;
    },
    showScreenEditingTool(id, screenName) {
      this.screnManagerToolEditingMode = true;
      this.screenToEditData = {
        id: id,
        name: screenName
      }
      this.isOn_NewScreenCreation = true;
    }
  }
}
</script>

<style scoped>
.project-view {
  margin-block-start: 75px;
}
</style>
