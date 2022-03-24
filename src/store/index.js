import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)


const state = {
  snackbarOpen: false,
  snackbarText: "",
}

export const mutations = {
  setPatient(state, patient) {
    state.currentPatient = patient;
  },
  openSnackbar(state, text) {
    state.snackbarOpen = true;
    state.snackbarText = text;
  },
  closeSnackbar(state) {
    state.snackbarOpen = false;
  }
}

export const actions = {
}

export const getters =  {
  isSnackbarOpened: (state) => state.snackbarOpen,
  snackbarText: (state) => state.snackbarText,
}

export default new Vuex.Store({
  state,
  mutations,
  actions,
  modules: {
  },
  getters,
})