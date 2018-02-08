using System;
using System.Data;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class LotteryDataDAL : ComData
	{
		public void GetListJSON(int lotteryId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 10 * from Sys_LotteryData where Type=" + lotteryId + " order by Title desc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetListJSON(int lotteryId, ref string _jsonstr, ref string _xml)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 20 * from Sys_LotteryData where Type=" + lotteryId + " order by Title desc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				_xml = base.ConverTableToLotteryXML(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetDnListJSON(int lotteryId, ref string _jsonstr)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Title");
			dataTable.Columns.Add("Number");
			dataTable.Columns.Add("Number1");
			dataTable.Columns.Add("Number2");
			dataTable.Columns.Add("Number3");
			dataTable.Columns.Add("Number4");
			dataTable.Columns.Add("Number5");
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 18 * from Sys_LotteryData where Type=" + lotteryId + " order by Title desc";
				DataTable dataTable2 = dbOperHandler.GetDataTable();
				for (int i = 0; i < dataTable2.Rows.Count; i++)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["Title"] = dataTable2.Rows[i]["Title"].ToString();
					dataRow["Number"] = dataTable2.Rows[i]["Number"].ToString() + "(" + CheckSSC_DN.CheckNNum(dataTable2.Rows[i]["Number"].ToString()) + ")";
					dataRow["Number1"] = CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 1) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 1)) + ")";
					dataRow["Number2"] = CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 2) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 2)) + ")";
					dataRow["Number3"] = CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 3) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 3)) + ")";
					dataRow["Number4"] = CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 4) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 4)) + ")";
					dataRow["Number5"] = CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 5) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 5)) + ")";
					dataTable.Rows.Add(dataRow);
				}
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public bool Exists(int _type, string _title)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Title=@Title and Type=@Type";
				dbOperHandler.AddConditionParameter("@Title", _title);
				dbOperHandler.AddConditionParameter("@Type", _type);
				if (dbOperHandler.Exist("Sys_LotteryData"))
				{
					num = 1;
				}
			}
			return num == 1;
		}

		public bool Exists(int _type, string _title, string _number)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Type=@Type and NumberAll=@NumberAll";
				dbOperHandler.AddConditionParameter("@Type", _type);
				dbOperHandler.AddConditionParameter("@NumberAll", _number);
				if (dbOperHandler.Exist("Sys_LotteryData"))
				{
					num = 1;
				}
			}
			return num == 1;
		}

		public bool Add(int type, string title, string Number, string opentime, string NumberAll = "")
		{
			int num = LotterySum.SumNumber(Number);
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				NumberAll = (string.IsNullOrEmpty(NumberAll) ? Number : NumberAll);
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("Type", type);
				dbOperHandler.AddFieldItem("Title", title);
				dbOperHandler.AddFieldItem("Number", Number);
				dbOperHandler.AddFieldItem("NumberAll", NumberAll);
				dbOperHandler.AddFieldItem("Total", num);
				dbOperHandler.AddFieldItem("Opentime", opentime);
				dbOperHandler.AddFieldItem("IsFill", "1");
				if (dbOperHandler.Insert("Sys_LotteryData") > 0)
				{
					return true;
				}
			}
			return false;
		}

		public bool AddYoule(int type, string title, string Number, string opentime, string NumberAll = "")
		{
			int num = LotterySum.SumNumber(Number);
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				NumberAll = (string.IsNullOrEmpty(NumberAll) ? Number : NumberAll);
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("Type", type);
				dbOperHandler.AddFieldItem("Title", title);
				dbOperHandler.AddFieldItem("Number", Number);
				dbOperHandler.AddFieldItem("NumberAll", NumberAll);
				dbOperHandler.AddFieldItem("Total", num);
				dbOperHandler.AddFieldItem("Opentime", opentime);
				dbOperHandler.AddFieldItem("IsFill", "1");
				if (dbOperHandler.Insert("Sys_LotteryData") > 0)
				{
					return true;
				}
			}
			return false;
		}

		public bool UpdateBetNumber(int type, string title)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 1 Type,Title,Number from Sys_LotteryData where Id=" + title;
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					LotteryCheck.AdminRunOper(Convert.ToInt32(dataTable.Rows[0]["Type"].ToString()), dataTable.Rows[0]["Title"].ToString(), dataTable.Rows[0]["Number"].ToString());
					return true;
				}
			}
			return false;
		}

		public bool UpdateAllBetNumber(int type)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 1 Type,Title,Number from Sys_LotteryData where Type=" + type + " order by Title desc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						LotteryCheck.AdminRunOper(Convert.ToInt32(dataTable.Rows[i]["Type"].ToString()), dataTable.Rows[i]["Title"].ToString(), dataTable.Rows[i]["Number"].ToString());
					}
				}
			}
			return true;
		}

		public DataTable GetListDataTable(int lotteryId, int top)
		{
			DataTable result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Concat(new object[]
				{
					"select * from (select top ",
					top,
					" * from Sys_LotteryData where Type=",
					lotteryId,
					" order by Title Desc) A order by Title asc"
				});
				DataTable dataTable = dbOperHandler.GetDataTable();
				result = dataTable;
			}
			return result;
		}

		public string GetHisNumber(string ltype)
		{
			string text = "";
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT TOP 1000 [Number]\r\n                                FROM [Sys_LotteryData] where '" + ltype + "'=substring(Convert(varchar(10),type),1,1) order by STime asc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 1)
				{
					Random random = new Random();
					string text2 = dataTable.Rows[random.Next(0, dataTable.Rows.Count - 1)]["Number"].ToString();
					int num = random.Next(0, 4);
					string[] array = text2.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						if (i == num)
						{
							text = text + random.Next(0, 9) + ",";
						}
						else
						{
							text = text + array[i] + ",";
						}
					}
					text = text.Substring(0, text.Length - 1);
				}
				else
				{
					text = NumberCode.CreateCode(5);
				}
			}
			return text;
		}

		public DataTable GetHisNumber2(string ltype)
		{
			DataTable result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT TOP 1000 [Number]\r\n                                FROM [Sys_LotteryData] where '" + ltype + "'=substring(Convert(varchar(10),type),1,1) order by STime asc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				result = dataTable;
			}
			return result;
		}
	}
}
