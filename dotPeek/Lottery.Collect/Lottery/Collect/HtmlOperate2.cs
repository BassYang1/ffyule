// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.HtmlOperate2
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
  public class HtmlOperate2
  {
    public static string GetHtml(string Url)
    {
      string str = "";
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(Url);
        httpWebRequest.Method = "GET";
        Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
        str = new StreamReader(responseStream, Encoding.UTF8).ReadToEnd();
        responseStream.Close();
      }
      catch
      {
        new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
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
        Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
        str = new StreamReader(responseStream, Encoding.Default).ReadToEnd();
        responseStream.Close();
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
        Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
        str = new StreamReader(responseStream, Encoding.GetEncoding("GB2312")).ReadToEnd();
        responseStream.Close();
      }
      catch
      {
        new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
      }
      return str;
    }

    public static string HtmlToJs(string source)
    {
      return string.Format("document.writeln(\"{0}\");", (object) string.Join("\");\r\ndocument.writeln(\"", source.Replace("\\", "\\\\").Replace("/", "\\/").Replace("'", "\\'").Replace("\"", "\\\"").Split(new char[2]
      {
        '\r',
        '\n'
      }, StringSplitOptions.RemoveEmptyEntries)));
    }

    public static string HttpGet(string url, Encoding enc)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
      httpWebRequest.Timeout = 10000;
      httpWebRequest.Proxy = (IWebProxy) null;
      httpWebRequest.Method = "GET";
      httpWebRequest.ContentType = "application/x-www-from-urlencoded";
      WebResponse response = httpWebRequest.GetResponse();
      StreamReader streamReader = new StreamReader(response.GetResponseStream(), enc);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(streamReader.ReadToEnd());
      streamReader.Close();
      streamReader.Dispose();
      response.Close();
      return stringBuilder.ToString();
    }
  }
}
