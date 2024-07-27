using System.Windows;

namespace Horizon.Utilities;

/// <summary>
/// A custom rect struct used only for native utilities.
/// </summary>
[Serializable]
internal struct RECT
{
    public int Bottom;

    public int Left;

    public int Right;

    public int Top;

    public RECT(int left, int top, int right, int bottom)
    {
        this.Left = left;
        this.Top = top;
        this.Right = right;
        this.Bottom = bottom;
    }

    public RECT(Rect rect)
    {
        this.Left = (int)rect.Left;
        this.Top = (int)rect.Top;
        this.Right = (int)rect.Right;
        this.Bottom = (int)rect.Bottom;
    }

    public int Height
    {
        readonly get => this.Bottom - this.Top;
        set => this.Bottom = this.Top + value;
    }

    public readonly Point Position => new(this.Left, this.Top);

    public readonly Size Size => new(this.Width, this.Height);

    public int Width
    {
        readonly get => this.Right - this.Left;
        set => this.Right = this.Left + value;
    }

    public void Offset(int dx, int dy)
    {
        this.Left += dx;
        this.Right += dx;
        this.Top += dy;
        this.Bottom += dy;
    }

    public readonly Int32Rect ToInt32Rect() => new(this.Left, this.Top, this.Width, this.Height);
}