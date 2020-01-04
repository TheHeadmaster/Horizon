using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Horizon.UI
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private PropertyDefinitionCollection displayCollection;

        public PropertyDefinitionCollection DisplayCollection
        {
            get
            {
                if (this.displayCollection is null)
                {
                    this.displayCollection = new PropertyDefinitionCollection();
                    List<PropertyInfo> properties = this.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(DisplayableAttribute), true)).ToList();
                    foreach (PropertyInfo property in properties)
                    {
                        DisplayableAttribute displayable = property.GetCustomAttribute<DisplayableAttribute>(true);
                        this.displayCollection.Add
                            (
                            new PropertyDefinition
                            {
                                Category = displayable.Category,
                                DisplayName = displayable.DisplayName,
                                Description = displayable.Description,
                                DisplayOrder = displayable.DisplayOrder,
                                IsExpandable = displayable.IsExpandable,
                                TargetProperties = new List<string> { property.Name }
                            });
                    }
                }
                return this.displayCollection;
            }
        }

        [DependsOn(nameof(IsSaved))]
        public Visibility DocumentAsteriskVisibility
        {
            get
            {
                if (this.IsSaved)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public bool IsSaved { get; set; } = true;

        public ObservableObject()
        {
            ErrorListener.RegisterForListening(this);
        }

        private void EvaluateSavableChanges(string propertyName)
        {
            List<PropertyInfo> properties = this.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(MementoAttribute), true)).ToList();
            if (properties.Any(x => x.Name == propertyName)) { this.IsSaved = false; }
        }

        public abstract List<Error> EvaluateErrors();

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            this.EvaluateSavableChanges(propertyName);
        }
    }
}