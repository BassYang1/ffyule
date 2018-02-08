using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxLottery : AdminCenter
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
			case "ajaxGetLotteryList":
				this.ajaxGetLotteryList();
				goto IL_182;
			case "ajaxStates":
				this.ajaxStates();
				goto IL_182;
			case "ajaxAutoStates":
				this.ajaxAutoStates();
				goto IL_182;
			case "ajaxGetPlayBigList":
				this.ajaxGetPlayBigList();
				goto IL_182;
			case "ajaxPlayBigStates":
				this.ajaxPlayBigStates();
				goto IL_182;
			case "ajaxGetPlaySmallList":
				this.ajaxGetPlaySmallList();
				goto IL_182;
			case "ajaxPlaySmallStates":
				this.ajaxPlaySmallStates();
				goto IL_182;
			case "ajaxGetTimeList":
				this.ajaxGetTimeList();
				goto IL_182;
			case "ajaxGetAutoUrlList":
				this.ajaxGetAutoUrlList();
				goto IL_182;
			case "ajaxGetLotteryCheckList":
				this.ajaxGetLotteryCheckList();
				goto IL_182;
			}
			this.DefaultResponse();
			IL_182:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetLotteryList()
		{
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text = "";
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("Sys_Lottery");
			string sql = SqlHelp.GetSql0("(select count(*) from Sys_PlayBigType where typeId=a.ltype) as childcount,case AutoUrl when 0 then '系统自动' else (select title from Sys_lotteryUrl where Id=a.AutoUrl) end AutoName,*", "Sys_Lottery a", "Id", pageSize, num, "asc", text);
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
			object field = this.doh.GetField("Sys_Lottery", "IsOpen");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsOpen", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Sys_Lottery");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "编辑了Id为" + text + "的彩种启用状态");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxAutoStates()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField("Sys_Lottery", "IsAuto");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsAuto", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Sys_Lottery");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxGetPlayBigList()
		{
			string text = base.q("type");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text2 = "";
			if (!string.IsNullOrEmpty(text) && text != "0")
			{
				text2 = text2 + " TypeId=" + text;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("Sys_PlayBigType");
			string sql = SqlHelp.GetSql0("(select count(*) from Sys_PlaySmallType where Radio=a.Id) as childcount,*", "Sys_PlayBigType a", "Id", pageSize, num, "asc", text2);
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

		private void ajaxPlayBigStates()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField("Sys_PlayBigType", "IsOpen");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsOpen", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Sys_PlayBigType");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "编辑了Id为" + text + "的玩法类别启用状态");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxGetPlaySmallList()
		{
			string text = base.q("type");
			string text2 = base.q("play");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text3 = "1=1";
			if (!string.IsNullOrEmpty(text) && text != "0")
			{
				text3 = text3 + " and LotteryId=" + text;
			}
			if (!string.IsNullOrEmpty(text2) && text2 != "0" && text2 != "null")
			{
				text3 = text3 + " and Radio=" + text2;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text3;
			int totalCount = this.doh.Count("V_PlaySmallType");
			string sql = SqlHelp.GetSql0("*", "V_PlaySmallType", "Id", pageSize, num, "asc", text3);
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

		private void ajaxPlaySmallStates()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField("Sys_PlaySmallType", "IsOpen");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsOpen", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Sys_PlaySmallType");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "编辑了Id为" + text + "的玩法启用状态");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxGetTimeList()
		{
			string text = base.q("type");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text2 = "";
			if (!string.IsNullOrEmpty(text) && text != "0")
			{
				text2 = text2 + " lotteryId=" + text;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("Sys_LotteryTime");
			string sql = SqlHelp.GetSql0("[Id],[LotteryId],dbo.f_GetLotteryName(LotteryId) as LotteryName,[Sn],[Time],[Sort],STime", "Sys_LotteryTime", "Id", pageSize, num, "asc", text2);
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

		private void ajaxGetAutoUrlList()
		{
			string text = base.q("lid");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text2 = "IsUsed = 0";
			if (!string.IsNullOrEmpty(text))
			{
				text2 = text2 + " and Lid=" + text;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("Sys_lotteryUrl");
			string sql = SqlHelp.GetSql0("*", "Sys_lotteryUrl a", "Id", pageSize, num, "asc", text2);
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

		private void ajaxGetLotteryCheckList()
		{
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text = "";
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("Sys_LotteryCheck");
			string sql = SqlHelp.GetSql0("(select Title from Sys_Lottery where Id=a.Id) as Name,*", "Sys_LotteryCheck a", "Id", pageSize, num, "asc", text);
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

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
