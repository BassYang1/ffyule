using System;
using System.Web;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class LogAdminOperDAL : ComData
	{
		public void SaveLog(string adminid, string userid, string title, string info)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				IPScaner iPScaner = new IPScaner();
				iPScaner.DataPath = HttpContext.Current.Server.MapPath("/statics/database/QQWry.Dat");
				iPScaner.IP = IPHelp.ClientIP;
				//iPScaner.IPLocation() + iPScaner.ErrMsg;
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("AdminId", adminid);
				dbOperHandler.AddFieldItem("UserId", userid);
				dbOperHandler.AddFieldItem("OperTitle", title);
				dbOperHandler.AddFieldItem("OperInfo", info);
				dbOperHandler.AddFieldItem("OperTime", DateTime.Now.ToString());
				dbOperHandler.AddFieldItem("OperIP", IPHelp.ClientIP);
				dbOperHandler.Insert("Log_AdminOper");
			}
		}

		public void DeleteLogs()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.Delete("Log_AdminOper");
			}
		}
	}
}
