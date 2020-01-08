﻿using Horizon.Json;
using Horizon.ObjectModel;
using Horizon.Windows;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class IDEWindowViewModel : ReactiveObject
    {
        [Reactive]
        public Project CurrentProject { get; set; }

        public void LoadUserMeta()
        {
            if (App.Metadata.OpenLastProjectOnStartup && !(App.Metadata.LastOpenedProject is null))
            {
                ProjectFile projectFile = JFile.Load<ProjectFile>(App.Metadata.LastOpenedProject.Path, "Project.json");
                this.CurrentProject = projectFile.CreateModel();
                this.CurrentProject.LoadMods();
            }
        }
    }
}