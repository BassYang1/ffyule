// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.TaskData
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using Lottery.DAL;
using System;
using System.Threading;
using System.Timers;

namespace Lottery.Collect
{
  public class TaskData
  {
    private static System.Timers.Timer timerOne = new System.Timers.Timer(600000.0);
    private static object obj_locOne = new object();

    public static void Run()
    {
      TaskData.timerOne.Elapsed += new ElapsedEventHandler(TaskData.timerOne_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TaskData.ThOne_Fun));
    }

    private static void ThOne_Fun(object stateInfo)
    {
      TaskData.timerOne_Elapsed((object) null, (ElapsedEventArgs) null);
      TaskData.timerOne.Start();
    }

    private static void timerOne_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TaskData.obj_locOne)
        {
          int hour = DateTime.Now.Hour;
          if (hour < 5 || hour > 6)
            return;
          new TiskDAL().TiskOper();
          new LogSysDAL().Save("系统自动", "自动执行了定时任务！");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception:" + ex.Message);
      }
    }
  }
}
