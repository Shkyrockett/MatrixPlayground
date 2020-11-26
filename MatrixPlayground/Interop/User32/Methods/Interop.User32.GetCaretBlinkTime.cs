using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class User32
    {
        /// <summary>
        /// Retrieves the time required to invert the caret's pixels. The user can set this value.
        /// </summary>
        /// <returns>
        /// If the function succeeds, the return value is the blink time, in milliseconds.
        /// A return value of INFINITE indicates that the caret does not blink.
        /// A return value is zero indicates that the function has failed. To get extended error information, call GetLastError.
        /// </returns>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getcaretblinktime
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.User32, SetLastError = true)]
        private static extern uint GetCaretBlinkTime();
    }
}
