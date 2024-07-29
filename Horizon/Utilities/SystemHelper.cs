using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace Horizon.Utilities;

/// <summary>
/// A helper used for low-level windows system methods such as getting DPI and mouse position.
/// </summary>
internal static class SystemHelper
{
    /// <summary>
    /// Gets the current system DPI.
    /// </summary>
    /// <returns>The DPI as an <see cref="int" />.</returns>
    public static int GetCurrentDPI() => ((int?)typeof(SystemParameters).GetProperty("Dpi", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null, null)) ?? 0;

    /// <summary>
    /// Gets the current system DPI scale factor.
    /// </summary>
    /// <returns>The scale factor as a <see cref="double" />.</returns>
    public static double GetCurrentDPIScaleFactor() => (double)GetCurrentDPI() / 96;

    /// <summary>
    /// Gets the mouse position as defined in Windows Forms.
    /// </summary>
    /// <returns>A <see cref="Point" /> representing the mouse position.</returns>
    public static Point GetMousePositionWindowsForms() => new(Control.MousePosition.X, Control.MousePosition.Y);
}