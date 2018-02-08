using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxHistory : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "ajaxGetList")
				{
					this.ajaxGetList();
					goto IL_A6;
				}
				if (operType == "ajaxGetListRG")
				{
					this.ajaxGetListRG();
					goto IL_A6;
				}
				if (operType == "ajaxGetListById")
				{
					this.ajaxGetListById();
					goto IL_A6;
				}
				if (operType == "ajaxGetAdminList")
				{
					this.ajaxGetAdminList();
					goto IL_A6;
				}
				if (operType == "ajaxGetRepeatList")
				{
					this.ajaxGetRepeatList();
					goto IL_A6;
				}
			}
			this.DefaultResponse();
			IL_A6:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("sel");
			string text4 = base.q("u");
			string text5 = base.q("tid");
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
			string text6 = "";
			string text7 = string.Concat(new string[]
			{
				" STime >='",
				text,
				"' and STime <'",
				text2,
				"'"
			});
			if (!string.IsNullOrEmpty(text5))
			{
				text7 = text7 + " and Code ='" + text5 + "'";
			}
			if (!string.IsNullOrEmpty(text4))
			{
				if (text3.Equals("Remark"))
				{
					string text8 = text7;
					text7 = string.Concat(new string[]
					{
						text8,
						" and ",
						text3,
						" like '%",
						text4,
						"%'"
					});
				}
				else
				{
					string text8 = text7;
					text7 = string.Concat(new string[]
					{
						text8,
						" and ",
						text3,
						" = '",
						text4,
						"'"
					});
				}
			}
			text6 += " select * from ( ";
			this.doh.Reset();
			this.doh.ConditionExpress = text7;
			int totalCount = this.doh.Count("V_History");
			text6 += SqlHelp.GetSql0("*", "V_History", "STime", pageSize, num, "desc", text7);
			text6 += " ) YouleTable order by STime desc ";
			this.doh.Reset();
			this.doh.SqlCmd = text6;
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

		private void ajaxGetRepeatList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("tid");
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
			string text4 = "Id in(select MIN(Id) from [N_UserMoneyLog] a where ";
			if (!string.IsNullOrEmpty(text3))
			{
				text4 = text4 + " Code ='" + text3 + "'";
			}
			else
			{
				text4 += " Code = 5";
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					" and STime >='",
					text,
					"' and STime <'",
					text2,
					"'"
				});
			}
			text4 += " group by a.SysId,UserId,MoneyChange having count(a.SysId)>1)";
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("V_History");
			string sql = SqlHelp.GetSql0("*", "V_History", "Id", pageSize, num, "desc", text4);
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

		private void ajaxGetListRG()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("sel");
			string text4 = base.q("u");
			string text5 = base.q("tid");
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
			string text6 = "IsSoft=0 and Code <> 7 and Code <>8 and Code <>9 and Code <>11";
			if (!string.IsNullOrEmpty(text4))
			{
				string text7 = text6;
				text6 = string.Concat(new string[]
				{
					text7,
					" and ",
					text3,
					" = '",
					text4,
					"'"
				});
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text7 = text6;
				text6 = string.Concat(new string[]
				{
					text7,
					" and STime >='",
					text,
					"' and STime <'",
					text2,
					"'"
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text6;
			int totalCount = this.doh.Count("V_History");
			string sql = SqlHelp.GetSql0("*", "V_History", "Id", pageSize, num, "desc", text6);
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

		private void ajaxGetListById()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("u");
			string str = base.q("id");
			string text4 = base.q("tid");
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
			string text5 = "UserId=" + str;
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text6 = text5;
				text5 = string.Concat(new string[]
				{
					text6,
					" and STime >='",
					text,
					"' and STime <'",
					text2,
					"'"
				});
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text5 = text5 + " and Code ='" + text4 + "'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text5;
			int totalCount = this.doh.Count("V_History");
			string sql = SqlHelp.GetSql0("Id,UserId,UName,LotteryId,LotteryName,PlayId,PlayName,SysId,IssueNum,SingleMoney,MoneyChange,MoneyAgo,MoneyAfter,IsOk,Content,STime,Code,CodeName,IsSoft,IsSoftName,IsFlag,IsFlag2,Remark", "V_History", "Id", pageSize, num, "desc", text5);
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

		private void ajaxGetAdminList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("u");
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
			string text4 = "";
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					" STime >='",
					text,
					"' and STime <'",
					text2,
					"'"
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("V_AdminHistory");
			string sql = SqlHelp.GetSql0("*", "V_AdminHistory", "Id", pageSize, num, "desc", text4);
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

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
