import axios from "axios";
axios.defaults.withCredentials = true;

export const addOperation = async (reqId) => {
  const url = `${process.env.REACT_APP_API_URL}/api/Operation`;
  try {
    return await axios.post(url, reqId, {
      headers: {
        "Content-Type": "application/json",
      },
    });
  } catch (error) {
    return error.response;
  }
};

export const getRequestsDuty = async (userId) => {
  const url = `${process.env.REACT_APP_API_URL}/api/Operation/duty/${userId}`;
  try {
    return await axios.get(url);
  } catch (error) {
    return error.response;
  }
};
