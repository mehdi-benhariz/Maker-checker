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
export const editRules = async (serviceTypeRules) => {
  const url = `${baseURL}/api/Rule/rules`;
  try {
    //todo change it later to patch

    const ruleDtos = serviceTypeRules.rules.map((r) => ({
      RoleId: r.roleId,
      Nbr: r.nbr,
    }));
    console.log(ruleDtos);

    return await axios.patch(url, ruleDtos, {
      headers: {
        "Content-Type": "application/json-patch+json",
        //add service type id here
        serviceTypeId: serviceTypeRules.serviceTypeId,
      },
    });
  } catch (error) {
    return error.response;
  }
};
