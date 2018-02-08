using System;
using System.Collections;
using System.Text;
using LitJson;
using Lottery.DAL;

namespace Lottery.Collect
{
	public class QqSscData
	{
		public static void QqSsc()
		{
			try
			{
				string text = HtmlOperate2.HttpGet("http://www.77tj.org/api/tencent/onlineim", Encoding.UTF8);
				text = "{\"rows\":10,\"data\":" + text + "}";
				JsonData jsonData = JsonMapper.ToObject(text);
				foreach (JsonData jsonData2 in ((IEnumerable)jsonData["data"]))
				{
					string text2 = jsonData2["onlinetime"].ToString();
					string text3 = jsonData2["onlinenumber"].ToString();
					TimeSpan timeSpan = Convert.ToDateTime(text2) - Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
					int num = timeSpan.Hours * 60 + timeSpan.Minutes + 1;
					string str = string.Concat(num);
					if (num.ToString().Length == 1)
					{
						str = "000" + num;
					}
					if (num.ToString().Length == 2)
					{
						str = "00" + num;
					}
					if (num.ToString().Length == 3)
					{
						str = "0" + num;
					}
					string text4 = DateTime.Now.ToString("yyyyMMdd") + "-" + str;
					if (string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text4) || string.IsNullOrEmpty(text3))
					{
						new LogExceptionDAL().Save("采集异常", "腾讯分分彩找不到开奖数据的关键字符");
						break;
					}
					string text5 = text4;
					if (!new LotteryDataDAL().Exists(1005, text5))
					{
						if (!new LotteryDataDAL().Exists(1005, text5, text3))
						{
							int num2 = 0;
							for (int i = Convert.ToInt32(text3); i > 0; i /= 10)
							{
								num2 += i % 10;
							}
							string[] value = text3.Split(new char[]
							{
								','
							});
							int num3 = num2 % 10;
							int num4 = Convert.ToInt32(text3.Substring(text3.Length - 4, 1));
							int num5 = Convert.ToInt32(text3.Substring(text3.Length - 3, 1));
							int num6 = Convert.ToInt32(text3.Substring(text3.Length - 2, 1));
							int num7 = Convert.ToInt32(text3.Substring(text3.Length - 1, 1));
							string number = string.Concat(new object[]
							{
								num3,
								",",
								num4,
								",",
								num5,
								",",
								num6,
								",",
								num7
							});
							new LotteryDataDAL().Add(1005, text5, number, text2, string.Join(",", value));
							Public.SetOpenListJson(1005);
							LotteryCheck.RunOfIssueNum(1005, text5);
						}
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
