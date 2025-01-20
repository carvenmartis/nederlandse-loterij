"use client";

import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../store";
import {
  setAreas,
  scratchArea,
  setAlreadyScratchedArea,
  setUserId,
} from "../store/scratchSlice";
import { getScratchableAreas, scratchSquare } from "../lib/api";
import { v4 as uuidv4 } from "uuid";
import Calendar from "../components/Calendar";
import { startSignalRConnection, stopSignalRConnection } from "../lib/signalr";

const HomePage: React.FC = () => {
  const dispatch = useDispatch();
  const { areas, hasScratched, userId } = useSelector(
    (state: RootState) => state.scratch
  );

  useEffect(() => {
    const initialize = async () => {
      if (!userId) {
        const newUserId = uuidv4();
        dispatch(setUserId(newUserId));
      }

      const data = await getScratchableAreas();
      dispatch(setAreas(data));

      startSignalRConnection((squareId, prize) => {
        dispatch(setAlreadyScratchedArea({ id: squareId, prize: prize ?? "" }));
      });
    };

    initialize();

    return () => {
      stopSignalRConnection();
    };
  }, [dispatch, userId]);

  const handleScratch = async (id: number) => {
    if (!hasScratched) {
      try {
        const result = await scratchSquare(id);
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
