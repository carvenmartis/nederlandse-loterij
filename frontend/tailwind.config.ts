import type { Config } from "tailwindcss";

export default {
  content: ["./src/**/*.{js,ts,jsx,tsx}", "./pages/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        scratched: "#4caf50", // Custom green for scratched squares
        unscratched: "#ffffff", // Custom white for unscratched squares
      },
      gridTemplateColumns: {
        50: "repeat(50, minmax(0, 1fr))", // Adds grid-cols-50
      },
    },
  },
  plugins: [],
} satisfies Config;
