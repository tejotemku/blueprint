import { api } from '@/api';

const defaultState = {
  username: null,
  token: null
};

export const actions = {
  async actionLogin(context, payload) {
    await api.logIn(payload).then(
      response => {
        const token = response.data;
        context.commit("setToken", token);
        saveLocalToken(token);
        context.commit("setUsername", payload.username);
      }
    ).catch(
      err => {
        context.dispatch("actionLogOut");
        console.log(err);
      }
    );
  },

  async actionRegister(context, payload) {
    await api.register(payload).then(
      response => {
        const token = response.data;
        context.commit("setToken", token);
        saveLocalToken(token);
        context.commit("setUsername", payload.username);
      }
    );
  },

  async actionCheckAndRefreshToken(context) {
    await api.checkAndRefreshToken(context.state.token).then(
      async function(response) {
        const token = response.data;
        context.commit("setToken", token);
        saveLocalToken(token);
        await context.dispatch("actionGetMe");
      }
    ).catch( 
      err => {
        context.dispatch("actionLogOut");
        console.log(err);
      }
    )
  },

  async actionGetMe(context) {
    await api.getMe(context.state.token).then(
      response => {
        const data = response.data;
        context.commit("setUsername", data.username);
      }
    ).catch(
      err => {
        console.log(err);
      }
    )
  },

  async actionCheckLoggedIn(context) {
    if(!context.state.token) {
      context.commit("setToken", getLocalToken());
    }
    if(context.state.token) {
      await context.dispatch("actionCheckAndRefreshToken");
    }
  },

  async actionLogOut(context) {
    context.commit("setToken", null);
    removeLocalToken();
  }
}

export const getters = {
  getToken: (state) => state.token,
  getUsername: (state) => state.username,
}

export const mutations = {
  setToken(state, payload) {
      state.token = payload;
  },
  setUsername(state, payload) {
      state.username = payload;
  },
}

export const userModule = {
  state: defaultState,
  mutations,
  actions,
  getters,
};


const getLocalToken = () => localStorage.getItem('token');

const saveLocalToken = (token) => localStorage.setItem('token', token);

const removeLocalToken = () => localStorage.removeItem('token');