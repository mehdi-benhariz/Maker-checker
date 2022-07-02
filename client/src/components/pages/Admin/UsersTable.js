import React from "react";
import { useState, useMemo, useEffect } from "react";
const UsersTable = () => {
  const [roles, setroles] = useState([
    { id: 2, name: "A" },
    { id: 3, name: "B" },
    { id: 4, name: "C" },
  ]);
  const [users, setUsers] = useState([
    {
      id: 1,
      username: "mehdi ben hariz",
      email: "mehdi@proxym.com",
      role: "A",
      date: "2022-06-20",
    },
    {
      id: 2,
      username: "mehdi ben hariz",
      email: "mehdi@proxym.com",
      role: "B",
      date: "2022-06-20",
    },
    {
      id: 3,
      username: "mehdi ben hariz",
      email: "mehdi@proxym.com",
      role: "C",
      date: "2022-06-20",
    },
  ]);
  return (
    <>
      <div class="py-4">
        <div class="bg-white p-4 rounded-md">
          <div>
            <div class="flex justify-between">
              <h2 class="mb-4 text-xl font-bold text-gray-700">
                Staff & Admins
              </h2>
              <div class="lg:ml-40 ml-10 space-x-8">
                <button class="bg-gradient-to-tr from-indigo-600 to-purple-600 px-4 py-2 rounded-md text-white font-semibold tracking-wide cursor-pointer">
                  New checker
                </button>
                <button class="bg-gradient-to-tr from-indigo-600 to-purple-600 px-4 py-2 rounded-md text-white font-semibold tracking-wide cursor-pointer">
                  New Role
                </button>
              </div>
            </div>
            <div>
              <div>
                <div class="flex justify-between bg-gradient-to-tr from-indigo-600 to-purple-600 rounded-md py-2 px-4 text-white font-bold text-md">
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
                      class="flex justify-between border-t text-sm font-normal mt-4 space-x-4"
                    >
                      <div class="px-2 flex">
                        <span>{row.username}</span>
                      </div>
                      <div>
                        <span>{row.email}</span>
                      </div>
                      <div class="px-2">
                        <span>{row.role}</span>
                      </div>
                      <div class="px-2">
                        <span>{row.date}</span>
                      </div>
                      <div class="px-2">
                        <select defaultValue={row.role.name}>
                          {roles.map((role) => (
                            <option key={role.id} value={role.id}>
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
          <div class="flex justify-end mt-2">
            <button class="bg-gradient-to-tr from-indigo-600 to-purple-600 px-4 py-2 rounded-md text-white font-semibold tracking-wide cursor-pointer">
              Submit
            </button>
          </div>
        </div>
      </div>
    </>
  );
};

export default UsersTable;
