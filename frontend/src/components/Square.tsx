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
  // Determine the background color based on the scratched state and prize
  const getBackgroundColor = () => {
    if (!isScratched) return "bg-white text-gray-500 hover:bg-gray-200"; // Unscratched
    if (!prize) return "bg-red-300 text-white"; // Scratched with no prize
    if (prize === "Consolation Prize") return "bg-blue-300 text-white"; // Consolation prize
    if (prize === "€25,000") return "bg-green-300 text-white"; // Grand prize
    return "bg-gray-300"; // Fallback for unexpected cases
  };

  return (
    <div
      className={`relative flex items-center justify-center border cursor-pointer aspect-square ${getBackgroundColor()}`}
      onClick={() => !isScratched && onScratch(id)}
    >
      {/* Overlay */}
      {isScratched && prize && (
        <div className="absolute inset-0 z-40 flex items-center justify-center bg-opacity-75 text-white rounded opacity-0 hover:opacity-100 transition-opacity">
          <p className="text-[.5rem] text-center whitespace-nowrap bg-black px-2 py-1 opacity-50">
            {prize}
          </p>
        </div>
      )}
    </div>
  );
};

export default Square;
