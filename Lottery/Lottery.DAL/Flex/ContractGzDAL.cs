using System;
using System.Data;
using System.Data.SqlClient;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL.Flex
{
	public class ContractGzDAL : ComData
	{
		public void IsContract(string UserId, string strWhere, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT top 1 Id from N_UserContract where Type=2 and UserId=" + UserId + strWhere, new object[0]);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"}";
				}
				else
				{
					_jsonstr = "{\"result\" :\"0\",\"returnval\" :\"操作成功\"}";
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void IsContract(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT top 1 Id from N_UserContract where Type=2 and UserId=" + UserId, new object[0]);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"}";
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT top 1 UserGroup from N_User where Id=" + UserId, new object[0]);
					dataTable = dbOperHandler.GetDataTable();
					if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 3)
					{
						_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"}";
					}
					else
					{
						_jsonstr = "{\"result\" :\"0\",\"returnval\" :\"操作成功\"}";
					}
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetContractInfo(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT top 1 UserGroup from N_User where Id=" + UserId, new object[0]);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 4)
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("SELECT 4 as [GroupId],[MinMoney],[Money],[IsUsed],[soft] FROM [Act_DayGzSet] where GroupId=4 and IsUsed=0", new object[0]);
						dataTable = dbOperHandler.GetDataTable();
						_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSONNOHTML(dataTable, 0, "recordcount", "table", true) + "}";
					}
					else if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 3)
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("SELECT 3 as [GroupId],[MinMoney],[Money],[IsUsed],[soft] FROM [Act_DayGzSet] where GroupId=3 and IsUsed=0", new object[0]);
						dataTable = dbOperHandler.GetDataTable();
						_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSONNOHTML(dataTable, 0, "recordcount", "table", true) + "}";
					}
					else if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 2)
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("SELECT 2 as [GroupId],[MinMoney],[Money],[IsUsed],[soft] FROM [Act_DayGzSet] where GroupId=2 and IsUsed=0", new object[0]);
						dataTable = dbOperHandler.GetDataTable();
						_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSONNOHTML(dataTable, 0, "recordcount", "table", true) + "}";
					}
					else if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) >= 5)
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("SELECT 5 as groupId", new object[0]);
						dataTable = dbOperHandler.GetDataTable();
						_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSONNOHTML(dataTable, 0, "recordcount", "table", true) + "}";
					}
					else
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("SELECT 0 as groupId,[Type],[ParentId],[UserId],[IsUsed],[STime],b.*\r\n                                            FROM [N_UserContract] a left join [N_UserContractDetail] b on a.Id=b.UcId where Type=2 and UserId=" + UserId, new object[0]);
						dataTable = dbOperHandler.GetDataTable();
						_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSONNOHTML(dataTable, 0, "recordcount", "table", true) + "}";
					}
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string UpdateContractState(string UserId, string state)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Type=2 and UserId=@UserId";
				dbOperHandler.AddConditionParameter("@UserId", UserId);
				dbOperHandler.AddFieldItem("IsUsed", state);
				dbOperHandler.AddFieldItem("STime2", DateTime.Now);
				if (dbOperHandler.Update("N_UserContract") > 0)
				{
					return base.JsonResult(1, "契约签订成功！");
				}
			}
			return base.JsonResult(0, "契约签订失败！");
		}

		public int SaveContract(UserContract list)
		{
			int num = 0;
			if (list.UserContractDetails.Count > 0)
			{
				using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
				{
					sqlConnection.Open();
					SqlCommand sqlCommand = new SqlCommand();
					sqlCommand.Connection = sqlConnection;
					try
					{
						sqlCommand.CommandText = string.Format("delete from [N_UserContract] where Type=2 and UserId={0}", list.UserId);
						sqlCommand.ExecuteScalar();
						sqlCommand.CommandText = string.Format("INSERT INTO [N_UserContract]([Type],[ParentId],[UserId],[IsUsed],[STime]) VALUES (2,{0},{1},0,getdate())", list.ParentId, list.UserId);
						SqlCommand expr_7A = sqlCommand;
						expr_7A.CommandText += " SELECT SCOPE_IDENTITY()";
						num = Convert.ToInt32(sqlCommand.ExecuteScalar());
						sqlCommand.CommandText = string.Format("delete from [N_UserContractDetail] where UcId={0}", num);
						sqlCommand.ExecuteScalar();
						foreach (UserContractDetail current in list.UserContractDetails)
						{
							sqlCommand.CommandText = string.Format("INSERT INTO [N_UserContractDetail]([UcId],[MinMoney],[Money],[Sort]) VALUES ({0},{1},{2},0)", num, current.MinMoney, current.Money);
							sqlCommand.ExecuteScalar();
						}
					}
					catch (Exception ex)
					{
						new LogExceptionDAL().Save("系统异常", ex.Message);
						num = 0;
					}
				}
			}
			return num;
		}

		public void Delete(string ucid)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Id=" + ucid;
				dbOperHandler.Delete("N_UserContract");
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "UcId=" + ucid;
				dbOperHandler.Delete("N_UserContractDetail");
			}
		}
	}
}
