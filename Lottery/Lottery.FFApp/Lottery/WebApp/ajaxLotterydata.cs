using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.WebApp
{
	public class ajaxLotterydata : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "ajaxGetList")
				{
					this.ajaxGetList();
					goto IL_3D;
				}
			}
			this.DefaultResponse();
			IL_3D:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetList()
		{
			string text = base.q("d1");
			string text2 = base.q("u");
			int num = base.Str2Int(base.q("gId"), 0);
			int num2 = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num3 = base.Str2Int(base.q("flag"), 0);
			string text3 = "[Type]=" + num3;
			if (!string.IsNullOrEmpty(text))
			{
				text3 = text3 + "and Convert(varchar(10),STime,120)='" + text + "'";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text3 = text3 + "and Title like '" + text2 + "%'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text3;
			int totalCount = this.doh.Count("Sys_LotteryData");
			string sql = SqlHelp.GetSql0("Id,Title,Number,NumberAll,Total,OpenTime,STime", "Sys_LotteryData", "STime", pageSize, num2, "desc", text3);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"jsonpCallback({\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(num3, "js", 2, totalCount, pageSize, num2, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"})"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
