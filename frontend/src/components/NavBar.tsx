import React from "react";
import { useDispatch } from "react-redux";
import { persistor } from "../store";
import { resetState } from "../store/scratchSlice";

const NavBar: React.FC = () => {
  const dispatch = useDispatch();

  const handleReset = async () => {
    // Clear Redux state
    dispatch(resetState());

    // Clear persisted state
    await persistor.purge();

    alert("State has been reset!");
  };

  return (
    <nav className="bg-blue-600 p-4 shadow-md">
      <div className="flex justify-between items-center max-w-7xl mx-auto">
        <h1 className="text-white text-lg font-semibold">Scratch Calendar</h1>
        <button
          onClick={handleReset}
          className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600 transition"
        >
          Reset
        </button>
      </div>
    </nav>
  );
};

export default NavBar;
