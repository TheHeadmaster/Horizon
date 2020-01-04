using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.UI
{
    public class Error
    {
        public string Description { get; set; }

        public string ErrorCode { get; set; }

        public ErrorType ErrorType { get; set; }

        public string Project { get; set; }

        public ObservableObject Source { get; private set; }

        public Error(ObservableObject source)
        {
            this.Source = source;
        }
    }
}