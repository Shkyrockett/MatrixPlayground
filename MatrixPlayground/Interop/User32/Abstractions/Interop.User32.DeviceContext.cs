using System;
using System.Runtime.CompilerServices;

internal static partial class Interop
{
    internal static partial class User32
    {
        public static class DeviceContext
        {
            /// <summary>
            /// Gets the Device Context.
            /// </summary>
            /// <param name="handle">The handle.</param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static IntPtr GetDC(IntPtr handle) => User32.GetDC(handle);

            /// <summary>
            /// Releases the Device Context.
            /// </summary>
            /// <param name="handle">The handle.</param>
            /// <param name="dcHandle">The dc handle.</param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool ReleaseDC(IntPtr handle, IntPtr dcHandle) => User32.ReleaseDC(handle, dcHandle);
        }
    }
}
