using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class RoundRobinTaskScheduler : TaskScheduler, IDisposable
{
    private readonly BlockingCollection<Task> tasksCollection = new BlockingCollection<Task>();
    private readonly List<Thread> workerThreads = new List<Thread>();
    private readonly int timeSlice; // Time slice in milliseconds
    private bool disposed = false;

    public RoundRobinTaskScheduler(int concurrencyLevel, int timeSlice)
    {
        this.timeSlice = timeSlice;

        for (int i = 0; i < concurrencyLevel; i++)
        {
            Thread workerThread = new Thread(Execute);
            workerThreads.Add(workerThread);
            workerThread.Start();
        }
    }

    private void Execute()
    {
        while (!disposed)
        {
            var tasks = tasksCollection.ToArray();
            foreach (var task in tasks)
            {
                if (tasksCollection.TryTake(out var currentTask))
                {
                    TryExecuteTask(currentTask);
                    Thread.Sleep(timeSlice);
                }
            }
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

        foreach (var thread in workerThreads)
        {
            thread.Join();
        }

        tasksCollection.Dispose();

        GC.SuppressFinalize(this);
    }
}
