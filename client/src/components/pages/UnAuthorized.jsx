import React from "react";
import { Link } from "react-router-dom";

const UnAuthorized = () => {
  return (
    <section className="flex items-center h-full p-16 dark:bg-gray-900 dark:text-gray-100">
      <div className="container flex flex-col items-center justify-center px-5 mx-auto my-8">
        <div className="max-w-md text-center">
          <h2 className="mb-8 font-extrabold text-9xl text-indigo-400 dark:text-indigo-600">
            <span className="sr-only ">Error</span>401
          </h2>
          <p className="text-2xl font-semibold md:text-3xl text-indigo-400 dark:text-indigo-600">
            Sorry, you are UnAuthorized to do this action.
          </p>
          <p className="mt-4 mb-8 dark:text-gray-400">
            But dont worry, you can find plenty of other things on our homepage.
          </p>
          <Link
            rel="noopener noreferrer"
            to="/login"
            className="px-8 py-3 bg-indigo-400 text-white font-semibold rounded dark:bg-indigo-400 dark:text-gray-900"
          >
            Back to homepage
          </Link>
        </div>
      </div>
    </section>
  );
};

export default UnAuthorized;
