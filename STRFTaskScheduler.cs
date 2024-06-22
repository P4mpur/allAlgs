using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Process : IDisposable
{
    public int PID { get; set; }
    public int ArrivalTime { get; set; }
    public int BurstTime { get; set; }
    public int RemainingTime { get; set; }

    public Process(int pid, int arrivalTime, int burstTime)
    {
        PID = pid;
        ArrivalTime = arrivalTime;
        BurstTime = burstTime;
        RemainingTime = burstTime;
    }

    public void Dispose()
    {
        // Cleanup code if needed
    }
}

class SRTFTaskScheduler : TaskScheduler, IDisposable
{
    private readonly List<Process> _processes;
    private int _currentTime = 0;
    private readonly List<Task> _tasks = new List<Task>();
    private readonly Thread _schedulerThread;

    public SRTFTaskScheduler(List<Process> processes)
    {
        _processes = processes.OrderBy(p => p.ArrivalTime).ToList();
        _schedulerThread = new Thread(RunScheduler);
    }

    public void Start()
    {
        _schedulerThread.Start();
    }

    private void RunScheduler()
    {
        Process currentProcess = null;

        while (_processes.Any(p => p.RemainingTime > 0))
        {
            var availableProcesses = _processes.Where(p => p.ArrivalTime <= _currentTime && p.RemainingTime > 0).ToList();

            if (availableProcesses.Count > 0)
            {
                var processToRun = availableProcesses.OrderBy(p => p.RemainingTime).First();

                if (currentProcess == null || processToRun != currentProcess)
                {
                    if (currentProcess != null && currentProcess.RemainingTime > 0 && processToRun != currentProcess)
                    {
                        Console.WriteLine($"Time {_currentTime}: Process {currentProcess.PID} preempted");
                    }
                    currentProcess = processToRun;
                }
                
                Console.WriteLine($"Time {_currentTime}: Process {currentProcess.PID} is running");

                // Simulate process execution for one time unit
                Thread.Sleep(1000); // Simulate 1 second of execution time
                _currentTime++;
                currentProcess.RemainingTime--;

                if (currentProcess.RemainingTime == 0)
                {
                    Console.WriteLine($"Time {_currentTime}: Process {currentProcess.PID} completed");
                    currentProcess = null;
                }
            }
            else
            {
                // If no processes are available to run, increment the time
                Console.WriteLine($"Time {_currentTime}: No process is running");
                Thread.Sleep(1000); // Simulate 1 second of idle time
                _currentTime++;
            }
        }

        Console.WriteLine("All processes have completed execution.");
    }

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        return _tasks;
    }

    protected override void QueueTask(Task task)
    {
        _tasks.Add(task);
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        return TryExecuteTask(task);
    }

    public void Dispose()
    {
        // Cleanup resources
        foreach (var process in _processes)
        {
            process.Dispose();
        }
    }
}
