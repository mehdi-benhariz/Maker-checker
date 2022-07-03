import React, { useContext } from "react";
import { Navigate, Outlet } from "react-router";
import { AuthContext } from "../context/AuthContext";

const ProtectedRoute = () => {
  const { currentUser } = useContext(AuthContext);
  if (!currentUser.isLoggedIn) return <Navigate to="/login" />;

  return <Outlet />;
};

export default ProtectedRoute;
