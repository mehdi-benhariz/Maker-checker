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

export const addServiceType = async (name) => {
  const url = `${process.env.REACT_APP_API_URL || baseUrl}/api/ServiceType`;
  try {
    return await axios.post(url, JSON.stringify(name), {
      headers: {
        "Content-Type": "application/json",
      },
    });
  } catch (error) {
    return error.response;
  }
};
export const deleteServiceType = async (stId) => {
  const url = `${
    process.env.REACT_APP_API_URL || baseUrl
  }/api/ServiceType/${parseInt(stId)}`;
  try {
    return await axios.delete(url, {
      headers: {
        "Content-Type": "application/json",
      },
    });
  } catch (error) {
    return error.response;
  }
};
