using System;
using System.Data;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class UserBankDAL : ComData
	{
		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("N_UserBank");
				string sql = SqlHelp.GetSql0("row_number() over (order by Id asc) as rowid,'************'+substring(Payaccount,len(Payaccount)-3,4) as tPayaccount,substring(PayName,1,1)+'**' as tPayName,*", "N_UserBank", "Id", _pagesize, _thispage, "asc", _wherestr1);
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

		public int Save(string userId, string PayMethod, string PayBank, string PayBankAddress, string PayAccount, string PayName)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				new DateTimePubDAL().GetDateTime();
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("UserId", userId);
				dbOperHandler.AddFieldItem("PayMethod", PayMethod);
				dbOperHandler.AddFieldItem("PayBank", PayBank);
				dbOperHandler.AddFieldItem("PayBankAddress", PayBankAddress);
				dbOperHandler.AddFieldItem("PayAccount", PayAccount);
				dbOperHandler.AddFieldItem("PayName", PayName);
				dbOperHandler.AddFieldItem("AddTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				dbOperHandler.AddFieldItem("IsLock", 0);
				result = dbOperHandler.Insert("N_UserBank");
			}
			return result;
		}

		public bool Exists(string _wherestr)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr;
				if (dbOperHandler.Exist("N_UserBank"))
				{
					num = 1;
				}
			}
			return num == 1;
		}

		public void Delete(string Id)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=" + Id;
				dbOperHandler.Delete("N_UserBank");
			}
		}
	}
}
