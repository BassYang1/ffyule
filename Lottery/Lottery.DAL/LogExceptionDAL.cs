using System;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class LogExceptionDAL : ComData
	{
		public void Save(string _title, string _info)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("Title", _title);
				dbOperHandler.AddFieldItem("Content", _info);
				dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString());
				dbOperHandler.Insert("Log_Exception");
			}
		}

		public void DeleteLogs()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.Delete("Log_Exception");
			}
		}
	}
}
