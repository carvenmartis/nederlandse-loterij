import {
  HubConnectionBuilder,
  LogLevel,
  HubConnection,
} from "@microsoft/signalr";

const SIGNALR_HUB_URL = process.env.NEXT_PUBLIC_API_BASE_URL + "/scratchHub";

let connection: HubConnection | null = null;

export const startSignalRConnection = (
  onScratchUpdate: (id: number, prize: string) => void
) => {
  connection = new HubConnectionBuilder()
    .withUrl(SIGNALR_HUB_URL!) // Ensure this matches the backend hub URL
    .configureLogging(LogLevel.Information)
    .withAutomaticReconnect()
    .build();

  connection.on("ReceiveScratchUpdate", (id: number, prize: string) => {
    onScratchUpdate(id, prize);
  });

  connection
    .start()
    .then(() => console.log("SignalR Connected"))
    .catch((err) => console.error("SignalR Connection Error:", err));

  return connection;
};

export const stopSignalRConnection = async () => {
  if (connection) {
    await connection.stop();
  }
};
