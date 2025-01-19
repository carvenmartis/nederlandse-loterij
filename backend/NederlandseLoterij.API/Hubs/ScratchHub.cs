using Microsoft.AspNetCore.SignalR;

namespace NederlandseLoterij.API.Hubs;

/// <summary>
/// Hub for handling scratch card related real-time communication.
/// </summary>
public class ScratchHub : Hub
{
    /// <summary>
    /// Called when a new connection is established with the hub.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async Task OnConnectedAsync()
       => await base.OnConnectedAsync();

    /// <summary>
    /// Called when a connection with the hub is terminated.
    /// </summary>
    /// <param name="exception">The exception that occurred during the disconnection, if any.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async Task OnDisconnectedAsync(Exception? exception)
       => await base.OnDisconnectedAsync(exception);
}