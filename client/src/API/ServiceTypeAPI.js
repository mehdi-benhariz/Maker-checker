import axios from "axios";
axios.defaults.withCredentials = true;

const baseUrl = "http://localhost:5000";

export const getAllServiceTypes = async () => {
  const url = `${process.env.REACT_APP_API_URL || baseUrl}/api/ServiceType`;
  try {
    return await axios.get(url);
  } catch (error) {
    return error.response;
  }
};
