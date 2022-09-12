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
    defaultScreenId: '0',
  },
  selectedElementId: null,
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
  addElementToCurrentScreen(state, element) {
    state.projectData.screens[state.projectData.currentScreenId].elements.unshift(element);
  },
  removeElementFromCurrentScreen(state, elementId) {
    const newElements = state.projectData.screens[state.projectData.currentScreenId].elements.filter( el => el.id != elementId);
    state.projectData.screens[state.projectData.currentScreenId].elements = newElements;
  },
  addScreenToProject(state, data) {
    const oldScreens = state.projectData.screens;
    // cheesy hack to force update after object mutation
    state.projectData.screens = {
      ...oldScreens,
      [data.id]:{
        name: data.name,
        elements: []
      }
    };
  },
  removeScreenFromProject(state, id) {
    let oldScreens = { ...state.projectData.screens};
    delete oldScreens[id];
    state.projectData.screens = oldScreens;
    if(id == state.projectData.currentScreenId) {
      state.projectData.currentScreenId = Object.keys(state.projectData.screens)[0];
    }
    if(id == state.projectData.defaultScreenId) {
      state.projectData.defaultScreenId = Object.keys(state.projectData.screens)[0];
    }
  },
  changeScreenName(state, data) {
    state.projectData.screens[data.id].name = data.name;
  },
  editElementProperties(state, data) {
    let elements = state.projectData.screens[state.projectData.currentScreenId].elements;
    let result = elements.find(element => {
      return element.id == data.id;
    })
    result.properties = { ...data.properties};
    result.description = data.description;
  },
  setDefaultScreenId(state, screenId) {
    state.projectData.defaultScreenId = screenId;
  },
  setSelectedElementId(state, elementId) {
    state.selectedElementId = elementId;
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
  actionAddElementToCurrentScreen(state, data) {
    state.commit('addElementToCurrentScreen', data);
  },
  actionRemoveElementFromCurrentScreen(state, data) {
    state.commit('removeElementFromCurrentScreen', data);
  },
  actionsAddScreenToProject(state, data) {
    state.commit('addScreenToProject', data)
  },
  actionsRemoveScreenFromProject(state, data) {
    state.commit('removeScreenFromProject', data)
  },
  actionChangeScreenName(state, data) {
    state.commit('changeScreenName', data)
  },
  actionEditElementProperties(state, data) {
    state.commit('editElementProperties', data);
  },
  actionSetDefaultScreenId(state, data) {
    state.commit('setDefaultScreenId', data);
  },
  actionSetSelectedElementId(state, data) {
    state.commit('setSelectedElementId', data);
  },
  actionResetSelectedElementId(state) {
    state.commit('setSelectedElementId', null);
  },
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
  },
  getDefaultScreenId() {
    return state.projectData.defaultScreenId;
  },
  getSelectedItemId() {
    return state.selectedElementId;
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