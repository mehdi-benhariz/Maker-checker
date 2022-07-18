import React, { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { logout } from "../../API/AuthAPI";
import { AuthContext } from "../../context/AuthContext";

const TemplatePage = ({ MainComponent }) => {
  const { currentUser } = useContext(AuthContext);
  const profileImg =
    "https://forbesthailand.com/wp-content/uploads/2021/08/https-specials-images.forbesimg.com-imageserve-5f47d4de7637290765bce495-0x0.jpgbackground000000cropX11699cropX23845cropY1559cropY22704.jpg";
  let navigate = useNavigate();
  let handlLogout = async (e) => {
    e.preventDefault();
    const res = await logout();
    if (res.status === 200) navigate("/login");
    //todo add custom error handling
  };

  const [showSidebar, setShowSidebar] = React.useState(false);
  let toggleSidebar = () => setShowSidebar(!showSidebar);
  return (
    <div className="bg-gray-50 min-h-screen">
      <div
        className={`top-0 left-0 w-[35vw] bg-white text-white fixed h-full z-40  ease-in-out duration-300 ${
          showSidebar ? "-translate-x-0 " : "-translate-x-full"
        }`}
      >
        <div className="p-10">
          <div>
            <div className="mt-8 text-center">
              <img
                src={profileImg}
                alt=""
                className="w-10 h-10 m-auto rounded-full object-cover lg:w-28 lg:h-28"
              />
              <h5 className="hidden mt-4 text-xl font-semibold text-gray-600 lg:block">
                {currentUser.username}
              </h5>
              <span className="hidden text-gray-400 lg:block">
                {currentUser.role}
              </span>
            </div>

            <ul className="space-y-2 tracking-wide mt-8">
              <li>
                <a
                  href="/"
                  aria-label="dashboard"
                  className="relative px-4 py-3 flex items-center space-x-4 rounded-xl text-white bg-gradient-to-r from-purple-600 to-indigo-400"
                >
                  <svg
                    className="-ml-1 h-6 w-6"
                    viewBox="0 0 24 24"
                    fill="none"
                  >
                    <path
                      d="M6 8a2 2 0 0 1 2-2h1a2 2 0 0 1 2 2v1a2 2 0 0 1-2 2H8a2 2 0 0 1-2-2V8ZM6 15a2 2 0 0 1 2-2h1a2 2 0 0 1 2 2v1a2 2 0 0 1-2 2H8a2 2 0 0 1-2-2v-1Z"
                      className="fill-current text-indigo-400 dark:fill-slate-600"
                    ></path>
                    <path
                      d="M13 8a2 2 0 0 1 2-2h1a2 2 0 0 1 2 2v1a2 2 0 0 1-2 2h-1a2 2 0 0 1-2-2V8Z"
                      className="fill-current text-indigo-200 group-hover:text-indigo-300"
                    ></path>
                    <path
                      d="M13 15a2 2 0 0 1 2-2h1a2 2 0 0 1 2 2v1a2 2 0 0 1-2 2h-1a2 2 0 0 1-2-2v-1Z"
                      className="fill-current group-hover:text-sky-300"
                    ></path>
                  </svg>
                  <span className="-mr-1 font-medium">Dashboard</span>
                </a>
              </li>
              <li>
                <a
                  href="/"
                  className="px-4 py-3 flex items-center space-x-4 rounded-md text-gray-600 group"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="h-5 w-5"
                    viewBox="0 0 20 20"
                    fill="currentColor"
                  >
                    <path
                      className="fill-current text-gray-300 group-hover:text-indigo-300"
                      fillRule="evenodd"
                      d="M2 6a2 2 0 012-2h4l2 2h4a2 2 0 012 2v1H8a3 3 0 00-3 3v1.5a1.5 1.5 0 01-3 0V6z"
                      clipRule="evenodd"
                    />
                    <path
                      className="fill-current text-gray-600 group-hover:text-indigo-600"
                      d="M6 12a2 2 0 012-2h8a2 2 0 012 2v2a2 2 0 01-2 2H2h2a2 2 0 002-2v-2z"
                    />
                  </svg>
                  <span className="group-hover:text-gray-700">Categories</span>
                </a>
              </li>
              <li>
                <a
                  href="/"
                  className="px-4 py-3 flex items-center space-x-4 rounded-md text-gray-600 group"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="h-5 w-5"
                    viewBox="0 0 20 20"
                    fill="currentColor"
                  >
                    <path
                      className="fill-current text-gray-600 group-hover:text-indigo-600"
                      fillRule="evenodd"
                      d="M2 5a2 2 0 012-2h8a2 2 0 012 2v10a2 2 0 002 2H4a2 2 0 01-2-2V5zm3 1h6v4H5V6zm6 6H5v2h6v-2z"
                      clipRule="evenodd"
                    />
                    <path
                      className="fill-current text-gray-300 group-hover:text-indigo-300"
                      d="M15 7h1a2 2 0 012 2v5.5a1.5 1.5 0 01-3 0V7z"
                    />
                  </svg>
                  <span className="group-hover:text-gray-700">Reports</span>
                </a>
              </li>
              <li>
                <a
                  href="/"
                  className="px-4 py-3 flex items-center space-x-4 rounded-md text-gray-600 group"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="h-5 w-5"
                    viewBox="0 0 20 20"
                    fill="currentColor"
                  >
                    <path
                      className="fill-current text-gray-600 group-hover:text-indigo-600"
                      d="M2 10a8 8 0 018-8v8h8a8 8 0 11-16 0z"
                    />
                    <path
                      className="fill-current text-gray-300 group-hover:text-indigo-300"
                      d="M12 2.252A8.014 8.014 0 0117.748 8H12V2.252z"
                    />
                  </svg>
                  <span className="group-hover:text-gray-700">Other data</span>
                </a>
              </li>
              <li>
                <a
                  href="/"
                  className="px-4 py-3 flex items-center space-x-4 rounded-md text-gray-600 group"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="h-5 w-5"
                    viewBox="0 0 20 20"
                    fill="currentColor"
                  >
                    <path
                      className="fill-current text-gray-300 group-hover:text-indigo-300"
                      d="M4 4a2 2 0 00-2 2v1h16V6a2 2 0 00-2-2H4z"
                    />
                    <path
                      className="fill-current text-gray-600 group-hover:text-indigo-600"
                      fillRule="evenodd"
                      d="M18 9H2v5a2 2 0 002 2h12a2 2 0 002-2V9zM4 13a1 1 0 011-1h1a1 1 0 110 2H5a1 1 0 01-1-1zm5-1a1 1 0 100 2h1a1 1 0 100-2H9z"
                      clipRule="evenodd"
                    />
                  </svg>
                  <span className="group-hover:text-gray-700">Finance</span>
                </a>
              </li>
            </ul>
          </div>

          <div className="px-6 -mx-6 pt-4 flex justify-between items-center border-t">
            <button
              onClick={handlLogout}
              className="px-4 py-3 flex items-center space-x-4 rounded-md text-gray-600 group"
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="h-6 w-6"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1"
                />
              </svg>
              <span className="group-hover:text-gray-700">Logout</span>
            </button>
          </div>
        </div>
      </div>
      <nav>
        <div className="flex justify-between items-center p-4 bg-white">
          <div>
            {showSidebar ? (
              <button
                className="flex text-4xl text-indigo items-center cursor-pointer fixed left-10 top-6 z-50"
                onClick={toggleSidebar}
              >
                x
              </button>
            ) : (
              <svg
                onClick={toggleSidebar}
                className="fixed  z-30 flex items-center cursor-pointer left-10 top-6"
                fill="#2563EB"
                viewBox="0 0 100 80"
                width="40"
                height="40"
              >
                <rect width="100" height="10"></rect>
                <rect y="30" width="100" height="10"></rect>
                <rect y="60" width="100" height="10"></rect>
              </svg>
            )}
          </div>
          <div className="flex items-center space-x-2">
            <div className="relative">
              <div className="absolute">
                <div className="animate-ping top-0 right-0 h-3 w-3 my-1 border-2 rounded-full bg-indigo-600 z-2"></div>
                <div className=" absolute top-0 right-0 h-3 w-3 my-1 border-2 rounded-full bg-indigo-600 z-2"></div>
              </div>

              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="h-7 w-7 cursor-pointer"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"
                />
              </svg>
            </div>

            <div className="w-10">
              <img className="rounded-full" src={profileImg} alt="" />
            </div>
          </div>
        </div>
        <div></div>
      </nav>
      {/*  */}

      {MainComponent}
      {/*  */}
    </div>
  );
};

export default TemplatePage;
