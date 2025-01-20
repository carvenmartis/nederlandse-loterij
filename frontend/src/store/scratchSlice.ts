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
  _persist?: {
    version: number;
    rehydrated: boolean;
  };
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
    setAlreadyScratchedArea(
      state,
      action: PayloadAction<{ id: number; prize: string }>
    ) {
      const area = state.areas.find((a) => a.id === action.payload.id);
      if (area) {
        area.isScratched = true;
        area.prize = action.payload.prize;
      }
    },
    scratchArea(state, action: PayloadAction<{ id: number; prize: string }>) {
      const area = state.areas.find((a) => a.id === action.payload.id);
      if (area) {
        area.isScratched = true;
        area.prize = action.payload.prize;
      }
      state.hasScratched = true;
    },
    setUserId(state, action: PayloadAction<string>) {
      state.userId = action.payload;
    },
    resetState() {
      return initialState; // Reset the state to the initial value
    },
  },
});

export const {
  setAreas,
  scratchArea,
  setAlreadyScratchedArea,
  setUserId,
  resetState,
} = scratchSlice.actions;
export default scratchSlice.reducer;
