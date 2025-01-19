using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NederlandseLoterij.Application.Hubs;
using NederlandseLoterij.Application.Interfaces;

namespace NederlandseLoterij.Application.Services;

public class BackgroundSquareScratcher(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly Random _random = new();

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scratchService = scope.ServiceProvider.GetRequiredService<IScratchService>();
                    var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<ScratchHub>>();

                    var availableSquares = (await scratchService.GetScratchableAreasAsync(cancellationToken)).ToList();

                    if (availableSquares.Any())
                    {
                        // Pick a random square to scratch
                        var randomIndex = _random.Next(availableSquares.Count);
                        var randomSquare = availableSquares[randomIndex];

                        var result = await scratchService.ScratchSquareAsync(randomSquare.Id, cancellationToken);

                        // Notify all clients about the scratched square
                        await hubContext.Clients.All.SendAsync("ReceiveScratchUpdate", result.Id, result.Prize);

                        // Simulate a delay between scratches
                        await Task.Delay(500, cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (use your logging mechanism)
                Console.WriteLine($"Error in BackgroundSquareScratcher: {ex.Message}");
            }

            // Wait before checking again (adjust interval as needed)
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        }
    }
}