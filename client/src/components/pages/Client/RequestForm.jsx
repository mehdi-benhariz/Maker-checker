import React, { useEffect, useState } from "react";
import { login } from "../../../API/AuthAPI";
import { addRequest } from "../../../API/RequestAPI";
import { TextField, NumberField } from "../../utils/InputFields";

const RequestForm = () => {
  const [serviceTypes, setServiceTypes] = useState([
    { id: 1, name: "international" },
    { id: 2, name: "intrabank" },
  ]);
  const [request, setrequest] = useState({});
  const [error, seterror] = useState([]);

  async function submitRequest() {
    const logging = await login("red", "123456789");
    if (!request.serviceType) request.serviceType = serviceTypes[0].id;
    // const res = ICall("createRequest", request);
    if (logging.status === 200) {
      const res = await addRequest(request);
      console.log(res);
    }
  }

  return (
    <div className="bg-gray-100 px-2 py-1  w-4/5 my-3">
      <h4 className="mb-4 text-lg font-semibold text-gray-600 dark:text-gray-300">
        Add Request
      </h4>
      <div className="w-full px-4 py-3 mb-8 grid grid-cols-2 bg-white rounded-lg shadow-md dark:bg-gray-800">
        <label className="block text-sm">
          <span className="text-gray-700 dark:text-gray-400">Name</span>
          <TextField
            value={request.name}
            cb={(x) => setrequest({ ...request, Name: x })}
          />
        </label>

        <label className="block text-sm mx-4">
          <span className="text-gray-700 dark:text-gray-400">Service Type</span>
          <select
            onChange={(e) =>
              setrequest({ ...request, ServiceTypeId: e.target.value })
            }
            className="block h-10 w-full rounded-lg text-sm dark:text-gray-300 dark:border-gray-500 dark:bg-gray-700 form-select focus:border-purple-400 focus:outline-none focus:shadow-outline-purple dark:focus:shadow-outline-gray"
          >
            {serviceTypes.map((serviceType) => (
              <option
                key={serviceType.id}
                value={serviceType.id}
                className="h-10 bg-gray-300"
              >
                {serviceType.name}
              </option>
            ))}
          </select>
        </label>
        <label className="block text-sm col-span-2  mr-4">
          <span className="text-gray-700 dark:text-gray-400">Amount</span>
          <NumberField
            value={request.amount}
            cb={(x) => setrequest({ ...request, Amount: x })}
          />
        </label>

        <div className="flex mt-6 text-sm">
          <label className="flex items-center dark:text-gray-400">
            <input
              type="checkbox"
              className="w-4 h-4 accent-purple-600 text-purple-600 bg-gray-100 rounded border-gray-300 focus:ring-purple-500 dark:focus:ring-purple-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
            />
            <span className="ml-2">
              I agree to the <span className="underline">privacy policy</span>
            </span>
          </label>
        </div>
      </div>
      <div className="flex justify-end">
        <button
          onClick={submitRequest}
          className="bg-gradient-to-tr from-indigo-600 to-purple-600 px-4 py-2 rounded-md text-white font-semibold tracking-wide cursor-pointer"
        >
          Add Request
        </button>
      </div>
    </div>
  );
};

export default RequestForm;
