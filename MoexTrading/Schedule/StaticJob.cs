using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            Schedule<StaticJob>().ToRunNow();
        }
    }
}