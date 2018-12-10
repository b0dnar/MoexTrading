using FluentScheduler;
using MoexTrading.Plaza.Job;

namespace MoexTrading.Schedule
{
    public class StaticJob : IJob
    {
        public void Execute()
        {
            TotalInfo.Run();
        }
    }

    public class UserJob : IJob
    {
        public void Execute()
        {
            UserInfo.Run();
        }
    }

    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
           // Schedule<StaticJob>().ToRunNow();
            Schedule<UserJob>().ToRunNow();
        }
    }
}