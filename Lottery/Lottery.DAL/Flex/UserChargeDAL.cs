using System;
using System.Data;
using System.Data.SqlClient;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL.Flex
{
	public class UserChargeDAL : ComData
	{
		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int num = dbOperHandler.Count("Flex_ChargeRecord");
				string sql = SqlHelp.GetSql0(num + " as totalcount,row_number() over (order by Id desc) as rowid,UserName,*", "Flex_ChargeRecord", "Id", _pagesize, _thispage, "desc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetIphoneListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("Flex_ChargeRecord");
				string sql = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,UserName,*", "Flex_ChargeRecord", "Id", _pagesize, _thispage, "desc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
					PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public int Save(string userId, string bankId, string checkCode, decimal money)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				int num = 0;
				if (bankId == "888")
				{
					num = 1;
				}
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 1 * from Sys_Info";
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (Convert.ToDecimal(money) < Convert.ToDecimal(dataTable.Rows[0]["MinCharge"].ToString()))
				{
					result = -1;
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.AddFieldItem("SsId", SsId.Charge);
					dbOperHandler.AddFieldItem("UserId", userId);
					dbOperHandler.AddFieldItem("BankId", bankId);
					dbOperHandler.AddFieldItem("CheckCode", checkCode);
					dbOperHandler.AddFieldItem("InMoney", money);
					dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					dbOperHandler.AddFieldItem("State", num);
					result = dbOperHandler.Insert("N_UserCharge");
				}
			}
			return result;
		}

		public int SaveChargeInfo(string SsId, string UserId, string BankId, string CheckCode, decimal Money)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				int num = 0;
				if (BankId == "888")
				{
					num = 1;
				}
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("SsId", SsId);
				dbOperHandler.AddFieldItem("UserId", UserId);
				dbOperHandler.AddFieldItem("BankId", BankId);
				dbOperHandler.AddFieldItem("CheckCode", CheckCode);
				dbOperHandler.AddFieldItem("InMoney", Money);
				dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				dbOperHandler.AddFieldItem("State", num);
				result = dbOperHandler.Insert("N_UserCharge");
			}
			return result;
		}

		public void DeleteLogs()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.Delete("N_UserCharge");
			}
		}

		public int SaveUpCharge(string Type, string userId, string toUserId, decimal money)
		{
			int result = 0;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				try
				{
					string chargeLog = SsId.ChargeLog;
					sqlCommand.CommandText = "select UserName from N_User where Id=" + userId;
					string text = string.Concat(sqlCommand.ExecuteScalar());
					sqlCommand.CommandText = "select UserName from N_User where Id=" + toUserId;
					string text2 = string.Concat(sqlCommand.ExecuteScalar());
					if (Type == "0")
					{
						if (new UserTotalTran().MoneyOpers(chargeLog, userId, -money, 0, 0, 0, 9, 99, "", "", string.Concat(new object[]
						{
							"转账给",
							text2,
							" ",
							money,
							"元"
						}), "") <= 0)
						{
							int result2 = 0;
							return result2;
						}
						if (new UserTotalTran().MoneyOpers(chargeLog, toUserId, money, 0, 0, 0, 9, 99, "", "", string.Concat(new object[]
						{
							text,
							"转账给你",
							money,
							"元"
						}), "") <= 0)
						{
							int result2 = 0;
							return result2;
						}
						SqlParameter[] values = new SqlParameter[]
						{
							new SqlParameter("@SsId", chargeLog),
							new SqlParameter("@Type", Type),
							new SqlParameter("@UserId", userId),
							new SqlParameter("@ToUserId", toUserId),
							new SqlParameter("@MoneyChange", money),
							new SqlParameter("@STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
							new SqlParameter("@Remark", userId + "为" + toUserId + "充值")
						};
						sqlCommand.CommandText = "INSERT INTO N_UserChargeLog(SsId,Type,UserId,ToUserId,MoneyChange,STime,Remark) VALUES (@SsId,@Type,@UserId,@ToUserId,@MoneyChange,@STime,@Remark)";
						SqlCommand expr_1E1 = sqlCommand;
						expr_1E1.CommandText += " SELECT SCOPE_IDENTITY()";
						sqlCommand.Parameters.AddRange(values);
						result = Convert.ToInt32(sqlCommand.ExecuteScalar());
						sqlCommand.Parameters.Clear();
					}
					if (Type == "1")
					{
						if (new UserTotalTran().MoneyOpers(chargeLog, userId, -money, 0, 0, 0, 10, 99, "", "", string.Concat(new object[]
						{
							"转账给",
							text2,
							" ",
							money,
							"元"
						}), "") <= 0)
						{
							int result2 = 0;
							return result2;
						}
						if (new UserTotalTran().MoneyOpers(chargeLog, toUserId, money, 0, 0, 0, 10, 99, "", "", string.Concat(new object[]
						{
							text,
							"转账给你",
							money,
							"元"
						}), "") <= 0)
						{
							int result2 = 0;
							return result2;
						}
						SqlParameter[] values2 = new SqlParameter[]
						{
							new SqlParameter("@SsId", chargeLog),
							new SqlParameter("@Type", Type),
							new SqlParameter("@UserId", userId),
							new SqlParameter("@ToUserId", toUserId),
							new SqlParameter("@MoneyChange", money),
							new SqlParameter("@STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
							new SqlParameter("@Remark", userId + "为" + toUserId + "充值")
						};
						sqlCommand.CommandText = "INSERT INTO N_UserChargeLog(SsId,Type,UserId,ToUserId,MoneyChange,STime,Remark) VALUES (@SsId,@Type,@UserId,@ToUserId,@MoneyChange,@STime,@Remark)";
						SqlCommand expr_3AB = sqlCommand;
						expr_3AB.CommandText += " SELECT SCOPE_IDENTITY()";
						sqlCommand.Parameters.AddRange(values2);
						result = Convert.ToInt32(sqlCommand.ExecuteScalar());
						sqlCommand.Parameters.Clear();
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
					int result2 = 0;
					return result2;
				}
			}
			return result;
		}

		public int SaveAgentFH(string UserId, string toUserId, string StartTime, string EndTime, decimal Bet, decimal Total, decimal Per, decimal InMoney)
		{
			int result = 0;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				try
				{
					string chargeLog = SsId.ChargeLog;
					sqlCommand.CommandText = "select UserName from N_User where Id=" + UserId;
					string text = string.Concat(sqlCommand.ExecuteScalar());
					sqlCommand.CommandText = "select UserName from N_User where Id=" + toUserId;
					string text2 = string.Concat(sqlCommand.ExecuteScalar());
					if (new UserTotalTran().MoneyOpers(chargeLog, UserId, -InMoney, 0, 0, 0, 12, 99, "", "", string.Concat(new object[]
					{
						"分红给",
						text2,
						" ",
						InMoney,
						"元"
					}), "") <= 0)
					{
						int result2 = 0;
						return result2;
					}
					if (new UserTotalTran().MoneyOpers(chargeLog, toUserId, InMoney, 0, 0, 0, 12, 99, "", "", string.Concat(new object[]
					{
						text,
						"分红给你",
						InMoney,
						"元"
					}), "") <= 0)
					{
						int result2 = 0;
						return result2;
					}
					SqlParameter[] values = new SqlParameter[]
					{
						new SqlParameter("@UserId", UserId),
						new SqlParameter("@StartTime", StartTime),
						new SqlParameter("@EndTime", EndTime),
						new SqlParameter("@Bet", Bet),
						new SqlParameter("@Total", Total),
						new SqlParameter("@Per", Per),
						new SqlParameter("@InMoney", InMoney),
						new SqlParameter("@STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
						new SqlParameter("@Remark", UserId + "为" + toUserId + "分红")
					};
					sqlCommand.CommandText = "INSERT INTO [Act_AgentFHRecord]([UserId],[AgentId],[StartTime],[EndTime],[Bet],[Total],[Per],[InMoney],[STime],[Remark])";
					SqlCommand expr_203 = sqlCommand;
					expr_203.CommandText += "VALUES(@UserId,99,@StartTime,@EndTime,@Bet,@Total,@Per,@InMoney,@STime,@Remark)";
					SqlCommand expr_219 = sqlCommand;
					expr_219.CommandText += " SELECT SCOPE_IDENTITY()";
					sqlCommand.Parameters.AddRange(values);
					result = Convert.ToInt32(sqlCommand.ExecuteScalar());
					sqlCommand.Parameters.Clear();
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
					int result2 = 0;
					return result2;
				}
			}
			return result;
		}
	}
}
