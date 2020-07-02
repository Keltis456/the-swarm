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
        Debug.Log(NonParallelExample());
        Debug.Log(ParallelExample());
    }

    private static int ParallelExample()
    {
        const int parallelCount = 3;
        var result = 0;
        Task[] tasks =
        {
            new Task(() => Action(ref result,new Random(0), parallelCount)),
            new Task(() => Action(ref result,new Random(0), parallelCount)),
            new Task(() => Action(ref result,new Random(0), parallelCount)),
            new Task(() => Action(ref result,new Random(0), parallelCount)),
            new Task(() => Action(ref result,new Random(0), parallelCount)),
            new Task(() => Action(ref result,new Random(0), parallelCount)),
            new Task(() => Action(ref result,new Random(0), parallelCount)),
            new Task(() => Action(ref result,new Random(0), parallelCount)),
            new Task(() => Action(ref result,new Random(0), parallelCount)),
            new Task(() => Action(ref result,new Random(0), parallelCount))
        };
        foreach (var t in tasks)
            t.Start();
        Task.WaitAll(tasks);
        return result;
    }

    private static int NonParallelExample()
    {
        const int nonParallelCount = 30;
        var result = 0;
        Action(ref result, new Random(0), nonParallelCount);
        return result;
    }

    private static void Action(ref int sum, Random random, int count)
    {
        for (var i = 0; i < count; i++) 
            sum += random.Next(0, 2);
    }
}
