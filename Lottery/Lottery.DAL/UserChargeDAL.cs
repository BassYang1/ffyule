using System;
using System.Data;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class UserChargeDAL : ComData
	{
		public UserChargeDAL()
		{
			this.site = new conSite().GetSite();
		}

		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("V_ChargeRecord");
				string sql = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,dbo.f_GetUserName(UserId) as UserName,*", "V_ChargeRecord", "Id", _pagesize, _thispage, "desc", _wherestr1);
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

		public int Save(string orderno, string userId, string bankId, string checkCode, decimal money)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				int num = 0;
				if (bankId == "888")
				{
					num = 1;
				}
				DateTime dateTime = new DateTimePubDAL().GetDateTime();
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("SsId", orderno);
				dbOperHandler.AddFieldItem("UserId", userId);
				dbOperHandler.AddFieldItem("BankId", bankId);
				dbOperHandler.AddFieldItem("CheckCode", checkCode);
				dbOperHandler.AddFieldItem("InMoney", money);
				dbOperHandler.AddFieldItem("DzMoney", money);
				dbOperHandler.AddFieldItem("STime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
				dbOperHandler.AddFieldItem("State", num);
				result = dbOperHandler.Insert("N_UserCharge");
			}
			return result;
		}

		public void DeleteLogs()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.Delete("N_UserCharge");
			}
		}

		public void GetListUpChargeJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("N_UserChargeLog");
				string sql = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,dbo.f_GetUserName(UserId) as UserName,dbo.f_GetUserName(ToUserId) as ToUserName,*", "N_UserChargeLog", "Id", _pagesize, _thispage, "desc", _wherestr1);
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

		protected SiteModel site;
	}
}
