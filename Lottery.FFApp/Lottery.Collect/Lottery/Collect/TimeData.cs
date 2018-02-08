// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.TimeData
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using System;
using System.Threading;
using System.Timers;

namespace Lottery.Collect
{
  public class TimeData
  {
    private static System.Timers.Timer timerCqSsc = new System.Timers.Timer(5000.0);
    private static object obj_locCqSsc = new object();
    private static System.Timers.Timer timerHg90 = new System.Timers.Timer(5000.0);
    private static object obj_locHg90 = new object();
    private static System.Timers.Timer timerXjp120 = new System.Timers.Timer(5000.0);
    private static object obj_locXjp120 = new object();
    private static System.Timers.Timer timerDj90 = new System.Timers.Timer(5000.0);
    private static object obj_locDj90 = new object();
    private static System.Timers.Timer timerOne = new System.Timers.Timer(1000.0);
    private static object obj_locOne = new object();
    private static System.Timers.Timer timerNy30 = new System.Timers.Timer(1000.0);
    private static object obj_locNy30 = new object();
    private static System.Timers.Timer timerQQ60 = new System.Timers.Timer(5000.0);
    private static object obj_locQQ60 = new object();
    private static System.Timers.Timer timerXdl90 = new System.Timers.Timer(5000.0);
    private static object obj_locXdl90 = new object();
    private static System.Timers.Timer timerXjp30 = new System.Timers.Timer(1000.0);
    private static object obj_locXjp30 = new object();
    private static System.Timers.Timer timerTw60 = new System.Timers.Timer(1000.0);
    private static object obj_locTw60 = new object();
    private static System.Timers.Timer timerSe60 = new System.Timers.Timer(1000.0);
    private static object obj_locSe60 = new object();
    private static System.Timers.Timer timerOne11x5 = new System.Timers.Timer(1000.0);
    private static object obj_locOne11x5 = new object();
    private static System.Timers.Timer timerHg11x5 = new System.Timers.Timer(1000.0);
    private static object obj_locHg11x5 = new object();
    private static System.Timers.Timer timer3d = new System.Timers.Timer(60000.0);
    private static object obj_loc3d = new object();
    private static System.Timers.Timer timerP3 = new System.Timers.Timer(60000.0);
    private static object obj_locP3 = new object();
    private static System.Timers.Timer timerOne3d = new System.Timers.Timer(1000.0);
    private static object obj_locOne3d = new object();
    private static System.Timers.Timer timerHg3d = new System.Timers.Timer(1000.0);
    private static object obj_locHg3d = new object();
    private static System.Timers.Timer timerSe3d = new System.Timers.Timer(1000.0);
    private static object obj_locSe3d = new object();
    private static System.Timers.Timer timerOnePk10 = new System.Timers.Timer(1000.0);
    private static object obj_locOnePk10 = new object();
    private static System.Timers.Timer timerYg60Pk10 = new System.Timers.Timer(1000.0);
    private static object obj_locYg60Pk10 = new object();
    private static System.Timers.Timer timerYg120Pk10 = new System.Timers.Timer(2000.0);
    private static object obj_locYg120Pk10 = new object();

