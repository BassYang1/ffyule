using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class LotteryDAL : ComData
	{
		public void GetLotteryTime(string Lid, ref string _jsonstr)
		{
			string text = "[{\"name\": \"名称\",\"lotteryid\": \"彩种类别\",\"ordertime\": \"倒计时\",\"closetime\": \"封单时间\",\"nestsn\": \"下期期号\",\"cursn\": \"当前期号\"}]";
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				text = text.Replace("名称", LotteryUtils.LotteryTitle(int.Parse(Lid))).Replace("彩种类别", Lid);
				DateTime d = DateTime.Now;
				DateTime now = DateTime.Now;
				string str = now.ToString("yyyyMMdd");
				string text2 = now.ToString("HH:mm:ss");
				now.ToString("yyyy-MM-dd");
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select dbo.f_GetCloseTime(" + Lid + ") as closetime";
				DataTable dataTable = dbOperHandler.GetDataTable();
				text = text.Replace("封单时间", dataTable.Rows[0]["closetime"].ToString());
				string text3;
				string newValue;
				TimeSpan timeSpan;
				if (Lid == "3002" || Lid == "3003")
				{
					DateTime dateTime = Convert.ToDateTime(now.Year.ToString() + "-01-01 20:30:00");
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Concat(new string[]
					{
						"select datediff(d,'",
						dateTime.ToString("yyyy-MM-dd HH:mm:ss"),
						"','",
						now.ToString("yyyy-MM-dd HH:mm:ss"),
						"') as d"
					});
					DataTable dataTable2 = dbOperHandler.GetDataTable();
					int num = Convert.ToInt32(dataTable2.Rows[0]["d"]) - 7;
					num++;
					//now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 20:30:00";
					string value = now.ToString("yyyy-MM-dd") + " 20:30:00";
					if (now > Convert.ToDateTime(now.ToString(" 20:30:00")))
					{
						value = now.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
					}
					else
					{
						num--;
					}
					text3 = now.Year.ToString() + Func.AddZero(num, 3);
					newValue = now.Year.ToString() + Func.AddZero(num + 1, 3);
					timeSpan = Convert.ToDateTime(value) - Convert.ToDateTime(text2);
				}
				else
				{
					if (ComData.LotteryTime == null)
					{
						ComData.LotteryTime = new LotteryTimeDAL().GetTable();
					}
					DataRow[] array = ComData.LotteryTime.Select(string.Concat(new object[]
					{
						"Time >'",
						text2,
						"' and LotteryId=",
						Lid
					}), "Time asc");
					if (array.Length == 0)
					{
						array = ComData.LotteryTime.Select(string.Concat(new object[]
						{
							"Time <='",
							text2,
							"' and LotteryId=",
							Lid
						}), "Time asc");
						newValue = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + array[0]["Sn"].ToString();
					}
					else
					{
						newValue = str + "-" + array[0]["Sn"].ToString();
						d = Convert.ToDateTime(array[0]["Time"].ToString());
						if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 10:00:01") && Lid == "1003")
						{
							newValue = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + array[0]["Sn"].ToString();
						}
					}
					if (Convert.ToDateTime(array[0]["Time"].ToString()) < Convert.ToDateTime(text2))
					{
						d = Convert.ToDateTime(now.AddDays(1.0).ToString("yyyy-MM-dd") + " " + array[0]["Time"].ToString());
					}
					timeSpan = d - Convert.ToDateTime(text2);
					DataRow[] array2 = ComData.LotteryTime.Select(string.Concat(new object[]
					{
						"Time <'",
						text2,
						"' and LotteryId=",
						Lid
					}), "Time desc");
					if (array2.Length == 0)
					{
						array2 = ComData.LotteryTime.Select(string.Concat(new object[]
						{
							"LotteryId=",
							Lid
						}), "Time desc");
						text3 = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + array2[0]["Sn"].ToString();
					}
					else
					{
						text3 = str + "-" + array2[0]["Sn"].ToString();
						if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 10:00:01") && Lid == "1003")
						{
							text3 = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + array2[0]["Sn"].ToString();
						}
					}
					if (Lid == "4001")
					{
						DateTime dateTime2 = Convert.ToDateTime("2016-01-01 00:00:00");
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Concat(new string[]
						{
							"select datediff(d,'",
							dateTime2.ToString("yyyy-MM-dd HH:mm:ss"),
							"','",
							now.ToString("yyyy-MM-dd HH:mm:ss"),
							"') as d"
						});
						DataTable dataTable3 = dbOperHandler.GetDataTable();
						if ((now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 09:07:01")) || (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:57:01") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:59:59")))
						{
							text3 = string.Concat(530900 + (Convert.ToInt32(dataTable3.Rows[0]["d"]) - 8) * 179 + Convert.ToInt32(array2[0]["Sn"]));
							newValue = string.Concat(530900 + (Convert.ToInt32(dataTable3.Rows[0]["d"]) - 8) * 179 + Convert.ToInt32(array2[0]["Sn"]) + 1);
						}
						else
						{
							text3 = string.Concat(530900 + (Convert.ToInt32(dataTable3.Rows[0]["d"]) - 7) * 179 + Convert.ToInt32(array2[0]["Sn"]));
							newValue = string.Concat(530900 + (Convert.ToInt32(dataTable3.Rows[0]["d"]) - 7) * 179 + Convert.ToInt32(array2[0]["Sn"]) + 1);
						}
					}
					if (Lid == "1010" || Lid == "1017" || Lid == "3004")
					{
						text3 = string.Concat(new LotteryTimeDAL().GetTsIssueNum("1010") + Convert.ToInt32(array2[0]["Sn"].ToString()));
						newValue = string.Concat(Convert.ToInt32(text3) + 1);
					}
					if (Lid == "1012")
					{
						text3 = string.Concat(new LotteryTimeDAL().GetTsIssueNum("1012") + Convert.ToInt32(array2[0]["Sn"].ToString()));
						newValue = string.Concat(Convert.ToInt32(text3) + 1);
					}
					if (Lid == "1013")
					{
						text3 = string.Concat(new LotteryTimeDAL().GetTsIssueNum("1013") + Convert.ToInt32(array2[0]["Sn"].ToString()));
						newValue = string.Concat(Convert.ToInt32(text3) + 1);
					}
				}
				string newValue2 = string.Concat(timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds);
				text = text.Replace("下期期号", newValue).Replace("当前期号", text3).Replace("倒计时", newValue2);
				_jsonstr = text;
			}
		}

		public void GetLotteryZhList(string Lid, ref string _jsonstr)
		{
			DateTime now = DateTime.Now;
			string str = now.ToString("yyyyMMdd");
			string text = now.ToString("HH:mm:ss");
			now.ToString("yyyy-MM-dd");
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				if (Lid == "3002" || Lid == "3003")
				{
					DateTime dateTime = Convert.ToDateTime(now.Year.ToString() + "-01-01 20:30:00");
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Concat(new string[]
					{
						"select datediff(d,'",
						dateTime.ToString("yyyy-MM-dd HH:mm:ss"),
						"','",
						now.ToString("yyyy-MM-dd HH:mm:ss"),
						"') as d"
					});
					DataTable dataTable = dbOperHandler.GetDataTable();
					int num = Convert.ToInt32(dataTable.Rows[0]["d"]) - 7;
					num++;
					//now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 20:30:00";
					//now.ToString("yyyy-MM-dd") + " 20:30:00";
					if (now > Convert.ToDateTime(now.ToString(" 20:30:00")))
					{
						//now.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
					}
					else
					{
						num--;
					}
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i <= 9; i++)
					{
						string text2 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},";
						text2 = text2.Replace("编号", (i + 1).ToString()).Replace("期号", now.Year.ToString() + Func.AddZero(num + (i + 1), 3)).Replace("倍数", "0").Replace("金额", "0.00").Replace("时间", now.AddDays((double)i).ToString("yyyy-MM-dd") + " 20:30:00");
						stringBuilder.Append(text2);
					}
					_jsonstr = "[" + stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1) + "]";
				}
				else
				{
					if (ComData.LotteryTime == null)
					{
						ComData.LotteryTime = new LotteryTimeDAL().GetTable();
					}
					DataRow[] array = ComData.LotteryTime.Select(string.Concat(new object[]
					{
						"Time >'",
						text,
						"' and LotteryId=",
						Lid
					}), "Time asc");
					if (array.Length == 0)
					{
						array = ComData.LotteryTime.Select(string.Concat(new object[]
						{
							"Time <='",
							text,
							"' and LotteryId=",
							Lid
						}), "Time asc");
						str = now.AddDays(1.0).ToString("yyyyMMdd");
					}
					int num2 = ComData.LotteryTime.Select(string.Concat(new object[]
					{
						"LotteryId=",
						Lid
					}), "Time asc").Length;
					if (num2 > 120)
					{
						num2 = 120;
					}
					StringBuilder stringBuilder2 = new StringBuilder();
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Concat(new object[]
					{
						"select top ",
						num2,
						" * from Sys_LotteryTime where lotteryid=",
						Lid,
						" and sn>=",
						array[0]["Sn"].ToString(),
						"order by sn asc"
					});
					DataTable dataTable2 = dbOperHandler.GetDataTable();
					for (int j = 0; j < dataTable2.Rows.Count; j++)
					{
						string text3 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},";
						string newValue = str + "-" + dataTable2.Rows[j]["sn"].ToString();
						if (Lid == "1010" || Lid == "1017" || Lid == "3004" || Lid == "1012" || Lid == "1013")
						{
							newValue = string.Concat(new LotteryTimeDAL().GetTsIssueNum(Lid) + Convert.ToInt32(dataTable2.Rows[j]["sn"].ToString()));
						}
						text3 = text3.Replace("编号", (j + 1).ToString()).Replace("期号", newValue).Replace("倍数", "0").Replace("金额", "0.00").Replace("时间", now.ToString("yyyy-MM-dd") + " " + dataTable2.Rows[j]["time"]);
						stringBuilder2.Append(text3);
					}
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Concat(new object[]
					{
						"select top ",
						num2 - dataTable2.Rows.Count,
						" * from Sys_LotteryTime where lotteryid=",
						Lid,
						" order by sn asc"
					});
					DataTable dataTable3 = dbOperHandler.GetDataTable();
					for (int k = 0; k < dataTable3.Rows.Count; k++)
					{
						string text4 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},";
						string newValue2 = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataTable3.Rows[k]["sn"].ToString();
						if (Lid == "1010" || Lid == "1017" || Lid == "3004" || Lid == "1012" || Lid == "1013")
						{
							newValue2 = string.Concat(new LotteryTimeDAL().GetTsIssueNum(Lid) + Convert.ToInt32(dataTable3.Rows[k]["sn"].ToString()));
						}
						text4 = text4.Replace("编号", (k + 1 + dataTable2.Rows.Count).ToString()).Replace("期号", newValue2).Replace("倍数", "0").Replace("金额", "0.00").Replace("时间", now.AddDays(1.0).ToString("yyyy-MM-dd") + " " + dataTable3.Rows[k]["time"]);
						stringBuilder2.Append(text4);
					}
					_jsonstr = "[" + stringBuilder2.ToString().Substring(0, stringBuilder2.ToString().Length - 1) + "]";
				}
			}
		}

		public void GetLotteryNumber(string Lid, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT Title,Number FROM Sys_LotteryData \r\n                            where Type={0} and Title=(select max(Title) from [Sys_LotteryData] where Type={0})", Lid);
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetNumberMmc(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT TOP 10 [Id]\r\n                                      ,[Type]\r\n                                      ,[Title]\r\n                                      ,[Number]\r\n                                      ,[Total]\r\n                                FROM [Sys_LotteryData] where Type=1006 and Title in (\r\n                                SELECT IssueNum FROM [N_UserBet] where lotteryId=1006 and UserId=" + UserId + ") order by Id desc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetPlayTypeXml(ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT [Id],[TypeId],[Title] FROM [Sys_PlayBigType] where TypeId=2 and IsOpen=1 order by sort ";
				DataTable dataTable = dbOperHandler.GetDataTable();
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT [Id]\r\n                                  ,[Title0]\r\n                                  ,[Title]\r\n                                  ,[Title2]\r\n                                  ,[Radio]\r\n                              FROM [Sys_PlaySmallType] \r\n                              where IsOpen=1 and flag=0 order by Id ";
				DataTable dataTable2 = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToXML(dataTable, dataTable2);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetPlayListXml(int lotteryId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				string sqlCmd = "SELECT * FROM [Sys_PlaySmallType] where lotteryId=" + lotteryId;
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sqlCmd;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.CDataToXml(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void Create(int loid)
		{
			string text = ConfigurationManager.AppSettings["DataUrl"].ToString();
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT top 1 [Title],[Number] FROM [Sys_LotteryData] where Type=" + loid + " order by Title desc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				string text2 = "{\"title\": \"期号\",\"number\": \"号码\"}";
				if (dataTable.Rows.Count > 0)
				{
					text2 = text2.Replace("期号", dataTable.Rows[0]["Title"].ToString()).Replace("号码", dataTable.Rows[0]["Number"].ToString());
				}
				else
				{
					text2 = text2.Replace("期号", "").Replace("号码", "");
				}
				dataTable.Clear();
				dataTable.Dispose();
				string text3 = string.Concat(new object[]
				{
					text,
					"EMindexData",
					loid,
					".json"
				});
				DirFile.CreateFolder(DirFile.GetFolderPath(false, text3));
				StreamWriter streamWriter = new StreamWriter(text3, false, Encoding.UTF8);
				streamWriter.Write(text2);
				streamWriter.Close();
			}
		}

		public bool IsAuto(int lotteryId)
		{
			bool result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Id=@Id";
				dbOperHandler.AddConditionParameter("@Id", lotteryId);
				int num = Convert.ToInt32(dbOperHandler.GetField("Sys_Lottery", "IsAuto"));
				result = (num == 0);
			}
			return result;
		}

		public DataTable GetAutoUrl(int lotteryId)
		{
			DataTable result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 1 IsAuto,isnull((select Url from Sys_lotteryUrl where Id=a.AutoUrl),'0') as AutoUrl \r\n                            ,isnull((select Title from Sys_lotteryUrl where Id=a.AutoUrl),'0') as Title\r\n                            from Sys_lottery a where Id=" + lotteryId;
				DataTable dataTable = dbOperHandler.GetDataTable();
				result = dataTable;
			}
			return result;
		}

		public static DataTable GetDataTable(string lotteryId, string IssueNum)
		{
			DataTable result;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				sqlDataAdapter.SelectCommand.CommandText = string.Concat(new string[]
				{
					"select [Id],[UserId],[PlayCode],[Times],[Total],[STime2],[Pos],[PointMoney],[Bonus],[SingleMoney] from N_UserBet where state=0 and lotteryId=",
					lotteryId,
					" and IssueNum='",
					IssueNum,
					"'"
				});
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				result = dataTable;
			}
			return result;
		}

		public static decimal GetCurRealGet(int lotteryId)
		{
			decimal result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT case isnull(sum(Total*Times),0) when 0 then 0 else isnull(-sum(realGet),-0)*100/isnull(sum(Total*Times),0) end as win FROM [N_UserBet] where state>=2 and DateDiff(dd,STime,getdate())=0 and lotteryId=" + lotteryId;
				DataTable dataTable = dbOperHandler.GetDataTable();
				result = Convert.ToDecimal(dataTable.Rows[0]["win"]);
			}
			return result;
		}

		public static DataTable GetLotteryCheck(int lotteryId)
		{
			DataTable result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT top 1 [CheckNum],[CheckPer] FROM [Sys_LotteryCheck] where Id=" + lotteryId;
				DataTable dataTable = dbOperHandler.GetDataTable();
				result = dataTable;
			}
			return result;
		}

		public string GetListSn(int loid)
		{
			string result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				DateTime now = DateTime.Now;
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Concat(new object[]
				{
					"select top 1 Sn from Sys_LotteryTime where LotteryId=",
					loid,
					" and Time < '",
					now.ToString("HH:mm:ss"),
					"' order by time desc"
				});
				DataTable dataTable = dbOperHandler.GetDataTable();
				string text;
				if (dataTable.Rows.Count < 1)
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = "select top 1 Sn from Sys_LotteryTime where LotteryId=" + loid + " order by time desc";
					dataTable = dbOperHandler.GetDataTable();
					text = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataTable.Rows[0]["Sn"].ToString();
				}
				else
				{
					text = now.ToString("yyyyMMdd") + "-" + dataTable.Rows[0]["Sn"].ToString();
				}
				if (loid == 4001)
				{
					DateTime dateTime = Convert.ToDateTime("2016-01-01 00:00:00");
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Concat(new string[]
					{
						"select datediff(d,'",
						dateTime.ToString("yyyy-MM-dd HH:mm:ss"),
						"','",
						now.ToString("yyyy-MM-dd HH:mm:ss"),
						"') as d"
					});
					DataTable dataTable2 = dbOperHandler.GetDataTable();
					text = string.Concat(530900 + Convert.ToInt32(dataTable2.Rows[0]["d"]) * 179 + Convert.ToInt32(dataTable.Rows[0]["Sn"].ToString()));
				}
				if (loid == 1010 || loid == 1012 || loid == 1017 || loid == 3004 || loid == 1013)
				{
					text = string.Concat(new LotteryTimeDAL().GetTsIssueNum(loid.ToString()) + Convert.ToInt32(dataTable.Rows[0]["Sn"].ToString()));
				}
				if (loid == 1014 || loid == 1016)
				{
					if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:01:30") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:59:59"))
					{
						text = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataTable.Rows[0]["Sn"].ToString();
					}
					text = text.Replace("-", "");
				}
				dataTable.Clear();
				dataTable.Dispose();
				result = text;
			}
			return result;
		}

		public string GetListNextSn(int loid)
		{
			string result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				DateTime now = DateTime.Now;
				string text;
				if (loid == 3002 || loid == 3003)
				{
					DateTime dateTime = Convert.ToDateTime(now.Year.ToString() + "-01-01 20:30:00");
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Concat(new string[]
					{
						"select datediff(d,'",
						dateTime.ToString("yyyy-MM-dd HH:mm:ss"),
						"','",
						now.ToString("yyyy-MM-dd HH:mm:ss"),
						"') as d"
					});
					DataTable dataTable = dbOperHandler.GetDataTable();
					int num = Convert.ToInt32(dataTable.Rows[0]["d"]) - 7;
					num++;
					if (now < Convert.ToDateTime(now.ToString(" 20:30:00")))
					{
						num--;
					}
					text = now.Year.ToString() + Func.AddZero(num + 1, 3);
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Concat(new object[]
					{
						"select top 1 Sn from Sys_LotteryTime where LotteryId=",
						loid,
						" and Time > '",
						now.ToString("HH:mm:ss"),
						"' order by time asc"
					});
					DataTable dataTable2 = dbOperHandler.GetDataTable();
					if (dataTable2.Rows.Count < 1)
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = "select top 1 Sn from Sys_LotteryTime where LotteryId=" + loid + " order by time asc";
						dataTable2 = dbOperHandler.GetDataTable();
						text = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataTable2.Rows[0]["Sn"].ToString();
					}
					else
					{
						text = now.ToString("yyyyMMdd") + "-" + dataTable2.Rows[0]["Sn"].ToString();
					}
					if (loid == 4001)
					{
						DateTime dateTime2 = Convert.ToDateTime("2016-01-01 00:00:00");
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Concat(new string[]
						{
							"select datediff(d,'",
							dateTime2.ToString("yyyy-MM-dd HH:mm:ss"),
							"','",
							now.ToString("yyyy-MM-dd HH:mm:ss"),
							"') as d"
						});
						DataTable dataTable3 = dbOperHandler.GetDataTable();
						text = string.Concat(530900 + Convert.ToInt32(dataTable3.Rows[0]["d"]) * 179 + Convert.ToInt32(dataTable2.Rows[0]["Sn"].ToString()));
					}
					dataTable2.Clear();
					dataTable2.Dispose();
				}
				result = text;
			}
			return result;
		}

		public string GetCurrentSn(int loid)
		{
			string result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				DateTime now = DateTime.Now;
				string text;
				if (loid == 3002 || loid == 3003)
				{
					DateTime dateTime = Convert.ToDateTime(now.Year.ToString() + "-01-01 20:30:00");
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Concat(new string[]
					{
						"select datediff(d,'",
						dateTime.ToString("yyyy-MM-dd HH:mm:ss"),
						"','",
						now.ToString("yyyy-MM-dd HH:mm:ss"),
						"') as d"
					});
					DataTable dataTable = dbOperHandler.GetDataTable();
					int num = Convert.ToInt32(dataTable.Rows[0]["d"]) - 7;
					text = now.Year.ToString() + Func.AddZero(num, 3);
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Concat(new object[]
					{
						"select top 1 Sn from Sys_LotteryTime where LotteryId=",
						loid,
						" and Time < '",
						now.ToString("HH:mm:ss"),
						"' order by time desc"
					});
					DataTable dataTable2 = dbOperHandler.GetDataTable();
					if (dataTable2.Rows.Count < 1)
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = "select top 1 Sn from Sys_LotteryTime where LotteryId=" + loid + " order by time desc";
						dataTable2 = dbOperHandler.GetDataTable();
						text = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataTable2.Rows[0]["Sn"].ToString();
					}
					else
					{
						text = now.ToString("yyyyMMdd") + "-" + dataTable2.Rows[0]["Sn"].ToString();
						if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 10:00:01") && loid == 1003)
						{
							text = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataTable2.Rows[0]["Sn"].ToString();
						}
					}
					if (loid == 4001)
					{
						DateTime dateTime2 = Convert.ToDateTime("2016-01-01 00:00:00");
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Concat(new string[]
						{
							"select datediff(d,'",
							dateTime2.ToString("yyyy-MM-dd HH:mm:ss"),
							"','",
							now.ToString("yyyy-MM-dd HH:mm:ss"),
							"') as d"
						});
						DataTable dataTable3 = dbOperHandler.GetDataTable();
						text = string.Concat(530900 + (Convert.ToInt32(dataTable3.Rows[0]["d"]) - 7) * 179 + Convert.ToInt32(dataTable2.Rows[0]["Sn"].ToString()));
					}
					dataTable2.Clear();
					dataTable2.Dispose();
				}
				result = text;
			}
			return result;
		}

		public string GetListTime(int loid)
		{
			string result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				DateTime now = DateTime.Now;
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Concat(new object[]
				{
					"select top 1 Time from Sys_LotteryTime where LotteryId=",
					loid,
					" and Time > '",
					now.ToString("HH:mm:ss"),
					"' order by time asc"
				});
				DataTable dataTable = dbOperHandler.GetDataTable();
				string text;
				if (dataTable.Rows.Count < 1)
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = "select top 1 Time from Sys_LotteryTime where LotteryId=" + loid + " order by time desc";
					dataTable = dbOperHandler.GetDataTable();
					text = now.AddDays(-1.0).ToString("yyyy-MM-dd") + " " + dataTable.Rows[0]["Time"].ToString();
				}
				else
				{
					text = now.ToString("yyyy-MM-dd") + " " + dataTable.Rows[0]["Time"].ToString();
				}
				dataTable.Clear();
				dataTable.Dispose();
				result = text;
			}
			return result;
		}

		public DataTable GetLotteryAutoMode()
		{
			DataTable result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT top 1 [AutoLottery],[ProfitModel],[ProfitMargin] FROM [Sys_Info]";
				DataTable dataTable = dbOperHandler.GetDataTable();
				result = dataTable;
			}
			return result;
		}

		public string GetLotteryNumber(int type, string title)
		{
			string result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Concat(new object[]
				{
					"SELECT Number from Sys_LotteryData where Type=",
					type,
					" and title=",
					title
				});
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					result = string.Concat(dataTable.Rows[0]["Number"]);
				}
				else
				{
					result = "0";
				}
			}
			return result;
		}
	}
}
