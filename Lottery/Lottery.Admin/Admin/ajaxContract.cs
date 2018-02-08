using System;
using System.Data;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxContract : AdminCenter
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
				if (operType == "ajaxGetDetail")
				{
					this.ajaxGetDetail();
					goto IL_A8;
				}
				if (operType == "ajaxGetDetailByUserId")
				{
					this.ajaxGetDetailByUserId();
					goto IL_A8;
				}
				if (operType == "ajaxDel")
				{
					this.ajaxDel();
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
			string text = base.q("type");
			string text2 = base.q("p");
			string text3 = base.q("u");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text4 = "1=1";
			if (!string.IsNullOrEmpty(text))
			{
				text4 = text4 + " and type = " + text;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text4 = text4 + " and dbo.f_GetUserName(ParentId) = '" + text2 + "'";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text4 = text4 + " and dbo.f_GetUserName(UserId) = '" + text3 + "'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("N_UserContract");
			string sql = SqlHelp.GetSql0("*,dbo.f_GetUserName(ParentId) as ParentName,dbo.f_GetUserName(UserId) as UserName", "N_UserContract", "id", pageSize, num, "desc", text4);
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

		private void ajaxGetDetail()
		{
			string str = base.q("id");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text = "UcId=" + str;
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("N_UserContractDetail");
			string sql = SqlHelp.GetSql0("*", "N_UserContractDetail", "id", pageSize, num, "desc", text);
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

		private void ajaxGetDetailByUserId()
		{
			string text = base.q("id");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			DataTable dataTable = new DataTable();
			string text2;
			if (string.IsNullOrEmpty(text))
			{
				text2 = "UcId=0";
			}
			else
			{
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 Id from [N_UserContract] where userid=" + text + " and type=1";
				dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					text2 = "UcId=" + dataTable.Rows[0]["Id"];
				}
				else
				{
					text2 = "UcId=0";
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("N_UserContractDetail");
			string sql = SqlHelp.GetSql0("*", "N_UserContractDetail", "id", pageSize, num, "desc", text2);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			dataTable = this.doh.GetDataTable();
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

		private void ajaxDel()
		{
			string text = base.f("id");
			new ContractDAL().Delete(text);
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "管理员删除了" + text + "契约！");
			this._response = base.JsonResult(1, "成功清空");
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
