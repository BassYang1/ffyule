using System;
using System.Data;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class UserEmailDAL : ComData
	{
		public UserEmailDAL()
		{
			this.site = new conSite().GetSite();
		}

		public void GetListCount(string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int num = dbOperHandler.Count("N_UserEmail");
				_jsonstr = "{\"result\" :\"1\",\"count\" :\"" + num + "\"}";
			}
		}

		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, string _userId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Id=@Id";
				dbOperHandler.AddConditionParameter("@Id", _userId);
				string str = string.Concat(dbOperHandler.GetField("N_User", "ParentId"));
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("N_UserEmail");
				string sql = SqlHelp.GetSql0("row_number() over (order by STime desc) as rowid," + str + " as parentid,dbo.f_GetUserName(SendId) as SendName,dbo.f_GetUserName(ReceiveId) as ReceiveName,*", "N_UserEmail", "STime", _pagesize, _thispage, "desc", _wherestr1);
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

		public int Save(string SendId, string ReceiveId, string Title, string Contents)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("SendId", SendId);
				dbOperHandler.AddFieldItem("ReceiveId", ReceiveId);
				dbOperHandler.AddFieldItem("Title", Title);
				dbOperHandler.AddFieldItem("Contents", Contents);
				dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				dbOperHandler.AddFieldItem("IsRead", "0");
				result = dbOperHandler.Insert("N_UserEmail");
			}
			return result;
		}

		public void UpdateState(string _id)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _id);
				dbOperHandler.AddFieldItem("IsRead", "1");
				dbOperHandler.Update("N_UserEmail");
			}
		}

		public void Deletes(string _id)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _id);
				dbOperHandler.AddFieldItem("IsDel", "1");
				dbOperHandler.Update("N_UserEmail");
			}
		}

		protected SiteModel site;
	}
}
