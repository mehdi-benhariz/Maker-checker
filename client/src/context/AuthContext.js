import React, { useEffect, useState } from "react";
import { getUserInfo } from "../API/AuthAPI";

export const AuthContext = React.createContext();

const AuthContextProvider = (props) => {
  const [currentUser, setCurrentUser] = useState({
    isLoggedIn: true,
  });
  let handleUserInfo = async () => {
    // let res = await getUserInfo();
    // if (res.status === 200)
    //   setCurrentUser({
    //     isLoggedIn: true,
    //     ...res.data,
    //   });
    // else alert(res.errors);
  };

  useEffect(() => handleUserInfo, []);
  return (
    <AuthContext.Provider value={{ currentUser, setCurrentUser }}>
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthContextProvider;
