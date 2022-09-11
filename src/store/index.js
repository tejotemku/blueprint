import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)


const state = {
  projectData : {
    title: "unknown_project_name",
    screens: {
      '0': {
        name: "",
        elements: [ ]
      },
    },
    assets: [ ],
    currentScreenId: '0',
  },
  draggedItem: null,
  userToken: null,
}

export const mutations = {
  setProjectData(state, data) {
    state.projectData = data;
  },
  setCurrentScreenElements(state, elements) {
    state.projectData.screens[state.projectData.currentScreenId].elements = elements;
  },
  setDraggedItem(state, item) {
    state.draggedItem = item;
  },
  setCurrentScreen(state, screenId) {
    state.projectData.currentScreenId = screenId;
  },
  addElementToCurrentScreenElements(state, element) {
    state.projectData.screens[state.projectData.currentScreenId].elements.push(element);
  },
  addScreenToProject(state, name) {
    const id = Date.now();
    state.projectData.screens[id] = {
      name: name,
      elements: []
    };
  },
  removeScreenFromProject(state, id) {
    delete state.projectData.screens[id];
  },
  changeScreenName(state, data) {
    state.projectData.screens[data.id].name = data.name;
  }
}

export const actions = {
  actionSetProjectData(state, data) {
    state.commit("setProjectData", data);
  },
  actionSetCurrentScreenElements(state, data) {
    state.commit('setCurrentScreenElements', data);
  },
  actionSetDraggedItem(state, data) {
    state.commit('setDraggedItem', data)
  },
  actionResetDraggedItem(state) {
    state.commit('setDraggedItem', null)
  },
  actionSetCurrentScreen(state, data) {
    state.commit('setCurrentScreen', data)
  },
  actionAddElementToCurrentScreenElements(state, data) {
    state.commit('addElementToCurrentScreenElements', data);
  },
  actionsAddScreenToProject(state, data) {
    state.commit('addScreenToProject', data)
  },
  actionsRemoveScreenFromProject(state, data) {
    state.commit('removeScreenFromProject', data)
  },
  actionChangeScreenName(state, data) {
    state.commit('changeScreenName', data)
  }
}

export const getters =  {
  getProjectData() {
    return state.projectData;
  },
  getCurrentScreenData() {
    return state.projectData.screens[state.projectData.currentScreenId];
  },
  getCurrentScreenElementsData() {
    return state.projectData.screens[state.projectData.currentScreenId].elements;
  },
  getProjectAssets() {
    return state.projectData.assets;
  },
  getDraggedItem() {
    return state.draggedItem;
  },
  getScreensInfo() {
    return state.projectData.screens;
  }
}

export default new Vuex.Store({
  state,
  mutations,
  actions,
  modules: {
  },
  getters,
})