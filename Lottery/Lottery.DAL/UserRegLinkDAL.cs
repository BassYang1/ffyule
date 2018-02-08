using System;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class UserRegLinkDAL : ComData
	{
		public void SaveUserRegLink(string UserId, decimal Point, string Url)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = string.Concat(new object[]
				{
					"UserId=",
					UserId,
					" and Point=",
					Point
				});
				if (dbOperHandler.Exist("N_UserRegLink"))
				{
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = string.Concat(new object[]
					{
						"UserId=",
						UserId,
						" and Point=",
						Point
					});
					dbOperHandler.AddFieldItem("UserId", UserId);
					dbOperHandler.AddFieldItem("Point", Point);
					dbOperHandler.AddFieldItem("Url", Url);
					dbOperHandler.Update("N_UserRegLink");
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.AddFieldItem("UserId", UserId);
					dbOperHandler.AddFieldItem("Point", Point);
					dbOperHandler.AddFieldItem("Url", Url);
					dbOperHandler.Insert("N_UserRegLink");
				}
			}
		}

		public void SaveUserRegLink(string UserId, decimal Point, string YxTime, string Times, string Url)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = string.Concat(new object[]
				{
					"UserId=",
					UserId,
					" and Point=",
					Point
				});
				if (dbOperHandler.Exist("N_UserRegLink"))
				{
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = string.Concat(new object[]
					{
						"UserId=",
						UserId,
						" and Point=",
						Point
					});
					dbOperHandler.AddFieldItem("UserId", UserId);
					dbOperHandler.AddFieldItem("Point", Point);
					dbOperHandler.AddFieldItem("YxTime", YxTime);
					dbOperHandler.AddFieldItem("Times", Times);
					dbOperHandler.AddFieldItem("Url", Url);
					dbOperHandler.Update("N_UserRegLink");
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.AddFieldItem("UserId", UserId);
					dbOperHandler.AddFieldItem("Point", Point);
					dbOperHandler.AddFieldItem("YxTime", YxTime);
					dbOperHandler.AddFieldItem("Times", Times);
					dbOperHandler.AddFieldItem("Url", Url);
					dbOperHandler.Insert("N_UserRegLink");
				}
			}
		}
	}
}
