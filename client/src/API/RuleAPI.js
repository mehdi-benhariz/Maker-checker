import axios from "axios";
axios.defaults.withCredentials = true;
axios.create({
  baseURL: `${process.env.REACT_APP_API_URL}`,
});
const baseURL = process.env.REACT_APP_API_URL;
const methods = {
  getAllRulesByService: function (serviceTypeId) {
    return axios.get(`/api/Rule/${serviceTypeId}`);
  },
  updateRules: function (rules) {
    return axios.patch("/api/Rule/rules", rules);
  },
};

export const ICall = (action, ...params) => {
  try {
    return methods[action](...params);
  } catch (error) {
    return error.response;
  }
};

//second approach
export const editRules = async (ruleDtos, serviceTypeId) => {
  const url = `${baseURL}/api/Rule/rules`;
  try {
    //todo change it later to patch

    console.log(JSON.stringify(ruleDtos), serviceTypeId);

    return await axios.put(url, JSON.stringify(ruleDtos), {
      headers: {
        "Content-Type": "application/json",
        serviceTypeId: parseInt(serviceTypeId),
      },
    });
  } catch (error) {
    return error.response;
  }
};

export const addRules = async (ruleDtos, serviceTypeId) => {
  const url = `${baseURL}/api/Rule/rules`;
  try {
    //todo change it later to patch

    console.log(ruleDtos, serviceTypeId);

    return await axios.post(url, JSON.stringify(ruleDtos), {
      headers: {
        "Content-Type": "application/json-patch+json",
        //add service type id here
        serviceTypeId: parseInt(serviceTypeId),
      },
    });
  } catch (error) {
    return error.response;
  }
};
