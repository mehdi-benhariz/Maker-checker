import React from "react";
import { ProgressCell, StatusCell } from "../utils/Cells";

const ClientPage = () => {
  return (
    <div class="bg-gray-50 min-h-screen">
      <nav>
        <div class="flex justify-between items-center p-4 bg-white">
          <div class="flex items-center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              class="h-5 w-5 hidden"
              viewBox="0 0 20 20"
              fill="currentColor"
            >
              <path d="M3 4a1 1 0 011-1h12a1 1 0 011 1v2a1 1 0 01-1 1H4a1 1 0 01-1-1V4zM3 10a1 1 0 011-1h6a1 1 0 011 1v6a1 1 0 01-1 1H4a1 1 0 01-1-1v-6zM14 9a1 1 0 00-1 1v6a1 1 0 001 1h2a1 1 0 001-1v-6a1 1 0 00-1-1h-2z" />
            </svg>
            <svg
              xmlns="http://www.w3.org/2000/svg"
              class="h-8 w-8 cursor-pointer"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M4 6h16M4 12h8m-8 6h16"
              />
            </svg>
          </div>
          <div class="flex items-center space-x-2">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              class="h-7 w-7 cursor-pointer"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"
              />
            </svg>
            <div class="w-10">
              <img
                class="rounded-full"
                src="https://forbesthailand.com/wp-content/uploads/2021/08/https-specials-images.forbesimg.com-imageserve-5f47d4de7637290765bce495-0x0.jpgbackground000000cropX11699cropX23845cropY1559cropY22704.jpg"
                alt=""
              />
            </div>
          </div>
        </div>
      </nav>
      {/*  */}
      <div className="rounded-md shadow-sm bg-gray-100 p-2  gap-4 w-4/5 h-2/3">
        <div className="rounded-md bg-white shadow-sm p-2 grid grid-cols-3 content-center  w-4/5">
          <div>
            <h2 className="text-gray-900 font-bold text-xl ">international</h2>
            <h3 className="text-gray-700 font-semibold text-lg">2022-06-30</h3>
          </div>
          <div className="flex   ">{ProgressCell(50)}</div>
          <div className="flex items-center flex-row-reverse gap-2">
            {StatusCell("Pending")}
            <p>900 DT</p>
          </div>
        </div>
      </div>
      {/*  */}
    </div>
  );
};

export default ClientPage;
