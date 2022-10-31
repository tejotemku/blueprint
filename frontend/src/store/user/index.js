import { api } from '@/api';

const defaultState = {
  username: null,
  token: null,
  loggedIn: false,
  logInError: false
};

export const actions = {
  async actionLogin(context, payload) {
    await api.logIn(payload).then(
      response => {
        const token = response.data;
        context.commit("setToken", token);
        context.commit("setUsername", payload.username);
        saveLocalToken(token);
      }
    );
  },

  async actionRegister(context, payload) {
    await api.register(payload).then(
      response => {
        const token = response.data;
        context.commit("setToken", token);
        context.commit("setUsername", payload.username);
        saveLocalToken(token);
      }
    );
  },

  // async actionLogIn(context, payload) {
  //     try {
  //         const response = await api.logIn(payload.username, payload.password)
  //         const token = response.data.access_token;
  //         if (token) 
  //         {
  //             saveLocalToken(token);
  //             context.commit("setToken", token);
  //             // context.commit("setLoggedIn", true)
  //             // context.commit("setLogInError", false)
  //             // await context.dispatch("actionGetMe");
  //         } 
  //         // else 
  //         // {
  //         //   await context.dispatch("actionLogOut");
  //         // }
  //     } catch (err) {
  //         // context.commit("setLoggedIn", false)
  //         // context.commit("setLogInError", true)
  //         // await context.dispatch("actionLogOut");
  //     }
  // },
}

export const getters = {
  getToken: (state) => state.token,
  getUsername: (state) => state.username,
  loginError: (state) => state.logInError,
}

export const mutations = {
  setToken(state, payload) {
      state.token = payload;
  },
  setUsername(state, payload) {
      state.username = payload;
  }
}

export const userModule = {
  state: defaultState,
  mutations,
  actions,
  getters,
};


export const getLocalToken = () => localStorage.getItem('token');

const saveLocalToken = (token) => localStorage.setItem('token', token);

// const removeLocalToken = () => localStorage.removeItem('token');