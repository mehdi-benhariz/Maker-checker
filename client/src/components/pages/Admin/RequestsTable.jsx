import React, { useEffect, useMemo, useState } from "react";
import { getRequestsToAdmin } from "../../../API/RequestAPI";
import { StatusCell, ProgressCell } from "../../utils/Cells";

const RequestsTable = () => {
  const [requests, setRequests] = useState([]);
  const [pagginationData, setPagginationData] = useState({});
  const [searchQuery, setsearchQuery] = useState("");
  const [pageNumber, setPageNumber] = useState(1);

  async function getRequests() {
    const res = await getRequestsToAdmin(pageNumber, searchQuery);

    if (res.status === 200) {
      setRequests([...res.data]);
      return res.headers;
    }
    //todo add error handling
    else console.log(res.data);
  }

  function init() {
    getRequests().then((headers) => {
      if (!headers) return;
      const parsed = JSON.parse(headers["x-paggination"]);
      setPagginationData(parsed);
    });
  }
  let increment = () =>
    pageNumber < pagginationData.TotalItems - 1 &&
    setPageNumber(pageNumber + 1);

  let decrement = () => pageNumber > 1 && setPageNumber(pageNumber - 1);

  useMemo(init, [searchQuery, pageNumber]);
  // useEffect(init, []);

  return (
    <div className="bg-white p-8 rounded-md w-full">
      <div className=" flex items-center justify-between pb-6">
        <div>
          <h2 className="mb-4 text-xl font-bold text-gray-700">
            Client's request
          </h2>
          <span className="text-xs">All request</span>
        </div>
        <div className="flex items-center justify-between">
          <div className="flex bg-gray-50 items-center p-2 rounded-md">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-5 w-5 text-gray-400"
              viewBox="0 0 20 20"
              fill="currentColor"
            >
              <path
                fillRule="evenodd"
                d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z"
                clipRule="evenodd"
              />
            </svg>
            <input
              onChange={(e) => setsearchQuery(e.target.value)}
              className="bg-gray-50 outline-none ml-1 block "
              type="text"
              name=""
              id=""
              placeholder="search..."
            />
          </div>
        </div>
      </div>
      <div>
        <div className="-mx-4 sm:-mx-8 px-4 sm:px-8 py-4 overflow-x-auto">
          <div className="inline-block min-w-full shadow rounded-lg overflow-hidden">
            <table className="min-w-full leading-normal">
              <thead>
                <tr>
                  <th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
                    Username
                  </th>
                  <th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
                    type
                  </th>
                  <th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
                    Created at
                  </th>
                  <th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
                    Progress
                  </th>
                  <th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
                    Status
                  </th>
                </tr>
              </thead>
              <tbody>
                {requests.map((request) => {
                  return (
                    <tr key={request.id}>
                      <td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                        <div className="flex items-center">
                          <div className="flex-shrink-0 w-10 h-10">
                            <img
                              className="w-full h-full rounded-full"
                              src={`${request.profileImg}`}
                              alt=""
                            />
                          </div>
                          <div className="ml-3">
                            <p className="text-gray-900 whitespace-no-wrap">
                              {request.owner}
                            </p>
                          </div>
                        </div>
                      </td>
                      <td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                        <p className="text-gray-900 whitespace-no-wrap">
                          {request.serviceType}
                        </p>
                      </td>
                      <td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                        <p className="text-gray-900 whitespace-no-wrap">
                          {request.creationDate}
                        </p>
                      </td>
                      <td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                        {ProgressCell(request.progress, request.status)}
                      </td>
                      <td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                        {StatusCell(request.status)}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
            <div className="px-5 py-5 bg-white border-t flex flex-col xs:flex-row items-center xs:justify-between          ">
              <span className="text-xs xs:text-sm text-gray-900">
                Showing {pagginationData.PageNumber} to
                {pagginationData.PageSize} of
                {pagginationData.TotalItems} Entries
              </span>
              <div className="inline-flex mt-2 xs:mt-0">
                <button
                  onClick={decrement}
                  className="text-sm text-indigo-50 transition duration-150 hover:bg-indigo-500 bg-indigo-600 font-semibold py-2 px-4 rounded-l"
                >
                  Prev
                </button>
                &nbsp; &nbsp;
                <button
                  onClick={increment}
                  className="text-sm text-indigo-50 transition duration-150 hover:bg-indigo-500 bg-indigo-600 font-semibold py-2 px-4 rounded-r"
                >
                  Next
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default RequestsTable;
