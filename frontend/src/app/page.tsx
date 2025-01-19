"use client";

import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../store";
import { setAreas, scratchArea, setUserId } from "../store/scratchSlice";
import { getScratchableAreas, scratchSquare } from "../lib/api";
import { v4 as uuidv4 } from "uuid";
import Calendar from "../components/Calendar";

const HomePage: React.FC = () => {
  const dispatch = useDispatch();
  const { areas, hasScratched } = useSelector(
    (state: RootState) => state.scratch
  );

  useEffect(() => {
    const initialize = async () => {
      // Generate and store userId if not already set
      const storedUserId = localStorage.getItem("userId") || uuidv4();
      if (!localStorage.getItem("userId")) {
        localStorage.setItem("userId", storedUserId);
      }
      dispatch(setUserId(storedUserId));

      // Fetch scratchable areas from the backend
      const data = await getScratchableAreas();
      dispatch(setAreas(data));
    };

    initialize();
  }, [dispatch]);

  const handleScratch = async (id: number) => {
    if (!hasScratched) {
      try {
        const result = await scratchSquare(id); // Replace with your scratch logic
        dispatch(scratchArea({ id: result.id, prize: result.prize }));
      } catch (err) {
        console.error("Scratch error:", err);
      }
    } else {
      alert("You can only scratch one square!");
    }
  };

  return <Calendar areas={areas} onScratch={handleScratch} />;
};

export default HomePage;
