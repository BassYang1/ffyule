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
	public static class YoulePk10Check
	{
		public static void RunOfIssueNum(int LotteryId, string IssueNum)
		{
			YoulePk10Check.DoWord doWord = new YoulePk10Check.DoWord(YoulePk10Check.Run);
			doWord.BeginInvoke(LotteryId, IssueNum, new AsyncCallback(YoulePk10Check.CallBack), doWord);
		}

		public static void CallBack(IAsyncResult r)
		{
			YoulePk10Check.DoWord arg_0B_0 = (YoulePk10Check.DoWord)r.AsyncState;
		}

		private static void Run(int LotteryId, string IssueNum)
		{
			try
			{
				YoulePk10Check.list.Clear();
				DataTable dataTable = LotteryDAL.GetDataTable(LotteryId.ToString(), IssueNum);
				if (dataTable.Rows.Count > 0)
				{
					DataTable lotteryCheck = LotteryDAL.GetLotteryCheck(LotteryId);
					decimal curRealGet = LotteryDAL.GetCurRealGet(LotteryId);
					if (curRealGet < Convert.ToDecimal(lotteryCheck.Rows[0]["CheckPer"]))
					{
						int num = Convert.ToInt32(lotteryCheck.Rows[0]["CheckNum"]);
						int num2 = 0;
						string text;
						do
						{
							decimal d = 0m;
							decimal num3 = 0m;
							text = NumberCode.CreateCodePk10(10);
							for (int i = 0; i < dataTable.Rows.Count; i++)
							{
								DataRow dataRow = dataTable.Rows[i];
								int num4 = Convert.ToInt32(dataRow["Id"]);
								int num5 = Convert.ToInt32(dataRow["UserId"]);
								string sType = dataRow["PlayCode"].ToString();
								string text2 = BetDetailDAL.GetBetDetail2(Convert.ToDateTime(dataRow["STime2"]).ToString("yyyyMMdd"), num5.ToString(), num4.ToString());
								if (string.IsNullOrEmpty(text2))
								{
									text2 = "";
								}
								string pos = dataRow["Pos"].ToString();
								decimal d2 = Convert.ToDecimal(dataRow["SingleMoney"]);
								decimal d3 = Convert.ToDecimal(dataRow["Bonus"]);
								decimal d4 = Convert.ToDecimal(dataRow["PointMoney"]);
								decimal d5 = Convert.ToDecimal(dataRow["Times"]);
								decimal d6 = Convert.ToDecimal(dataRow["Total"]);
								d += d6 * d5;
								int value = CheckPlay.Check(text, text2, pos, sType);
								num3 += d3 * d5 * d2 * value / 2m + d4;
							}
							decimal num6 = d - num3;
							if (num6 > 0m)
							{
								num2 = num;
							}
							KeyValue keyValue = new KeyValue();
							keyValue.tKey = text;
							keyValue.tValue = num6;
							YoulePk10Check.list.Add(keyValue);
							num2++;
						}
						while (num2 < num);
						IOrderedEnumerable<KeyValue> source = from a in YoulePk10Check.list
						orderby a.tValue descending
						select a;
						List<KeyValue> list = source.ToList<KeyValue>();
						if (!new LotteryDataDAL().Exists(LotteryId, IssueNum))
						{
							new LotteryDataDAL().AddYoule(LotteryId, IssueNum, list[0].tKey, DateTime.Now.ToString(), text);
							LotteryCheck.RunYouleOfIssueNum(LotteryId, IssueNum, list[0].tKey);
							YoulePk10Check.SetOpenListJson(LotteryId);
						}
					}
					else
					{
						string text3 = NumberCode.CreateCodePk10(10);
						if (!new LotteryDataDAL().Exists(LotteryId, IssueNum))
						{
							new LotteryDataDAL().AddYoule(LotteryId, IssueNum, text3, DateTime.Now.ToString(), text3);
							LotteryCheck.RunYouleOfIssueNum(LotteryId, IssueNum, text3);
							YoulePk10Check.SetOpenListJson(LotteryId);
						}
					}
				}
				else
				{
					string text4 = NumberCode.CreateCodePk10(10);
					if (!new LotteryDataDAL().Exists(LotteryId, IssueNum))
					{
						new LotteryDataDAL().AddYoule(LotteryId, IssueNum, text4, DateTime.Now.ToString(), text4);
						LotteryCheck.RunYouleOfIssueNum(LotteryId, IssueNum, text4);
						YoulePk10Check.SetOpenListJson(LotteryId);
					}
				}
			}
			catch (Exception ex)
			{
				new LogExceptionDAL().Save("派奖异常", ex.Message);
			}
		}

		public static void SetOpenListJson(int lotteryId)
		{
			string value = "";
			string value2 = "";
			new LotteryDataDAL().GetListJSON(lotteryId, ref value2, ref value);
			string text = ConfigurationManager.AppSettings["DataUrl"].ToString();
			string text2 = string.Concat(new object[]
			{
				text,
				"OpenList",
				lotteryId,
				".xml"
			});
			DirFile.CreateFolder(DirFile.GetFolderPath(false, text2));
			StreamWriter streamWriter = new StreamWriter(text2, false, Encoding.UTF8);
			streamWriter.Write(value2);
			streamWriter.Close();
			string text3 = string.Concat(new object[]
			{
				text,
				"lottery",
				lotteryId,
				".xml"
			});
			DirFile.CreateFolder(DirFile.GetFolderPath(false, text3));
			StreamWriter streamWriter2 = new StreamWriter(text3, false, Encoding.UTF8);
			streamWriter2.Write(value);
			streamWriter2.Close();
		}

		private static List<KeyValue> list = new List<KeyValue>();

		public delegate void DoWord(int LotteryId, string IssueNum);
	}
}
