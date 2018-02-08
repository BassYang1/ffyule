using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxUserlevel : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.CheckFormUrl())
			{
				base.Response.End();
			}
			base.Admin_Load("master", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			switch (operType)
			{
			case "ajaxGetList":
				this.ajaxGetList();
				goto IL_116;
			case "ajaxGetQuotaList":
				this.ajaxGetQuotaList();
				goto IL_116;
			case "ajaxCreate":
				this.ajaxCreate();
				goto IL_116;
			case "ajaxCreateAll":
				this.ajaxCreateAll();
				goto IL_116;
			case "ajaxGetUserGroupQuotaList":
				this.ajaxGetUserGroupQuotaList();
				goto IL_116;
			case "ajaxGetUserPointQuotaList":
				this.ajaxGetUserPointQuotaList();
				goto IL_116;
			}
			this.DefaultResponse();
			IL_116:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetList()
		{
			string text = base.q("keys");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			this.doh.Reset();
			this.doh.ConditionExpress = "";
			int totalCount = this.doh.Count("N_UserLevel");
			string sql = SqlHelp.GetSql0("Id,CONVERT(DECIMAL(10,2),CONVERT(DECIMAL(10,2),[Point])/10) as Point,Title,Bonus,Score,Times,Sort", "N_UserLevel", "Id", pageSize, num, "desc", "");
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetUserGroupQuotaList()
		{
			string text = base.q("group");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			string text2 = "1=1";
			if (!string.IsNullOrEmpty(text))
			{
				text2 = text2 + " and [group] = " + text;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("N_UserGroupQuota");
			this.doh.Reset();
			this.doh.SqlCmd = SqlHelp.GetSql0("[Id],[Group],(select name from N_UserGroup where Id=a.Group) as GroupName,[ToGroup],(select name from N_UserGroup where Id=a.ToGroup) as ToGroupName,[ChildNums],[Sort]", "N_UserGroupQuota a", "Id", pageSize, num, "asc", text2);
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetUserPointQuotaList()
		{
			string text = base.q("point");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			string text2 = "1=1";
			if (!string.IsNullOrEmpty(text))
			{
				text2 = text2 + " and [point] = " + text;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("N_UserPointQuota");
			this.doh.Reset();
			this.doh.SqlCmd = SqlHelp.GetSql0("[Id],[Point],[ChildNums],[Sort]", "N_UserPointQuota", "Id", pageSize, num, "asc", text2);
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetQuotaList()
		{
			string text = base.q("uname");
			string text2 = base.q("ulevel");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			string text3 = "1=1";
			if (!string.IsNullOrEmpty(text))
			{
				text3 = text3 + " and dbo.f_GetUserName(UserId) LIKE '%" + text + "%'";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text3 = text3 + " and UserLevel = " + text2;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text3;
			int totalCount = this.doh.Count("V_UserQuota");
			this.doh.Reset();
			this.doh.SqlCmd = SqlHelp.GetSql0("*", "V_UserQuota", "Id", pageSize, num, "desc", text3);
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxCreate()
		{
			string text = base.q("userId");
			string value = base.q("num");
			if (!string.IsNullOrEmpty(text))
			{
				if (Convert.ToInt32(value) >= 0)
				{
					this.doh.Reset();
					this.doh.SqlCmd = "select Id,Point from N_User with(nolock) where Id=" + text + " and IsEnable=0 and IsDel=0";
					DataTable dataTable = this.doh.GetDataTable();
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						this.doh.Reset();
						this.doh.SqlCmd = "SELECT [Point] FROM [N_UserLevel] where Point>=125.00 and Point<=" + Convert.ToDecimal(dataTable.Rows[i]["Point"]) + " order by [Point] desc";
						DataTable dataTable2 = this.doh.GetDataTable();
						for (int j = 0; j < dataTable2.Rows.Count; j++)
						{
							if (!new UserQuotaDAL().Exists(string.Concat(new object[]
							{
								"UserId=",
								dataTable.Rows[i]["Id"].ToString(),
								" and UserLevel=",
								Convert.ToDecimal(dataTable2.Rows[j]["Point"]) / 10m
							})))
							{
								new UserQuotaDAL().SaveUserQuota(dataTable.Rows[i]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[j]["Point"]) / 10m, Convert.ToInt32(value));
							}
						}
						new LogAdminOperDAL().SaveLog(this.AdminId, dataTable.Rows[i]["Id"].ToString(), "会员管理", "自动生成了Id为" + dataTable.Rows[i]["Id"] + "的会员的配额");
					}
				}
				this._response = base.JsonResult(1, "配额生成成功！");
			}
			else
			{
				this._response = base.JsonResult(0, "请输入会员编号再生成配额！");
			}
		}

		private void ajaxCreateAll()
		{
			string value = base.q("num2");
			if (!string.IsNullOrEmpty(value))
			{
				if (Convert.ToInt32(value) >= 0)
				{
					this.doh.Reset();
					this.doh.SqlCmd = "select Id,Point from N_User with(nolock) where Id not in (select UserId from N_UserQuota with(nolock) group by UserId) and IsEnable=0 and IsDel=0";
					DataTable dataTable = this.doh.GetDataTable();
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						this.doh.Reset();
						this.doh.SqlCmd = "SELECT [Point] FROM [N_UserLevel] where Point>=125.00 and Point<=" + Convert.ToDecimal(dataTable.Rows[i]["Point"]) + " order by [Point] desc";
						DataTable dataTable2 = this.doh.GetDataTable();
						for (int j = 0; j < dataTable2.Rows.Count; j++)
						{
							if (!new UserQuotaDAL().Exists(string.Concat(new object[]
							{
								"UserId=",
								dataTable.Rows[i]["Id"].ToString(),
								" and UserLevel=",
								Convert.ToDecimal(dataTable2.Rows[j]["Point"]) / 10m
							})))
							{
								new UserQuotaDAL().SaveUserQuota(dataTable.Rows[i]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[j]["Point"]) / 10m, Convert.ToInt32(value));
							}
						}
						new LogAdminOperDAL().SaveLog(this.AdminId, dataTable.Rows[i]["Id"].ToString(), "会员管理", "自动生成了Id为" + dataTable.Rows[i]["Id"] + "的会员的配额");
					}
				}
				this._response = base.JsonResult(1, "配额生成成功！");
			}
			else
			{
				this._response = base.JsonResult(0, "请输入配额数量！");
			}
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
