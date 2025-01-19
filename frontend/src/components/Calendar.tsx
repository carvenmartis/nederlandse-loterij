import React from "react";
import Square from "./Square";

interface CalendarProps {
  areas: { id: number; isScratched: boolean; prize: string | null }[];
  onScratch: (id: number) => void;
}

const Calendar: React.FC<CalendarProps> = ({ areas, onScratch }) => {
  return (
    <div className="flex justify-center items-center min-h-screen bg-gray-100 p-4">
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
  );
};

export default Calendar;
