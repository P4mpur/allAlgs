public class Program
{
    // Task scheduler koji koristi obican queue
    // public static void Main(string[] args)
    // {
    //     // Create a custom task scheduler
    //     using (CustomTaskScheduler customTaskScheduler = new CustomTaskScheduler())
    //     {
    //         // Create a task factory with the custom scheduler
    //         TaskFactory factory = new TaskFactory(customTaskScheduler);

    //         // Define a cancellation token source
    //         CancellationTokenSource cts = new CancellationTokenSource();

    //         // Create a few tasks
    //         Task task1 = factory.StartNew(() => {
    //             Console.WriteLine("Task 1 is starting.");
    //             Thread.Sleep(2000); // Simulacija rada
    //             Console.WriteLine("Task 1 is completed.");
    //         }, cts.Token);

    //         Task task2 = factory.StartNew(() => {
    //             Console.WriteLine("Task 2 is starting.");
    //             Thread.Sleep(1000); // Simulacija rada
    //             Console.WriteLine("Task 2 is completed.");
    //         }, cts.Token);

    //         Task task3 = factory.StartNew(() => {
    //             Console.WriteLine("Task 3 is starting.");
    //             Thread.Sleep(500); // Simulacija rada
    //             Console.WriteLine("Task 3 is completed.");
    //         }, cts.Token);

    //         // Wait for all tasks to complete
    //         Task.WaitAll(task1, task2, task3);

    //         Console.WriteLine("All tasks have completed.");
    //     }
    // }

    // task Scheduler koristeci konkurentnost
    // public static void Main(string[] args)
    // {
    //     // Create a custom task scheduler with a specified concurrency level
    //     using (CustomTaskSchedulerConcurrent customTaskScheduler = new CustomTaskSchedulerConcurrent(3))
    //     {
    //         // Create a task factory with the custom scheduler
    //         TaskFactory factory = new TaskFactory(customTaskScheduler);

    //         // Define a cancellation token source
    //         CancellationTokenSource cts = new CancellationTokenSource();

    //         // Create a few tasks
    //         Task task1 = factory.StartNew(() =>
    //         {
    //             Console.WriteLine("Task 1 is starting.");
    //             Thread.Sleep(1000); // Simulacija rada
    //             Console.WriteLine("Task 1 is completed.");
    //         }, cts.Token);

    //         Task task2 = factory.StartNew(() =>
    //         {
    //             Console.WriteLine("Task 2 is starting.");
    //             Thread.Sleep(2000); // Simulacija rada
    //             Console.WriteLine("Task 2 is completed.");
    //         }, cts.Token);

    //         Task task3 = factory.StartNew(() =>
    //         {
    //             Console.WriteLine("Task 3 is starting.");
    //             Thread.Sleep(500); // Simulacija rada
    //             Console.WriteLine("Task 3 is completed.");
    //         }, cts.Token);

    //         Task task4 = factory.StartNew(() =>
    //         {
    //             Console.WriteLine("Task 4 is starting.");
    //             Thread.Sleep(1500); // Simulacija rada
    //             Console.WriteLine("Task 4 is completed.");
    //         }, cts.Token);

    //         // Wait for all tasks to complete
    //         Task.WaitAll(task1, task2, task3, task4);

    //         Console.WriteLine("All tasks have completed.");
    //     }
    // }

    // Round Robin
    // public static void Main(string[] args)
    // {

    //     using (RoundRobinTaskScheduler roundRobinScheduler = new RoundRobinTaskScheduler(3, 100))
    //     {
    //         // kreiranje task-facotyr sa CustomScheduler-om
    //         TaskFactory factory = new TaskFactory(roundRobinScheduler);

    //         // define cancellationtokensource
    //         CancellationTokenSource cts = new CancellationTokenSource();

    //         Task task1 = factory.StartNew(() =>
    //         {
    //             Console.WriteLine("Task 1 is starting.");
    //             for (int i = 0; i < 5; i++)
    //             {
    //                 Thread.Sleep(200); // Simulacija rada
    //                 Console.WriteLine($"Task 1 is working {i + 1}");
    //             }
    //             Console.WriteLine("Task 1 is completed.");
    //         }, cts.Token);

    //         Task task2 = factory.StartNew(() =>
    //         {
    //             Console.WriteLine("Task 2 is starting.");
    //             for (int i = 0; i < 5; i++)
    //             {
    //                 Thread.Sleep(200); // Simulacija rada
    //                 Console.WriteLine($"Task 2 is working {i + 1}");
    //             }
    //             Console.WriteLine("Task 2 is completed.");
    //         }, cts.Token);

    //         Task task3 = factory.StartNew(() =>
    //         {
    //             Console.WriteLine("Task 3 is starting.");
    //             for (int i = 0; i < 5; i++)
    //             {
    //                 Thread.Sleep(200); // Simulacija rada
    //                 Console.WriteLine($"Task 3 is working {i + 1}");
    //             }
    //             Console.WriteLine("Task 3 is completed.");
    //         }, cts.Token);

    //         // cekas na sve taskove
    //         Task.WaitAll(task1, task2, task3);

    //         Console.WriteLine("All tasks have completed.");
    //     }
    // }


    // FCFS

    // public static void Main(string[] args)
    // {
    //     // Create a FCFS task scheduler
    //     using (FCFSTaskScheduler fcfsScheduler = new FCFSTaskScheduler())
    //     {
    //         // Create a task factory with the custom scheduler
    //         TaskFactory factory = new TaskFactory(fcfsScheduler);

    //         // Define a cancellation token source
    //         CancellationTokenSource cts = new CancellationTokenSource();

    //         // Create a few tasks
    //         Task task1 = factory.StartNew(() =>
    //         {
    //             Console.WriteLine("Task 1 is starting.");
    //             Thread.Sleep(3000); // Simulacija rada
    //             Console.WriteLine("Task 1 is completed.");
    //         }, cts.Token);

    //         Task task2 = factory.StartNew(() =>
    //         {
    //             Console.WriteLine("Task 2 is starting.");
    //             Thread.Sleep(2000); // Simulacija rada
    //             Console.WriteLine("Task 2 is completed.");
    //         }, cts.Token);

    //         Task task3 = factory.StartNew(() =>
    //         {
    //             Console.WriteLine("Task 3 is starting.");
    //             Thread.Sleep(500); // Simulacija rada
    //             Console.WriteLine("Task 3 is completed.");
    //         }, cts.Token);

    //         // Wait for all tasks to complete
    //         Task.WaitAll(task1, task2, task3);

    //         Console.WriteLine("All tasks have completed.");
    //     }
    // }

    // SFJ task scheduler
    // public static void Main(string[] args)
    // {
    //     using (SJFTaskScheduler sjfScheduler = new SJFTaskScheduler())
    //     {
    //         TaskFactory factory = new TaskFactory(sjfScheduler);
    //         CancellationTokenSource cts = new CancellationTokenSource();

    //         // Create and queue tasks with estimated execution times
    //         Task task1 = new Task(() =>
    //         {
    //             Console.WriteLine("Task 1 is starting.");
    //             Thread.Sleep(1000); // Simulacija rada
    //             Console.WriteLine("Task 1 is completed.");
    //         }, cts.Token);

    //         Task task2 = new Task(() =>
    //         {
    //             Console.WriteLine("Task 2 is starting.");
    //             Thread.Sleep(2000); // Simulacija rada
    //             Console.WriteLine("Task 2 is completed.");
    //         }, cts.Token);

    //         Task task3 = new Task(() =>
    //         {
    //             Console.WriteLine("Task 3 is starting.");
    //             Thread.Sleep(500); // Simulacija rada
    //             Console.WriteLine("Task 3 is completed.");
    //         }, cts.Token);

    //         Task task4 = new Task(() =>
    //         {
    //             Console.WriteLine("Task 4 is starting.");
    //             Thread.Sleep(1200); // Simulacija rada
    //             Console.WriteLine("Task 4 is completed.");
    //         }, cts.Token);

    //         sjfScheduler.QueueTask(task1, 1000);
    //         sjfScheduler.QueueTask(task2, 2000);
    //         sjfScheduler.QueueTask(task3, 500);
    //         sjfScheduler.QueueTask(task4,1200);

    //         task1.Start(sjfScheduler);
    //         task2.Start(sjfScheduler);
    //         task3.Start(sjfScheduler);
    //         task4.Start(sjfScheduler);

    //         Task.WaitAll(task1, task2, task3,task4);

    //         Console.WriteLine("All tasks have completed.");
    //     }
    // }

    static void Main()
    {
        var processes = new List<Process>
        {
            new Process(1, 0, 7),
            new Process(2, 1, 5),
            new Process(3, 4, 1)
        };

        using (var scheduler = new SRTFTaskScheduler(processes))
        {
            scheduler.Start();
        }
    }
    // }
}
