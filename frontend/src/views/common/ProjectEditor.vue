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
        color="info"
        @click="fullWindowSwitch"
        class="mx-2"
      >
        Full Window Mode
      </v-btn>
      <v-btn
        color="secondary"
        @click="generatePrototypeActions"
        class="mx-2"
      >
        Generate Prototype
      </v-btn>
      <v-btn
        color="success"
        @click="savePrototype(autosave=false)"
        class="mx-2"
      >
        {{ isGuestMode? 'Save locally' : 'Save Prototype' }}
      </v-btn>
    </LoggedInNavbarVue>
    <div v-if="projectLoaded" class="project-view">
        <ScreenElementsManager
          v-if="!fullWindowMode" 
          @elementEditingTool:show="showManageElementsPropertiesTool"
        />
        <AssetsManager
          v-if="!fullWindowMode"
          @newAssetTool:show="showNewAssetTool"
        />
        <ScreenManager 
          v-if="!fullWindowMode"
          @newScreenCreation:show="showNewScreenCreator" 
          @screenEditingTool:show="showScreenEditingTool"
        />
        <ComponentsLibrary 
          v-if="!fullWindowMode"
        />
      <PrototypeScreenEditorArea
        :style="prototypeEditorPadding" 
        :maxWidthPercentage="prototypeEditorMaxWidthPercentage"
        :maxHeightPercentage="prototypeEditorMaxHeightPercentage"
        />
    </div>
    <v-snackbar
      v-model="snackbar"
      style="z-index: 99999;"
    >
      {{snackbarMessage}}
      <template v-slot:action="{ attrs }">
        <v-btn
          color="red"
          text
          v-bind="attrs"
          @click="closeSnackbar"
        >
          Close
        </v-btn>
      </template>
    </v-snackbar>
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
import { generatePrototype } from '@/utils/generatePrototype.js';



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
      isOn_NewAssetTool: false,
      prototypeEditorMaxWidthPercentage: 0.65,
      prototypeEditorMaxHeightPercentage: 0.65,
      fullWindowMode: false,
      snackbar: false,
      snackbarMessage: '',
      snackbarHideTimer: null
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
    prototypeEditorPadding() {
      return this.fullWindowMode ? '' : 'bottom: 10vh !important';
    }
  },
  beforeMount() {
    this.isGuestMode = this.checkGuestMode();
    this.getProjectData();
  },
  methods: {
    fullWindowSwitch() {
      this.fullWindowMode = !this.fullWindowMode;
      this.prototypeEditorMaxWidthPercentage = this.fullWindowMode ? 0.9 : 0.65;
      this.prototypeEditorMaxHeightPercentage  = this.fullWindowMode ? 0.85 : 0.65;
    },
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
    async savePrototype(autosave=false) {
      if(this.isGuestMode) {
        localStorage.setItem('projectData', JSON.stringify(this.projectData));
        this.openSnackbar(`${autosave?'Autosave - ':''}Save successful`);
      } else {
        await api.updateProjectFile(
          this.token, 
          this.$route.params.id, 
          {
            "file": JSON.stringify(this.projectData)
          }
        ).then(
          () => this.openSnackbar(`${autosave?'Autosave - ':''}Save successful`)
        ).catch( err => {
          this.openSnackbar(`${autosave?'Autosave - ':''}Save failed`);
          console.log(err);
        });
      }
    },
    getProjectData() {
      if(this.isGuestMode) {
        let data = JSON.parse(localStorage.getItem('projectData'));
        console.log(data);
        if (data) {
          this.setProjectData(data);
          this.projectLoaded = true;
        }
        else this.isOn_GuestCreateProject = true;
      } else {
        try {
          api.getProjectFile(this.token, this.$route.params.id).then(
            response => {
              this.setProjectData(response.data);
              let autosavePeriod = 300000; // 5 minutes
              setInterval(() => this.savePrototype(true), autosavePeriod);
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
    },
    openSnackbar(snackbarMessage) {
      this.snackbarMessage = snackbarMessage;
      this.snackbarHideTimer = setTimeout(() => this.closeSnackbar(), 5000);
      this.snackbar = true;
    },
    closeSnackbar() {
      this.snackbar = false;
      this.snackbarHideTimer = false;
    }
  }
}
</script>

<style scoped>
.project-view {
  margin-block-start: 75px;
}
</style>
