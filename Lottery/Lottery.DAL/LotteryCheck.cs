using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class LotteryCheck : ComData
	{
		public static void RunOfIssueNum(int LotteryId, string IssueNum)
		{
			LotteryCheck.DoWord doWord = new LotteryCheck.DoWord(LotteryCheck.RunOper);
			doWord.BeginInvoke(LotteryId, IssueNum, new AsyncCallback(LotteryCheck.CallBack), doWord);
		}

		public static void CallBack(IAsyncResult r)
		{
			LotteryCheck.DoWord arg_0B_0 = (LotteryCheck.DoWord)r.AsyncState;
		}

		public static string RunOper(int Type, string Title)
		{
			string result = "";
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
					sqlDataAdapter.SelectCommand.CommandText = string.Format("select top 1 Type,Title,Number from Sys_LotteryData where Type={0} and Title='{1}'", Type, Title);
					DataTable dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						string lotteryNumber = dataTable.Rows[0]["Number"].ToString();
						sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
						sqlDataAdapter.SelectCommand.CommandText = string.Format("select b.username,b.point as uPoint,a.* \r\n                                                    From N_UserBet a with(nolock) left join N_User b on a.UserId=b.Id \r\n                                                    where a.State=0 and LotteryId={0} and IssueNum='{1}'", dataTable.Rows[0]["Type"].ToString(), dataTable.Rows[0]["Title"].ToString());
						DataTable dataTable2 = new DataTable("N_UserBet");
						sqlDataAdapter.Fill(dataTable2);
						if (dataTable2.Rows.Count > 0)
						{
							foreach (DataRow dataRow in dataTable2.Rows)
							{
								if (Convert.ToInt32(dataRow["State"].ToString()) == 0)
								{
									CheckOperation.Checking(dataRow, lotteryNumber, sqlCommand);
								}
							}
							foreach (DataRow dataRow2 in dataTable2.Rows)
							{
								string userName = dataRow2["UserName"].ToString();
								int userPoint = Convert.ToInt32(dataRow2["uPoint"]);
								int betId = Convert.ToInt32(dataRow2["Id"]);
								string ssId = dataRow2["SsId"].ToString();
								int userId = Convert.ToInt32(dataRow2["UserId"]);
								int lotteryId = Convert.ToInt32(dataRow2["LotteryId"]);
								int playId = Convert.ToInt32(dataRow2["PlayId"]);
								decimal d = Convert.ToDecimal(dataRow2["Total"]);
								decimal d2 = Convert.ToDecimal(dataRow2["Times"]);
								CheckOperation.AgencyPoint(ssId, userId, userName, userPoint, lotteryId, playId, betId, Convert.ToDecimal(d * d2), sqlCommand);
							}
							sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
							sqlDataAdapter.SelectCommand.CommandText = string.Format("select UserId,sum(times*total) as bet,sum(WinBonus) as win,sum(RealGet) as RealGet  From N_UserBet with(nolock) \r\n                                                            where lotteryId={0} and IssueNum='{1}' group by UserId", Type, Title);
							DataTable dataTable3 = new DataTable();
							sqlDataAdapter.Fill(dataTable3);
							foreach (DataRow dataRow3 in dataTable3.Rows)
							{
								string userId2 = dataRow3["UserId"].ToString();
								string str = LotteryUtils.LotteryTitle(Type);
								string str2 = Convert.ToDecimal(dataRow3["bet"]).ToString("0.0000");
								string str3 = Convert.ToDecimal(dataRow3["win"]).ToString("0.0000");
								string str4 = Convert.ToDecimal(dataRow3["RealGet"]).ToString("0.0000");
								string text = "投注彩种 " + str + "<br/>";
								text = text + "投注期号 " + Title + "<br/>";
								text = text + "投注金额 " + str2 + "元<br/>";
								text = text + "中奖金额 " + str3 + "元<br/>";
								text = text + "本次盈亏 " + str4 + "元";
								LotteryCheck.SetUserJson(userId2, Type + Title, text);
							}
							dataTable2.Dispose();
							dataTable.Dispose();
						}
						else
						{
							result = "该期没有开奖号码，请手动添加！";
						}
					}
					else
					{
						result = "该期没有开奖号码，请手动添加！";
					}
				}
				catch (Exception ex)
				{
					result = "派奖出现错误，请重试！";
					new LogExceptionDAL().Save("派奖异常", ex.Message);
				}
				finally
				{
					sqlConnection.Dispose();
					sqlConnection.Close();
				}
			}
			return result;
		}

		public static void RunYouleOfIssueNum(int LotteryId, string IssueNum, string Number)
		{
			LotteryCheck.DoWordYoule doWordYoule = new LotteryCheck.DoWordYoule(LotteryCheck.YouleRunOper);
			doWordYoule.BeginInvoke(LotteryId, IssueNum, Number, new AsyncCallback(LotteryCheck.CallBackYoule), doWordYoule);
		}

		public static void CallBackYoule(IAsyncResult r)
		{
			LotteryCheck.DoWordYoule arg_0B_0 = (LotteryCheck.DoWordYoule)r.AsyncState;
		}

		public static string YouleRunOper(int LotteryId, string IssueNum, string Number)
		{
			string result = "";
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
					sqlDataAdapter.SelectCommand.CommandText = string.Concat(new object[]
					{
						"select b.username,b.point as uPoint,* From N_UserBet a with(nolock) left join N_User b on a.UserId=b.Id where State=0 and LotteryId=",
						LotteryId,
						" and IssueNum='",
						IssueNum,
						"'"
					});
					DataTable dataTable = new DataTable("N_UserBet");
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						foreach (DataRow dataRow in dataTable.Rows)
						{
							if (Convert.ToInt32(dataRow["State"].ToString()) == 0)
							{
								CheckOperation.Checking(dataRow, Number, sqlCommand);
							}
						}
						foreach (DataRow dataRow2 in dataTable.Rows)
						{
							string userName = dataRow2["UserName"].ToString();
							int userPoint = Convert.ToInt32(dataRow2["uPoint"]);
							int betId = Convert.ToInt32(dataRow2["Id"]);
							string ssId = dataRow2["SsId"].ToString();
							int userId = Convert.ToInt32(dataRow2["UserId"]);
							int playId = Convert.ToInt32(dataRow2["PlayId"]);
							decimal d = Convert.ToDecimal(dataRow2["Total"]);
							decimal d2 = Convert.ToDecimal(dataRow2["Times"]);
							CheckOperation.AgencyPoint(ssId, userId, userName, userPoint, LotteryId, playId, betId, Convert.ToDecimal(d * d2), sqlCommand);
						}
						sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
						sqlDataAdapter.SelectCommand.CommandText = string.Format("select UserId,sum(times*total) as bet,sum(WinBonus) as win,sum(RealGet) as RealGet  From N_UserBet with(nolock) \r\n                                                            where lotteryId={0} and IssueNum='{1}' group by UserId", LotteryId, IssueNum);
						DataTable dataTable2 = new DataTable();
						sqlDataAdapter.Fill(dataTable2);
						foreach (DataRow dataRow3 in dataTable2.Rows)
						{
							string userId2 = dataRow3["UserId"].ToString();
							string str = LotteryUtils.LotteryTitle(LotteryId);
							string str2 = Convert.ToDecimal(dataRow3["bet"]).ToString("0.0000");
							string str3 = Convert.ToDecimal(dataRow3["win"]).ToString("0.0000");
							string str4 = Convert.ToDecimal(dataRow3["RealGet"]).ToString("0.0000");
							string text = "投注彩种 " + str + "<br/>";
							text = text + "投注期号 " + IssueNum + "<br/>";
							text = text + "投注金额 " + str2 + "元<br/>";
							text = text + "中奖金额 " + str3 + "元<br/>";
							text = text + "本次盈亏 " + str4 + "元";
							LotteryCheck.SetUserJson(userId2, LotteryId + IssueNum, text);
						}
						dataTable2.Dispose();
						dataTable.Dispose();
					}
					else
					{
						result = "该期没有开奖号码，请手动添加！";
					}
				}
				catch (Exception ex)
				{
					result = "派奖出现错误，请重试！";
					new LogExceptionDAL().Save("派奖异常", ex.Message);
				}
				finally
				{
					sqlConnection.Dispose();
					sqlConnection.Close();
				}
			}
			return result;
		}

		public static string AdminRunOper(int LotteryId, string IssueNum, string Number)
		{
			string result = "";
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
					sqlDataAdapter.SelectCommand.CommandText = string.Concat(new object[]
					{
						"select b.username,b.point as uPoint,* From N_UserBet a with(nolock) left join N_User b on a.UserId=b.Id where State=0 and LotteryId=",
						LotteryId,
						" and IssueNum='",
						IssueNum,
						"'"
					});
					DataTable dataTable = new DataTable("N_UserBet");
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						foreach (DataRow dataRow in dataTable.Rows)
						{
							if (Convert.ToInt32(dataRow["State"].ToString()) == 0)
							{
								CheckOperation.Checking(dataRow, Number, sqlCommand);
							}
						}
						foreach (DataRow dataRow2 in dataTable.Rows)
						{
							string userName = dataRow2["UserName"].ToString();
							int userPoint = Convert.ToInt32(dataRow2["uPoint"]);
							int betId = Convert.ToInt32(dataRow2["Id"]);
							string ssId = dataRow2["SsId"].ToString();
							int userId = Convert.ToInt32(dataRow2["UserId"]);
							int playId = Convert.ToInt32(dataRow2["PlayId"]);
							decimal d = Convert.ToDecimal(dataRow2["Total"]);
							decimal d2 = Convert.ToDecimal(dataRow2["Times"]);
							CheckOperation.AgencyPoint(ssId, userId, userName, userPoint, LotteryId, playId, betId, Convert.ToDecimal(d * d2), sqlCommand);
						}
						dataTable.Dispose();
					}
					else
					{
						result = "该期没有开奖号码，请手动添加！";
					}
				}
				catch (Exception ex)
				{
					result = "派奖出现错误，请重试！";
					new LogExceptionDAL().Save("派奖异常", ex.Message);
				}
				finally
				{
					sqlConnection.Dispose();
					sqlConnection.Close();
				}
			}
			return result;
		}

		public string RunOfBetId(string BetId)
		{
			return "";
		}

		public void Cancel(int LotteryId, string title, int state)
		{
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					string text = LotteryUtils.LotteryTitle(LotteryId);
					if (state == 0)
					{
						sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
						sqlDataAdapter.SelectCommand.CommandText = string.Concat(new string[]
						{
							"select * From N_UserBet with(nolock)  where state=0 and LotteryId='",
							LotteryId.ToString(),
							"' and IssueNum='",
							title,
							"' order by id asc"
						});
					}
					else
					{
						sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
						sqlDataAdapter.SelectCommand.CommandText = string.Concat(new string[]
						{
							"select * From N_UserBet with(nolock)  where (state<>0 and state<>1) and LotteryId='",
							LotteryId.ToString(),
							"' and IssueNum='",
							title,
							"' order by id asc"
						});
					}
					DataTable dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					foreach (DataRow dataRow in dataTable.Rows)
					{
						int num = Convert.ToInt32(dataRow["id"].ToString());
						Convert.ToInt32(dataRow["UserId"].ToString());
						if (!CheckOperation.AdminCancel(num, sqlCommand))
						{
							new LogExceptionDAL().Save("派奖异常", string.Concat(new object[]
							{
								text,
								" 投注ID:",
								num,
								"撤单"
							}));
						}
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("程序异常", ex.Message);
				}
				finally
				{
					if (sqlConnection != null)
					{
						sqlConnection.Close();
					}
				}
			}
		}

		public void CancelOfBetId(string betId)
		{
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				try
				{
					CheckOperation.AdminCancel(Convert.ToInt32(betId), sqlCommand);
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("程序异常", ex.Message);
				}
				finally
				{
					if (sqlConnection != null)
					{
						sqlConnection.Close();
					}
				}
			}
		}

		public void CancelToNoOfTitle(string lotteryId, string title)
		{
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
					sqlDataAdapter.SelectCommand.CommandText = string.Concat(new string[]
					{
						"select * From N_UserBet with(nolock)  where lotteryId=",
						lotteryId,
						" and (state<>0 and state<>1) and IssueNum='",
						title,
						"' order by id asc"
					});
					DataTable dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					foreach (DataRow dataRow in dataTable.Rows)
					{
						int betId = Convert.ToInt32(dataRow["id"].ToString());
						Convert.ToInt32(dataRow["UserId"].ToString());
						CheckOperation.AdminCancelToNO(betId, sqlCommand);
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("程序异常", ex.Message);
				}
				finally
				{
					if (sqlConnection != null)
					{
						sqlConnection.Close();
					}
				}
			}
		}

		public static string ReplaceStr(string str)
		{
			return str.Replace("0", "一区").Replace("1", "一区").Replace("2", "二区").Replace("3", "二区").Replace("4", "三区").Replace("5", "三区").Replace("6", "四区").Replace("7", "四区").Replace("8", "五区").Replace("9", "五区");
		}

		public static string ReplaceDX(string str)
		{
			return str.Replace("0", "小").Replace("1", "小").Replace("2", "小").Replace("3", "小").Replace("4", "小").Replace("5", "大").Replace("6", "大").Replace("7", "大").Replace("8", "大").Replace("9", "大");
		}

		public static void GroupDataRows(IEnumerable<DataRow> source, List<DataTable> destination, string[] groupByFields, int fieldIndex, DataTable schema)
		{
			if (fieldIndex >= groupByFields.Length || fieldIndex < 0)
			{
				DataTable dataTable = schema.Clone();
				foreach (DataRow current in source)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow.ItemArray = current.ItemArray;
					dataTable.Rows.Add(dataRow);
				}
				destination.Add(dataTable);
				return;
			}
			IEnumerable<IGrouping<object, DataRow>> enumerable = from o in source
			group o by o[groupByFields[fieldIndex]];
			foreach (IGrouping<object, DataRow> current2 in enumerable)
			{
				LotteryCheck.GroupDataRows(current2, destination, groupByFields, fieldIndex + 1, schema);
			}
			fieldIndex++;
		}

		public static void SetUserJson(string UserId, string title, string content)
		{
			string value = string.Concat(new string[]
			{
				"[{\"userid\":\"",
				UserId,
				"\",\"title\":\"",
				title,
				"\",\"content\":\"",
				content,
				"\"}]"
			});
			string str = ConfigurationManager.AppSettings["DataUrl"].ToString();
			string text = str + "User\\User" + UserId + ".xml";
			DirFile.CreateFolder(DirFile.GetFolderPath(false, text));
			StreamWriter streamWriter = new StreamWriter(text, false, Encoding.UTF8);
			streamWriter.Write(value);
			streamWriter.Close();
		}

		public delegate string DoWord(int LotteryId, string IssueNum);

		public delegate string DoWordYoule(int LotteryId, string IssueNum, string Number);
	}
}
