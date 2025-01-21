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

  const refreshAreas = React.useCallback(async () => {
    try {
      const data = await getScratchableAreas();
      dispatch(setAreas(data));
    } catch (error) {
      console.error("Error refreshing scratchable areas:", error);
    }
  }, [dispatch]);

  useEffect(() => {
    const initialize = async () => {
      try {
        await refreshAreas();

        const throttledBatchUpdate = throttle(
          (updates: { id: number; prize: string }[]) => {
            updates.forEach((update) => {
              dispatch(scratchArea(update));
            });
          },
          200
        );

        // Start SignalR connection
        startSignalRConnection(throttledBatchUpdate, refreshAreas);
      } catch (error) {
        console.error("Error initializing data:", error);
      }
    };

    initialize();

    return () => {
      stopSignalRConnection();
    };
  }, [dispatch, refreshAreas, userId]);

  const handleScratch = async (id: number) => {
    try {
      const result = await scratchSquare({ id, userId });
      dispatch(scratchArea({ id: result.id, prize: result.prize }));
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
    } catch (err: any) {
      if (err.response && err.response.status === 400) {
        alert("You can only scratch one square!");
      } else {
        alert("An unexpected error occurred.s");
      }
    }
  };

  return <Calendar areas={areas} onScratch={handleScratch} />;
};

export default HomePage;
