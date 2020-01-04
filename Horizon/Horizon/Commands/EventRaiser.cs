﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Commands
{
    public static class EventRaiser
    {
        public static void Raise(this EventHandler handler, object sender) => handler?.Invoke(sender, EventArgs.Empty);

        public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, T value) => handler?.Invoke(sender, new EventArgs<T>(value));

        public static void Raise<T>(this EventHandler<T> handler, object sender, T value) where T : EventArgs => handler?.Invoke(sender, value);

        public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, EventArgs<T> value) => handler?.Invoke(sender, value);
    }
}