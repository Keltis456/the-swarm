using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = System.Random;

public class SeededRandom : MonoBehaviour
{
    private void Start()
    {
        NonParallelExample();

        ParallelExample();
    }

    private static void ParallelExample()
    {
        var watch1 = Stopwatch.StartNew();

        const int parallelCount = 100000;
        Task[] tasks =
        {
            new Task(() => Action(new Random(0), parallelCount)),
            new Task(() => Action(new Random(0), parallelCount)),
            new Task(() => Action(new Random(0), parallelCount)),
            new Task(() => Action(new Random(0), parallelCount)),
            new Task(() => Action(new Random(0), parallelCount)),
            new Task(() => Action(new Random(0), parallelCount)),
            new Task(() => Action(new Random(0), parallelCount)),
            new Task(() => Action(new Random(0), parallelCount)),
            new Task(() => Action(new Random(0), parallelCount)),
            new Task(() => Action(new Random(0), parallelCount))
        };
        foreach (var t in tasks)
            t.Start();
        Task.WaitAll(tasks);

        watch1.Stop();
        Debug.Log("Parallel whole : " + watch1.ElapsedMilliseconds);
    }

    private static void NonParallelExample()
    {
        var watch = Stopwatch.StartNew();

        const int nonParallelCount = 1000000;
        Action(new Random(0), nonParallelCount);

        watch.Stop();
        Debug.Log("Non parallel : " + watch.ElapsedMilliseconds);
    }

    private static void Action(Random random, int count)
    {
        var watch1 = Stopwatch.StartNew();
        for (var i = 0; i < count; i++) 
            random.Next(int.MinValue, int.MaxValue);
        watch1.Stop();
        Debug.Log("Parallel task : " + watch1.ElapsedMilliseconds);
    }
}
