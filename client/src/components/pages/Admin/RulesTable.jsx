import React, { useEffect, useState } from "react";
import { getAllStaffRoles } from "../../../API/RoleAPI";
import { editRules, addRules } from "../../../API/RuleAPI";
import {
  getAllServiceTypes,
  addServiceType,
  deleteServiceType,
} from "../../../API/ServiceTypeAPI";

import { RoleNbrCell } from "../../utils/Cells";
import { TextField } from "../../utils/InputFields";
import { notify } from "../../utils/Notify";

const RulesTable = () => {
  const [heads, setHeads] = useState(["serviceType", "Action"]);
  const [data, setData] = useState([]);
  const [addST, setAddST] = useState(false);
  const [errors, setErrors] = useState([]);

  const handleChangeRule = async (e, row) => {
    e.preventDefault();

    const ruleExist = row.rules[0].roleId;
    let res;
    console.log({ row });
    const ruleDtos = row.rules.map((r) => ({
      roleId: r.RoleId,
      nbr: r.nbr,
    }));
    console.log({ ruleDtos, row });
    // update rule
    if (ruleExist) res = await editRules(ruleDtos, row.id);
    // add rule
    else res = await addRules(ruleDtos, row.id);
    console.log({ res });
    if (res.status === 200) {
      notify(e, "success", "Success", "Rule updated successfully");
    } else {
      setErrors(res.data.errors);
      notify(e, "error", "Error", "Error updating rule");
    }
  };

  //api call to get all service types
  const getData = async () => {
    const res = await getAllServiceTypes();
    if (res.status === 200) return res.data;
  };
  //todo :get it from api
  const [rules, setRules] = useState([
    { nbr: 0, RoleId: 2 },
    { nbr: 0, RoleId: 3 },
    { nbr: 0, RoleId: 4 },
  ]);
  async function initRules() {
    const res = await getAllStaffRoles();
    if (res.status === 200) {
      const data = res.data.map((r) => ({
        RoleId: r.id,
        nbr: 0,
      }));
      setRules(data);
      const roleNames = res.data.map((r) => r.name);
      setHeads(["serviceType", ...roleNames, "Action"]);
    }
  }
  //call when update the table cell data
  const updateRule = (rowId, ruleIndex, value) => {
    let newData = [...data];
    newData[rowId].rules[ruleIndex].nbr = parseInt(value);
    setData([...newData]);
    console.log({ newData });
    console.log(rowId, ruleIndex, value);
  };

  const getRoleNbr = (stId, roleId) => data[stId].rules[roleId].nbr;

  //1 -get the data from api in data
  //2- call setTableData to update the table

  //the init function
  let initData = async () => {
    const res = await getData();

    //if rules table is empty , fill it with default values

    for (let i = 0; i < res.length; i++) {
      console.log(res[i].rules.length == 0);
      if (res[i].rules.length == 0) res[i].rules = [...rules];
    }

    setData(res);
  };

  const [serviceType, setServiceType] = useState("");
  async function handleAddST(e) {
    e.preventDefault();
    const res = await addServiceType(serviceType);
    if (res.status === 201) {
      notify(e, "success", "Success", "Service type added successfully");
      console.log(res.data);
      // setData([...data, res.data]);
    } else console.log(res);
  }
  //
  async function handleDeleteST(e, stId) {
    e.preventDefault();
    const res = await deleteServiceType(stId);
    if (res.status === 200) {
      notify(e, "success", "Success", "Service type deleted successfully");
      setData(data.filter((st) => st.id !== stId));
    } else console.log(res);
  }

  //init the table here
  useEffect(() => {
    initData();
    initRules();
  }, []);

  return (
    <div className="max-w-full overflow-x-auto flex-col  flex justify-center bg-white p-4 mt-2 ">
      <div className="flex flex-row justify-between items-center">
        <h2 className="mb-4 text-xl font-bold text-gray-700">
          Services & Validation
        </h2>
        <button
          onClick={() => setAddST(!addST)}
          className="bg-gradient-to-tr from-indigo-600 to-purple-600 px-4 py-2 rounded-md text-white font-semibold tracking-wide cursor-pointer"
        >
          New Service Type
        </button>
      </div>
      <table className="table-auto">
        <thead>
          <tr className="bg-gradient-to-tr from-indigo-600 to-purple-600 text-white font-bold text-md text-center rounded-md">
            {heads.map((head, i) => (
              <th
                key={head}
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
            ))}
          </tr>
        </thead>
        <tbody>
          {data.map((row, i) => {
            // console.log(row);
            return (
              <tr key={`st-${row.id}`}>
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
                  {row.name}
                </td>
                {rules.map((rule, j) => {
                  return (
                    <td
                      key={j}
                      className="text-center text-dark
                           font-medium
                           text-base
                           py-5
                           px-2
                           bg-white
                           border-b border-[#E8E8E8]"
                    >
                      {RoleNbrCell({
                        value: getRoleNbr(i, j),
                        cb: (x) => updateRule(i, j, x),
                        max: rule.maxNbr || 5,
                      })}
                    </td>
                  );
                })}

                <td
                  className="text-center text-dark
                           font-medium
                           text-base
                           py-5
                           px-2
                           bg-white
                           border-b border-r border-[#E8E8E8]
                           flex justify-between
                           "
                >
                  <button
                    onClick={(e) => handleChangeRule(e, row)}
                    className="border border-purple-500
                              py-2
                              px-6
                              text-purple-500
                              inline-block
                              rounded
                              hover:bg-purple-500 hover:text-white
                              transition-colors duration-300 ease-linear
                              "
                  >
                    Edit
                  </button>
                  <button
                    onClick={(e) => handleDeleteST(e, row.id)}
                    className="border border-red-500
                              py-2
                              px-6
                              text-red-500
                              inline-block
                              rounded
                              hover:bg-red-500 hover:text-white
                              transition-colors duration-300 ease-linear
                              "
                  >
                    <svg
                      className="h-6 w-6"
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 24 24"
                      stroke="currentColor"
                      aria-hidden="true"
                    >
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth="2"
                        d="M6 18L18 6M6 6l12 12"
                      />
                    </svg>
                  </button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
      <div className="flex justify-end mt-2">
        {addST && (
          <div className="flex my-4 justify-between gap-4 scale-100">
            <TextField
              value={serviceType}
              cb={setServiceType}
              placeholder="new service type"
            />
            <button
              onClick={handleAddST}
              className="bg-gradient-to-tr from-indigo-600 to-purple-600 px-4 py-2 rounded-md text-white font-semibold tracking-wide cursor-pointer"
            >
              Add
            </button>
          </div>
        )}
      </div>
    </div>
  );
};

export default RulesTable;
