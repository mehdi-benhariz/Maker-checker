import React, { useEffect } from "react";
import { useState } from "react";
import { TextField } from "../../utils/InputFields";
import { SubmitRole, getAllStaffRoles } from "../../../API/RoleAPI";
import { getAllStaff, UpdateUserRole } from "../../../API/UserAPI";

const UsersTable = () => {
  //create a map of all staff
  //todo change the array with a Map later
  // const [roles, setroles] = useState(new Map([2, "A"], [3, "B"], [4, "C"]));
  const [roles, setRoles] = useState([
    { id: 2, name: "A" },
    { id: 3, name: "B" },
    { id: 4, name: "C" },
  ]);
  async function initRoles() {
    const res = await getAllStaffRoles();
    if (res.status === 200) setRoles(res.data);
    //todo add error handling
    else console.log(res.data.error);
  }

  const [roleId, setRoleId] = useState(null);
  const [users, setUsers] = useState([]);
  let initStaff = async () => {
    const res = await getAllStaff();
    if (res.status === 200) setUsers(res.data);
    //todo add error handling
    else console.log(res.data);
  };

  const [addRole, setAddRole] = useState(false);
  let toggleAddRole = () => setAddRole(!addRole);

  const [role, setRole] = useState("");
  async function handleAddRole(e) {
    e.preventDefault();
    const res = await SubmitRole(role);
    if (res.status === 200) {
      alert("added role");
      setRole("");
    } else console.log(res);
  }
  const [addChecker, setAddChecker] = useState(false);
  function init() {
    initStaff();
    initRoles();
  }
  useEffect(init, []);
  const [userId, setUserId] = useState(null);

  let handleUserChange = (e, row) => {
    setRoleId(e.target.value);
    setUserId(row.id);
    console.log(row.id, e.target.value);
  };
  async function handleUpdateRule() {
    console.log(roleId, userId);
    const res = await UpdateUserRole(userId, roleId);
    if (res.status === 200) {
      setRole("");
      setUserId(null);
    } else console.log(res);
  }
  return (
    <>
      <div className="py-4">
        <div className="bg-white p-4 rounded-md">
          <div>
            <div className="flex justify-between">
              <h2 className="mb-4 text-xl font-bold text-gray-700">
                Staff & Admins
              </h2>
              <div className="lg:ml-40 ml-10 space-x-8">
                <button className="bg-gradient-to-tr from-indigo-600 to-purple-600 px-4 py-2 rounded-md text-white font-semibold tracking-wide cursor-pointer">
                  New checker
                </button>
                <button
                  onClick={toggleAddRole}
                  className="bg-gradient-to-tr from-indigo-600 to-purple-600 px-4 py-2 rounded-md text-white font-semibold tracking-wide cursor-pointer"
                >
                  New Role
                </button>
              </div>
            </div>
            <div>
              <div>
                <div className="flex justify-between bg-gradient-to-tr from-indigo-600 to-purple-600 rounded-md py-2 px-4 text-white font-bold text-md">
                  <div>
                    <span>Name</span>
                  </div>
                  <div>
                    <span>Email</span>
                  </div>
                  <div>
                    <span>Role</span>
                  </div>
                  <div>
                    <span>Date</span>
                  </div>
                  <div>
                    <span>Edit</span>
                  </div>
                </div>
                <div>
                  {users.map((row) => (
                    <div
                      key={row.id}
                      className="flex justify-between border-t text-sm font-normal mt-4 space-x-4"
                    >
                      <div className="px-2 flex">
                        <span>{row.username}</span>
                      </div>
                      <div>
                        <span>{row.email}</span>
                      </div>
                      <div className="px-2">
                        <span>{row.role}</span>
                      </div>
                      <div className="px-2">
                        <span>{row.creationDate}</span>
                      </div>
                      <div className="px-2">
                        <select
                          defaultValue={
                            roles.find((r) => r.name === row.role).id
                          }
                          onChange={(e) => {
                            handleUserChange(e, row);
                          }}
                        >
                          {roles.map((role) => (
                            <option
                              className="h-8 w-8"
                              key={role.id}
                              value={role.id}
                            >
                              {role.name}
                            </option>
                          ))}
                        </select>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            </div>
          </div>
          <div className="flex justify-end mt-2">
            {addRole && (
              <div className="flex my-4 justify-between gap-4 scale-100">
                <TextField value={role} cb={setRole} placeholder="new role" />
                <button
                  onClick={handleAddRole}
                  className="bg-gradient-to-tr from-indigo-600 to-purple-600 px-4 py-2 rounded-md text-white font-semibold tracking-wide cursor-pointer"
                >
                  Add
                </button>
              </div>
            )}
          </div>
          <div className="flex justify-end mt-2">
            <button
              onClick={handleUpdateRule}
              className="bg-gradient-to-tr from-indigo-600 to-purple-600 px-4 py-2 rounded-md text-white font-semibold tracking-wide cursor-pointer"
            >
              Submit
            </button>
          </div>
        </div>
      </div>
    </>
  );
};

export default UsersTable;
