using System;
using System.Data;
using System.Data.SqlClient;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class UserMessageDAL : ComData
	{
		public void Save(string _adminid, string _title, string _info)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("UserId", _adminid);
				dbOperHandler.AddFieldItem("Title", _title);
				dbOperHandler.AddFieldItem("Msg", _info);
				dbOperHandler.AddFieldItem("Second", 8);
				dbOperHandler.Insert("N_UserMessage");
			}
		}

		public void Save(SqlCommand cmd, int _userId, string _title, string _msg)
		{
			try
			{
				SqlParameter[] values = new SqlParameter[]
				{
					new SqlParameter("@UserId", _userId),
					new SqlParameter("@Title", _title),
					new SqlParameter("@Msg", _msg)
				};
				cmd.CommandText = "insert into N_UserMessage(UserId,Title,Msg,Second) values (@UserId,@Title,@Msg,8)";
				cmd.Parameters.AddRange(values);
				cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void GetListJSON(ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 100 UserId,Msg from N_UserMessage with(nolock) where IsRead=0 order by Id desc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
				}
				else
				{
					_jsonstr = "";
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void Update(DataTable dt)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = "Id=@Id";
					dbOperHandler.AddConditionParameter("@Id", dt.Rows[i]["Id"].ToString());
					dbOperHandler.AddFieldItem("IsRead", "1");
					dbOperHandler.Update("N_UserMessage");
				}
			}
		}

		public void Delete()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.Delete("N_UserMessage");
			}
		}
	}
}
