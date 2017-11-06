using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Core2APIExercise.Common
{
    public static class OS
    {
        public static bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static bool IsMacOs() => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }
}
