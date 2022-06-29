import axios from "axios";
axios.defaults.withCredentials = true;

export const register = (username, password) => {
  const url = `${process.env.REACT_APP_API_URL}/api/auth/register`;
  try {
    return axios.post(url, { username, password });
  } catch (error) {
    return error.response;
  }
};

export const login = (username, password) => {
  const url = `${process.env.REACT_APP_API_URL}/api/auth/login`;
  try {
    return axios.post(url, { username, password });
  } catch (error) {
    return error.response;
  }
};

export const logout = () => {
  const url = `${process.env.REACT_APP_API_URL}/api/auth/logout`;
  try {
    return axios.post(url);
  } catch (error) {
    return error.response;
  }
};
