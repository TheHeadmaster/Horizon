using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.UI
{
    public static class ErrorListener
    {
        public delegate void ListenerUpdateDelegate(ListenerUpdateEventArgs args);

        public static event ListenerUpdateDelegate ListenerUpdated;

        private static List<ObservableObject> RegisteredObjects { get; } = new List<ObservableObject>();

        public static List<Error> Errors { get; } = new List<Error>();

        private static void ObjectToRegister_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ObservableObject registeredObject = (ObservableObject)sender;
            Errors.RemoveAll(x => x.Source == registeredObject);
            Errors.AddRange(registeredObject.EvaluateErrors());
            ListenerUpdated?.Invoke(new ListenerUpdateEventArgs());
        }

        public static void RegisterForListening(ObservableObject objectToRegister)
        {
            objectToRegister.PropertyChanged += ObjectToRegister_PropertyChanged;
            RegisteredObjects.Add(objectToRegister);
        }

        public static void Update()
        {
            Errors.Clear();
            foreach (ObservableObject obj in RegisteredObjects)
            {
                Errors.AddRange(obj.EvaluateErrors());
                ListenerUpdated?.Invoke(new ListenerUpdateEventArgs());
            }
        }
    }
}