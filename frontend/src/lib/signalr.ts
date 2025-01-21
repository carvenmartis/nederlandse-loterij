import {
  HubConnectionBuilder,
  LogLevel,
  HubConnection,
} from "@microsoft/signalr";

const SIGNALR_HUB_URL = process.env.NEXT_PUBLIC_API_BASE_URL + "/hub/scratch";

let connection: HubConnection | null = null;

export const startSignalRConnection = (
  onScratchUpdate: (updates: { id: number; prize: string }[]) => void,
  refreshAreas: () => Promise<void>
) => {
  connection = new HubConnectionBuilder()
    .withUrl(SIGNALR_HUB_URL)
    .configureLogging(LogLevel.Information)
    .withAutomaticReconnect({
      nextRetryDelayInMilliseconds: (retryContext) =>
        Math.min(10000, Math.pow(2, retryContext.previousRetryCount) * 1000), // Exponential backoff, max 10s
    })
    .build();

  connection.on(
    "ReceiveScratchUpdate",
    (updates: { id: number; prize: string }[]) => {
      if (Array.isArray(updates)) {
        onScratchUpdate(updates);
      } else {
        console.log("Received non-array data from SignalR:", updates);
      }
    }
  );

  connection.onclose((error) => {
    console.log("SignalR connection lost.", error);
    if (error) {
      console.log("Connection disconnected with error:", error.message);
    } else {
      console.warn("Connection disconnected without error.");
    }
  });

  connection.onreconnecting((error) => {
    console.warn("SignalR reconnecting...", error);
  });

  connection.onreconnected(async (connectionId) => {
    console.log("SignalR reconnected. Connection ID:", connectionId);
    await refreshAreas();
  });

  connection
    .start()
    .then(() => console.log("SignalR Connected"))
    .catch((err) => {
      console.error("SignalR Connection Error:", err.message || err);
    });

  return connection;
};

export const stopSignalRConnection = async () => {
  if (connection) {
    await connection.stop();
  }
};
