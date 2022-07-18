import React from "react";
import { Toaster } from "react-hot-toast";

import RequestsTable from "./RequestsTable";
import UsersTable from "./UsersTable";
import RulesTable from "./RulesTable";
import { logout } from "../../../API/AuthAPI";
import { useNavigate } from "react-router-dom";
import TemplatePage from "../../Shared/TemplatePage";
// import { getAllServiceTypes } from "../api/serviceType";

const AdminPage = () => {
  return (
    <TemplatePage
      MainComponent={
        <>
          <RulesTable />
          <UsersTable />
          <RequestsTable />
          <Toaster />
        </>
      }
    />
  );
};

export default AdminPage;
