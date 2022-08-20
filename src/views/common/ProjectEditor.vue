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
      <PrototypeScreenEditorArea  />
    </div>
    <!-- Project Editor site - project id {{this.$route.params.id}} -->

    <!-- TODO: Assets -->
    <!-- TODO: Library -->
    <!-- TODO: Screens -->
  </div>
</template>

<script>
import LoggedInNavbarVue from '../../components/general/LoggedInNavbar.vue'
import ScreenElementsManager from '../../components/editor/ScreenElementsManager.vue'
import AssetsManager from '../../components/editor/AssetsManager.vue'
import MinimiseComponentBtn from '../../components/editor/MinimiseComponentBtn.vue'
import PrototypeScreenEditorArea from '../../components/editor/PrototypeScreenEditorArea.vue'


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
      return {
        title: "Mock Project Name",
        screens: {
          '0': {
            id: '0',
            name: "Mock Screen 1",
            elements: [
              {
                id: 0,
                depth: 0,
                top: 10,
                left: 10,
                description: "Button - 1",
                html: '<div style="background-color: red"> Click Me #1 </div>',
                properties: {
                  text: 'Click Me #1',
                }
              },
              {
                id: 1,
                depth: 2,
                top: 10,
                left: 100,
                description: 'Button - 2',
                html: '<div style="background-color: cyan"> Click Me #2 </div>',
                properties: {
                  text: 'Click Me #2',
                }
              },
              {
                id: 2,
                depth: 1,
                top: 300,
                left: 10,
                description: 'Email Field - 2',
                html: '<div style="background-color: limegreen"> Email </div>',
                properties: {
                  text: 'Input e-mail adress',
                }
              },
            ]
          },
          '1': {
            id: '1',
            name: "Mock Screen 2",
            elements: []
          }
        },
        assets: [
          'https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Google_2015_logo.svg/2560px-Google_2015_logo.svg.png',
          'https://i.pinimg.com/originals/ce/af/83/ceaf8384322af790486cff176a0a2f24.png',
          'https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/Playstation_logo_colour.svg/1009px-Playstation_logo_colour.svg.png',
          ],
        currentScreenId: '0'
      }
    },
    getProjectData() {
      try {
        const projectData = this.fetchData();
        this.$store.dispatch("actionSetProjectData", projectData);
        this.projectData = projectData;
      }
      catch(e) {
        console.log(e);
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
