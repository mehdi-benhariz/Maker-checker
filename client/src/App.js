import React from "react";
import { Route, Routes } from "react-router";

import "./App.css";
import AdminPage from "./components/Admin/AdminPage";
import ClientPage from "./components/Client/ClientPage";

function App() {
  return (
    <div className="m-0 ">
      <Routes>
        <Route path="/" element={<ClientPage />} />
        <Route path="/admin" element={<AdminPage />} />
      </Routes>
    </div>
  );
}

export default App;
