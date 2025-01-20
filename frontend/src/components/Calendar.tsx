import React from "react";
import Square from "./Square";
import NavBar from "./NavBar";

interface CalendarProps {
  areas: { id: number; isScratched: boolean; prize: string | null }[];
  onScratch: (id: number) => void;
}

const Calendar: React.FC<CalendarProps> = ({ areas, onScratch }) => {
  return (
    <div className="flex flex-col min-h-screen bg-gray-100">
      {/* Navigation Bar */}
      <NavBar />

      {/* Calendar Content */}
      <div className="flex justify-center items-center p-4 flex-grow">
        <div className="grid grid-cols-50 gap-1 w-full">
          {areas.map((area) => (
            <Square
              key={area.id}
              id={area.id}
              isScratched={area.isScratched}
              prize={area.prize}
              onScratch={onScratch}
            />
          ))}
        </div>
      </div>
    </div>
  );
};

export default Calendar;
