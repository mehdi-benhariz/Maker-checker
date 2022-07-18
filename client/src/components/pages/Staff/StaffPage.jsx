import React from "react";
import { Toaster } from "react-hot-toast";
import TemplatePage from "../../Shared/TemplatePage";
import RequestCarousel from "./RequestCarousel";

const StaffPage = () => {
  return (
    <TemplatePage
      MainComponent={
        <div className="h-full flex items-center flex-col gap-4">
          <RequestCarousel />
          <Toaster />
        </div>
      }
    />
  );
};

export default StaffPage;
