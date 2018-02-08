using System;
using System.Data;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class LogUserLoginDAL : ComData
	{
		public void Save(string _userid, string _address, string _browser, string _system, string _IP)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("UserId", _userid);
				dbOperHandler.AddFieldItem("Address", _address);
				dbOperHandler.AddFieldItem("Browser", _browser);
				dbOperHandler.AddFieldItem("System", _system);
				dbOperHandler.AddFieldItem("LoginTime", DateTime.Now.ToString());
				dbOperHandler.AddFieldItem("IP", _IP);
				dbOperHandler.Insert("Log_UserLogin");
			}
		}

		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("Log_UserLogin");
				string sql = SqlHelp.GetSql0("*", "Log_UserLogin", "LoginTime", _pagesize, _thispage, "desc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
					PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void Deletes()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.Delete("Log_UserLogin");
			}
		}
	}
}
