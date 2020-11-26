using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class User32
    {
        /// <summary>
        /// Removes the caret from the screen. Hiding a caret does not destroy its current shape or invalidate the insertion point.
        /// </summary>
        /// <param name="hWnd">A handle to the window that owns the caret. If this parameter is NULL, HideCaret searches the current task for the window that owns the caret.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// HideCaret hides the caret only if the specified window owns the caret. If the specified window does not own the caret, HideCaret does nothing and returns FALSE.
        /// Hiding is cumulative. If your application calls HideCaret five times in a row, it must also call ShowCaret five times before the caret is displayed.
        /// </remarks>
        /// <example>
        /// For an example, see Hiding a Caret. https://docs.microsoft.com/windows/desktop/menurc/using-carets
        /// </example>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-hidecaret
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.User32, SetLastError = true)]
        private static extern bool HideCaret(IntPtr hWnd);
    }
}
