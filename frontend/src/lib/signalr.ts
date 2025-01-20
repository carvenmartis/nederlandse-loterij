import {
  HubConnectionBuilder,
  LogLevel,
  HubConnection,
} from "@microsoft/signalr";

const SIGNALR_HUB_URL = process.env.NEXT_PUBLIC_API_BASE_URL + "/hub/scratch";

let connection: HubConnection | null = null;

export const startSignalRConnection = (
  onScratchUpdate: (squareId: number, prize: string) => void
) => {
  connection = new HubConnectionBuilder()
    .withUrl(SIGNALR_HUB_URL) // Ensure this matches the backend endpoint
    .configureLogging(LogLevel.Information) // Enables detailed logging
    .withAutomaticReconnect()
    .build();

  // Listen for the "ReceiveScratchUpdate" event
  connection.on("ReceiveScratchUpdate", (squareId: number, prize: string) => {
    console.log(
      `SignalR Event Received: Square ID = ${squareId}, Prize = ${prize}`
    );
    onScratchUpdate(squareId, prize); // Call the callback with the received data
  });

  // Start the connection
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
