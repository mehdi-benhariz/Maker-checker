import React, { useState } from "react";
import { addOperation, getRequestsDuty } from "../../API/Operation";
import { notify } from "../utils/Notify";
const RequestCarousel = () => {
  const [requests, setRequests] = useState([
    {
      id: 1,
      serviceType: "international",
      creationDate: "2020-01-01",
    },
    {
      id: 2,
      serviceType: "international",
      creationDate: "2020-01-01",
    },
    {
      id: 3,
      serviceType: "international",
      creationDate: "2020-01-01",
    },
    {
      id: 4,
      serviceType: "international",
      creationDate: "2020-01-01",
    },
    {
      id: 6,
      serviceType: "international",
      creationDate: "2020-01-01",
    },
  ]);
  async function validateRequest(reqId) {
    const res = await addOperation(reqId);
    console.log(res);
    if (res.status === 200) notify("success", "Request added successfully");
    else {
      notify("error", "Error validating request");
    }
  }
  async function getRequestByStuff(userId) {
    const res = await getRequestsDuty(userId);
    console.log(res);
    if (res.status === 200) setRequests(res.data);
    else {
      notify("error", "Error validating request");
    }
  }

  return (
    <div className="rounded-md grid lg:grid-cols-3 md:grid-cols-2 shadow-sm bg-gray-100 p-2  gap-4 mx-4 my-2 h-2/3">
      {requests.length === 0 && (
        <div className="flex justify-center">
          <p className="text-gray-600 font-medium text-lg">No Request yet !</p>
        </div>
      )}
      {requests.length > 0 &&
        requests.map((request) => (
          <div className="bg-white rounded-sm p-2">
            <h2 className="text-gray-600 font-bold text-xl text-center">
              #{request.id}
            </h2>
            <div>
              Type :<span>{request.serviceType} </span>
            </div>
            <div>
              Date : <span>{request.creationDate} </span>
            </div>
            <button className="border border-purple-500 py-2 w-full text-purple-500 inline-block rounded hover:bg-purple-500 hover:text-white">
              Validate
            </button>
          </div>
        ))}
    </div>
  );
};

export default RequestCarousel;
