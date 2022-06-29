import React from "react";
import { Route, Routes } from "react-router";

import "./App.css";
import Home from "./components/Home";

function App() {
  return (
    <div className="App">
      <h1>Maker Checker</h1>
      <Routes>
        <Route path="/" element={<Home />} />
      </Routes>
    </div>
  );
}

export default App;
