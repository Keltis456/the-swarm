using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = System.Random;

public class SeededRandom : MonoBehaviour
{
    //private int[] noiseValues;

    private void Start()
    {
        Random random = new Random(0);
        
        //noiseValues = new int[5000000];
        var watch = Stopwatch.StartNew();

        Action(random, 1000000);
        
        watch.Stop();
        Debug.Log("Non parallel : " + watch.ElapsedMilliseconds);
        
        //noiseValues = new int[1000000];
        
        var watch1 = Stopwatch.StartNew();

        var parallelCount = 100000;
        Task[] tasks = {
            new Task(() => Action(random, parallelCount)),
            new Task(() => Action(random, parallelCount)),
            new Task(() => Action(random, parallelCount)),
            new Task(() => Action(random, parallelCount)),
            new Task(() => Action(random, parallelCount)),
            new Task(() => Action(random, parallelCount)),
            new Task(() => Action(random, parallelCount)),
            new Task(() => Action(random, parallelCount)),
            new Task(() => Action(random, parallelCount)),
            new Task(() => Action(random, parallelCount))
        };
        foreach (var t in tasks)
            t.Start();
        Task.WaitAll(tasks);
        
        watch1.Stop();
        Debug.Log("Parallel whole : " + watch1.ElapsedMilliseconds);
    }

    private static void Action(Random random, int count)
    {
        var watch1 = Stopwatch.StartNew();
        //Thread.Sleep(1000);
        for (var i = 0; i < count; i++) 
            random.Next(int.MinValue, int.MaxValue);
        watch1.Stop();
        Debug.Log("Parallel task : " + watch1.ElapsedMilliseconds);
    }
}
