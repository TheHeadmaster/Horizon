﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Horizon.Utilities
{
    internal static class NativeUtilities
    {
        internal static uint TPM_LEFTALIGN;

        internal static uint TPM_RETURNCMD;

        static NativeUtilities()
        {
            TPM_LEFTALIGN = 0;
            TPM_RETURNCMD = 256;
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        internal static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        internal static extern IntPtr PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        internal static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);
    }

    [Serializable]
    internal struct RECT
    {
        public int Bottom;

        public int Left;

        public int Right;

        public int Top;

        public int Height
        {
            get => this.Bottom - this.Top;
            set => this.Bottom = this.Top + value;
        }

        public Point Position => new Point((double)this.Left, (double)this.Top);

        public Size Size => new Size((double)this.Width, (double)this.Height);

        public int Width
        {
            get => this.Right - this.Left;
            set => this.Right = this.Left + value;
        }

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

        public void Offset(int dx, int dy)
        {
            this.Left += dx;
            this.Right += dx;
            this.Top += dy;
            this.Bottom += dy;
        }

        public Int32Rect ToInt32Rect() => new Int32Rect(this.Left, this.Top, this.Width, this.Height);
    }
}