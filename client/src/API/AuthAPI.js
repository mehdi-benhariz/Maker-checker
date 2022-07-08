import axios from "axios";
axios.defaults.withCredentials = true;

export const register = async (username, password) => {
  const url = `${process.env.REACT_APP_API_URL}/api/Auth/register`;
  try {
    return await axios.post(url, { username, password });
  } catch (error) {
    return error.response;
  }
};

export const login = async (username, password) => {
  const url = `${process.env.REACT_APP_API_URL}/api/auth/login`;
  try {
    return await axios.post(url, { username, password });
  } catch (error) {
    return error.response;
  }
};

export const logout = async () => {
  const url = `${process.env.REACT_APP_API_URL}/api/auth/logout`;
  try {
    return await axios.post(url);
  } catch (error) {
    return error.response;
  }
};

export const getUserInfo = () => {
  const url = `${process.env.REACT_APP_API_URL}/api/Auth/user`;
  try {
    return axios.get(url);
  } catch (error) {
    return error.response;
  }
};
