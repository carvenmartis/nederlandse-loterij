using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NederlandseLoterij.Application.Hubs;
using NederlandseLoterij.Application.Scratchable.Commands;
using NederlandseLoterij.Application.Scratchable.Dtos;
using NederlandseLoterij.Application.Scratchable.Queries;

namespace NederlandseLoterij.Application.Services;

/// <summary>
/// Service for handling the background processing of scratchable squares.
/// </summary>
public class SquareScratcherService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHubContext<ScratchHub> _hubContext;
    private readonly HubConnection _hubConnection;
    private readonly Random _random = new();
    private static List<ScratchableRecordDto>? _cachedSquares;

    public SquareScratcherService(IServiceScopeFactory serviceScopeFactory, IHubContext<ScratchHub> hubContext)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _hubContext = hubContext;

        // Initialize the SignalR client connection
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5104/hub/scratch") // Adjust URL as needed
            .WithAutomaticReconnect()
            .Build();

        // Register the listener for "SquareScratched" events
        _hubConnection.On<int>("SquareScratched", scratchedSquareId =>
        {
            // Remove the scratched square from the cache
            _cachedSquares?.RemoveAll(square => square.Id == scratchedSquareId);
        });
    }

    /// <summary>
    /// Executes the background service.
    /// </summary>
    /// <param name="stoppingToken">Token to monitor for cancellation requests.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _hubConnection.StartAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await ProcessBatch(mediator, stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in BackgroundSquareScratcher: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Retrieves the cached available squares or fetches them if not cached.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending queries.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A list of available scratchable squares.</returns>
    private async Task<List<ScratchableRecordDto>> GetCachedAvailableSquaresAsync(IMediator mediator, CancellationToken cancellationToken)
    {
        if (_cachedSquares == null || !_cachedSquares.Any())
        {
            _cachedSquares = await mediator.Send(new GetScratchableRecordsQuery(), cancellationToken);
        }

        return _cachedSquares;
    }

    /// <summary>
    /// Processes a batch of scratchable squares.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    private async Task ProcessBatch(IMediator mediator, CancellationToken cancellationToken)
    {
        const int batchSize = 20;
        var availableSquares = await GetCachedAvailableSquaresAsync(mediator, cancellationToken);

        if (availableSquares.Any())
        {
            var randomBatch = availableSquares
                .OrderBy(_ => _random.Next())
                .Take(batchSize)
                .ToList();

            var tasks = randomBatch.Select(async square =>
            {
                var result = await mediator.Send(new ScratchRecordCommand
                {
                    UserId = Guid.NewGuid(),
                    Id = square.Id
                }, cancellationToken);

                await _hubContext.Clients.All.SendAsync("ReceiveScratchUpdate", result.Id, result.Prize, cancellationToken);

                _cachedSquares?.Remove(square);
            });

            await Task.WhenAll(tasks);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _hubConnection.StopAsync(cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}