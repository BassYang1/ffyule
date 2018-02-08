using System;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL
{
	public class UserTotalTran : ComData
	{
		public int MoneyOpers(string ssId, string userId, decimal Money, int logLotteryId, int logPlayId, int logSysId, int logCode, int logIsSoft, string messageTitle, string messageContent, string reMark, string STime2 = "")
		{
			switch (logCode)
			{
			case 1:
				return this.UserOpersCmd(ssId, userId, Money, Money, "Charge", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			case 2:
				return this.UserOpersCmd(ssId, userId, -Money, Money, "GetCash", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			case 3:
				return this.UserOpersCmd(ssId, userId, -Money, Money, "Bet", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			case 4:
				return this.UserOpersCmd(ssId, userId, Money, Money, "Point", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			case 5:
				return this.UserOpersCmd(ssId, userId, Money, Money, "Win", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			case 6:
				return this.UserOpersCmd(ssId, userId, Money, Money, "Cancellation", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			case 7:
				return this.UserOpersCmd(ssId, userId, -Money, Money, "TranAccOut", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			case 8:
				return this.UserOpersCmd(ssId, userId, Money, Money, "TranAccIn", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			case 9:
				return this.UserOpersCmd(ssId, userId, Money, Money, "Give", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			case 10:
				return this.UserOpersCmd(ssId, userId, Money, Money, "Other", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			case 11:
				return this.UserOpersCmd(ssId, userId, Money, Money, "Change", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			case 12:
				return this.UserOpersCmd(ssId, userId, Money, Money, "AgentFH", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
			default:
				return 0;
			}
		}

		private int UserOpersCmd(string ssId, string userId, decimal userMoney, decimal statMoney, string statType, int logLotteryId, int logPlayId, int logSysId, int logCode, int logIsSoft, string messageTitle, string messageContent, string reMark, string STime2)
		{
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand("Global_UserOperTran", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				try
				{
					sqlCommand.Parameters.Add("@LogSsId", SqlDbType.VarChar, 50).Value = ssId;
					sqlCommand.Parameters.Add("@LogUserId", SqlDbType.VarChar, 20).Value = userId;
					sqlCommand.Parameters.Add("@LogUserMoney", SqlDbType.Decimal, 18).Value = userMoney;
					sqlCommand.Parameters.Add("@LogStatMoney", SqlDbType.Decimal, 18).Value = statMoney;
					sqlCommand.Parameters.Add("@LogStatType", SqlDbType.VarChar, 20).Value = statType;
					sqlCommand.Parameters.Add("@LogLotteryId", SqlDbType.Int, 8).Value = logLotteryId;
					sqlCommand.Parameters.Add("@LogPlayId", SqlDbType.Int, 8).Value = logPlayId;
					sqlCommand.Parameters.Add("@LogSysId", SqlDbType.Int, 8).Value = logSysId;
					sqlCommand.Parameters.Add("@LogCode", SqlDbType.Int, 8).Value = logCode;
					sqlCommand.Parameters.Add("@LogIsSoft", SqlDbType.Int, 8).Value = logIsSoft;
					sqlCommand.Parameters.Add("@LogReMark", SqlDbType.VarChar, 200).Value = reMark;
					sqlCommand.Parameters.Add("@LogMessageTitle", SqlDbType.VarChar, 50).Value = messageTitle;
					sqlCommand.Parameters.Add("@LogMessageContent", SqlDbType.VarChar, 200).Value = messageContent;
					sqlCommand.Parameters.Add("@STime2", SqlDbType.VarChar, 50).Value = STime2;
					sqlCommand.Parameters.Add("@output", SqlDbType.VarChar, 200);
					sqlCommand.Parameters["@output"].Direction = ParameterDirection.Output;
					sqlCommand.ExecuteNonQuery();
					object value = sqlCommand.Parameters["@output"].Value;
					sqlConnection.Close();
					result = Convert.ToInt32(value);
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
					result = 0;
				}
			}
			return result;
		}

		private static int UserMoneyStatTran(string UserId, string StatType, decimal StatValue)
		{
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				try
				{
					sqlCommand.CommandText = "select Id From N_UserMoneyStatAll with(nolock) where UserId=" + UserId + " and DateDiff(D,STime,getDate())=0";
					int num = Convert.ToInt32(sqlCommand.ExecuteScalar());
					int num2;
					if (num == 0)
					{
						sqlCommand.CommandText = string.Concat(new object[]
						{
							"insert into N_UserMoneyStatAll(UserId,",
							StatType,
							",STime) values (",
							UserId,
							",",
							StatValue,
							",getdate())"
						});
						num2 = sqlCommand.ExecuteNonQuery();
					}
					else
					{
						sqlCommand.CommandText = string.Concat(new object[]
						{
							"update N_UserMoneyStatAll set ",
							StatType,
							"=",
							StatType,
							"+",
							StatValue,
							" where Id=",
							num
						});
						num2 = sqlCommand.ExecuteNonQuery();
					}
					result = num2;
				}
				catch (Exception)
				{
					result = 0;
				}
				finally
				{
					sqlConnection.Dispose();
					sqlConnection.Close();
				}
			}
			return result;
		}
	}
}
