using Horizon.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class ErrorListViewModel
    {
        public ObservableCollection<Error> Errors { get; set; } = new ObservableCollection<Error>();

        public ErrorListViewModel()
        {
            this.Update();
        }

        private void Update()
        {
        }
    }
}