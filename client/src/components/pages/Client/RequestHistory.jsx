import React, { useEffect, useState } from "react";
import { ProgressCell, StatusCell } from "../../utils/Cells";
import { getRequestByClient } from "../../../API/RequestAPI";
const RequestHistory = () => {
  const [requests, setRequests] = useState([
    // {
    //   id: 1,
    //   serviceType: "intrabank",
    //   status: "Pending",
    //   creationDate: "2020-01-01",
    //   progress: 80,
    //   amount: 900,
    // },
    // {
    //   id: 2,
    //   serviceType: "intrabank",
    //   status: "Rejected",
    //   creationDate: "2020-01-01",
    //   progress: 30,
    //   amount: 1900,
    // },
    // {
    //   id: 3,
    //   serviceType: "intrabank",
    //   status: "Approved",
    //   creationDate: "2020-01-01",
    //   progress: 100,
    //   amount: 2000,
    // },
  ]);

  async function getRequestHistory() {
    const res = await getRequestByClient();
    console.log(res);
    if (res.status === 200) {
      setRequests([...res.data]);
    }
  }

  useEffect(() => {
    getRequestHistory();
  }, []);

  //*the main content to display
  let content = () => {
    if (requests.length > 0)
      return requests.map((request) => (
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
      ));
    return (
      <div className="flex justify-center snap-center">
        <p className="text-gray-600 font-medium text-lg">No Request yet !</p>
      </div>
    );
  };

  return (
    <div className="rounded-md shadow-sm bg-gray-100 p-2  gap-4 w-4/5 h-2/3">
      <h2 className="mb-4 text-lg font-semibold text-gray-600 dark:text-gray-300">
        History
      </h2>
      <div className="overflow-y-auto h-40">{content()}</div>
    </div>
  );
};

export default RequestHistory;
