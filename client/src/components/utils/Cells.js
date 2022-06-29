import React from "react";

export function RoleNbrCell(value, cb, max = 5) {
  return (
    <input
      type="number"
      min={0}
      max={max}
      className="bg-gray-200 appearance-none border-2 border-gray-200 rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:bg-white focus:border-purple-500"
      // className="mt-1 px-8 block rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
      value={value}
      onChange={(e) => {
        const x = parseInt(e.target.value);
        if (x >= 0 && x <= max) cb(e.target.value);
      }}
    />
  );
}
