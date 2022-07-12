import axios from "axios";
axios.defaults.withCredentials = true;

export const getAllStaff = async () => {
  const url = `${process.env.REACT_APP_API_URL}/api/Staff/`;
  try {
    return await axios.get(url);
  } catch (error) {
    return error.response;
  }
};
export const UpdateUserRole = async (userId, roleId) => {
  const url = `${process.env.REACT_APP_API_URL}/api/Staff/${userId}`;
  try {
    return await axios.patch(url, roleId, {
      headers: {
        "Content-Type": "application/json-patch+json",
        accept: "text/plain",
      },
    });
  } catch (error) {
    return error.response;
  }
};
