"use client";

import ScratchCalendar from "../../components/ScratchCalendar";

const ScratchPage: React.FC = () => {
  return (
    <div className="container mx-auto text-center p-4">
      <h1 className="text-2xl font-bold mb-6">Scratch Game Calendar</h1>
      <ScratchCalendar />
    </div>
  );
};

export default ScratchPage;
