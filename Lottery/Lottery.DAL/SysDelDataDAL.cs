using System;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class SysDelDataDAL : ComData
	{
		public void DeleteUserBet(string d1, string d2)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = string.Concat(new string[]
				{
					"Stime2<='",
					d2,
					"' and Stime2>='",
					d1,
					"'"
				});
				dbOperHandler.Delete("N_UserBet");
			}
		}

		public void DeleteUserGetCash(string d1, string d2)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = string.Concat(new string[]
				{
					"Stime<='",
					d2,
					"' and Stime>='",
					d1,
					"'"
				});
				dbOperHandler.Delete("N_UserGetCash");
			}
		}

		public void DeleteUserMoneyLog(string d1, string d2)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = string.Concat(new string[]
				{
					"Stime<='",
					d2,
					"' and Stime>='",
					d1,
					"'"
				});
				dbOperHandler.Delete("N_UserMoneyLog");
			}
		}

		public void DeleteUserMoneyStat(string d1, string d2)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = string.Concat(new string[]
				{
					"Stime<='",
					d2,
					"' and Stime>='",
					d1,
					"'"
				});
				dbOperHandler.Delete("N_UserMoneyStatAll");
			}
		}

		public void DeleteLotteryData(string d1, string d2)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = string.Concat(new string[]
				{
					"Stime<='",
					d2,
					"' and Stime>='",
					d1,
					"'"
				});
				dbOperHandler.Delete("Sys_LotteryData");
			}
		}

		public void DeleteUserLogs(string d1, string d2)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = string.Concat(new string[]
				{
					"LoginTime<='",
					d2,
					"' and LoginTime>='",
					d1,
					"'"
				});
				dbOperHandler.Delete("Log_UserLogin");
			}
		}

		public void DeleteLogs(string d1, string d2)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = string.Concat(new string[]
				{
					"Stime<='",
					d2,
					"' and Stime>='",
					d1,
					"'"
				});
				dbOperHandler.Delete("Log_Sys");
			}
		}

		public void DeleteUserBetZh(string d1, string d2)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = string.Concat(new string[]
				{
					"Stime<='",
					d2,
					"' and Stime>='",
					d1,
					"'"
				});
				dbOperHandler.Delete("N_UserBetZh");
			}
		}

		public void ClearUser()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "datediff(minute,ontime ,getdate())>5";
				if (dbOperHandler.Exist("N_User"))
				{
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = "datediff(minute,ontime ,getdate())>5";
					dbOperHandler.AddFieldItem("IsOnline", "0");
					dbOperHandler.AddFieldItem("SessionId", Guid.NewGuid().ToString().Replace("-", ""));
					dbOperHandler.Update("N_User");
				}
			}
		}
	}
}
