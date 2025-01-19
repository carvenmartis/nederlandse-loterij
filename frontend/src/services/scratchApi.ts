import axios from "axios";
import { ScratchableArea } from "../types";

export const fetchScratchableAreas = async (): Promise<{
  scratchableAreas: ScratchableArea[];
  userScratch: number | null;
}> => {
  const response = await axios.get("/api/scratch");
  return response.data;
};

export const scratchSquare = async (id: number): Promise<ScratchableArea> => {
  const response = await axios.post("/api/scratch", { id });
  return response.data;
};
