using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Commands
{
    /// <summary>
    /// Arguments for routed events.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class EventArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public EventArgs(T value)
        {
            this.Value = value;
        }
    }
}