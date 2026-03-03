using System.Diagnostics;

static public class KTime
{
    private static Stopwatch stopwatch = new Stopwatch();
 
    public static double DeltaTime { get; private set; }
    public static double ElapsedTime_Main { get; private set; }
    public static double ElapsedTime_UI { get; private set; }

    static KTime()
    {// starts time when the class is first accessed
        stopwatch.Start();
    }

    public static void Update()
    {
        DeltaTime = stopwatch.Elapsed.TotalSeconds;
        ElapsedTime_Main += DeltaTime;
        ElapsedTime_UI += DeltaTime;
        stopwatch.Restart();
    }

    //consume instead of reset, to avoid time waste
    public static void ConsumeElapsedTime_Main(double targetFPS_Main)
    {
        ElapsedTime_Main -= targetFPS_Main;
    }
    public static void ConsumeElapsedTime_UI(double targetFPS_UI)
    {
        ElapsedTime_UI -= targetFPS_UI;
    }
}
