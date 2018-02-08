using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.WebApp
{
	public class ajaxAgentFH : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("master", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "ajaxGetAgentFHRecord")
				{
					this.ajaxGetAgentFHRecord();
					goto IL_64;
				}
				if (operType == "ajaxGetContractFHRecord")
				{
					this.ajaxGetContractFHRecord();
					goto IL_64;
				}
			}
			this.DefaultResponse();
			IL_64:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetAgentFHRecord()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = this.StartTime;
			}
			if (text2.Trim().Length == 0)
			{
				text2 = this.EndTime;
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text3 = "UserId=" + this.AdminId;
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text4 = text3;
				text3 = string.Concat(new string[]
				{
					text4,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text3;
			int totalCount = this.doh.Count("V_AgentFHRecord");
			string sql = SqlHelp.GetSql0("*", "V_AgentFHRecord a", "id", pageSize, num, "desc", text3);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(80, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetContractFHRecord()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = this.StartTime;
			}
			if (text2.Trim().Length == 0)
			{
				text2 = this.EndTime;
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text3 = "AgentId=99 and dbo.f_GetUserCode(UserId) like '%," + this.AdminId + ",%'";
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text4 = text3;
				text3 = string.Concat(new string[]
				{
					text4,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text3;
			int totalCount = this.doh.Count("V_AgentFHRecord");
			string sql = SqlHelp.GetSql0("*", "V_AgentFHRecord a", "id", pageSize, num, "desc", text3);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(80, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
