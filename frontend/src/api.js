import { default as Axios } from 'axios';
import { API_URL } from "./config";

const axios = Axios.create({
  validateStatus: function (status) {
    return status >= 200 && status < 300;
  },
  baseURL: API_URL
});


function authHeaders(token) {
  return {
    headers: {
      "Authorization": `bearer ${token}`,
      "Access-Control-Allow-Origin": "*"
    },
  };
}

const basicHeader = {
  headers: {
    "Access-Control-Allow-Origin": "*"
  }
}

const APISUFFIX = ""


export const api = {
  async register(data) {
    return await axios.post(`${APISUFFIX}/api/auth/register/`, data, basicHeader);
  },

  async logIn(data) {
    return await axios.post(`${APISUFFIX}/api/auth/login/`, data, basicHeader);
  },

  async createProject(token, data) {
    return await axios.post(`${APISUFFIX}/api/projects/`, data, authHeaders(token));
  },

  async getUsersProjectsInfo(token, username) {

    return await axios.get(`${APISUFFIX}/api/projects-info/${username}`, authHeaders(token));
  },

  // async test() {
  //   const params = new URLSearchParams();
  //   params.append('name', 'test');
  //   return axios.get(`${APISUFFIX}/api/test/`, params, basicHeader)
  // },

  // async testToken(token) {
  //   return axios.get(`${APISUFFIX}/api/test-token/`, authHeaders(token))
  // }
}



// ==========================
// reference 
// ==========================

// export const api = {
//   async logIn(username, password) {
//     const params = new URLSearchParams();
//     params.append('username', username);
//     params.append('password', password);

//     return axios.post(`${APISUFFIX}/api/login/access-token`, params);
//   },

//   async getMe(token) {
//     return axios.get(`${APISUFFIX}/api/users/me`, authHeaders(token));
//   },

//   async updateMe(token, data) {
//     return axios.put(`${APISUFFIX}/api/users/me`, data, authHeaders(token));
//   },

//   async getUsers(token, params) {
//     let config = authHeaders(token);
//     if (params !== undefined) {
//       config['params'] = params;
//     }
//     return axios.get(`${APISUFFIX}/api/users/`, config);
//   },
//   async getUser(token, id) {
//     return axios.get(`${APISUFFIX}/api/users/${id}`, authHeaders(token));
//   },
//   async updateUser(token, userId, data) {
//     return axios.put(`${APISUFFIX}/api/users/${userId}`, data, authHeaders(token));
//   },

//   async createUser(token, data) {
//     return axios.post(`${APISUFFIX}/api/users/`, data, authHeaders(token));
//   },

//   async createUserOpen(data) {
//     return axios.post(`${APISUFFIX}/api/users/open`, data);
//   },

//   async uploadFile(token, file, patientID) {
//     let config = authHeaders(token);
//     config.headers['Content-Type'] = 'multipart/form-data';
//     config["params"] = {};
//     config.params["patient_id"] = patientID;
//     return axios.post(`${APISUFFIX}/api/analysis/recordings/upload`,
//       file,
//       config);
//   },

//   async startAnalysis(token, recordingID) {
//     return axios.post(`${APISUFFIX}/api/analysis/recordings/${recordingID}/analyze`, null, authHeaders(token));
//   },
// }