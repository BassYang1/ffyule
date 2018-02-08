using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxActive : AdminCenter
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
			case "ajaxAutoActive":
				this.ajaxAutoActive();
				goto IL_116;
			case "ajaxGetList":
				this.ajaxGetList();
				goto IL_116;
			case "ajaxStates":
				this.ajaxStates();
				goto IL_116;
			case "ajaxGetActDetail":
				this.ajaxGetActDetail();
				goto IL_116;
			case "ajaxActStates":
				this.ajaxActStates();
				goto IL_116;
			case "ajaxGetActiveRecord":
				this.ajaxGetActiveRecord();
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

		private void ajaxGetActDetail()
		{
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text = "";
			string text2 = base.q("table");
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count(text2);
			this.doh.Reset();
			this.doh.SqlCmd = SqlHelp.GetSql0("*", text2, "Id", pageSize, num, "asc", text);
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

		private void ajaxActStates()
		{
			string text = base.f("id");
			string tableName = base.q("table");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField(tableName, "IsUsed");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsUsed", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update(tableName);
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxAutoActive()
		{
			string text = base.q("flag");
			string text2 = text;
			if (text2 != null)
			{
				if (!(text2 == "ActGongZi"))
				{
					if (!(text2 == "ActYongJin2"))
					{
						if (text2 == "ActYongJin3")
						{
							new ActiveAutoDAL().AutoActiveOper(this.AdminId, "ActYongJin", "直属佣金", "Task_AutoActYongJin_Group3");
						}
					}
					else
					{
						new ActiveAutoDAL().AutoActiveOper(this.AdminId, "ActYongJin", "总代佣金", "Task_AutoActYongJin_Group2");
					}
				}
				else
				{
					new ActiveAutoDAL().AutoActiveOper(this.AdminId, "ActGongZi", "日结工资", "Task_AutoActGongZi");
				}
			}
			this._response = base.JsonResult(1, "活动已补发，请查看活动记录！");
		}

		private void ajaxGetList()
		{
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text = "flag=1";
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("Act_ActiveSet");
			string sql = SqlHelp.GetSql0("*", "Act_ActiveSet", "id", pageSize, num, "asc", text);
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
			object field = this.doh.GetField("Act_ActiveSet", "IsUse");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsUse", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Act_ActiveSet");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "活动管理", "编辑了Id为" + text + "的活动");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxGetActiveRecord()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("type");
			string text4 = base.q("u");
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
			string text5 = "";
			string text6 = "1=1";
			string text7 = base.q("id");
			if (!string.IsNullOrEmpty(text7))
			{
				text6 = text6 + " and ssid ='" + text7 + "'";
			}
			else
			{
				if (!string.IsNullOrEmpty(text4))
				{
					text6 = text6 + " and dbo.f_GetUserName(UserId) LIKE '%" + text4 + "%'";
				}
				if (!string.IsNullOrEmpty(text3))
				{
					text6 = text6 + " and ActiveType ='" + text3 + "'";
				}
				if (text.Trim().Length > 0 && text2.Trim().Length > 0)
				{
					string text8 = text6;
					text6 = string.Concat(new string[]
					{
						text8,
						" and STime >='",
						text,
						"' and STime <'",
						text2,
						"'"
					});
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text6;
			int totalCount = this.doh.Count("Act_ActiveRecord");
			text5 = text5 + " select '全部合计' as UserName,'0' as Id,'-' as SsId,'-' as UserId,'-' as ActiveType,'-' as ActiveName\r\n                        ,0 as Bet,isnull(sum(InMoney),0) as InMoney,getdate() as STime,'-' as CheckIp,'-' as CheckMachine,'0' as FromUserId,'-' as Remark \r\n                        from Act_ActiveRecord where " + text6;
			text5 += " union all ";
			text5 += " select * from ( ";
			text5 += SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "Act_ActiveRecord", "Id", pageSize, num, "desc", text6);
			text5 += " ) YouleTable order by Id desc ";
			this.doh.Reset();
			this.doh.SqlCmd = text5;
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
