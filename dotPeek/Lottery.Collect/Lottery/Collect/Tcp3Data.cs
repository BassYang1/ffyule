// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.Tcp3Data
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using Lottery.DAL;
using System;
using System.Xml;

namespace Lottery.Collect
{
  public class Tcp3Data
  {
    public static void Tcp3()
    {
      try
      {
        string html = HtmlOperate.GetHtml("http://f.apiplus.net/pl3.xml");
        if (string.IsNullOrEmpty(html))
        {
          new LogExceptionDAL().Save("采集异常", "P3找不到开奖数据的关键字符");
        }
        else
        {
          XmlNodeList xmlNode1 = Public.GetXmlNode(html, "row");
          if (xmlNode1 == null)
            new LogExceptionDAL().Save("采集异常", "P3找不到开奖数据的关键字符");
          else if (xmlNode1.Count == 0)
          {
            new LogExceptionDAL().Save("采集异常", "P3找不到开奖数据的关键字符");
          }
          else
          {
            foreach (XmlNode xmlNode2 in xmlNode1)
            {
              string innerText1 = xmlNode2.Attributes["opentime"].InnerText;
              string str1 = xmlNode2.Attributes["expect"].InnerText;
              string innerText2 = xmlNode2.Attributes["opencode"].InnerText;
              if (string.IsNullOrEmpty(innerText1) || string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(innerText2))
              {
                new LogExceptionDAL().Save("采集异常", "P3找不到开奖数据的关键字符");
                break;
              }
              if (str1.Length == 5)
                str1 = "20" + str1;
              string str2 = str1;
              if (innerText2.Length == 5 && !new LotteryDataDAL().Exists(3003, str2) && !innerText2.Contains("255"))
              {
                new LotteryDataDAL().Add(3003, str2, innerText2, DateTime.Now.ToString("yyyy-MM-dd") + " 20:30:00", "");
                Public.SetOpenListJson(3003);
                LotteryCheck.RunOfIssueNum(3003, str2);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        new LogExceptionDAL().Save("采集异常", "P3获取开奖数据出错，错误代码：" + ex.Message);
      }
    }
  }
}
