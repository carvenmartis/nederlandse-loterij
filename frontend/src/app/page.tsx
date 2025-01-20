"use client";

import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../store";
import { setAreas, scratchArea } from "../store/scratchSlice";
import { getScratchableAreas, scratchSquare } from "../lib/api";
import Calendar from "../components/Calendar";
import { startSignalRConnection, stopSignalRConnection } from "../lib/signalr";
import { throttle } from "lodash";

const HomePage: React.FC = () => {
  const dispatch = useDispatch();
  const { areas, userId } = useSelector((state: RootState) => state.scratch);

  useEffect(() => {
    const initialize = async () => {
      try {
        const data = await getScratchableAreas();
        dispatch(setAreas(data));

        const throttledScratchUpdate = throttle(
          (squareId: number, prize: string) => {
            dispatch(scratchArea({ id: squareId, prize }));
          },
          200
        );

        startSignalRConnection((squareId, prize) => {
          throttledScratchUpdate(squareId, prize);
        });
      } catch (error) {
        console.error("Error initializing data:", error);
      }
    };

    initialize();

    return () => {
      stopSignalRConnection();
    };
  }, [dispatch, userId]);

  const handleScratch = async (id: number) => {
    try {
      console.log("Scratching square", id);
      console.log("User ID", userId);
      const result = await scratchSquare({ id, userId });
      dispatch(scratchArea({ id: result.id, prize: result.prize }));
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
    } catch (err: any) {
      if (err.response && err.response.status === 400) {
        alert("You can only scratch one square!");
      } else {
        alert("An unexpected error occurred.");
      }
    }
  };

  return <Calendar areas={areas} onScratch={handleScratch} />;
};

export default HomePage;
