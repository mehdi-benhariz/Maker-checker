import React, { useEffect, useState } from "react";
import { addOperation } from "../../../API/OperationAPI";
import { getRequestsToStaff } from "../../../API/RequestAPI";

import { notify } from "../../utils/Notify";

const RequestCarousel = () => {
  const [requests, setRequests] = useState([]);
  const [selectedReqId, setSelectedReqId] = useState(2);
  //validate the request
  async function validateRequest(e, reqId) {
    e.preventDefault();
    const res = await addOperation(reqId);
    console.log(res);
    if (res.status === 200) {
      notify(e, "success", "valdiated", "Request added successfully");
      let newRequests = requests.filter((req) => req.id !== reqId);
      setRequests(newRequests);
    } else notify(e, "failure", "error", "Error validating request");
  }
  //get the request for specific staff
  async function getRequestByStaff() {
    const res = await getRequestsToStaff();
    if (res.status === 200) {
      setRequests(res.data);
      setSelectedReqId(res.data[0].id);
      console.log(res.data);
    } else notify("error", "Error validating request");
  }

  let currentReq = () =>
    requests.length > 0 && requests.find((r) => r.id === selectedReqId);
  //init data
  async function init() {
    getRequestByStaff();
  }

  useEffect(() => {
    init();
  }, []);

  let listReq =
    requests.length === 0 ? (
      <div className="flex justify-center">
        <p className="text-gray-600 font-medium text-lg">No Request yet !</p>
      </div>
    ) : (
      requests.map((request, i) => (
        <div
          key={request.id}
          onClick={() => {
            console.log(request);
            setSelectedReqId(i);
          }}
          className="bg-white rounded-sm p-2"
        >
          <h2 className="text-gray-600 font-bold text-xl text-center">
            #{request.id}
          </h2>
          <div>
            Type :<span>{request.serviceType} </span>
          </div>
          <div>
            Date : <span>{request.creationDate} </span>
          </div>
          <button
            onClick={(e) => validateRequest(e, request.id)}
            className="border border-purple-500 py-2 w-full text-purple-500 inline-block rounded hover:bg-purple-500 hover:text-white"
          >
            Validate
          </button>
        </div>
      ))
    );
  return (
    <div className="rounded-md  grid md:grid-cols-4 gap-2 shadow-sm bg-gray-100 p-2 mx-4 my-2 h-2/3">
      <div className="col-span-3 grid  md:grid-cols-3 sm:grid-cols-2 gap-4">
        {listReq}
      </div>
      {/* todo: later */}
      {currentReq() && (
        <div className="bg-gray-200 p-2 rounded-sm">
          <div className="flex flex-col items-center">
            <h2 className="md font-semibold py-1">
              {currentReq().serviceType}{" "}
            </h2>
            <p className="italic sm text-gray-600 py-1">
              {currentReq().creationDate}
            </p>
          </div>
          <div className="bg-white rounded-md">
            <div className="relative flex justify-center flex-no-shrink ">
              <svg
                width="36"
                height="36"
                viewBox="0 0 36 36"
                xmlns="http://www.w3.org/2000/svg"
                className="inline-flex rounded-full overflow-visible"
              >
                <circle fill="#0EA5E9" cx="18" cy="18" r="18" />
                <path
                  d="M18 26a8 8 0 1 1 8-8 8.009 8.009 0 0 1-8 8Zm0-14a6 6 0 1 0 0 12 6 6 0 0 0 0-12Z"
                  fill="#F0F9FF"
                />
              </svg>
            </div>

            <div className="flex items-center flex-col">
              <p className="text-green-400 text-xl font-bold ">+ 1300 DT</p>
              <p>Acme LTD UK</p>
            </div>
            <div className="px-4 py-2">
              <div className="border-dashed border-2"></div>
            </div>
            <div className="flex flex-col px-4 py-4 shadow-md">
              <div className="flex justify-between ">
                <p className="italic text-sm text-gray-400">IBAN:</p>
                <p className="text-gray-800">IT17 2207 1010 0504 0006 88</p>
              </div>
              <div className="flex justify-between">
                <p className="italic text-sm text-gray-400">BIC:</p>
                <p className="text-gray-800">BARIT22</p>
              </div>
              <div className="flex justify-between">
                <p className="italic text-sm text-gray-400">Reference:</p>
                <p className="text-gray-800">Freelance Work</p>
              </div>
              <div className="flex justify-between">
                <p className="italic text-sm text-gray-400">Emitter:</p>
                <p className="text-gray-800">Acme LTD UK</p>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default RequestCarousel;
