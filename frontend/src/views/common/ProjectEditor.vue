<template>
  <div>    
    <Modal 
      v-if="isModalOn()"
      @modal:hide="hideModal"
    >
      <ManageScreenTool
        v-if="isOn_ScreenManagementTool"
        @closeTool="hideModal" 
        :editingMode="screnManagerToolEditingMode"
        :screenInfo="screenToEditData"
      />
      <ManageElementsPropertiesTool
        v-if="isOn_ManageElementsPropertiesTool"
        @closeTool="hideModal" 
        :elementProperties="elementProperties"
        :elementId="elementId"
        :elementDescription="elementDescription"
      />
    </Modal>
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
        <ScreenElementsManager 
          @elementEditingTool:show="showManageElementsPropertiesTool"
        />
        <AssetsManager />
        <ScreenManager 
          @newScreenCreation:show="showNewScreenCreator" 
          @screenEditingTool:show="showScreenEditingTool"
        />
        <ComponentsLibrary />
      <PrototypeScreenEditorArea style="bottom: 10vh !important" />
    </div>

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
import ManageElementsPropertiesTool from '../../components/editor/ManageElementsPropertiesTool.vue'
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
    ManageScreenTool,
    ManageElementsPropertiesTool
  },
  data() {
    return {
      projectData: {},
      isOn_ScreenManagementTool: false,
      isOn_ManageElementsPropertiesTool: false,
      screnManagerToolEditingMode: false,
      screenToEditData: {},
      elementProperties: {},
      elementId: '',
      elementDescription: '',
    }
  },
  beforeMount() {
    this.getProjectData();
  },
  methods: {
    isModalOn() {
      return this.isOn_ScreenManagementTool || this.isOn_ManageElementsPropertiesTool;
    },
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
      this.isOn_ScreenManagementTool = false;
      this.isOn_ManageElementsPropertiesTool = false;
    },
    showNewScreenCreator() {
      this.screnManagerToolEditingMode = false;
      this.screenToEditData = {};
      this.isOn_ScreenManagementTool = true;
    },
    showScreenEditingTool(id, screenName) {
      this.screnManagerToolEditingMode = true;
      this.screenToEditData = {
        id: id,
        name: screenName
      }
      this.isOn_ScreenManagementTool = true;
    },
    showManageElementsPropertiesTool(id, properties, description) {
      this.elementId = id;
      this.elementProperties = properties;
      this.elementDescription = description;
      this.isOn_ManageElementsPropertiesTool = true;
    }
  }
}
</script>

<style scoped>
.project-view {
  margin-block-start: 75px;
}
</style>