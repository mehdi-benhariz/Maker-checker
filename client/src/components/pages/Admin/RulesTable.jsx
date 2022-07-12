import React, { useEffect, useState } from "react";
import { editRules, addRules } from "../../../API/RuleAPI";
import { getAllServiceTypes } from "../../../API/ServiceTypeAPI";
import { RoleNbrCell } from "../../utils/Cells";
import { notify } from "../../utils/Notify";

const RulesTable = () => {
  const heads = ["serviceType", "A", "B", "C", "Action"];
  const [data, setData] = useState([]);

  const [errors, setErrors] = useState([]);

  const handleChangeRule = async (e, row) => {
    e.preventDefault();

    const ruleExist = row.rules[0].roleId;
    console.log(row, ruleExist);
    let res;
    console.log({ row });
    const ruleDtos = row.rules.map((r) => ({
      roleId: r.roleId,
      nbr: r.nbr,
    }));
    if (ruleExist) {
      res = await editRules(ruleDtos, row.id);
      console.log(res);

      // update rule
    } else {
      // add rule
      res = await addRules(ruleDtos, row.id);
      console.log(res);
    }
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

  const rules = [
    { Nbr: 0, RoleId: 2 },
    { Nbr: 0, RoleId: 3 },
    { Nbr: 0, RoleId: 4 },
  ];
  //call when update the table cell data
  const updateRule = (rowId, ruleIndex, value) => {
    let newData = [...data];
    newData[rowId].rules[ruleIndex].nbr = value;
    setData([...newData]);
    console.log(newData);
    // console.log(stId, roleId, value);
  };

  const getRoleNbr = (stId, roleId) => data[stId].rules[roleId].nbr;

  //1 -get the data from api in data
  //2- call setTableData to update the table

  //the init function
  let initData = async () => {
    const res = await getData();
    console.log(res);
    //if rules table is empty , fill it with default values

    for (let i = 0; i < res.length; i++)
      if (res[i].rules.length === 0) res[i].rules = [...rules];

    setData(res);
  };

  //init the table here
  useEffect(() => {
    initData();
  }, []);

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
                  const ruleExist = rule.roleId === row.rules[i]?.roleId;
                  return (
                    <td
                      key={i}
                      className="text-center text-dark
                           font-medium
                           text-base
                           py-5
                           px-2
                           bg-white
                           border-b border-[#E8E8E8]"
                    >
                      {RoleNbrCell(
                        getRoleNbr(i, j),
                        (x) => updateRule(i, j, x),
                        rule.MaxNbr
                      )}
                    </td>
                  );
                })}

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
