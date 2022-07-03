import axios from "axios";
axios.defaults.withCredentials = true;

export const addOperation = async (reqId) => {
  const url = `${process.env.REACT_APP_API_URL}/api/operation`;
  try {
    return await axios.post(url, reqId);
  } catch (error) {
    return error.response;
  }
};

export const getRequestsDuty = async (userId) => {
  const url = `${process.env.REACT_APP_API_URL}/api/operation/duty/${userId}`;
  try {
    return await axios.get(url);
  } catch (error) {
    return error.response;
  }
};