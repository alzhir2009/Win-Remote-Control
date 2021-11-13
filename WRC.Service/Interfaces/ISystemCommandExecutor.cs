using System.Threading.Tasks;

namespace WRC.Service.Interfaces
{
    public interface ISystemCommandExecutor
    {
        void Shutdown();
        void Restart();
        void ToggleMute();
        Task VolumeUpAsync(int step);
        Task VolumeDownAsync(int step);
    }
}
