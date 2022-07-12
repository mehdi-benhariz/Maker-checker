import axios from "axios";
axios.defaults.withCredentials = true;
const baseUrl = process.env.REACT_APP_API_URL;

export const SubmitRole = async (roleName) => {
  const url = `${baseUrl}/api/Role`;
  try {
    return await axios.post(url, roleName, {
      headers: {
        "Content-Type": "application/json",
      },
    });
  } catch (error) {
    return error.response;
  }
};
export const getAllStaffRoles = async () => {
  const url = `${baseUrl}/api/Role/staff`;
  try {
    return await axios.get(url);
  } catch (error) {
    return error.response;
  }
};
