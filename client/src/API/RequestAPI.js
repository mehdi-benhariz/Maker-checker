import axios from "axios";
axios.defaults.withCredentials = true;
//! didn't work
// axios.create({
//   baseURL: `${process.env.REACT_APP_API_URL}`,
// });
// axios.defaults.baseURL = process.env.REACT_APP_API_URL;

//option 1
export const getRequestByClient = async (pageNumber) => {
  const url = `${process.env.REACT_APP_API_URL}/api/Request/client?pageNumber=${pageNumber}`;
  try {
    return await axios.get(url);
  } catch (error) {
    return error.response;
  }
};

export const addRequest = async (request) => {
  try {
    return await axios.post(
      `${process.env.REACT_APP_API_URL}/api/Request/`,
      request
    );
  } catch (error) {
    return error.response;
  }
};
export const getRequestsToAdmin = async (pageNumber, search) => {
  const url = `${process.env.REACT_APP_API_URL}/api/Request/admin?pageNumber=${pageNumber}&&search=${search}`;
  try {
    return await axios.get(url);
  } catch (error) {
    return error.response;
  }
};
export const getRequestsToStaff = async () => {
  const url = `${process.env.REACT_APP_API_URL}/api/Request/staff`;
  try {
    return await axios.get(url);
  } catch (error) {
    return error.response;
  }
};
//option 2+
const methods = {
  getRequestHistory: function () {
    return axios.get(`/api/Request/client?pageNumber=1`);
  },
};

export const ICall = async (action, ...params) => {
  try {
    return await methods[action](...params);
  } catch (error) {
    console.log(error);
    return error.response;
  }
};
