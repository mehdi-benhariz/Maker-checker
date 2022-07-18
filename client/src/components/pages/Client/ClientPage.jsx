import React, { useState } from "react";
import { Toaster } from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import { logout } from "../../../API/AuthAPI";
import TemplatePage from "../../Shared/TemplatePage";
import RequestForm from "./RequestForm";
import RequestHistory from "./RequestHistory";

const ClientPage = () => {
  const profileImg =
    "https://forbesthailand.com/wp-content/uploads/2021/08/https-specials-images.forbesimg.com-imageserve-5f47d4de7637290765bce495-0x0.jpgbackground000000cropX11699cropX23845cropY1559cropY22704.jpg";
  let navigate = useNavigate();
  let handlLogout = async (e) => {
    e.preventDefault();
    const res = await logout();
    if (res.status === 200) navigate("/login");
    //todo add custom error handling
  };
  const [changeReq, setChangeReq] = useState(false);
  let toggleChange = () => setChangeReq(!changeReq);

  return (
    <TemplatePage
      MainComponent={
        <div className="h-full flex items-center flex-col gap-4">
          <RequestHistory depend={changeReq} />
          <RequestForm action={toggleChange} />
          <Toaster />
        </div>
      }
    ></TemplatePage>
  );
};

export default ClientPage;
