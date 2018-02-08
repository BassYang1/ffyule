using System;
using System.Data;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class ChargeSetDAL
	{
		public DataTable getChargeSetDataTable(string Id)
		{
			DataTable result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.SqlCmd = "SELECT * from Sys_ChargeSet where Id=" + Id;
				DataTable dataTable = dbOperHandler.GetDataTable();
				result = dataTable;
			}
			return result;
		}
	}
}
