using System;
using Lottery.DBUtility;
using Lottery.Entity;

namespace Lottery.DAL
{
	public class UserQuotaDAL : ComData
	{
		public UserQuotaDAL()
		{
			this.site = new conSite().GetSite();
		}

		public bool Exists(string _wherestr)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr;
				if (dbOperHandler.Exist("N_UserQuota"))
				{
					num = 1;
				}
			}
			return num == 1;
		}

		public void SaveUserQuota(string UserId, decimal UserLevel, int ChildNums)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("UserId", UserId);
				dbOperHandler.AddFieldItem("UserLevel", UserLevel);
				dbOperHandler.AddFieldItem("ChildNums", ChildNums);
				dbOperHandler.Insert("N_UserQuota");
			}
		}

		protected SiteModel site;
	}
}
