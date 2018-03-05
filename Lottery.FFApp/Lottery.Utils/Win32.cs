using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Lottery.Utils
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SystemTime
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMiliseconds;
    }

    public class Win32
    {
        [DllImport("Kernel32.dll")]
        public static extern bool SetSystemTime(ref SystemTime sysTime);
        [DllImport("Kernel32.dll")]
        public static extern void GetSystemTime(ref SystemTime sysTime);

        public static void SetSystemTime()
        {
            SystemTime sysTime = new SystemTime();
            sysTime.wYear = 2018;
            sysTime.wMonth = 3;
            sysTime.wDay = 2;
            //sysTime.wDayOfWeek = 1;
            sysTime.wHour = 23;
            sysTime.wMinute = 59;
            sysTime.wSecond = 59;
            sysTime.wMiliseconds = 0;

            Win32.SetSystemTime(ref sysTime);
        }
    }
}
