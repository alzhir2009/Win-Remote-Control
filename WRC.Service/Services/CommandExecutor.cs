using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WRC.Service.Interfaces;

namespace WRC.Service.Services
{
    public class CommandExecutor : ISystemCommandExecutor
    {
        private const int DefaultKeyStep = 2; 

        public CommandExecutor()
        {

        }

        public void Shutdown() => Process.Start("shutdown", "/s /t 0");

        public void Restart() => Process.Start("shutdown", "/r /t 0");

        public void ToggleMute() => VolumeHelper.MuteVolume();

        public Task VolumeUpAsync(int step) => VolumeHelper.VolumeUpAsync(step / DefaultKeyStep);

        public Task VolumeDownAsync(int step) => VolumeHelper.VolumeDownAsync(step / DefaultKeyStep);
    }
}
