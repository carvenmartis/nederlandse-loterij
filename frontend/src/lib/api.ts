import axios from "axios";

// Base URL for backend
const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL + "/api";

// Fetch scratchable areas
export const getScratchableAreas = async () => {
  try {
    const response = await axios.get(`${API_BASE_URL}/scratch`);
    return response.data;
  } catch (error) {
    console.error("Error fetching scratchable areas:", error);
    throw error;
  }
};

// Scratch a specific square
export const scratchSquare = async ({
  id,
  userId,
}: {
  id: number;
  userId: string;
}) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/scratch`, {
      id,
      userId,
    });
    return response.data;
  } catch {}
};
