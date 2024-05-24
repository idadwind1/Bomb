using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Bomb.Classes
{
    public class SoundPlayer
    {
        /// <summary>
        /// Alias of player
        /// </summary>
        public string alias
        {
            get => _alias;
            set => _alias = value.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
        }

        private string _alias;

        //public static uint SND_ASYNC = 0x0001;
        //public static uint SND_FILENAME = 0x00020000;

        [DllImport("winmm.dll")]
        public static extern uint mciSendString(string lpstrCommand, string lpstrReturnString, uint uReturnLength, uint hWndCallback);

        /// <summary>
        /// Construct MusicPlayer
        /// </summary>
        /// <param name="alias">Alias of player</param>
        public SoundPlayer(string alias)
        {
            this.alias = alias;
        }

        /// <summary>
        /// Close file
        /// </summary>
        public void Close()
        {
            mciSendString(@"close " + alias, null, 0, 0);
        }
        /// <summary>
        /// Close file
        /// </summary>
        /// <param name="alias">Alias of player</param>
        public static void Close(string alias)
        {
            mciSendString(@"close " + alias, null, 0, 0);
        }

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="file">File location</param>
        public void Open(string file)
        {
            mciSendString("open \"" + file + "\" alias " + alias, null, 0, 0);
        }
        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="file">File location</param>
        /// <param name="alias">Alias of player</param>
        public static void Open(string file, string alias)
        {
            mciSendString("open \"" + file + "\" alias " + alias, null, 0, 0);
        }

        /// <summary>
        /// Stop current music playing
        /// </summary>
        public void Stop()
        {
            mciSendString(@"close " + alias, null, 0, 0);
        }
        /// <summary>
        /// Stop current music playing
        /// </summary>
        /// <param name="alias">Alias of player</param>
        public static void Stop(string alias)
        {
            mciSendString(@"close " + alias, null, 0, 0);
        }

        /// <summary>
        /// Jump to a location of music
        /// </summary>
        public void JumpTo(long millisecond)
        {
            mciSendString(@"seek " + alias + " to " + millisecond, null, 0, 0);
        }
        /// <summary>
        /// Jump to a location of music
        /// </summary>
        /// <param name="alias">Alias of player</param>
        public static void JumpTo(long millisecond, string alias)
        {
            mciSendString(@"seek " + alias + " to " + millisecond, null, 0, 0);
        }

        /// <summary>
        /// Play music
        /// </summary>
        /// <param name="hWndCallback">Callback handle</param>
        public void Play(uint hWndCallback)
        {
            mciSendString(@"play " + alias + " notify", null, 0, hWndCallback);
        }
        /// <summary>
        /// Play music
        /// </summary>
        /// <param name="hWndCallback">Callback handle</param>
        /// <param name="alias">Alias of music</param>
        public static void Play(uint hWndCallback, string alias)
        {
            mciSendString(@"play " + alias + " notify", null, 0, hWndCallback);
        }
        /// <summary>
        /// Play music
        /// </summary>
        /// <param name="loop">Does loop</param>
        public void Play()
        //{ mciSendString(@"play " + alias + (loop ? " repeat" : ""), null, 0, 0); }
        {
            mciSendString("play " + alias, null, 0, 0);
        }
        /// <summary>
        /// Play music
        /// </summary>
        /// <param name="loop">Does loop</param>
        /// <param name="alias">Alias of player</param>
        public static void Play(string alias)
        {
            mciSendString(@"play " + alias, null, 0, 0);
        }

        /// <summary>
        /// Get music length
        /// </summary>
        public long GetMusicLength()
        {
            var length = "";
            mciSendString("status " + alias + " length", length, 128, 0);
            length = length.Trim();
            if (string.IsNullOrEmpty(length)) return 0;
            return Convert.ToInt64(length);
        }
        /// <summary>
        /// Get music length
        /// </summary>
        /// <param name="alias">Alias of player</param>
        public static long GetMusicLength(string alias)
        {
            var length = "";
            mciSendString("status " + alias + " length", length, 128, 0);
            length = length.Trim();
            if (string.IsNullOrEmpty(length)) return 0;
            return Convert.ToInt64(length);
        }


        public static void PlaySound(UnmanagedMemoryStream resource, IntPtr Handle, double start_at = -1)
        {
            var sound_player = new SoundPlayer("countdown");
            sound_player.Stop();
            sound_player.Close();
            var path = Environment.GetEnvironmentVariable("tmp") + "\\sound.wav";
            Functions.ExtractFile(resource, path);
            sound_player.Open(path);
            if (start_at != -1) sound_player.JumpTo(18300 - (long)(start_at * 1000));
            sound_player.Play((uint)Handle);
        }
    }
}
