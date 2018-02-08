using System;
using System.Data;
using Lottery.DBUtility;

namespace Lottery.DAL.Flex
{
	public static class PublicDAL
	{
		public static DateTime GetServerTime()
		{
			string value = "";
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				string sqlCmd = "select GETDATE() AS ServerTime";
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sqlCmd;
				DataTable dataTable = dbOperHandler.GetDataTable();
				value = dataTable.Rows[0]["ServerTime"].ToString();
				dataTable.Clear();
				dataTable.Dispose();
			}
			return Convert.ToDateTime(value);
		}
	}
}
