import React, { useEffect, useState, useMemo } from "react";
import { getAllServiceTypes } from "../../../API/ServiceTypeAPI";
import { RoleNbrCell } from "../../utils/Cells";
import { notify } from "../../utils/Notify";
const RulesTable = () => {
  const heads = ["serviceType", "A", "B", "C", "Action"];
  const handleChangeRule = (e, row) => {};
  const [error, seterror] = useState([]);
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

  return (
    <div className="max-w-full overflow-x-auto flex-col  flex justify-center bg-white p-4 mt-2 ">
      <div className="flex flex-row justify-between items-center">
        <h2 className="mb-4 text-xl font-bold text-gray-700">
          Services & Validation
        </h2>
        <button className="bg-gradient-to-tr from-indigo-600 to-purple-600 px-4 py-2 rounded-md text-white font-semibold tracking-wide cursor-pointer">
          New Service Type
        </button>
      </div>
      <table className="table-auto">
        <thead>
          <tr className="bg-gradient-to-tr from-indigo-600 to-purple-600 text-white font-bold text-md text-center rounded-md">
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
                           py-1
                           lg:py-2
                           px-3
                           lg:px-4
                           border-l border-transparent"
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
                           border-b border-l border-[#E8E8E8]"
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
                           bg-white
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
  );
};

export default RulesTable;
