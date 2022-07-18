import React, { useEffect, useState } from "react";
import { getUserInfo } from "../API/AuthAPI";
import { getAllStaffRoles } from "../API/RoleAPI";

export const AuthContext = React.createContext();

const AuthContextProvider = (props) => {
  const [currentUser, setCurrentUser] = useState({
    isLoggedIn: false,
  });
  const [isLoading, setIsLoading] = useState(true);
  const [roles, setRoles] = useState([]);
  let handleUserInfo = async () => {
    try {
      let res = await getUserInfo();
      console.log(res);
      if (res.status === 200)
        setCurrentUser({
          isLoggedIn: true,
          ...res.data,
        });
    } catch (error) {
      console.log(error);
    } finally {
      setIsLoading(false);
    }
    setIsLoading(false);
  };
  let getRoles = async () => {
    let res = await getAllStaffRoles();
    if (res.status === 200) setRoles(res.data);
  };

  useEffect(() => {
    handleUserInfo();
    getRoles();
  }, []);
  return (
    <AuthContext.Provider
      value={{ currentUser, setCurrentUser, handleUserInfo, isLoading, roles }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthContextProvider;
