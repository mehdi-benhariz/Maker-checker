import React from "react";
import { Route, Routes } from "react-router";

import "./App.css";
import AdminPage from "./components/AdminPage";
import Home from "./components/Home";

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<AdminPage />} />
      </Routes>
    </div>
  );
}

export default App;
