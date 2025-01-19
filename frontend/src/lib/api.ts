import axios from "axios";

// Base URL for backend
const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL;

const axiosInstance = axios.create({
  baseURL: "https://api.example.com",
  withCredentials: true, // Similar to 'mode: "cors"' and 'credentials: "include"'
  headers: {
    "Content-Type": "application/json",
  },
});

// Fetch scratchable areas
export const getScratchableAreas = async () => {
  const response = await axiosInstance.get(`${API_BASE_URL}/scratch`);
  return response.data;
};

// Scratch a specific square
export const scratchSquare = async (id: number) => {
  const response = await axiosInstance.post(`${API_BASE_URL}/scratch`, { id });
  return response.data;
};
