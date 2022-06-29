import axios from "axios";
axios.defaults.withCredentials = true;
axios.create({
  baseURL: `{process.env.REACT_APP_API_URL}`,
});
const methods = {
  getAllRulesByService: function(serviceTypeId) {
    return axios.get(`/api/Rule/${serviceTypeId}`);
  },
  updateRules: function(rules) {
    return axios.patch("/api/Rule/rules", rules);
  },
};

export const addRules = () => {
  const url = `${process.env.REACT_APP_API_URL}/api/Rule/rules`;
  try {
    return axios.patch(url, {});
  } catch (error) {
    return;
  }
};
export const ICall = (action, ...params) => {
  try {
    return methods[action](...params);
  } catch (error) {
    return error.response;
  }
};
