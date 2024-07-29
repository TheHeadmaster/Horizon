using System.Runtime.InteropServices;

namespace Horizon.Utilities;

/// <summary>
/// Magic native methods used for windows and popups.
/// </summary>
internal static partial class NativeUtilities
{
    internal static uint TPM_LEFTALIGN;

    internal static uint TPM_RETURNCMD;

    static NativeUtilities()
    {
        TPM_LEFTALIGN = 0;
        TPM_RETURNCMD = 256;
    }

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

    [LibraryImport("user32.dll", SetLastError = true)]
    internal static partial IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);

    [LibraryImport("user32.dll")]
    internal static partial IntPtr PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [LibraryImport("user32.dll")]
    internal static partial int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);
}