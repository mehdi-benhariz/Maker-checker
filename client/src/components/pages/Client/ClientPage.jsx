import React from "react";
import RequestForm from "./RequestForm";
import RequestHistory from "./RequestHistory";

const ClientPage = () => {
  return (
    <div className="bg-gray-50 min-h-screen">
      <nav>
        <div className="flex justify-between items-center p-4 bg-white">
          <div className="flex items-center ">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-5 w-5 hidden"
              viewBox="0 0 20 20"
              fill="currentColor"
            >
              <path d="M3 4a1 1 0 011-1h12a1 1 0 011 1v2a1 1 0 01-1 1H4a1 1 0 01-1-1V4zM3 10a1 1 0 011-1h6a1 1 0 011 1v6a1 1 0 01-1 1H4a1 1 0 01-1-1v-6zM14 9a1 1 0 00-1 1v6a1 1 0 001 1h2a1 1 0 001-1v-6a1 1 0 00-1-1h-2z" />
            </svg>
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-8 w-8 cursor-pointer "
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M4 6h16M4 12h8m-8 6h16"
              />
            </svg>
            {/* <span className="flex h-3 w-3">
              <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-sky-400 opacity-75"></span>
              <span className="relative inline-flex rounded-full h-3 w-3 bg-sky-500"></span>
            </span> */}
          </div>
          <div className="flex items-center space-x-2">
            <div className="relative">
              <div className="absolute">
                <div className="animate-ping top-0 right-0 h-3 w-3 my-1 border-2 rounded-full bg-indigo-600 z-2"></div>
                <div className=" absolute top-0 right-0 h-3 w-3 my-1 border-2 rounded-full bg-indigo-600 z-2"></div>
              </div>

              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="h-7 w-7 cursor-pointer"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"
                />
              </svg>
            </div>

            <div className="w-10">
              <img
                className="rounded-full"
                src="https://forbesthailand.com/wp-content/uploads/2021/08/https-specials-images.forbesimg.com-imageserve-5f47d4de7637290765bce495-0x0.jpgbackground000000cropX11699cropX23845cropY1559cropY22704.jpg"
                alt=""
              />
            </div>
          </div>
        </div>
      </nav>
      <div className="h-full flex items-center flex-col gap-4">
        {/*  */}
        <RequestHistory />
        <RequestForm />
        {/*  */}
      </div>
    </div>
  );
};

export default ClientPage;