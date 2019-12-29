using System;

namespace Horizon.Testing
{
    public static class Experiment
    {
        /// <summary>
        /// Asserts that the experiment should be expected not to throw. Doesn't actually do
        /// anything, since if the test throws it would fail, but exists for the purpose of stating intent.
        /// </summary>
        public static Action ShouldNotThrow => () => { };
    }
}