import React from "react";
import { Route, Routes } from "react-router";

import "./App.css";
import AdminPage from "./components/pages/Admin/AdminPage";
import SignInPage from "./components/pages/Auth/SignInPage";
import SignUpPage from "./components/pages/Auth/SignUpPage";
import ClientPage from "./components/pages/Client/ClientPage";

import NotFound from "./components/pages/NotFound";
import StaffPage from "./components/pages/Staff/StaffPage";

import ProtectedRoute from "./components/ProtectedRoute";

function App() {
  return (
    <div className="m-0 ">
      <Routes>
        <Route element={<ProtectedRoute />}>
          <Route path="/client" element={<ClientPage />} />
          <Route path="/admin" element={<AdminPage />} />
          <Route path="/staff" element={<StaffPage />} />
        </Route>
        <Route path="/register" element={<SignUpPage />} />
        <Route path="/login" element={<SignInPage />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </div>
  );
}

export default App;
