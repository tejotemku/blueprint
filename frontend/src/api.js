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
    return await axios.post(`${APISUFFIX}/api/auth/register`, data, basicHeader);
  },

  async logIn(data) {
    return await axios.post(`${APISUFFIX}/api/auth/login`, data, basicHeader);
  },

  async getMe(token) {
    return await axios.get(`${APISUFFIX}/api/auth/me`, authHeaders(token));
  },

  async checkAndRefreshToken(token) {
    return await axios.get(`${APISUFFIX}/api/auth/check-and-refresh-token`, authHeaders(token));
  },

  async createProject(token, data) {
    return await axios.post(`${APISUFFIX}/api/projects/`, data, authHeaders(token));
  },

  async getUsersProjectsInfo(token, username) {
    return await axios.get(`${APISUFFIX}/api/projects-info/${username}`, authHeaders(token));
  },

  async getProjectFile(token, projectId) {
    return await axios.get(`${APISUFFIX}/api/projects/${projectId}/file`, authHeaders(token));
  },

  async updateProjectFile(token, projectId, data) {
    return await axios.put(`${APISUFFIX}/api/projects/${projectId}/file`, data, authHeaders(token));
  },

  async deleteProject(token, projectId) {
    return await axios.delete(`${APISUFFIX}/api/projects/${projectId}`, authHeaders(token));
  },

}
