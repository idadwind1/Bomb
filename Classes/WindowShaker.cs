using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomb.Classes
{

    public static class WindowShaker
    {
        public static void ShakeMouse(bool ex = false)
        {
            var Ex = 2;
            var random = new Random();
            var newPosition = new Size(random.Next(-10 * (ex ? Ex : 1), 10 * (ex ? Ex : 1)), random.Next(-10 * (ex ? Ex : 1), 10 * (ex ? Ex : 1)));
            var oldPosition = Cursor.Position;
            Cursor.Position = Point.Add(Cursor.Position, newPosition);
            Thread.Sleep(20);
            Cursor.Position = oldPosition;
        }
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static void ShakeCurrentWindows(bool ex = false)
        {
            var Ex = 5;

            var random = new Random();

            foreach (var form in Application.OpenForms)
            {
                var handle = ((Form)form).Handle;
                RECT rect;
                GetWindowRect(handle, out rect);
                var width = rect.Right - rect.Left;
                var height = rect.Bottom - rect.Top;
                var newX = random.Next(-10 * (ex ? Ex : 1), 10 * (ex ? Ex : 1));
                var newY = random.Next(-10 * (ex ? Ex : 1), 10 * (ex ? Ex : 1));
                MoveWindow(handle, rect.Left + newX, rect.Top + newY, width, height, true);
                Thread.Sleep(20);
                MoveWindow(handle, rect.Left, rect.Top, width, height, true);
            }
        }

    }
}
