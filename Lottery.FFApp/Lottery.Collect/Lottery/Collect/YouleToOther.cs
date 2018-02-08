// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.YouleToOther
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using Lottery.DAL;
using System;
using System.Configuration;
using System.Xml;

namespace Lottery.Collect
{
  public class YouleToOther
  {
    public static void DataToOther(int lotteryId)
    {
      try
      {
        string html1 = HtmlOperate.GetHtml(string.Format(Config.DefaultUrlYoule, (object) lotteryId));
        if (string.IsNullOrEmpty(html1))
        {
          new LogExceptionDAL().Save("采集异常", "采集主站找不到开奖数据的关键字符1" + (object) lotteryId);
        }
        else
        {
          XmlNodeList xmlNode1 = Public.GetXmlNode(html1, "row");
          if (xmlNode1 == null)
            new LogExceptionDAL().Save("采集异常", "采集主站找不到开奖数据的关键字符2" + (object) lotteryId);
          else if (xmlNode1.Count == 0)
          {
            new LogExceptionDAL().Save("采集异常", "采集主站找不到开奖数据的关键字符3" + (object) lotteryId);
          }
          else
          {
            foreach (XmlNode xmlNode2 in xmlNode1)
            {
              string innerText1 = xmlNode2.Attributes["opentime"].InnerText;
              string innerText2 = xmlNode2.Attributes["expect"].InnerText;
              string innerText3 = xmlNode2.Attributes["opencode"].InnerText;
              if (string.IsNullOrEmpty(innerText1) || string.IsNullOrEmpty(innerText2) || string.IsNullOrEmpty(innerText3))
              {
                new LogExceptionDAL().Save("采集异常", "采集主站找不到开奖数据的关键字符4" + (object) lotteryId);
                break;
              }
              bool flag = true;
              string html2 = HtmlOperate.GetHtml(ConfigurationManager.AppSettings["RootUrl"].ToString() + "/Data/lottery" + (object) lotteryId + ".xml");
              if (!string.IsNullOrEmpty(html2))
              {
                foreach (XmlNode xmlNode3 in Public.GetXmlNode(html2, "row"))
                {
                  if (xmlNode3.Attributes["expect"].InnerText.Equals(innerText2))
                    flag = false;
                }
              }
              if (flag)
              {
                string str = innerText2;
                if (!new LotteryDataDAL().Exists(lotteryId, str))
                {
                  string[] strArray = innerText3.Split(',');
                  string Number = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object) ((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object) ((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object) ((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                  new LotteryDataDAL().Add(lotteryId, str, Number, innerText1, string.Join(",", strArray));
                  Public.SetOpenListJson(lotteryId);
                  LotteryCheck.RunOfIssueNum(lotteryId, str);
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        new LogExceptionDAL().Save("采集异常", "采集主站获取开奖数据出错，错误代码：" + ex.Message);
      }
    }
  }
}
