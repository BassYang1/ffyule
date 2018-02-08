using System;
using System.Data;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class DateTimePubDAL : ComData
	{
		public DateTime GetDateTime()
		{
			DateTime result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT CONVERT(VARCHAR(100),GETDATE(),120) as d";
				DataTable dataTable = dbOperHandler.GetDataTable();
				result = Convert.ToDateTime(dataTable.Rows[0]["d"]);
			}
			return result;
		}
	}
}
