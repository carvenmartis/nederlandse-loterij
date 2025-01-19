"use client";

import React, { useEffect } from "react";
import GridSquare from "./GridSquare";
import { useSelector, useDispatch } from "react-redux";
import { RootState, AppDispatch } from "../redux/store";
import {
  fetchScratchableAreas,
  scratchArea,
  receiveScratchUpdate,
} from "../redux/scratchSlice";
import { HubConnectionBuilder } from "@microsoft/signalr";

const ScratchCalendar: React.FC = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { areas, userScratch, loading, error } = useSelector(
    (state: RootState) => state.scratch
  );

  useEffect(() => {
    // Fetch initial data
    dispatch(fetchScratchableAreas());

    // Connect to SignalR hub
    const connection = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/scratchHub") // Adjust URL based on backend configuration
      .withAutomaticReconnect()
      .build();

    connection
      .start()
      .catch((err) => console.error("SignalR connection error:", err));

    // Listen for updates
    connection.on("ReceiveScratchUpdate", (id: number, prize: string) => {
      dispatch(receiveScratchUpdate({ id, prize }));
    });

    return () => {
      connection.stop();
    };
  }, [dispatch]);

  const handleScratch = (id: number) => {
    if (userScratch) {
      alert("You can only scratch one square.");
      return;
    }
    dispatch(scratchArea(id));
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p className="text-red-500">{error}</p>;
  if (!areas || areas.length === 0)
    return <p>No scratchable areas available.</p>; // Check for undefined or empty array

  return (
    <div className="grid grid-cols-10 gap-2">
      {areas.map((area) => (
        <GridSquare
          key={area.id}
          area={area}
          onClick={() => handleScratch(area.id)}
          disabled={!!userScratch || area.isScratched}
        />
      ))}
    </div>
  );
};

export default ScratchCalendar;
