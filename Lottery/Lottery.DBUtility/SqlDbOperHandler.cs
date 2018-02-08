using System;
using System.Data.SqlClient;

namespace Lottery.DBUtility
{
	public class SqlDbOperHandler : DbOperHandler
	{
		public SqlDbOperHandler(SqlConnection _conn)
		{
			this.conn = _conn;
			this.dbType = DatabaseType.SqlServer;
			this.conn.Open();
			this.cmd = this.conn.CreateCommand();
			this.da = new SqlDataAdapter();
		}

		protected override void GenParameters()
		{
			SqlCommand sqlCommand = (SqlCommand)this.cmd;
			if (this.alFieldItems.Count > 0)
			{
				for (int i = 0; i < this.alFieldItems.Count; i++)
				{
					sqlCommand.Parameters.AddWithValue("@para" + i.ToString(), ((DbKeyItem)this.alFieldItems[i]).fieldValue.ToString());
				}
			}
			if (this.alSqlCmdParameters.Count > 0)
			{
				for (int j = 0; j < this.alSqlCmdParameters.Count; j++)
				{
					sqlCommand.Parameters.AddWithValue(((DbKeyItem)this.alSqlCmdParameters[j]).fieldName.ToString(), ((DbKeyItem)this.alSqlCmdParameters[j]).fieldValue.ToString());
				}
			}
			if (this.alConditionParameters.Count > 0)
			{
				for (int k = 0; k < this.alConditionParameters.Count; k++)
				{
					sqlCommand.Parameters.AddWithValue(((DbKeyItem)this.alConditionParameters[k]).fieldName.ToString(), ((DbKeyItem)this.alConditionParameters[k]).fieldValue.ToString());
				}
			}
		}
	}
}
