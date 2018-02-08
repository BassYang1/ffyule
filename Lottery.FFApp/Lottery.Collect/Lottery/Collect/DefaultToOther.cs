// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.DefaultToOther
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;

namespace Lottery.Collect
{
  public class DefaultToOther
  {
    public static void CqSsc()
    {
      try
      {
        string html1 = HtmlOperate.GetHtml(Config.DefaultUrl);
        if (string.IsNullOrEmpty(html1))
        {
          new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符1");
        }
        else
        {
          XmlNodeList xmlNode1 = Public.GetXmlNode(html1, "row");
          if (xmlNode1 == null)
            new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符2");
          else if (xmlNode1.Count == 0)
          {
            new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符3");
          }
          else
          {
            foreach (XmlNode xmlNode2 in xmlNode1)
            {
              string innerText1 = xmlNode2.Attributes["code"].InnerText;
              string innerText2 = xmlNode2.Attributes["expect"].InnerText;
              string Number1 = xmlNode2.Attributes["opencode"].InnerText.Replace("+", ",");
              string innerText3 = xmlNode2.Attributes["opentime"].InnerText;
              if (string.IsNullOrEmpty(innerText3) || string.IsNullOrEmpty(innerText2) || string.IsNullOrEmpty(Number1))
              {
                new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符4");
                return;
              }
              bool flag = true;
              string html2 = HtmlOperate.GetHtml(ConfigurationManager.AppSettings["RootUrl"].ToString() + "/Data/hisStory.xml");
              if (!string.IsNullOrEmpty(html2))
              {
                foreach (XmlNode xmlNode3 in Public.GetXmlNode(html2, "row"))
                {
                  string innerText4 = xmlNode3.Attributes["code"].InnerText;
                  string innerText5 = xmlNode3.Attributes["expect"].InnerText;
                  if (innerText4.Equals(innerText1) && innerText5.Equals(innerText2))
                    flag = false;
                }
              }
              if (flag && !Number1.Contains("255"))
              {
                switch (innerText1)
                {
                  case "bjpk10":
                    if (!new LotteryDataDAL().Exists(4001, innerText2))
                    {
                      if (Number1.Split(',').Length == 10)
                      {
                        new LotteryDataDAL().Add(4001, innerText2, Number1, innerText3, "");
                        Public.SetOpenListJson(4001);
                        LotteryCheck.RunOfIssueNum(4001, innerText2);
                        continue;
                      }
                      continue;
                    }
                    continue;
                  case "cqssc":
                    string str1 = innerText2.Substring(0, 8) + "-" + innerText2.Substring(8);
                    if (Number1.Length == 9 && !new LotteryDataDAL().Exists(1001, str1))
                    {
                      new LotteryDataDAL().Add(1001, str1, Number1, innerText3, "");
                      Public.SetOpenListJson(1001);
                      LotteryCheck.RunOfIssueNum(1001, str1);
                      continue;
                    }
                    continue;
                  case "gd11x5":
                    string str2 = innerText2.Substring(0, 8) + "-" + innerText2.Substring(8);
                    if (Number1.Length == 14 && !new LotteryDataDAL().Exists(2002, str2))
                    {
                      new LotteryDataDAL().Add(2002, str2, Number1, innerText3, "");
                      Public.SetOpenListJson(2002);
                      LotteryCheck.RunOfIssueNum(2002, str2);
                      continue;
                    }
                    continue;
                  case "jx11x5":
                    string str3 = innerText2.Substring(0, 8) + "-" + innerText2.Substring(8);
                    if (Number1.Length == 14 && !new LotteryDataDAL().Exists(2004, str3))
                    {
                      new LotteryDataDAL().Add(2004, str3, Number1, innerText3, "");
                      Public.SetOpenListJson(2004);
                      LotteryCheck.RunOfIssueNum(2004, str3);
                      continue;
                    }
                    continue;
                  case "krkeno":
                    if (!new LotteryDataDAL().Exists(1017, innerText2))
                    {
                      string[] strArray = Number1.Split(',');
                      string Number2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object) ((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object) ((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object) ((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                      new LotteryDataDAL().Add(1017, innerText2, Number2, innerText3, string.Join(",", strArray));
                      Public.SetOpenListJson(1017);
                      LotteryCheck.RunOfIssueNum(1017, innerText2);
                      continue;
                    }
                    continue;
                  case "phkeno":
                    if (!new LotteryDataDAL().Exists(1015, innerText2))
                    {
                      string[] strArray = Number1.Split(',');
                      string Number2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object) ((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object) ((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object) ((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                      new LotteryDataDAL().Add(1015, innerText2, Number2, innerText3, string.Join(",", strArray));
                      Public.SetOpenListJson(1015);
                      LotteryCheck.RunOfIssueNum(1015, innerText2);
                      continue;
                    }
                    continue;
                  case "sd11x5":
                    string str4 = innerText2.Substring(0, 8) + "-" + innerText2.Substring(8);
                    if (Number1.Length == 14 && !new LotteryDataDAL().Exists(2001, str4))
                    {
                      new LotteryDataDAL().Add(2001, str4, Number1, innerText3, "");
                      Public.SetOpenListJson(2001);
                      LotteryCheck.RunOfIssueNum(2001, str4);
                      continue;
                    }
                    continue;
                  case "sh11x5":
                    string str5 = innerText2.Substring(0, 8) + "-" + innerText2.Substring(8);
                    if (Number1.Length == 14 && !new LotteryDataDAL().Exists(2003, str5))
                    {
                      new LotteryDataDAL().Add(2003, str5, Number1, innerText3, "");
                      Public.SetOpenListJson(2003);
                      LotteryCheck.RunOfIssueNum(2003, str5);
                      continue;
                    }
                    continue;
                  case "shssl":
                    string str6 = innerText2.Substring(0, 8) + "-" + innerText2.Substring(8);
                    if (Number1.Length == 5 && !new LotteryDataDAL().Exists(3001, str6))
                    {
                      new LotteryDataDAL().Add(3001, str6, Number1, innerText3, "");
                      Public.SetOpenListJson(3001);
                      LotteryCheck.RunOfIssueNum(3001, str6);
                      continue;
                    }
                    continue;
                  case "tjssc":
                    string str7 = innerText2.Substring(0, 8) + "-" + innerText2.Substring(8);
                    if (Number1.Length == 9 && !new LotteryDataDAL().Exists(1007, str7))
                    {
                      new LotteryDataDAL().Add(1007, str7, Number1, innerText3, "");
                      Public.SetOpenListJson(1007);
                      LotteryCheck.RunOfIssueNum(1007, str7);
                      continue;
                    }
                    continue;
                  case "twbingo":
                    if (!new LotteryDataDAL().Exists(1013, innerText2))
                    {
                      string[] strArray = Number1.Split(',');
                      string Number2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object) ((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object) ((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object) ((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                      new LotteryDataDAL().Add(1013, innerText2, Number2, innerText3, string.Join(",", strArray));
                      Public.SetOpenListJson(1013);
                      LotteryCheck.RunOfIssueNum(1013, innerText2);
                      continue;
                    }
                    continue;
                  case "xdlkl8":
                    string str8 = innerText2.Substring(0, 8) + "-" + innerText2.Substring(8);
                    if (!new LotteryDataDAL().Exists(1011, str8))
                    {
                      string[] strArray = Number1.Split(',');
                      string Number2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object) ((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object) ((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object) ((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                      new LotteryDataDAL().Add(1011, str8, Number2, innerText3, string.Join(",", strArray));
                      Public.SetOpenListJson(1011);
                      LotteryCheck.RunOfIssueNum(1011, str8);
                      continue;
                    }
                    continue;
                  case "xjssc":
                    string str9 = innerText2.Substring(0, 8) + "-" + innerText2.Substring(9);
                    if (Number1.Length == 9 && !new LotteryDataDAL().Exists(1003, str9))
                    {
                      new LotteryDataDAL().Add(1003, str9, Number1, innerText3, "");
                      Public.SetOpenListJson(1003);
                      LotteryCheck.RunOfIssueNum(1003, str9);
                      continue;
                    }
                    continue;
                  default:
                    continue;
                }
              }
            }
            string str = ConfigurationManager.AppSettings["DataUrl"].ToString() + "hisStory.xml";
            DirFile.CreateFolder(DirFile.GetFolderPath(false, str));
            StreamWriter streamWriter = new StreamWriter(str, false, Encoding.UTF8);
            streamWriter.Write(html1);
            streamWriter.Close();
          }
        }
      }
      catch (Exception ex)
      {
        new LogExceptionDAL().Save("采集异常", "采集获取开奖数据出错，错误代码111：" + ex.Message);
      }
    }
  }
}
