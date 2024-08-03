using ReactiveUI;
using System.Linq.Expressions;
using System.Reactive.Linq;

namespace Horizon.Resolvers;

/// <summary>
/// Resolves observables for custom properties.
/// </summary>
public class CustomPropertyResolver : ICreatesObservableForProperty
{
    /// <inheritdoc />
    public int GetAffinityForObject(Type type, string propertyName, bool beforeChanged = false)
    {
        return 1;
    }

    /// <inheritdoc />
    public IObservable<IObservedChange<object, object>> GetNotificationForProperty(object sender, Expression expression, string propertyName, bool beforeChanged = false, bool suppressWarnings = false)
    {
        return Observable.Return(new ObservedChange<object, object>(sender, expression, default!), RxApp.MainThreadScheduler)
            .Concat(Observable.Never<IObservedChange<object, object>>());
    }
}