using System;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class AgentFHDAL : ComData
	{
		public bool Exists(string _wherestr)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr;
				if (dbOperHandler.Exist("Act_AgentFHRecord"))
				{
					num = 1;
				}
			}
			return num == 1;
		}

		public void Save(string Adminid, string AgentId, string StartTime, string EndTime, decimal Bet, decimal Total, decimal Per, decimal InMoney, string Remark)
		{
			using (new ComData().Doh())
			{
			}
		}

		public void Delete()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.Delete("Act_AgentFHRecord");
			}
		}

		public void UpdateStime()
		{
			using (new ComData().Doh())
			{
			}
		}
	}
}
