import React, { useContext } from "react";
import { Navigate, useLocation } from "react-router";
import { AuthContext } from "../context/AuthContext";
import AdminPage from "./pages/Admin/AdminPage";
import Loading from "./pages/Loading";
import StaffPage from "./pages/Staff/StaffPage";
import ClientPage from "./pages/Client/ClientPage";

import UnAuthorized from "./pages/UnAuthorized";

const ProtectedRoute = () => {
  const { currentUser, isLoading, roles } = useContext(AuthContext);

  const location = useLocation();

  if (isLoading) return <Loading />;
  if (!currentUser.isLoggedIn) return <Navigate to="/login" />;
  console.log(location.pathname);
  switch (location.pathname) {
    case "/Admin":
      console.log("test");
      if (currentUser.role === "Admin") return <AdminPage />;
      break;
    case "/Staff":
      console.log(roles.some((r) => r.name === currentUser.role));
      if (roles.some((r) => r.name === currentUser.role)) {
        return <StaffPage />;
      }

      break;
    case "/Client":
      if (currentUser.role === "Client") return <ClientPage />;
      break;
    default:
      return <Navigate to="/login" />;
  }
  return <UnAuthorized />;
};

export default ProtectedRoute;
