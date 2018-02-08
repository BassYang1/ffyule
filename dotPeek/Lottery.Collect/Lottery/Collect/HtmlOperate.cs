// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.HtmlOperate
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using Lottery.DAL;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Lottery.Collect
{
  public class HtmlOperate
  {
    public static string GetHtml(string Url)
    {
      string str = "";
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(Url);
        httpWebRequest.Method = "GET";
        httpWebRequest.UserAgent = "MSIE";
        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        str = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();
      }
      catch (Exception ex)
      {
        new LogExceptionDAL().Save("采集异常", ex.Message);
      }
      return str;
    }

    public static string GetHtmlGB2132(string Url)
    {
      string str = "";
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(Url);
        httpWebRequest.Method = "GET";
        httpWebRequest.UserAgent = "MSIE";
        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        str = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
      }
      catch
      {
        new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
      }
      return str;
    }

    public static string GetHtmlGB2132_2(string Url)
    {
      string str = "";
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(Url);
        httpWebRequest.Method = "GET";
        httpWebRequest.UserAgent = "MSIE";
        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        str = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.GetEncoding("GBK")).ReadToEnd();
      }
      catch
      {
        new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
      }
      return str;
    }
  }
}
