using System;
using System.Runtime.InteropServices;
namespace ConsoleSnakeGame.AdditionalDLL
{
    class CheckSymbol
    {
        /// <summary>
        /// Подлючены библиотеки для считывания символа под курсором
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadConsoleOutputCharacter(IntPtr hConsoleOutput,
                                                             [Out] char[] lpCharacter,
                                                             int nLength,
                                                             COORD dwReadCoord,
                                                             out int lpNumberOfCharsRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);
    }
}