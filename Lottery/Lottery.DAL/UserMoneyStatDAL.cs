using System;
using System.Data;
using System.Data.SqlClient;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class UserMoneyStatDAL : ComData
	{
		public UserMoneyStatDAL()
		{
			this.site = new conSite().GetSite();
		}

		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("V_UserMoneyStatAllPoint");
				string sql = SqlHelp.GetSql0("row_number() over (order by STime desc) as rowid,[UserId],dbo.f_GetUserName(UserId) as UserName,STime,Point", "V_UserMoneyStatAllPoint", "STime", _pagesize, _thispage, "desc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
					PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON(dataTable),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public bool Exists(string _wherestr)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr;
				if (dbOperHandler.Exist("N_UserMoneyStatAll"))
				{
					num = 1;
				}
			}
			return num == 1;
		}

		public void InsertUserMoneyStat(string _adminid)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("UserId", _adminid);
				dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				dbOperHandler.Insert("N_UserMoneyStatAll");
			}
		}

		public void AddUserMoneyStat(string _sql, string _type, decimal _money)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _sql;
				dbOperHandler.Add("N_UserMoneyStatAll", _type, _money);
			}
		}

		public void Delete()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.Delete("N_UserMoneyStatAll");
			}
		}

		public void Save(SqlCommand cmd, int _userId, string _statType, decimal _statValue)
		{
			try
			{
				SqlParameter[] values = new SqlParameter[]
				{
					new SqlParameter("@UserId", _userId),
					new SqlParameter("@statType", _statType),
					new SqlParameter("@statValue", _statValue)
				};
				cmd.CommandText = "select Id From N_UserMoneyStatAll with(nolock) where UserId=" + _userId + " and DateDiff(D,STime,getDate())=0";
				if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
				{
					cmd.CommandText = "insert into N_UserMoneyStatAll(UserId," + _statType + ",STime) values (@UserId,@statValue,getdate())";
					cmd.Parameters.AddRange(values);
					cmd.ExecuteNonQuery();
					cmd.Parameters.Clear();
				}
				else
				{
					cmd.CommandText = string.Concat(new string[]
					{
						"update N_UserMoneyStatAll set ",
						_statType,
						"=",
						_statType,
						"+@statValue where UserId=@UserId and DateDiff(D,STime,getDate())=0"
					});
					cmd.Parameters.AddRange(values);
					cmd.ExecuteNonQuery();
					cmd.Parameters.Clear();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected SiteModel site;
	}
}
