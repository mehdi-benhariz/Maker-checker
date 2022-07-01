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
export function StatusCell(value) {
  //
  let colors = {
    Pending: "yellow",
    Approved: "green",
    Rejected: "red",
  };
  let primaryStyle = `relative inline-block px-3 py-1 font-semibold text-${colors[value]}-900 leading-tight text-orange-900`;
  let secondaryStyle = `absolute inset-0 bg-${colors[value]}-200 opacity-50 rounded-full`;

  return (
    <span className={primaryStyle}>
      <span aria-hidden className={secondaryStyle}></span>
      <span className="relative">{value} </span>
    </span>
  );
}

export function ProgressCell(value) {
  let colors = (val) => {
    if (val < 50) return "red";
    if (val < 80) return "yellow";
    return "green";
  };
  let bgStyle = `overflow-hidden h-4 text-xs w-auto flex rounded bg-${colors(
    value
  )}-200 w-full`;
  let barStyle = `shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-${colors(
    value
  )}-500 `;
  return (
    <div className="flex justify-center items-center w-full">
      <div className={bgStyle}>
        <div style={{ width: (value / 100) * 100 + "%" }} className={barStyle}>
          {value}%
        </div>
      </div>
    </div>
  );
}
