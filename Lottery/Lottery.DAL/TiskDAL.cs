using System;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL
{
	public class TiskDAL : ComData
	{
		public void TiskOper()
		{
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					sqlDataAdapter.SelectCommand.CommandText = "SELECT * FROM [Sys_TaskSet] where getdate()>=StartTime and getdate()<=EndTime and IsUsed=1 and (BeforeTime is null or Convert(varchar(10),BeforeTime,120)<>Convert(varchar(10),getdate(),120)) order by Sort asc";
					DataTable dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						for (int i = 0; i < dataTable.Rows.Count; i++)
						{
							sqlCommand.CommandText = dataTable.Rows[i]["StrSql"].ToString();
							sqlCommand.ExecuteNonQuery();
							sqlCommand.CommandText = "update Sys_TaskSet set BeforeTime=getdate() where id=" + dataTable.Rows[i]["Id"].ToString();
							sqlCommand.ExecuteNonQuery();
						}
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
				}
			}
		}
	}
}
