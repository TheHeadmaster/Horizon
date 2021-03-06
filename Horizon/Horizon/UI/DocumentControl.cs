﻿using Horizon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.UI
{
    public abstract class DocumentControl : ObservableObject
    {
        public abstract string Header { get; }

        public abstract DocumentControlViewModel CreateViewModel();
    }
}