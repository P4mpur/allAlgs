using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public class CustomTaskSchedulerConcurrent : TaskScheduler, IDisposable
{
    private BlockingCollection<Task> tasksCollection = new BlockingCollection<Task>();
    private readonly Thread[] workerThreads;
    private bool disposed = false;

    public CustomTaskSchedulerConcurrent(int concurrencyLevel)
    {
        workerThreads = new Thread[concurrencyLevel];
        for (int i = 0; i < concurrencyLevel; i++)
        {
            workerThreads[i] = new Thread(Execute);
            workerThreads[i].Start();
        }
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

        tasksCollection.CompleteAdding();
        foreach (var thread in workerThreads)
        {
            thread.Join();
        }
        tasksCollection.Dispose();
        disposed = true;

        GC.SuppressFinalize(this);
    }
}

//test case
// public class Program
// {
//     public static void Main(string[] args)
//     {
//         // Create a custom task scheduler with a specified concurrency level
//         using (CustomTaskSchedulerConcurrent customTaskScheduler = new CustomTaskSchedulerConcurrent(3))
//         {
//             // Create a task factory with the custom scheduler
//             TaskFactory factory = new TaskFactory(customTaskScheduler);

//             // Define a cancellation token source
//             CancellationTokenSource cts = new CancellationTokenSource();

//             // Create a few tasks
//             Task task1 = factory.StartNew(() =>
//             {
//                 Console.WriteLine("Task 1 is starting.");
//                 Thread.Sleep(1000); // Simulate work
//                 Console.WriteLine("Task 1 is completed.");
//             }, cts.Token);

//             Task task2 = factory.StartNew(() =>
//             {
//                 Console.WriteLine("Task 2 is starting.");
//                 Thread.Sleep(2000); // Simulate work
//                 Console.WriteLine("Task 2 is completed.");
//             }, cts.Token);

//             Task task3 = factory.StartNew(() =>
//             {
//                 Console.WriteLine("Task 3 is starting.");
//                 Thread.Sleep(500); // Simulate work
//                 Console.WriteLine("Task 3 is completed.");
//             }, cts.Token);

//             Task task4 = factory.StartNew(() =>
//             {
//                 Console.WriteLine("Task 4 is starting.");
//                 Thread.Sleep(1500); // Simulate work
//                 Console.WriteLine("Task 4 is completed.");
//             }, cts.Token);

//             // Wait for all tasks to complete
//             Task.WaitAll(task1, task2, task3, task4);

//             Console.WriteLine("All tasks have completed.");
//         }
//     }
// }
