// Decompiled with JetBrains decompiler
// Type: Lottery.Collect.QqSscData
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Collect.dll

using LitJson;
using Lottery.DAL;
using System;
using System.Collections;
using System.Text;

namespace Lottery.Collect
{
  public class QqSscData
  {
    public static void QqSsc()
    {
      try
      {
        foreach (JsonData jsonData in (IEnumerable) JsonMapper.ToObject("{\"rows\":10,\"data\":" + HtmlOperate2.HttpGet("http://www.77tj.org/api/tencent/onlineim", Encoding.UTF8) + "}")["data"])
        {
          string opentime = jsonData["onlinetime"].ToString();
          string _number = jsonData["onlinenumber"].ToString();
          DateTime dateTime1 = Convert.ToDateTime(opentime);
          DateTime now = DateTime.Now;
          DateTime dateTime2 = Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00");
          TimeSpan timeSpan = dateTime1 - dateTime2;
          int num1 = timeSpan.Hours * 60 + timeSpan.Minutes + 1;
          string str1 = string.Concat((object) num1);
          if (num1.ToString().Length == 1)
            str1 = "000" + (object) num1;
          if (num1.ToString().Length == 2)
            str1 = "00" + (object) num1;
          if (num1.ToString().Length == 3)
            str1 = "0" + (object) num1;
          now = DateTime.Now;
          string str2 = now.ToString("yyyyMMdd") + "-" + str1;
          if (string.IsNullOrEmpty(opentime) || string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(_number))
          {
            new LogExceptionDAL().Save("采集异常", "腾讯分分彩找不到开奖数据的关键字符");
            break;
          }
          string str3 = str2;
          if (!new LotteryDataDAL().Exists(1005, str3) && !new LotteryDataDAL().Exists(1005, str3, _number))
          {
            int num2 = 0;
            int int32 = Convert.ToInt32(_number);
            while (int32 > 0)
            {
              num2 += int32 % 10;
              int32 /= 10;
            }
            string[] strArray = _number.Split(',');
            string Number = (num2 % 10).ToString() + "," + (object) Convert.ToInt32(_number.Substring(_number.Length - 4, 1)) + "," + (object) Convert.ToInt32(_number.Substring(_number.Length - 3, 1)) + "," + (object) Convert.ToInt32(_number.Substring(_number.Length - 2, 1)) + "," + (object) Convert.ToInt32(_number.Substring(_number.Length - 1, 1));
            new LotteryDataDAL().Add(1005, str3, Number, opentime, string.Join(",", strArray));
            Public.SetOpenListJson(1005);
            LotteryCheck.RunOfIssueNum(1005, str3);
          }
        }
      }
      catch (Exception ex)
      {
        new LogExceptionDAL().Save("采集异常", "腾讯分分彩获取开奖数据出错，错误代码：" + ex.Message);
      }
    }
  }
}
