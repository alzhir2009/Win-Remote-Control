using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using WRC.Service.Interfaces;
using WRC.Service.Models;
using WRC.Shared;

namespace WRC.Service.Services
{
    public class ServerListener : IServerListener
    {
        private readonly IOptionsProvider _optionsProvider;
        private readonly ISystemCommandExecutor _commandExecutor;
        private HubConnection _connection;

        public ServerListener(IOptionsProvider optionsProvider, ISystemCommandExecutor commandExecutor)
        {
            _optionsProvider = optionsProvider;
            _commandExecutor = commandExecutor;
        }

        public Task StartAsync(CancellationToken token)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(_optionsProvider.Get(Constants.RegistryServerUrlKey))
                .WithAutomaticReconnect()
                .Build();

            _connection.On<WindowsEventDto>("Event", async (windowsEvent) =>
            {
                switch (windowsEvent.Event)
                {
                    case WindowsEvent.Shutdown:
                        _commandExecutor.Shutdown();
                        break;
                    case WindowsEvent.Restart:
                        _commandExecutor.Restart();
                        break;
                    case WindowsEvent.VolumeUp:
                        await _commandExecutor.VolumeUpAsync((int)windowsEvent.Args);
                        break;
                    case WindowsEvent.VolumeDown:
                        await _commandExecutor.VolumeDownAsync((int)windowsEvent.Args);
                        break;
                    case WindowsEvent.ToggleMute:
                        _commandExecutor.ToggleMute();
                        break;

                }
            });

            _connection.Closed += async (error) =>
            {
                if (error is not OperationCanceledException)
                {
                    await Task.Delay(5000, token);
                    await _connection.StartAsync(token);
                }
            };

            return _connection.StartAsync(token);
        }

        public Task StopAsync(CancellationToken token) => _connection.StopAsync(token);
    }
}
