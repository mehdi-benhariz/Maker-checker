import React, { useEffect, useState } from "react";
import { ProgressCell, StatusCell } from "../../utils/Cells";
import { getRequestByClient } from "../../../API/RequestAPI";
import { login } from "../../../API/AuthAPI";
const RequestHistory = () => {
  const [requests, setRequests] = useState([
    {
      id: 1,
      serviceType: "intrabank",
      status: "Pending",
      creationDate: "2020-01-01",
      progress: 80,
      amount: 900,
    },
    {
      id: 2,
      serviceType: "intrabank",
      status: "Rejected",
      creationDate: "2020-01-01",
      progress: 30,
      amount: 1900,
    },
    {
      id: 3,
      serviceType: "intrabank",
      status: "Approved",
      creationDate: "2020-01-01",
      progress: 100,
      amount: 2000,
    },
  ]);

  async function getRequestHistory() {
    // await login("red", "123456789");
    // const res = await getRequestByClient();
    // console.log(res.status === 200);
    const res = await login("red", "123456789");
    // if (res.status === 200) {
    //   setRequests(res.data);
    // }
  }

  useEffect(() => {
    getRequestHistory();
  }, []);
  return (
    <div className="rounded-md shadow-sm bg-gray-100 p-2  gap-4 w-4/5 h-2/3">
      <h2 className="mb-4 text-lg font-semibold text-gray-600 dark:text-gray-300">
        History
      </h2>
      {requests.length === 0 && (
        <div className="flex justify-center">
          <p className="text-gray-600 font-medium text-lg">No Request yet !</p>
        </div>
      )}
      {requests.length > 0 &&
        requests.map((request) => (
          <div
            key={request.id}
            className="rounded-md bg-white shadow-sm p-2 my-4 grid grid-cols-3 content-center  w-4/5"
          >
            <div>
              <h2 className="text-gray-900 font-bold text-xl ">
                {request.serviceType}
              </h2>
              <h3 className="text-gray-700 font-semibold text-lg">
                {request.creationDate}
              </h3>
            </div>
            <div className="flex   ">
              {ProgressCell(request.progress, request.status)}
            </div>
            <div className="flex items-center flex-row-reverse gap-4">
              {StatusCell(request.status)}
              <p>{request.amount} DT </p>
            </div>
          </div>
        ))}
    </div>
  );
};

export default RequestHistory;