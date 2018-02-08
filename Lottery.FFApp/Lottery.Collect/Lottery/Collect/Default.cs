// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.Default
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using System;
using System.Web.UI;

namespace Lottery.Collect
{
  public class Default : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      this.Response.Write("程序已经启动，开奖进行中。" + DateTime.Now.ToString());
      this.Response.End();
    }
  }
}
