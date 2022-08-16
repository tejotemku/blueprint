<template>
  <div>
    <LoggedInNavbarVue />
    <MinimiseComponentBtn>
      <ScreenElementsManager :screenElements="projectData.screens[currentScreenId].elements" />
    </MinimiseComponentBtn>

    <PrototypeScreenEditorArea />
    <!-- Project Editor site - project id {{this.$route.params.id}} -->

    <!-- TODO: Project view -->
    <!-- TODO: Assets -->
    <!-- TODO: Library -->
    <!-- TODO: Pages -->
    <!-- TODO: Layers -->
  </div>
</template>

<script>
import LoggedInNavbarVue from '../../components/elementary/LoggedInNavbar.vue'
import ScreenElementsManager from '../../components/complex/ScreenElementsManager.vue'
import MinimiseComponentBtn from '../../components/complex/MinimiseComponentBtn.vue'
import PrototypeScreenEditorArea from '../../components/complex/PrototypeScreenEditorArea.vue'


export default {
  name: "ProjectEditor",
  components: {
    LoggedInNavbarVue,
    ScreenElementsManager,
    MinimiseComponentBtn,
    PrototypeScreenEditorArea
  },
  data() {
    return {
      projectData: {},
      currentScreenId: 0
    }
  },
  mounted() {
    try {
      this.projectData = this.fetchData();
    }
    catch(e) {
      console.log(e);
    }
  },
  methods: {
    fetchData() {
      // let projectId = this.$route.params.id;
      // TODO: this is a mock
      return {
        screens: [
          {
            id: 0,
            name: "Mock Screen 1",
            elements: [
              {
                id: 0,
                depth: 0,
                description: "Button - 1",
                class: "btn btn-primary",
                properties: {
                  text: "Click Me #1"
                }
              },
              {
                id: 1,
                depth: 2,
                description: "Button - 2",
                class: "btn btn-secondary",
                properties: {
                  text: "Click Me #2"
                }
              },
              {
                id: 2,
                depth: 1,
                description: "Email Field - 2",
                class: "form-control",
                properties: {
                  text: "Click Me #2"
                }
              },
            ]
          }
        ],
        title: "Mock Project Name",
        assets: []
      }
    },
    printProjectData() {
      console.log(this.projectData);
    }
  }
}
</script>
