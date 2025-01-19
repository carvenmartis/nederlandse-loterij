import { NextApiRequest, NextApiResponse } from "next";
import { v4 as uuidv4 } from "uuid";
import { HubConnectionBuilder } from "@microsoft/signalr";

// In-memory storage for demonstration
const scratchableAreas = Array.from({ length: 100 }, (_, i) => ({
  id: i + 1,
  isScratched: false,
  prize: i === 0 ? "€25,000" : i < 101 ? "Consolation Prize" : null,
}));

const userScratchData: Record<string, number> = {}; // Maps userId to scratched square ID

// SignalR notification
const notifyClients = async (id: number, prize: string) => {
  const connection = new HubConnectionBuilder()
    .withUrl("http://localhost:5000/scratchHub") // Adjust this to match your SignalR setup
    .withAutomaticReconnect()
    .build();

  await connection.start();
  await connection.invoke("ReceiveScratchUpdate", id, prize);
  connection.stop();
};

export default async function handler(
  req: NextApiRequest,
  res: NextApiResponse
) {
  const { method } = req;
  const userId = req.cookies.userId || uuidv4(); // Fetch or generate userId

  // Set a userId cookie if it doesn't exist
  if (!req.cookies.userId) {
    res.setHeader(
      "Set-Cookie",
      `userId=${userId}; HttpOnly; Path=/; Max-Age=31536000`
    );
  }

  if (method === "GET") {
    // Respond with the scratchable areas and user's scratch status
    return res.status(200).json({
      scratchableAreas,
      userScratch: userScratchData[userId] || null,
    });
  }

  if (method === "POST") {
    const { id } = req.body;

    // Ensure the user hasn't already scratched a square
    if (userScratchData[userId]) {
      return res
        .status(403)
        .json({ error: "You can only scratch one square." });
    }

    // Find and update the square
    const area = scratchableAreas.find((a) => a.id === id);
    if (!area || area.isScratched) {
      return res.status(400).json({ error: "Invalid or already scratched." });
    }

    area.isScratched = true;
    userScratchData[userId] = id; // Record the user's scratch

    // Notify other clients about the update
    await notifyClients(area.id, area.prize || "");

    return res.status(200).json(area);
  }

  // Method not allowed
  res.setHeader("Allow", ["GET", "POST"]);
  return res.status(405).end(`Method ${method} Not Allowed`);
}
