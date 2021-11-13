using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using WRC.Service.Interfaces;

namespace WRC.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServerListener _listener;

        public Worker(ILogger<Worker> logger, IServerListener listener)
        {
            _logger = logger;
            _listener = listener;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _listener.StartAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                //TODO: check for configuration changed by manager 
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
