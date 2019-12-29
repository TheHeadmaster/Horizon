using System;
using System.Threading.Tasks;
using Xbehave;
using Xbehave.Sdk;

namespace Horizon.Testing
{
    /// <summary>
    /// Collection of IStepBuilder extensions for Experiment.
    /// </summary>
    public static class ExperimentExtensions
    {
        public static IStepBuilder Cleanup(this string text, Action body) => text.x(body);

        public static IStepBuilder Cleanup(this string text, Action<IStepContext> body) => text.x(body);

        public static IStepBuilder Cleanup(this string text, Func<Task> body) => text.x(body);

        public static IStepBuilder Cleanup(this string text, Func<IStepContext, Task> body) => text.x(body);

        public static IStepBuilder Conclude(this string text, Action body) => text.x(body);

        public static IStepBuilder Conclude(this string text, Action<IStepContext> body) => text.x(body);

        public static IStepBuilder Conclude(this string text, Func<Task> body) => text.x(body);

        public static IStepBuilder Conclude(this string text, Func<IStepContext, Task> body) => text.x(body);

        public static IStepBuilder Hypothesize(this string text, Action body) => text.x(body);

        public static IStepBuilder Hypothesize(this string text, Action<IStepContext> body) => text.x(body);

        public static IStepBuilder Hypothesize(this string text, Func<Task> body) => text.x(body);

        public static IStepBuilder Hypothesize(this string text, Func<IStepContext, Task> body) => text.x(body);

        public static IStepBuilder Observe(this string text, Action body) => text.x(body);

        public static IStepBuilder Observe(this string text, Action<IStepContext> body) => text.x(body);

        public static IStepBuilder Observe(this string text, Func<Task> body) => text.x(body);

        public static IStepBuilder Observe(this string text, Func<IStepContext, Task> body) => text.x(body);

        public static IStepBuilder ObserveConclude(this string text, Action body) => text.x(body);

        public static IStepBuilder ObserveConclude(this string text, Action<IStepContext> body) => text.x(body);

        public static IStepBuilder ObserveConclude(this string text, Func<Task> body) => text.x(body);

        public static IStepBuilder ObserveConclude(this string text, Func<IStepContext, Task> body) => text.x(body);

        public static IStepBuilder Prepare(this string text, Action body) => text.x(body);

        public static IStepBuilder Prepare(this string text, Action<IStepContext> body) => text.x(body);

        public static IStepBuilder Prepare(this string text, Func<Task> body) => text.x(body);

        public static IStepBuilder Prepare(this string text, Func<IStepContext, Task> body) => text.x(body);
    }

    [IgnoreXunitAnalyzersRule1013]
    public class EnvironmentAttribute : BackgroundAttribute { }

    public class ExperimentAttribute : ScenarioAttribute { }

    public sealed class IgnoreXunitAnalyzersRule1013Attribute : System.Attribute { }
}