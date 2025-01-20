import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { v4 as uuidv4 } from "uuid";
interface ScratchableArea {
  id: number;
  isScratched: boolean;
  prize: string;
}

interface ScratchState {
  areas: ScratchableArea[];
  userId: string;
}

const initialState: ScratchState = {
  areas: [] as ScratchableArea[],
  userId: uuidv4(),
};

const scratchSlice = createSlice({
  name: "scratch",
  initialState,
  reducers: {
    setAreas(state, action: PayloadAction<ScratchableArea[]>) {
      state.areas = action.payload;
    },

    scratchArea(state, action: PayloadAction<{ id: number; prize: string }>) {
      const area = state.areas.find((a) => a.id === action.payload.id);
      if (area) {
        area.isScratched = true;
        area.prize = action.payload.prize;
      }
    },
    setUserId(state, action: PayloadAction<string>) {
      state.userId = action.payload;
    },
    resetState(state) {
      state.userId = uuidv4();
    },
  },
});

export const { setAreas, scratchArea, setUserId, resetState } =
  scratchSlice.actions;
export default scratchSlice.reducer;
