using System;
using System.Data;
using System.Data.SqlClient;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class UserMoneyLogDAL : ComData
	{
		public bool Exists(string _wherestr)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr;
				if (dbOperHandler.Exist("N_UserMoneyLog"))
				{
					num = 1;
				}
			}
			return num == 1;
		}

		public void Delete()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.Delete("N_UserMoneyLog");
			}
		}

		public decimal GetMaxMoney(int userId)
		{
			decimal result = 0m;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 1 ISNULL(MoneyAfter,0) as MoneyAfter from N_UserMoneyLog with(nolock) where userId=" + userId + " order by Id desc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					result = Convert.ToDecimal(dataTable.Rows[0]["MoneyAfter"].ToString());
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = "id=@id";
					dbOperHandler.AddConditionParameter("@id", userId);
					object field = dbOperHandler.GetField("N_User", "Money");
					result = Convert.ToDecimal(field);
				}
			}
			return result;
		}

		public void Save(SqlCommand cmd, int _userId, int _LotteryId, int _PlayId, int _SysId, decimal _MoneyChange, int _Code, int _IsSoft, string _Remark)
		{
			try
			{
				decimal maxMoney = this.GetMaxMoney(_userId);
				SqlParameter[] values = new SqlParameter[]
				{
					new SqlParameter("@UserId", _userId),
					new SqlParameter("@LotteryId", _LotteryId),
					new SqlParameter("@PlayId", _PlayId),
					new SqlParameter("@SysId", _SysId),
					new SqlParameter("@MoneyChange", _MoneyChange),
					new SqlParameter("@MoneyAgo", maxMoney),
					new SqlParameter("@MoneyAfter", maxMoney + _MoneyChange),
					new SqlParameter("@IsOk", 1),
					new SqlParameter("@Code", _Code),
					new SqlParameter("@IsSoft", _IsSoft),
					new SqlParameter("@Remark", _Remark)
				};
				cmd.CommandText = string.Format("insert into N_UserMoneyLog(UserId,LotteryId,PlayId,SysId,MoneyChange,MoneyAgo,MoneyAfter,STime,IsOk,Code,IsSoft,Remark) \r\n                    values(@UserId,@LotteryId,@PlayId,@SysId,@MoneyChange,@MoneyAgo,@MoneyAfter,getdate(),@IsOk,@Code,@IsSoft,@Remark)", new object[0]);
				cmd.Parameters.AddRange(values);
				cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void AgencyPoint(SqlCommand cmd, int UserId, int LotteryId, int PlayId, int BetId, decimal BetMoney)
		{
			cmd.CommandText = "select ParentId from N_User with(nolock) where Id=" + UserId;
			int num = Convert.ToInt32(cmd.ExecuteScalar());
			if (num != 0)
			{
				DataTable dataTable = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter
                {
                    SelectCommand = cmd

                };
                sda.SelectCommand.CommandText = "select ParentId,Point,Money from N_User with(nolock) where Id=" + num.ToString();
                sda.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					int num2 = Convert.ToInt32(dataRow["Point"]);
					cmd.CommandText = "select top 1 MoneyAfter From N_UserMoneyLog with(nolock) Where UserId=" + num.ToString() + " order by Id desc";
					decimal d = Convert.ToDecimal(cmd.ExecuteScalar());
					if (d == 0m)
					{
						d = Convert.ToDecimal(dataRow["Money"]);
					}
					cmd.CommandText = "select Point from N_User with(nolock) where Id=" + UserId;
					int num3 = Convert.ToInt32(cmd.ExecuteScalar());
					if (num2 >= num3)
					{
						decimal num4 = BetMoney * Convert.ToDecimal(num2 - num3) / 1000m;
						new UserMoneyLogDAL().Save(cmd, num, LotteryId, PlayId, BetId, num4, 7, 99, "下级返点");
						new UserMoneyStatDAL().Save(cmd, num, "Point", num4);
						this.AgencyPoint(cmd, num, LotteryId, PlayId, BetId, BetMoney);
						dataTable.Dispose();
					}
				}
			}
		}
	}
}
