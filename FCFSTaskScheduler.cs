using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class FCFSTaskScheduler : TaskScheduler, IDisposable
{
    private BlockingCollection<Task> tasksCollection = new BlockingCollection<Task>();
    private readonly Thread workerThread;
    private bool disposed = false;

    public FCFSTaskScheduler()
    {
        workerThread = new Thread(new ThreadStart(Execute));
        workerThread.Start();
    }

    private void Execute()
    {
        foreach (var task in tasksCollection.GetConsumingEnumerable())
        {
            TryExecuteTask(task);
        }
    }

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        return tasksCollection.ToArray();
    }

    protected override void QueueTask(Task task)
    {
        if (task != null)
        {
            tasksCollection.Add(task);
        }
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        return false;
    }

    public void Dispose()
    {
        if (disposed) return;

        disposed = true;
        tasksCollection.CompleteAdding();
        workerThread.Join();
        tasksCollection.Dispose();

        GC.SuppressFinalize(this);
    }
}
