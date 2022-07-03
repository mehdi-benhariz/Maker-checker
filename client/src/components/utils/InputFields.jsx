import React from "react";
export function TextField({ value, cb, placeholder }) {
  return (
    <input
      type="text"
      className="bg-gray-200 appearance-none border-2 border-gray-200 rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:bg-white focus:border-purple-500"
      value={value}
      placeholder={placeholder}
      onChange={(e) => {
        cb(e.target.value);
      }}
    />
  );
}

export function NumberField({ value, cb }) {
  return (
    <input
      type="number"
      className="bg-gray-200 appearance-none border-2 border-gray-200 rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:bg-white focus:border-purple-500"
      value={value}
      min="0"
      onChange={(e) => {
        cb(e.target.value);
      }}
    />
  );
}
