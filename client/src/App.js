import React from "react";
import { Route, Routes } from "react-router";

import "./App.css";
import AdminPage from "./components/pages/Admin/AdminPage";
import ClientPage from "./components/pages/Client/ClientPage";
import NotFound from "./components/pages/NotFound";

function App() {
  return (
    <div className="m-0 ">
      <Routes>
        <Route path="/" element={<ClientPage />} />
        <Route path="/admin" element={<AdminPage />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </div>
  );
}

export default App;
