import React, { useState, useMemo, useEffect } from "react";
import toast, { Toaster } from "react-hot-toast";
import { getAllServiceTypes } from "../API/ServiceTypeAPI";
import { RoleNbrCell } from "./utils/Cells";
// import { getAllServiceTypes } from "../api/serviceType";

const AdminPage = () => {
  //get initial service types and their api
  const getData = async () => {
    const res = await getAllServiceTypes();
    console.log(res);
  };
  useEffect(() => {
    getData();
  }, []);

  const [rules, setRules] = useState([
    {
      stID: 0,
      A: 0,
      B: 0,
      C: 0,
    },
    {
      stID: 1,
      A: 0,
      B: 0,
      C: 0,
    },
  ]);

  const updateRule = (stId, roleId, value) => {
    console.log(stId, roleId, value);
    let st = rules.find((s) => s.stID === stId);
    st[roleId] = value;
    setRules((x) => [...rules, st]);
  };

  const getRoleNbr = (stId, roleId) =>
    rules.find((s) => s.stID === stId)[roleId];

  const data = useMemo(
    () => [
      {
        serviceType: "international",
        id: 1,
        A: RoleNbrCell(getRoleNbr(0, 1), (x) => updateRule(0, "A", x), 5),
        B: RoleNbrCell(getRoleNbr(0, 2), (x) => updateRule(0, "B", x), 5),
        C: RoleNbrCell(getRoleNbr(0, 3), (x) => updateRule(0, "C", x), 5),
      },
      {
        serviceType: "intrabank",
        id: 2,
        B: RoleNbrCell(getRoleNbr(1, 1), (x) => updateRule(1, "B", x), 5),
        A: RoleNbrCell(getRoleNbr(1, 2), (x) => updateRule(1, "A", x), 5),
        C: RoleNbrCell(getRoleNbr(1, 3), (x) => updateRule(1, "C", x), 5),
      },
    ],
    []
  );
  const notify = (e) => {
    e.preventDefault();
    return toast.custom(
      (t) => (
        <div
          className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative"
          role="alert"
        >
          <strong className="font-bold">Error!</strong>
          <span className="block sm:inline">
            Something seriously bad happened.
          </span>
          <span
            className="absolute top-0 bottom-0 right-0 px-4 py-3"
            onClick={() => toast.dismiss(t.id)}
          >
            <svg
              className="fill-current h-6 w-6 text-red-500"
              role="button"
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 20 20"
            >
              <title>Close</title>
              <path d="M14.348 14.849a1.2 1.2 0 0 1-1.697 0L10 11.819l-2.651 3.029a1.2 1.2 0 1 1-1.697-1.697l2.758-3.15-2.759-3.152a1.2 1.2 0 1 1 1.697-1.697L10 8.183l2.651-3.031a1.2 1.2 0 1 1 1.697 1.697l-2.758 3.152 2.758 3.15a1.2 1.2 0 0 1 0 1.698z" />
            </svg>
          </span>
        </div>
      ),
      { id: "unique-notification", position: "top-center" }
    );
  };
  const heads = ["serviceType", "A", "B", "C", "Action"];
  const handleChangeRule = (e, row) => {};
  const [error, seterror] = useState([]);
  return (
    <div className="container ">
      <h2 className="text-lg font-bold text-gray-800 ">Admin Request Rule</h2>
      <div className="flex flex-wrap  -mx-4">
        <div className="w-full px-4">
          <div className="max-w-full overflow-x-auto">
            <table className="table-auto w-full">
              <thead>
                <tr className="bg-purple-500 text-center">
                  {heads.map((head, i) => {
                    return (
                      <th
                        key={i}
                        className="
                           w-1/6
                           min-w-[160px]
                           text-lg
                           font-semibold
                           text-white
                           py-4
                           lg:py-7
                           px-3
                           lg:px-4
                           border-l border-transparent
                           "
                      >
                        {head}
                      </th>
                    );
                  })}
                </tr>
              </thead>
              <tbody>
                {data.map((row, i) => {
                  console.log(row);
                  return (
                    <tr key={i}>
                      <td
                        className="
                           text-center text-dark
                           font-medium
                           text-base
                           py-5
                           px-2
                           bg-[#F3F6FF]
                           border-b border-l border-[#E8E8E8]
                           "
                      >
                        {row.serviceType}
                      </td>
                      <td
                        className="
                           text-center text-dark
                           font-medium
                           text-base
                           py-5
                           px-2
                           bg-white
                           border-b border-[#E8E8E8]"
                      >
                        {row.A}
                      </td>
                      <td
                        className="
                           text-center text-dark
                           font-medium
                           text-base
                           py-5
                           px-2
                           bg-[#F3F6FF]
                           border-b border-[#E8E8E8]
                           "
                      >
                        {row.B}
                      </td>
                      <td
                        className="
                           text-center text-dark
                           font-medium
                           text-base
                           py-5
                           px-2
                           bg-white
                           border-b border-[#E8E8E8]"
                      >
                        {row.C}
                      </td>

                      <td
                        className="
                           text-center text-dark
                           font-medium
                           text-base
                           py-5
                           px-2
                           bg-white
                           border-b border-r border-[#E8E8E8]"
                      >
                        <button
                          onClick={(e) => notify(e)}
                          className="
                              border border-purple-500
                              py-2
                              px-6
                              text-purple-500
                              inline-block
                              rounded
                              hover:bg-purple-500 hover:text-white"
                          // onClick={(row) => handleChangeRule(row)}
                        >
                          Edit
                        </button>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </div>
      </div>
      <Toaster />
    </div>
  );
};

export default AdminPage;
