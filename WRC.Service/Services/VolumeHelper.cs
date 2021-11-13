using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WRC.Service.Services
{
    internal static class VolumeHelper
    {
        private const byte Up = 175;
        private const byte Down = 174;
        private const byte Mute = 173;

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        public static void MuteVolume() => keybd_event(Mute, 0, 0, 0);

        public static async Task VolumeDownAsync(int step)
        {
            for (int i = 0; i < step; i++)
            {
                keybd_event(Down, 0, 0, 0);
                await Task.Delay(30);
            }
        }

        public static async Task VolumeUpAsync(int step)
        {
            for (int i = 0; i < step; i++)
            {
                keybd_event(Up, 0, 0, 0);
                await Task.Delay(30);
            }
        }
    }
}
