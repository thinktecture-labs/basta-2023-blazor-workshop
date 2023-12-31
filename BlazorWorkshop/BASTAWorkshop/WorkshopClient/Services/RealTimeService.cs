﻿using Microsoft.AspNetCore.SignalR.Client;
using WorkshopClient.Features.Notification;
using WorkshopShared;

namespace WorkshopClient.Services;

public class RealTimeService
{
    private HubConnection? _hubConnection;

    public Action<ConferenceDetails>? OnConferenceAdded { get; set; }
    public Action<ConferenceDetails>? OnConferenceUpdated { get; set; }

    public async Task InitializeAsync()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"https://localhost:7218{SignalRNames.HubName}")
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<ConferenceDetails>(SignalRNames.ConferenceAdded, handler => OnConferenceAdded?.Invoke(handler));
        _hubConnection.On<ConferenceDetails>(SignalRNames.ConferenceUpdated, handler => OnConferenceUpdated?.Invoke(handler));

        await _hubConnection.StartAsync();
    }
}
