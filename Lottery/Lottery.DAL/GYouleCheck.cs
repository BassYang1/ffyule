using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Lottery.DAL.Flex;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL
{
	public static class GYouleCheck
	{
		public static void RunOfIssueNum(int LotteryId, string IssueNum)
		{
			GYouleCheck.DoWord doWord = new GYouleCheck.DoWord(GYouleCheck.Run);
			doWord.BeginInvoke(LotteryId, IssueNum, new AsyncCallback(GYouleCheck.CallBack), doWord);
		}

		public static void CallBack(IAsyncResult r)
		{
			GYouleCheck.DoWord arg_0B_0 = (GYouleCheck.DoWord)r.AsyncState;
		}

		private static void Run(int LotteryId, string IssueNum)
		{
			try
			{
				DataTable dataTable = LotteryDAL.GetDataTable(LotteryId.ToString(), IssueNum);
				if (dataTable.Rows.Count > 0)
				{
					DataTable lotteryCheck = LotteryDAL.GetLotteryCheck(LotteryId);
					decimal curRealGet = LotteryDAL.GetCurRealGet(LotteryId);
					if (curRealGet < Convert.ToDecimal(lotteryCheck.Rows[0]["CheckPer"]))
					{
						List<KeyValue> list = new List<KeyValue>();
						int num = Convert.ToInt32(lotteryCheck.Rows[0]["CheckNum"]);
						string[] array = new string[20];
						int num2 = 0;
						do
						{
							decimal d = 0m;
							decimal num3 = 0m;
							array = NumberCode.CreateCode20();
							int num4 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3])) % 10;
							int num5 = (Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6]) + Convert.ToInt32(array[7])) % 10;
							int num6 = (Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11])) % 10;
							int num7 = (Convert.ToInt32(array[12]) + Convert.ToInt32(array[13]) + Convert.ToInt32(array[14]) + Convert.ToInt32(array[15])) % 10;
							int num8 = (Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
							string lotteryNumber = string.Concat(new object[]
							{
								num4,
								",",
								num5,
								",",
								num6,
								",",
								num7,
								",",
								num8
							});
							for (int i = 0; i < dataTable.Rows.Count; i++)
							{
								DataRow dataRow = dataTable.Rows[i];
								int num9 = Convert.ToInt32(dataRow["Id"]);
								int num10 = Convert.ToInt32(dataRow["UserId"]);
								string sType = dataRow["PlayCode"].ToString();
								string text = BetDetailDAL.GetBetDetail2(Convert.ToDateTime(dataRow["STime2"]).ToString("yyyyMMdd"), num10.ToString(), num9.ToString());
								if (string.IsNullOrEmpty(text))
								{
									text = "";
								}
								string pos = dataRow["Pos"].ToString();
								decimal d2 = Convert.ToDecimal(dataRow["SingleMoney"]);
								decimal d3 = Convert.ToDecimal(dataRow["Bonus"]);
								decimal d4 = Convert.ToDecimal(dataRow["PointMoney"]);
								decimal d5 = Convert.ToDecimal(dataRow["Times"]);
								decimal d6 = Convert.ToDecimal(dataRow["Total"]);
								d += d6 * d5;
								int value = CheckPlay.Check(lotteryNumber, text, pos, sType);
								num3 += d3 * d5 * d2 * value / 2m + d4 * d5;
							}
							decimal num11 = d - num3;
							if (num11 > 0m)
							{
								num2 = num;
							}
							list.Add(new KeyValue
							{
								tKey = string.Join(",", array),
								tValue = num11
							});
							num2++;
						}
						while (num2 < num);
						IOrderedEnumerable<KeyValue> source = from a in list
						orderby a.tValue descending
						select a;
						List<KeyValue> list2 = source.ToList<KeyValue>();
						GYouleCheck.SetOpenListJson(LotteryId, IssueNum, list2[0].tKey, DateTime.Now.ToString(), string.Concat(list2[0].tValue));
					}
					else
					{
						string[] value2 = NumberCode.CreateCode20();
						GYouleCheck.SetOpenListJson(LotteryId, IssueNum, string.Join(",", value2), DateTime.Now.ToString(), "0");
					}
				}
				else
				{
					string[] value3 = NumberCode.CreateCode20();
					GYouleCheck.SetOpenListJson(LotteryId, IssueNum, string.Join(",", value3), DateTime.Now.ToString(), "0");
				}
			}
			catch (Exception ex)
			{
				new LogExceptionDAL().Save("派奖异常", ex.Message);
			}
		}

		public static void SetOpenListJson(int lotteryId, string expect, string opencode, string opentime, string realget)
		{
			string text = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
			text += "<xml rows=\"1\" code=\"ssc\" remain=\"1hrs\">";
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"<row expect=\"",
				expect,
				"\" opencode=\"",
				opencode,
				"\" opentime=\"",
				opentime,
				"\" realget=\"",
				realget,
				"\"/>"
			});
			text += "</xml>";
			string text3 = ConfigurationManager.AppSettings["DataUrl"].ToString();
			string text4 = string.Concat(new object[]
			{
				text3,
				"lottery",
				lotteryId,
				".xml"
			});
			DirFile.CreateFolder(DirFile.GetFolderPath(false, text4));
			StreamWriter streamWriter = new StreamWriter(text4, false, Encoding.UTF8);
			streamWriter.Write(text);
			streamWriter.Close();
		}

		public delegate void DoWord(int LotteryId, string IssueNum);
	}
}
