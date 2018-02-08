using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxSysBank : AdminCenter
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
			case "ajaxStates":
				this.ajaxStates();
				goto IL_148;
			case "ajaxStates2":
				this.ajaxStates2();
				goto IL_148;
			case "ajaxGetUserBankList":
				this.ajaxGetUserBankList();
				goto IL_148;
			case "ajaxGetUserBankAllList":
				this.ajaxGetUserBankAllList();
				goto IL_148;
			case "ajaxUnLock":
				this.ajaxUnLock();
				goto IL_148;
			case "ajaxDel":
				this.ajaxDel();
				goto IL_148;
			case "ajaxGetUserBankLog":
				this.ajaxGetUserBankLog();
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
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text = "";
			if (num2 == 0)
			{
				text += " IsCharge=0";
			}
			else
			{
				text += " IsGetCash=0";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("Sys_Bank");
			string sql = SqlHelp.GetSql0("*", "Sys_Bank", "id", pageSize, num, "asc", text);
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
			object field = this.doh.GetField("Sys_Bank", "IsUsed");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsUsed", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Sys_Bank");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "编辑了Id为" + text + "的银行启用状态");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxStates2()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField("Sys_Bank", "flag");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("flag", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Sys_Bank");
			new LogAdminOperDAL().SaveLog(this.AdminId, text, "会员银行", "编辑了Id为" + text + "的提现银行启用状态");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxGetUserBankList()
		{
			int num = base.Str2Int(base.q("Id"), 0);
			this.Session["returnlink"] = num;
			int num2 = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num3 = base.Str2Int(base.q("flag"), 0);
			string text = "UserId =" + num;
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("N_UserBank");
			string sql = SqlHelp.GetSql0("*", "N_UserBank", "Id", pageSize, num2, "asc", text);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num2, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetUserBankLog()
		{
			int num = base.Str2Int(base.q("Id"), 0);
			this.Session["returnlink"] = num;
			int num2 = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num3 = base.Str2Int(base.q("flag"), 0);
			string text = "UserId =" + num;
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("N_UserBankLog");
			string sql = SqlHelp.GetSql0("*", "N_UserBankLog", "Id", pageSize, num2, "asc", text);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num2, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetUserBankAllList()
		{
			string text = base.q("u");
			string text2 = base.q("payname");
			string text3 = base.q("payaccount");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text4 = "1=1";
			if (!string.IsNullOrEmpty(text))
			{
				text4 = text4 + " and dbo.f_GetUserName(UserId) like '%" + text + "%'";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text4 = text4 + " and payname like '%" + text2 + "%'";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text4 = text4 + " and payaccount like '%" + text3 + "%'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("N_UserBank");
			string sql = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as username,*", "N_UserBank", "Id", pageSize, num, "asc", text4);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxUnLock()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsLock", 0);
			int num = this.doh.Update("N_UserBank");
			new LogAdminOperDAL().SaveLog(this.AdminId, text, "会员银行", "解锁Id为" + text + "的会员银行信息");
			if (num > 0)
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
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			int num = this.doh.Delete("N_UserBank");
			new LogAdminOperDAL().SaveLog(this.AdminId, text, "会员银行", "删除了Id为" + text + "的会员银行信息");
			if (num > 0)
			{
				this._response = base.JsonResult(1, "删除成功");
			}
			else
			{
				this._response = base.JsonResult(0, "删除失败");
			}
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
