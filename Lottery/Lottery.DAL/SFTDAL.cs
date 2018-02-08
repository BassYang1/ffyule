using System;
using System.Data;
using System.Data.SqlClient;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class SFTDAL : ComData
	{
		public int SaveUrl(string userId, string url)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				new DateTimePubDAL().GetDateTime();
				try
				{
					dbOperHandler.Reset();
					dbOperHandler.AddFieldItem("UserId", userId);
					dbOperHandler.AddFieldItem("Url", url);
					result = dbOperHandler.Insert("PayUrl_temp");
				}
				catch (Exception)
				{
					result = 0;
				}
			}
			return result;
		}

		public int Save(string code, string userId, string billno, string amount, string ordertime, string ipsbillno, string stime, string states)
		{
			int result = 0;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					string value = string.Concat(new string[]
					{
						ordertime.Substring(0, 4),
						"-",
						ordertime.Substring(4, 2),
						"-",
						ordertime.Substring(6, 2),
						" ",
						ordertime.Substring(8, 2),
						":",
						ordertime.Substring(10, 2),
						":",
						ordertime.Substring(12, 2)
					});
					sqlDataAdapter.SelectCommand.CommandText = string.Format("select Id from Pay_temp where IpsNo='{0}'", ipsbillno);
					DataTable dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count >= 1)
					{
						return 0;
					}
					SqlParameter[] values = new SqlParameter[]
					{
						new SqlParameter("@Code", code),
						new SqlParameter("@UserId", userId),
						new SqlParameter("@OrderNO", billno),
						new SqlParameter("@Amount", amount),
						new SqlParameter("@OrderTime", value),
						new SqlParameter("@IpsNo", ipsbillno),
						new SqlParameter("@STime", stime),
						new SqlParameter("@Status", states)
					};
					sqlCommand.CommandText = "INSERT INTO Pay_temp(Code,UserId,OrderNO,Amount,OrderTime,IpsNo,STime,States)\r\n                                            values(@Code,@UserId,@OrderNO,@Amount,@OrderTime,@IpsNo,@STime,@States)";
					sqlCommand.Parameters.AddRange(values);
					sqlCommand.ExecuteNonQuery();
					sqlCommand.Parameters.Clear();
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
				}
			}
			return result;
		}

		public int SaveAdminPayInfo(string UserId, string PayCode, string PayRequestId, string PayAmount, string PaySTime, string IpsRequestId, string IpsCompleteSTime, string PayState)
		{
			int result = 0;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					sqlDataAdapter.SelectCommand.CommandText = string.Format("select Id from Pay_temp where IpsRequestId='{0}'", IpsRequestId);
					DataTable dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						return 0;
					}
					if (new UserTotalTran().MoneyOpers(PayRequestId, UserId, Convert.ToDecimal(PayAmount), 0, 0, 0, 1, 0, "在线充值", "您成功充值" + PayAmount + "元！请注意查看您的账变信息，如有疑问请联系在线客服！", "在线充值", "") > 0)
					{
						SqlParameter[] values = new SqlParameter[]
						{
							new SqlParameter("@UserId", UserId),
							new SqlParameter("@PayCode", PayCode),
							new SqlParameter("@PayRequestId", PayRequestId),
							new SqlParameter("@PayAmount", PayAmount),
							new SqlParameter("@PaySTime", PaySTime),
							new SqlParameter("@IpsRequestId", IpsRequestId),
							new SqlParameter("@IpsCompleteSTime", IpsCompleteSTime),
							new SqlParameter("@PayState", PayState)
						};
						sqlCommand.CommandText = "INSERT INTO Pay_temp(UserId,PayCode,PayRequestId,PayAmount,PaySTime,IpsRequestId,IpsCompleteSTime,PayState)\r\n\t\t\t                                VALUES(@UserId,@PayCode,@PayRequestId,@PayAmount,@PaySTime,@IpsRequestId,@IpsCompleteSTime,@PayState)";
						sqlCommand.Parameters.AddRange(values);
						sqlCommand.ExecuteNonQuery();
						sqlCommand.Parameters.Clear();
						sqlCommand.CommandText = string.Concat(new string[]
						{
							"update N_UserCharge set InMoney=",
							PayAmount,
							",State=1 where SsId='",
							PayRequestId,
							"'"
						});
						sqlCommand.ExecuteNonQuery();
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("充值异常", ex.Message);
				}
			}
			return result;
		}

		public int SavePayInfo(string UserId, string PayCode, string PayRequestId, string PayAmount, string PayDzAmount, string PaySTime, string IpsRequestId, string IpsCompleteSTime, string PayState)
		{
			int result = 0;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					sqlDataAdapter.SelectCommand.CommandText = string.Format("select Id from Pay_temp where IpsRequestId='{0}'", IpsRequestId);
					DataTable dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						int result2 = 0;
						return result2;
					}
					sqlDataAdapter.SelectCommand.CommandText = string.Format("select top 1 InMoney from N_UserCharge where SsId='{0}'", PayRequestId);
					dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count <= 0)
					{
						int result2 = 0;
						return result2;
					}
					if (Convert.ToDecimal(dataTable.Rows[0]["InMoney"].ToString()) != Convert.ToDecimal(PayAmount))
					{
						int result2 = 0;
						return result2;
					}
					if (new UserTotalTran().MoneyOpers(PayRequestId, UserId, Convert.ToDecimal(PayAmount), 0, 0, 0, 1, 1, "在线充值", "您成功充值" + PayAmount + "元！请注意查看您的账变信息，如有疑问请联系在线客服！", "在线充值", "") > 0)
					{
						SqlParameter[] values = new SqlParameter[]
						{
							new SqlParameter("@UserId", UserId),
							new SqlParameter("@PayCode", PayCode),
							new SqlParameter("@PayRequestId", PayRequestId),
							new SqlParameter("@PayAmount", PayAmount),
							new SqlParameter("@PaySTime", PaySTime),
							new SqlParameter("@IpsRequestId", IpsRequestId),
							new SqlParameter("@IpsCompleteSTime", IpsCompleteSTime),
							new SqlParameter("@PayState", PayState)
						};
						sqlCommand.CommandText = "INSERT INTO Pay_temp(UserId,PayCode,PayRequestId,PayAmount,PaySTime,IpsRequestId,IpsCompleteSTime,PayState)\r\n\t\t\t                                VALUES(@UserId,@PayCode,@PayRequestId,@PayAmount,@PaySTime,@IpsRequestId,@IpsCompleteSTime,@PayState)";
						sqlCommand.Parameters.AddRange(values);
						sqlCommand.ExecuteNonQuery();
						sqlCommand.Parameters.Clear();
						sqlCommand.CommandText = string.Concat(new string[]
						{
							"update N_UserCharge set InMoney=",
							PayAmount,
							",DzMoney=",
							PayDzAmount,
							",State=1 where SsId='",
							PayRequestId,
							"'"
						});
						sqlCommand.ExecuteNonQuery();
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("充值异常", ex.Message);
				}
			}
			return result;
		}

		public int SaveGfbPayInfo(string UserId, string PayCode, string PayRequestId, string PayAmount, string PayDzAmount, string PaySTime, string IpsRequestId, string IpsCompleteSTime, string PayState)
		{
			int result = 0;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					sqlDataAdapter.SelectCommand.CommandText = string.Format("select Id from Pay_temp where IpsRequestId='{0}'", IpsRequestId);
					DataTable dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						int result2 = 0;
						return result2;
					}
					sqlDataAdapter.SelectCommand.CommandText = string.Format("select top 1 InMoney from N_UserCharge where SsId='{0}'", PayRequestId);
					dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count <= 0)
					{
						int result2 = 0;
						return result2;
					}
					if (Convert.ToDecimal(dataTable.Rows[0]["InMoney"].ToString()) != Convert.ToDecimal(PayAmount))
					{
						int result2 = 0;
						return result2;
					}
					if (new UserTotalTran().MoneyOpers(PayRequestId, UserId, Convert.ToDecimal(PayDzAmount), 0, 0, 0, 1, 1, "在线充值", "您成功充值" + PayDzAmount + "元！请注意查看您的账变信息，如有疑问请联系在线客服！", "在线充值", "") > 0)
					{
						PaySTime = string.Concat(new string[]
						{
							PaySTime.Substring(0, 4),
							"-",
							PaySTime.Substring(4, 2),
							"-",
							PaySTime.Substring(6, 2),
							" ",
							PaySTime.Substring(8, 2),
							":",
							PaySTime.Substring(10, 2),
							":",
							PaySTime.Substring(12, 2)
						});
						IpsCompleteSTime = string.Concat(new string[]
						{
							IpsCompleteSTime.Substring(0, 4),
							"-",
							IpsCompleteSTime.Substring(4, 2),
							"-",
							IpsCompleteSTime.Substring(6, 2),
							" ",
							IpsCompleteSTime.Substring(8, 2),
							":",
							IpsCompleteSTime.Substring(10, 2),
							":",
							IpsCompleteSTime.Substring(12, 2)
						});
						SqlParameter[] values = new SqlParameter[]
						{
							new SqlParameter("@UserId", UserId),
							new SqlParameter("@PayCode", PayCode),
							new SqlParameter("@PayRequestId", PayRequestId),
							new SqlParameter("@PayAmount", PayAmount),
							new SqlParameter("@PaySTime", PaySTime),
							new SqlParameter("@IpsRequestId", IpsRequestId),
							new SqlParameter("@IpsCompleteSTime", IpsCompleteSTime),
							new SqlParameter("@PayState", PayState)
						};
						sqlCommand.CommandText = "INSERT INTO Pay_temp(UserId,PayCode,PayRequestId,PayAmount,PaySTime,IpsRequestId,IpsCompleteSTime,PayState)\r\n\t\t\t                                VALUES(@UserId,@PayCode,@PayRequestId,@PayAmount,@PaySTime,@IpsRequestId,@IpsCompleteSTime,@PayState)";
						sqlCommand.Parameters.AddRange(values);
						sqlCommand.ExecuteNonQuery();
						sqlCommand.Parameters.Clear();
						sqlCommand.CommandText = string.Concat(new string[]
						{
							"update N_UserCharge set InMoney=",
							PayAmount,
							",DzMoney=",
							PayDzAmount,
							",State=1 where SsId='",
							PayRequestId,
							"'"
						});
						sqlCommand.ExecuteNonQuery();
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("充值异常", ex.Message);
				}
			}
			return result;
		}

		public bool Exists(string _wherestr)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr;
				if (dbOperHandler.Exist("Pay_temp"))
				{
					num = 1;
				}
			}
			return num == 1;
		}
	}
}
