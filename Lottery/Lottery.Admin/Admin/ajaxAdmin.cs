using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxAdmin : AdminCenter
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
				goto IL_148;
			case "ajaxGetList2":
				this.ajaxGetList2();
				goto IL_148;
			case "ajaxStates":
				this.ajaxStates();
				goto IL_148;
			case "ajaxDel":
				this.ajaxDel();
				goto IL_148;
			case "ajaxUnLock":
				this.ajaxUnLock();
				goto IL_148;
			case "ajaxGetRoleList":
				this.ajaxGetRoleList();
				goto IL_148;
			case "ajaxRoleStates":
				this.ajaxRoleStates();
				goto IL_148;
			case "ajaxRoleDel":
				this.ajaxRoleDel();
				goto IL_148;
			}
			this.DefaultResponse();
			IL_148:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetList()
		{
			string text = base.q("type");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text2 = "GroupId=1";
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("Sys_Admin");
			string sql = SqlHelp.GetSql0("*,(select Name from Sys_Role where Id=a.RoleId) as RoleName", "Sys_Admin a", "id", pageSize, num, "asc", text2);
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

		private void ajaxGetList2()
		{
			string text = base.q("type");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text2 = "GroupId=2";
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("Sys_Admin");
			string sql = SqlHelp.GetSql0("*,(select Name from Sys_Role where Id=a.RoleId) as RoleName", "Sys_Admin a", "id", pageSize, num, "asc", text2);
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

		private void ajaxStates()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField("Sys_Admin", "flag");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("flag", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Sys_Admin");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "编辑了Id为" + text + "的管理员");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxDel()
		{
			string str = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + str;
			int num = this.doh.Delete("Sys_Admin");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "删除Id为" + str + "的管理员！");
			if (num > 0)
			{
				this._response = base.JsonResult(1, "操作成功");
			}
			else
			{
				this._response = base.JsonResult(0, "操作失败");
			}
		}

		private void ajaxUnLock()
		{
			string s = base.f("p");
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id";
			this.doh.AddConditionParameter("@Id", this.AdminId);
			object field = this.doh.GetField("Sys_Admin", "Password");
			if (field != null)
			{
				if (field.ToString().ToLower() == MD5.Last64(MD5.Lower32(s)))
				{
					this._response = base.JsonResult(1, "解锁成功");
				}
				else
				{
					this._response = base.JsonResult(0, "解锁失败");
				}
			}
			else
			{
				this._response = base.JsonResult(0, "解锁失败");
			}
		}

		private void ajaxGetRoleList()
		{
			string text = base.q("type");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text2 = "";
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("Sys_Role");
			string sql = SqlHelp.GetSql0("*", "Sys_Role", "id", pageSize, num, "asc", text2);
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

		private void ajaxRoleStates()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField("Sys_Role", "IsUsed");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsUsed", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Sys_Role");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "编辑了Id为" + text + "的管理员角色");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxRoleDel()
		{
			string str = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + str;
			int num = this.doh.Delete("Sys_Role");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "删除Id为" + str + "的管理员角色！");
			if (num > 0)
			{
				this._response = base.JsonResult(1, "操作成功");
			}
			else
			{
				this._response = base.JsonResult(0, "操作失败");
			}
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
