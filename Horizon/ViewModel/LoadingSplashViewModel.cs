using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Serilog;

namespace Horizon.ViewModel;

public sealed class LoadingSplashViewModel : ReactiveObject
{
    public LoadingSplashViewModel()
    {
        this.WhenAnyValue(x => x.CurrentTask, x => x.NumberOfTasks,
            (current, total) => (double)current / total)
            .ToPropertyEx(this, x => x.ProgressPercentage);
    }

    [Reactive]
    public int CurrentTask { get; set; }

    [Reactive]
    public int NumberOfTasks { get; set; } = 1;

    [Reactive]
    public string? Status { get; set; }

    [ObservableAsProperty]
    public double ProgressPercentage { get; }

    public async Task StartRunningInBackground(Queue<(string label, Action action)> tasks)
    {
        DateTime startTime = DateTime.UtcNow;

        while (tasks.TryDequeue(out (string label, Action action) task))
        {
            this.CurrentTask++;
            this.Status = task.label;
            Log.Information("{Label}", task.label);
            await Task.Run(() => task.action());
        }

        TimeSpan runTime = DateTime.UtcNow - startTime;

        TimeSpan remainingMinimumWait = TimeSpan.FromSeconds(5) - runTime;

        if (remainingMinimumWait > TimeSpan.Zero)
        {
            await Task.Delay(remainingMinimumWait);
        }
    }
}