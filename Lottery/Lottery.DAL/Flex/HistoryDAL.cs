using System;
using System.Data;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL.Flex
{
	public class HistoryDAL : ComData
	{
		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int num = dbOperHandler.CountId("Flex_History");
				string sql = SqlHelp.GetSql0(num + " as totalcount,row_number() over (order by STime desc) as rowid,case when moneychange>0 then Convert(varchar(20),moneychange) else '---' end inmoney,case when moneychange<0 then Convert(varchar(20),moneychange) else '---' end outmoney,*", "Flex_History", "Id", _pagesize, _thispage, "desc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}
	}
}
