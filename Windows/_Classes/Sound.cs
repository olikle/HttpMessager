using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HttpMessenger
{
    class Sound
    {
        //
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interop/how-to-use-platform-invoke-to-play-a-wave-file
        // 
        [DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true, CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        private static extern bool PlaySound(string szSound, System.IntPtr hMod, PlaySoundFlags flags);

        [System.Flags]
        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_LOOP = 0x0008,
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004
        }

        /// <summary>
        /// Plays the sound file.
        /// </summary>
        /// <param name="wavFile">The wav file.</param>
        /// <returns></returns>
        public static bool PlaySoundFile(string wavFile, PlaySoundFlags soundFlag = PlaySoundFlags.SND_SYNC)
        {
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.Sound);
            //player.Play();

            return PlaySound(wavFile, new System.IntPtr(), soundFlag);
        }
    }
}
