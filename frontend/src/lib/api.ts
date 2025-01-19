import axios from "axios";

// Base URL for backend
const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL + "/api";

// Fetch scratchable areas
export const getScratchableAreas = async () => {
  const response = await axios.get(`${API_BASE_URL}/scratch`);
  return response.data;
};

// Scratch a specific square
export const scratchSquare = async (id: number) => {
  const response = await axios.post(`${API_BASE_URL}/scratch`, { id });
  return response.data;
};
