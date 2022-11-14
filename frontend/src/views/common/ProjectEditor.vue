<template>
  <div>
    <Modal 
      v-if="isModalOn"
      @modal:hide="hideModal"
    >
      <ManageScreenTool
        v-if="isOn_ScreenManagementTool"
        @closeTool="hideModal" 
        :editingMode="screnManagerToolEditingMode"
        :screenInfo="screenToEditData"
      />
      <NewAssetTool
        v-if="isOn_NewAssetTool"
        @closeTool="hideModal" 
      />

      <ManageElementsPropertiesTool
        v-if="isOn_ManageElementsPropertiesTool"
        @closeTool="hideModal" 
        :elementProperties="elementProperties"
        :elementId="elementId"
        :elementDescription="elementDescription"
      />
      <GuestCreateProjectForm 
        v-if="isOn_GuestCreateProject"
        @createdProject="createdGuestProject"
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
      <v-btn
        v-if="!isGuestMode"
        color="success"
        @click="savePrototype"
        class="mx-2"
      >
      Save Prototype
      </v-btn>
    </LoggedInNavbarVue>
    <div v-if="projectLoaded" class="project-view">
        <ScreenElementsManager 
          @elementEditingTool:show="showManageElementsPropertiesTool"
        />
        <AssetsManager
          @newAssetTool:show="showNewAssetTool"
        />
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
import LoggedInNavbarVue from '@/components/general/LoggedInNavbar.vue';
import ScreenElementsManager from '@/components/editor/ScreenElementsManager.vue';
import AssetsManager from '@/components/editor/AssetsManager.vue';
import PrototypeScreenEditorArea from '@/components/editor/PrototypeScreenEditorArea.vue';
import ScreenManager from '@/components/editor/ScreenManager.vue';
import ComponentsLibrary from '@/components/editor/ComponentsLibrary.vue';
import Modal from '@/components/general/Modal.vue';
import ManageScreenTool from '@/components/editor/ManageScreenTool.vue';
import ManageElementsPropertiesTool from '@/components/editor/ManageElementsPropertiesTool.vue';
import NewAssetTool from '@/components/editor/NewAssetTool.vue';
import GuestCreateProjectForm from '@/components/general/GuestCreateProjectForm.vue';

import { api } from '@/api';
import { mapGetters } from "vuex";
import { generatePrototype } from '@/common/generatePrototype.js';



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
    ManageElementsPropertiesTool,
    GuestCreateProjectForm,
    NewAssetTool
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
      projectLoaded: false,
      isGuestMode: false,
      isOn_GuestCreateProject: false,
      isOn_NewAssetTool: false
    }
  },
  computed: {
    ...mapGetters({
      token: 'getToken',
      storedProjectData: 'getProjectData'
    }),
    isModalOn() {
      return this.isOn_ScreenManagementTool 
      || this.isOn_ManageElementsPropertiesTool 
      || this.isOn_GuestCreateProject
      || this.isOn_NewAssetTool;
    },
  },
  beforeMount() {
    this.isGuestMode = this.checkGuestMode();
    this.getProjectData();
  },
  methods: {
    createdGuestProject() {
      this.isOn_GuestCreateProject = false;
      this.projectData = this.storedProjectData;
      this.projectLoaded = true;
    },
    setProjectData(data) {
      this.$store.dispatch("actionSetProjectData", data);
      this.projectData = data;
      this.projectLoaded = true;
    },
    checkGuestMode() {
      return this.$route.path == "/guest";
    },
    savePrototype() {
      try {
        api.updateProjectFile(
          this.token, 
          this.$route.params.id, 
          {
            "file": JSON.stringify(this.projectData)
          }
        );
        console.log("saved");
      }
      catch(err) {
        console.log(err);
      }
    },
    getProjectData() {
      if(this.isGuestMode) {
        let data = localStorage.getItem('projectData');
        if (data) this.setProjectData(data);
        else this.isOn_GuestCreateProject = true;
      } else {
        try {
          api.getProjectFile(this.token, this.$route.params.id).then(
            response => {
              this.setProjectData(response.data);
              let autosavePeriod = 5*60*1000;
              setInterval(() => this.savePrototype(), autosavePeriod);
            }
          );

        }
        catch(err) {
          console.log(err);
        }
      }
    },
    generatePrototypeActions() {
      generatePrototype();
    },
    hideModal() {
      this.isOn_ScreenManagementTool = false;
      this.isOn_ManageElementsPropertiesTool = false;
      this.isOn_GuestCreateProject = false;
      this.isOn_NewAssetTool = false;
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
    },
    showNewAssetTool() {
      this.isOn_NewAssetTool = true;
    }
  }
}
</script>

<style scoped>
.project-view {
  margin-block-start: 75px;
}
</style>
