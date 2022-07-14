import React, { useEffect, useState } from "react";
import { ProgressCell, StatusCell } from "../../utils/Cells";
import { getRequestByClient } from "../../../API/RequestAPI";
const RequestHistory = ({ depend }) => {
  const [requests, setRequests] = useState([]);
  const [pagginationData, setPagginationData] = useState({});
  const [pageNumber, setPageNumber] = useState(1);

  async function getRequestHistory(pageNumber) {
    const res = await getRequestByClient(pageNumber);
    console.log(res);
    if (res.status === 200) {
      setRequests([...res.data]);
      const parsed = JSON.parse(res.headers["x-paggination"]);
      setPagginationData(parsed);
    }
  }
  //helpers with constraint
  let increment = () =>
    pageNumber < pagginationData.TotalItems - 1 &&
    setPageNumber(pageNumber + 1);

  let decrement = () => pageNumber > 1 && setPageNumber(pageNumber - 1);

  useEffect(() => {
    getRequestHistory(pageNumber);
  }, [depend, pageNumber]);

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
          <div className="flex">
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
      <div className="flex items-center justify-between mb-4 ">
        <h2 className=" text-lg font-semibold text-gray-600 dark:text-gray-300">
          History
        </h2>

        <div className="inline-flex mt-2 xs:mt-0">
          <button
            onClick={decrement}
            className="text-sm text-indigo-50 transition duration-150 hover:bg-indigo-500 bg-indigo-600 font-semibold py-2 px-4 rounded-l"
          >
            Prev
          </button>
          &nbsp;
          <span className="h-full w-10 text-center text-base text-gray-800">
            {pagginationData.PageNumber}
          </span>
          &nbsp;
          <button
            onClick={increment}
            className="text-sm text-indigo-50 transition duration-150 hover:bg-indigo-500 bg-indigo-600 font-semibold py-2 px-4 rounded-r"
          >
            Next
          </button>
        </div>
      </div>

      <div className="overflow-y-auto h-40">{content()}</div>
    </div>
  );
};

export default RequestHistory;
