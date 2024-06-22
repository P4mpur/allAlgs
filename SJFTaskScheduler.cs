using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class SJFScheduledTask
{
    public Task Task { get; }
    public int EstimatedExecutionTime { get; }

    public SJFScheduledTask(Task task, int estimatedExecutionTime)
    {
        Task = task;
        EstimatedExecutionTime = estimatedExecutionTime;
    }
}

public class SJFTaskScheduler : TaskScheduler, IDisposable
{
    private readonly BlockingCollection<SJFScheduledTask> tasksCollection = new BlockingCollection<SJFScheduledTask>();
    private readonly Thread workerThread;
    private bool disposed = false;

    public SJFTaskScheduler()
    {
        workerThread = new Thread(Execute);
        workerThread.Start();
    }

    private void Execute()
    {
        while (!disposed)
        {
            if (tasksCollection.Count > 0)
            {
                var sortedTasks = tasksCollection.OrderBy(t => t.EstimatedExecutionTime).ToList();
                foreach (var scheduledTask in sortedTasks)
                {
                    if (tasksCollection.TryTake(out _))
                    {
                        TryExecuteTask(scheduledTask.Task);
                    }
                }
            }
            else
            {
                Thread.Sleep(10); // Avoid busy waiting
            }
        }
    }

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        return tasksCollection.Select(st => st.Task).ToArray();
    }

    protected override void QueueTask(Task task)
    {
        // This method is left empty to comply with TaskScheduler's abstract method signature.
    }

    public void QueueTask(Task task, int estimatedExecutionTime)
    {
        if (task != null)
        {
            tasksCollection.Add(new SJFScheduledTask(task, estimatedExecutionTime));
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
