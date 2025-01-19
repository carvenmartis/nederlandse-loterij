import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface ScratchableArea {
  id: number;
  isScratched: boolean;
  prize: string | null;
}

interface ScratchState {
  areas: ScratchableArea[];
  userId: string;
  hasScratched: boolean;
}

const initialState: ScratchState = {
  areas: [],
  userId: "",
  hasScratched: false,
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
        state.hasScratched = true;
        localStorage.setItem("hasScratched", "true");
      }
    },
    setUserId(state, action: PayloadAction<string>) {
      state.userId = action.payload;
      // localStorage.setItem("userId", action.payload);
    },
  },
});

export const { setAreas, scratchArea, setUserId } = scratchSlice.actions;
export default scratchSlice.reducer;
