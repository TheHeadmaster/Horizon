using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Commands
{
    public class EventArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public EventArgs(T value)
        {
            this.Value = value;
        }
    }
}