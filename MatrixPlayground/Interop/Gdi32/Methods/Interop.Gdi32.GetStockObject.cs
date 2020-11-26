using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Gdi32
    {
        /// <summary>
        /// 
        /// </summary>
        public enum StockObjects
        {
            /// <summary>
            /// The white brush
            /// </summary>
            WHITE_BRUSH = 0,

            /// <summary>
            /// The ltgray brush
            /// </summary>
            LTGRAY_BRUSH = 1,

            /// <summary>
            /// The gray brush
            /// </summary>
            GRAY_BRUSH = 2,

            /// <summary>
            /// The dkgray brush
            /// </summary>
            DKGRAY_BRUSH = 3,

            /// <summary>
            /// The black brush
            /// </summary>
            BLACK_BRUSH = 4,

            /// <summary>
            /// The null brush
            /// </summary>
            NULL_BRUSH = 5,

            /// <summary>
            /// The hollow brush
            /// </summary>
            HOLLOW_BRUSH = NULL_BRUSH,

            /// <summary>
            /// The white pen
            /// </summary>
            WHITE_PEN = 6,

            /// <summary>
            /// The black pen
            /// </summary>
            BLACK_PEN = 7,

            /// <summary>
            /// The null pen
            /// </summary>
            NULL_PEN = 8,

            /// <summary>
            /// The oem fixed font
            /// </summary>
            OEM_FIXED_FONT = 10,

            /// <summary>
            /// The ANSI fixed font
            /// </summary>
            ANSI_FIXED_FONT = 11,

            /// <summary>
            /// The ANSI variable font
            /// </summary>
            ANSI_VAR_FONT = 12,

            /// <summary>
            /// The system font
            /// </summary>
            SYSTEM_FONT = 13,

            /// <summary>
            /// The device default font
            /// </summary>
            DEVICE_DEFAULT_FONT = 14,

            /// <summary>
            /// The default palette
            /// </summary>
            DEFAULT_PALETTE = 15,

            /// <summary>
            /// The system fixed font
            /// </summary>
            SYSTEM_FIXED_FONT = 16,

            /// <summary>
            /// The default GUI font
            /// </summary>
            DEFAULT_GUI_FONT = 17,

            /// <summary>
            /// The dc brush
            /// </summary>
            DC_BRUSH = 18,

            /// <summary>
            /// The dc pen
            /// </summary>
            DC_PEN = 19,
        }

        /// <summary>
        /// The GetStockObject function retrieves a handle to one of the stock pens, brushes, fonts, or palettes.
        /// </summary>
        /// <param name="fnObject">The type of stock object. This parameter can be one of the following values.</param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the requested logical object.
        /// If the function fails, the return value is NULL.
        /// </returns>
        /// <remarks>
        /// It is not recommended that you employ this method to obtain the current font used by dialogs and windows. Instead, use the SystemParametersInfo function with the SPI_GETNONCLIENTMETRICS parameter to retrieve the current font. SystemParametersInfo will take into account the current theme and provides font information for captions, menus, and message dialogs.
        /// Use the DKGRAY_BRUSH, GRAY_BRUSH, and LTGRAY_BRUSH stock objects only in windows with the CS_HREDRAW and CS_VREDRAW styles. Using a gray stock brush in any other style of window can lead to misalignment of brush patterns after a window is moved or sized. The origins of stock brushes cannot be adjusted.
        /// The HOLLOW_BRUSH and NULL_BRUSH stock objects are equivalent.
        /// It is not necessary (but it is not harmful) to delete stock objects by calling DeleteObject.
        /// Both DC_BRUSH and DC_PEN can be used interchangeably with other stock objects like BLACK_BRUSH and BLACK_PEN. For information on retrieving the current pen or brush color, see GetDCBrushColor and GetDCPenColor. See Setting the Pen or Brush Color for an example of setting colors. The GetStockObject function with an argument of DC_BRUSH or DC_PEN can be used interchangeably with the SetDCPenColor and SetDCBrushColor functions.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getstockobject
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Gdi32, EntryPoint = "GetStockObject")]
        private static extern IntPtr GetStockObject(StockObjects fnObject);
    }
}
