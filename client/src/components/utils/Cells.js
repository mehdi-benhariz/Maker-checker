import React from "react";

export function RoleNbrCell({ value, cb, max }) {
  return (
    <input
      type="number"
      min={0}
      // max={max}
      className="bg-gray-200 appearance-none border-2 border-gray-200 rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:bg-white focus:border-purple-500"
      value={value}
      onChange={(e) => {
        const x = parseInt(e.target.value);
        // if (x >= 0 && x <= max)
        console.log({ max });
        console.log(x <= parseInt(max));
        cb(e.target.value);
      }}
    />
  );
}
export function StatusCell(value) {
  let colors = {
    Pending: (
      <span className="relative inline-block px-3 py-1 font-semibold text-orange-900 leading-tight">
        <span
          aria-hidden
          className="absolute inset-0 bg-orange-200 opacity-50 rounded-full"
        ></span>
        <span className="relative">{value} </span>
      </span>
    ),
    Approved: (
      <span className="relative inline-block px-3 py-1 font-semibold text-green-900 leading-tight">
        <span
          aria-hidden
          className="absolute inset-0 bg-green-200 opacity-50 rounded-full"
        ></span>
        <span className="relative">{value} </span>
      </span>
    ),
    Rejected: (
      <span className="relative inline-block px-3 py-1 font-semibold text-red-900 leading-tight">
        <span
          aria-hidden
          className="absolute inset-0 bg-red-200 opacity-50 rounded-full"
        ></span>
        <span className="relative">{value} </span>
      </span>
    ),
  };
  return colors[value];
}

export function ProgressCell(value, status) {
  let colors = (val) => {
    if (status === "Rejected")
      return (
        <div className="overflow-hidden h-4 text-xs flex rounded bg-red-200 w-full">
          <div
            style={{ width: (value / 100) * 100 + "%" }}
            className="shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-red-500"
          >
            {value}%
          </div>
        </div>
      );
    if (parseInt(val) === 100)
      return (
        <div className="overflow-hidden h-4 text-xs flex rounded bg-green-200 w-full">
          <div
            style={{ width: "100%" }}
            className="shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-green-500"
          >
            {value}%
          </div>
        </div>
      );
    return (
      <div className="overflow-hidden h-4 text-xs flex rounded bg-orange-200 w-full">
        <div
          style={{ width: (value / 100) * 100 + "%" }}
          className="shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-orange-500"
        >
          {value}%
        </div>
      </div>
    );
  };

  return (
    <div className="flex justify-center items-center w-full">
      {colors(value)}
    </div>
  );
}
