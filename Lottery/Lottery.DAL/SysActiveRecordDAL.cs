using System;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class SysActiveRecordDAL : ComData
	{
		public bool Exists(string _wherestr)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr;
				if (dbOperHandler.Exist("Act_ActiveRecord"))
				{
					num = 1;
				}
			}
			return num == 1;
		}

		public void SaveLog(string _adminid, string _type, string _name, decimal _money, string _remark)
		{
			string clientIP = IPHelp.ClientIP;
			string mNum = MachineCode.getMNum();
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("UserId", _adminid);
				dbOperHandler.AddFieldItem("ActiveType", _type);
				dbOperHandler.AddFieldItem("ActiveName", _name);
				dbOperHandler.AddFieldItem("InMoney", _money);
				dbOperHandler.AddFieldItem("Remark", _remark);
				dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				dbOperHandler.AddFieldItem("CheckIp", clientIP);
				dbOperHandler.AddFieldItem("CheckMachine", mNum);
				dbOperHandler.Insert("Act_ActiveRecord");
			}
		}

		public void DeleteLogs()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.Delete("Act_ActiveRecord");
			}
		}
	}
}
