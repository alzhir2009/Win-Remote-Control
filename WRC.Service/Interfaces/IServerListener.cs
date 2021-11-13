using System.Threading;
using System.Threading.Tasks;

namespace WRC.Service.Interfaces
{
    public interface IServerListener
    {
        Task StartAsync(CancellationToken token);
        Task StopAsync(CancellationToken token);
    }
}
