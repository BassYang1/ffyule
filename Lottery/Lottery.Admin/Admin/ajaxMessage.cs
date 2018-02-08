using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxMessage : AdminCenter
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
					goto IL_7C;
				}
				if (operType == "ajaxClear")
				{
					this.ajaxClear();
					goto IL_7C;
				}
			}
			this.DefaultResponse();
			IL_7C:
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
			string text = "Title='即时信息'";
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("N_UserMessage");
			string sql = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as userName,*", "N_UserMessage", "id", pageSize, num, "desc", text);
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

		private void ajaxClear()
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "title='即时信息'";
			int num = this.doh.Delete("N_UserMessage");
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
