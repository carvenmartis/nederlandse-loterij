"use client";

import React from "react";
import { ScratchableArea } from "../types";

interface GridSquareProps {
  area: ScratchableArea;
  onClick: () => void;
  disabled: boolean;
}

const GridSquare: React.FC<GridSquareProps> = ({ area, onClick, disabled }) => {
  const { isScratched, prize } = area;

  return (
    <button
      onClick={disabled ? undefined : onClick}
      className={`w-12 h-12 border border-gray-400 flex items-center justify-center ${
        isScratched
          ? "bg-gray-300 cursor-not-allowed"
          : disabled
          ? "bg-gray-100 cursor-not-allowed"
          : "bg-white hover:bg-blue-100"
      }`}
    >
      {isScratched ? prize || "No Prize" : "Scratch"}
    </button>
  );
};

export default GridSquare;
