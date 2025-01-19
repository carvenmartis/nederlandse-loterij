import { createSlice, createAsyncThunk, PayloadAction } from "@reduxjs/toolkit";
import {
  fetchScratchableAreas as apiFetch,
  scratchSquare as apiScratch,
} from "../services/scratchApi";
import { ScratchableArea } from "../types";

export const fetchScratchableAreas = createAsyncThunk(
  "scratch/fetchScratchableAreas",
  async () => await apiFetch()
);

export const scratchArea = createAsyncThunk(
  "scratch/scratchArea",
  async (id: number) => await apiScratch(id)
);

interface ScratchState {
  areas: ScratchableArea[];
  userScratch: number | null;
  loading: boolean;
  error: string | null;
}

const initialState: ScratchState = {
  areas: [],
  userScratch: null,
  loading: false,
  error: null,
};

const scratchSlice = createSlice({
  name: "scratch",
  initialState,
  reducers: {
    // Real-time updates via SignalR
    receiveScratchUpdate(
      state,
      action: PayloadAction<{ id: number; prize: string }>
    ) {
      const { id, prize } = action.payload;
      const index = state.areas.findIndex((area) => area.id === id);
      if (index >= 0) {
        state.areas[index].isScratched = true;
        state.areas[index].prize = prize;
      }
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchScratchableAreas.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchScratchableAreas.fulfilled, (state, action) => {
        state.areas = action.payload.scratchableAreas;
        state.userScratch = action.payload.userScratch;
        state.loading = false;
      })
      .addCase(fetchScratchableAreas.rejected, (state) => {
        state.loading = false;
        state.error = "Failed to load scratchable areas.";
      })
      .addCase(scratchArea.fulfilled, (state, action) => {
        const index = state.areas.findIndex((a) => a.id === action.payload.id);
        if (index >= 0) {
          state.areas[index].isScratched = true;
          state.areas[index].prize = action.payload.prize;
        }
        state.userScratch = action.payload.id;
      })
      .addCase(scratchArea.rejected, (state) => {
        state.error = "Failed to scratch the square.";
      });
  },
});

export const { receiveScratchUpdate } = scratchSlice.actions;
export default scratchSlice.reducer;
