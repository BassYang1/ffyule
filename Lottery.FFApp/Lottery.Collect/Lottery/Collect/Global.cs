// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.Global
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using System;
using System.Web;

namespace Lottery.Collect
{
  public class Global : HttpApplication
  {
    public static HttpContext myContext = HttpContext.Current;

    protected void Application_Start(object sender, EventArgs e)
    {
      TimeData.Run();
    }

    private void Application_End(object sender, EventArgs e)
    {
    }

    private void Application_Error(object sender, EventArgs e)
    {
    }

    protected void Session_Start(object sender, EventArgs e)
    {
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
    }

    protected void Session_End(object sender, EventArgs e)
    {
    }
  }
}
