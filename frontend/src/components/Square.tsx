import React from "react";

interface SquareProps {
  id: number;
  isScratched: boolean;
  prize: string | null;
  onScratch: (id: number) => void;
}

const Square: React.FC<SquareProps> = ({
  id,
  isScratched,
  prize,
  onScratch,
}) => {
  return (
    <div
      className={`flex items-center justify-center border cursor-pointer aspect-square 
      ${
        isScratched
          ? "bg-green-300 text-black" // Scratched squares: green background
          : "bg-white text-gray-500 hover:bg-gray-200" // Unscratched squares: white background
      }`}
      onClick={() => !isScratched && onScratch(id)}
    >
      {isScratched ? prize || "" : ""}
    </div>
  );
};

export default Square;