    public static void Run()
    {
      TimeData.timerCqSsc.Elapsed += new ElapsedEventHandler(TimeData.timerCqSsc_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThCqSsc_Fun));
      TimeData.timerHg90.Elapsed += new ElapsedEventHandler(TimeData.timerHg90_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThHg90_Fun));
      TimeData.timerXjp120.Elapsed += new ElapsedEventHandler(TimeData.timerXjp120_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThXjp120_Fun));
      TimeData.timerDj90.Elapsed += new ElapsedEventHandler(TimeData.timerDj90_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThDj90_Fun));
      TimeData.timerNy30.Elapsed += new ElapsedEventHandler(TimeData.timerNy30_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThNy30_Fun));
      TimeData.timerQQ60.Elapsed += new ElapsedEventHandler(TimeData.timerQQ60_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThQQ60_Fun));
      TimeData.timerXjp30.Elapsed += new ElapsedEventHandler(TimeData.timerXjp30_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThXjp30_Fun));
      TimeData.timerTw60.Elapsed += new ElapsedEventHandler(TimeData.timerTw60_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThTw60_Fun));
      TimeData.timerSe60.Elapsed += new ElapsedEventHandler(TimeData.timerSe60_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThSe60_Fun));
      TimeData.timerOne11x5.Elapsed += new ElapsedEventHandler(TimeData.timerOne11x5_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThOne11x5_Fun));
      TimeData.timerHg11x5.Elapsed += new ElapsedEventHandler(TimeData.timerHg11x5_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThHg11x5_Fun));
      TimeData.timer3d.Elapsed += new ElapsedEventHandler(TimeData.timer3d_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.Th3d_Fun));
      TimeData.timerP3.Elapsed += new ElapsedEventHandler(TimeData.timerP3_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThP3_Fun));
      TimeData.timerHg3d.Elapsed += new ElapsedEventHandler(TimeData.timerHg3d_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThHg3d_Fun));
      TimeData.timerSe3d.Elapsed += new ElapsedEventHandler(TimeData.timerSe3d_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThSe3d_Fun));
      TimeData.timerOnePk10.Elapsed += new ElapsedEventHandler(TimeData.timerOnePk10_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThOnePk10_Fun));
      TimeData.timerYg60Pk10.Elapsed += new ElapsedEventHandler(TimeData.timerYg60Pk10_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThYg60Pk10_Fun));
      TimeData.timerYg120Pk10.Elapsed += new ElapsedEventHandler(TimeData.timerYg120Pk10_Elapsed);
      ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThYg120Pk10_Fun));
    }

    private static void ThCqSsc_Fun(object stateInfo)
    {
      TimeData.timerCqSsc_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerCqSsc.Start();
    }

    private static void timerCqSsc_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locCqSsc)
          DefaultToOther.CqSsc();
      }
      catch (Exception ex)
      {
        Console.WriteLine("SSC Exception:" + ex.Message);
      }
    }

    private static void ThHg90_Fun(object stateInfo)
    {
      TimeData.timerHg90_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerHg90.Start();
    }

    private static void timerHg90_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locHg90)
        {
          int second = DateTime.Now.Second;
          YouleToOther.DataToOther(1010);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("SSC Exception:" + ex.Message);
      }
    }

    private static void ThXjp120_Fun(object stateInfo)
    {
      TimeData.timerXjp120_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerXjp120.Start();
    }

    private static void timerXjp120_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locXjp120)
        {
          int second = DateTime.Now.Second;
          YouleToOther.DataToOther(1012);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("SSC Exception:" + ex.Message);
      }
    }

    private static void ThDj90_Fun(object stateInfo)
    {
      TimeData.timerDj90_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerDj90.Start();
    }

    private static void timerDj90_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locDj90)
        {
          int second = DateTime.Now.Second;
          YouleToOther.DataToOther(1016);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("SSC Exception:" + ex.Message);
      }
    }

    private static void ThOne_Fun(object stateInfo)
    {
      TimeData.timerOne_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerOne.Start();
    }

    private static void timerOne_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locOne)
        {
          int second = DateTime.Now.Second;
          YouleToOther.DataToOther(1009);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("SSC Exception:" + ex.Message);
      }
    }

    private static void ThNy30_Fun(object stateInfo)
    {
      TimeData.timerNy30_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerNy30.Start();
    }

    private static void timerNy30_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locNy30)
        {
          int second = DateTime.Now.Second;
          YouleToOther.DataToOther(1004);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("SSC Exception:" + ex.Message);
      }
    }

    private static void ThQQ60_Fun(object stateInfo)
    {
      TimeData.timerQQ60_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerQQ60.Start();
    }

    private static void timerQQ60_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locQQ60)
        {
          DateTime now = DateTime.Now;
          int hour = now.Hour;
          int minute = now.Minute;
          int second = now.Second;
          int num = minute % 2;
          if (second <= 0 || second > 15)
            return;
          QqSscData.QqSsc();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("SSC Exception:" + ex.Message);
      }
    }

    private static void ThXdl90_Fun(object stateInfo)
    {
      TimeData.timerXdl90_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerXdl90.Start();
    }

    private static void timerXdl90_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locXdl90)
          YouleToOther.DataToOther(1011);
      }
      catch (Exception ex)
      {
        Console.WriteLine("SSC Exception:" + ex.Message);
      }
    }

    private static void ThXjp30_Fun(object stateInfo)
    {
      TimeData.timerXjp30_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerXjp30.Start();
    }

    private static void timerXjp30_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locXjp30)
          YouleToOther.DataToOther(1018);
      }
      catch (Exception ex)
      {
        Console.WriteLine("SSC Exception:" + ex.Message);
      }
    }

    private static void ThTw60_Fun(object stateInfo)
    {
      TimeData.timerTw60_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerTw60.Start();
    }

    private static void timerTw60_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locTw60)
          YouleToOther.DataToOther(1019);
      }
      catch (Exception ex)
      {
        Console.WriteLine("SSC Exception:" + ex.Message);
      }
    }

    private static void ThSe60_Fun(object stateInfo)
    {
      TimeData.timerSe60_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerSe60.Start();
    }

    private static void timerSe60_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locSe60)
          YouleToOther.DataToOther(1020);
      }
      catch (Exception ex)
      {
        Console.WriteLine("SSC Exception:" + ex.Message);
      }
    }

    private static void ThOne11x5_Fun(object stateInfo)
    {
      TimeData.timerOne11x5_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerOne11x5.Start();
    }

    private static void timerOne11x5_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locOne11x5)
          YouleToOther11x5.DataToOther(2005);
      }
      catch (Exception ex)
      {
        Console.WriteLine("One11x5 Exception:" + ex.Message);
      }
    }

    private static void ThHg11x5_Fun(object stateInfo)
    {
      TimeData.timerHg11x5_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerHg11x5.Start();
    }

    private static void timerHg11x5_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locHg11x5)
          YouleToOther11x5.DataToOther(2006);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Hg11x5 Exception:" + ex.Message);
      }
    }

    private static void Th3d_Fun(object stateInfo)
    {
      TimeData.timer3d_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timer3d.Start();
    }

    private static void timer3d_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_loc3d)
        {
          DateTime now = DateTime.Now;
          int minute = now.Minute;
          int second = now.Second;
          if (minute % 10 != 0)
            return;
          Fc3dData.Fc3d();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("3d Exception:" + ex.Message);
      }
    }

    private static void ThP3_Fun(object stateInfo)
    {
      TimeData.timerP3_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerP3.Start();
    }

    private static void timerP3_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locP3)
        {
          DateTime now = DateTime.Now;
          int minute = now.Minute;
          int second = now.Second;
          if (minute % 10 != 0)
            return;
          Tcp3Data.Tcp3();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("P3 Exception:" + ex.Message);
      }
    }

    private static void ThOne3d_Fun(object stateInfo)
    {
      TimeData.timerOne3d_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerOne3d.Start();
    }

    private static void timerOne3d_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locOne3d)
          YouleToOther3d.DataToOther(3006);
      }
      catch (Exception ex)
      {
        Console.WriteLine("One3d Exception:" + ex.Message);
      }
    }

    private static void ThHg3d_Fun(object stateInfo)
    {
      TimeData.timerHg3d_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerHg3d.Start();
    }

    private static void timerHg3d_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locHg3d)
          YouleToOther3d.DataToOther(3004);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Hg3d Exception:" + ex.Message);
      }
    }

    private static void ThSe3d_Fun(object stateInfo)
    {
      TimeData.timerSe3d_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerSe3d.Start();
    }

    private static void timerSe3d_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locSe3d)
          YouleToOther3d.DataToOther(3005);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Se3d Exception:" + ex.Message);
      }
    }

    private static void ThOnePk10_Fun(object stateInfo)
    {
      TimeData.timerOnePk10_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerOnePk10.Start();
    }

    private static void timerOnePk10_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locOnePk10)
          YouleToOther11x5.DataToOther(4002);
      }
      catch (Exception ex)
      {
        Console.WriteLine("OnePk10 Exception:" + ex.Message);
      }
    }

    private static void ThYg60Pk10_Fun(object stateInfo)
    {
      TimeData.timerYg60Pk10_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerYg60Pk10.Start();
    }

    private static void timerYg60Pk10_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locYg60Pk10)
          YouleToOther11x5.DataToOther(4003);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Yg60Pk10 Exception:" + ex.Message);
      }
    }

    private static void ThYg120Pk10_Fun(object stateInfo)
    {
      TimeData.timerYg120Pk10_Elapsed((object) null, (ElapsedEventArgs) null);
      TimeData.timerYg120Pk10.Start();
    }

    private static void timerYg120Pk10_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        lock (TimeData.obj_locYg120Pk10)
          YouleToOther11x5.DataToOther(4004);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Yg120Pk10 Exception:" + ex.Message);
      }
    }
  }
}
