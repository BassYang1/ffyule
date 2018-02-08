using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxAutoRank : AdminCenter
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
			if (operType != null)
			{
				if (operType == "ajaxGetList")
				{
					this.ajaxGetList();
					goto IL_A8;
				}
				if (operType == "ajaxStates")
				{
					this.ajaxStates();
					goto IL_A8;
				}
				if (operType == "ajaxDel")
				{
					this.ajaxDel();
					goto IL_A8;
				}
				if (operType == "ajaxSave")
				{
					this.ajaxSave();
					goto IL_A8;
				}
			}
			this.DefaultResponse();
			IL_A8:
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
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("Sys_AutoRank");
			string sql = SqlHelp.GetSql0("*", "Sys_AutoRank", "id", pageSize, num, "asc", text);
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
			object field = this.doh.GetField("Sys_AutoRank", "IsUsed");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsUsed", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Sys_AutoRank");
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
			int num = this.doh.Delete("Sys_AutoRank");
			if (num > 0)
			{
				this._response = base.JsonResult(1, "操作成功");
			}
			else
			{
				this._response = base.JsonResult(0, "操作失败");
			}
		}

		private void ajaxSave()
		{
			string fieldValue = base.f("rank");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=1";
			this.doh.AddFieldItem("AutoRanking", fieldValue);
			int num = this.doh.Update("Sys_Info");
			if (num > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
