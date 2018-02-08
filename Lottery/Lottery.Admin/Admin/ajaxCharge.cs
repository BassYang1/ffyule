using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxCharge : AdminCenter
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
				goto IL_162;
			case "ajaxGetListOfNo":
				this.ajaxGetListOfNo();
				goto IL_162;
			case "ajaxGetTranAccList":
				this.ajaxGetTranAccList();
				goto IL_162;
			case "ajaxGetCashList":
				this.ajaxGetCashList();
				goto IL_162;
			case "ajaxGetCashCheck":
				this.ajaxGetCashCheck();
				goto IL_162;
			case "ajaxStates":
				this.ajaxStates();
				goto IL_162;
			case "ajaxChargeSetList":
				this.ajaxChargeSetList();
				goto IL_162;
			case "ajaxChargeSetStates":
				this.ajaxChargeSetStates();
				goto IL_162;
			case "ajaxGetActChargeList":
				this.ajaxGetActChargeList();
				goto IL_162;
			}
			this.DefaultResponse();
			IL_162:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetList()
		{
			string text = base.q("keys");
			string text2 = base.q("bank");
			string text3 = base.q("state");
			string text4 = base.q("sel");
			string text5 = base.q("u");
			string text6 = base.q("money");
			string text7 = base.q("d1");
			string text8 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text9 = "";
			string text10 = "";
			if (text7.Trim().Length == 0)
			{
				text7 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
			}
			if (text8.Trim().Length == 0)
			{
				text8 = this.EndTime;
			}
			if (Convert.ToDateTime(text7) > Convert.ToDateTime(text8))
			{
				text7 = text8;
			}
			string text11 = base.q("id");
			if (!string.IsNullOrEmpty(text11))
			{
				text10 = text10 + " ssid ='" + text11 + "'";
			}
			else
			{
				if (text7.Trim().Length > 0 && text8.Trim().Length > 0)
				{
					string text12 = text10;
					text10 = string.Concat(new string[]
					{
						text12,
						" STime >='",
						text7,
						"' and STime <'",
						text8,
						"'"
					});
				}
				if (!string.IsNullOrEmpty(text2))
				{
					text10 = text10 + " and BankId=" + text2;
				}
				if (!string.IsNullOrEmpty(text3))
				{
					text10 = text10 + " and state=" + text3;
				}
				if (!string.IsNullOrEmpty(text5))
				{
					if (text4.Equals("username"))
					{
						text10 = text10 + " and dbo.f_GetUserName(UserId) like '" + text5 + "%'";
					}
					if (text4.Equals("ssid"))
					{
						text10 = text10 + " and ssid like '" + text5 + "%'";
					}
					if (text4.Equals("checkcode"))
					{
						text10 = text10 + " and checkcode like '" + text5 + "%'";
					}
				}
				if (!string.IsNullOrEmpty(text6))
				{
					text10 = text10 + " and Inmoney >=" + text6;
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text10;
			int totalCount = this.doh.Count("V_ChargeRecord");
			text9 = text9 + " SELECT '' as UserName,'0' as [Id],'全部合计' as [ssid],'' as [UserId],'0' as [money],'' as [BankId],\r\n                        '' as [BankName],'' as [CheckCode],isnull(sum([InMoney]),0) as[InMoney],isnull(sum([DzMoney]),0) as[DzMoney],getdate() as [STime],\r\n                        '-1' as [State],'' as [StateName],'' as [ActState] FROM [V_ChargeRecord] where " + text10;
			text9 += " union all ";
			text9 += " select * from ( ";
			text9 += SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_ChargeRecord", "Id", pageSize, num, "desc", text10);
			text9 += " ) YouleTable order by Id desc ";
			this.doh.Reset();
			this.doh.SqlCmd = text9;
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

		private void ajaxGetListOfNo()
		{
			string text = base.q("keys");
			string text2 = base.q("bank");
			string text3 = base.q("state");
			string text4 = base.q("sel");
			string text5 = base.q("u");
			string text6 = base.q("money");
			string text7 = base.q("d1");
			string text8 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text9 = "";
			if (text7.Trim().Length == 0)
			{
				text7 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
			}
			if (text8.Trim().Length == 0)
			{
				text8 = this.EndTime;
			}
			if (Convert.ToDateTime(text7) > Convert.ToDateTime(text8))
			{
				text7 = text8;
			}
			string text10 = base.q("id");
			if (!string.IsNullOrEmpty(text10))
			{
				text9 = text9 + " ssid ='" + text10 + "'";
			}
			else
			{
				if (text7.Trim().Length > 0 && text8.Trim().Length > 0)
				{
					string text11 = text9;
					text9 = string.Concat(new string[]
					{
						text11,
						" STime >='",
						text7,
						"' and STime <'",
						text8,
						"'"
					});
				}
				if (!string.IsNullOrEmpty(text2))
				{
					text9 = text9 + " and BankId=" + text2;
				}
				if (!string.IsNullOrEmpty(text3))
				{
					text9 = text9 + " and state=" + text3;
				}
				if (!string.IsNullOrEmpty(text5))
				{
					if (text4.Equals("username"))
					{
						text9 = text9 + " and dbo.f_GetUserName(UserId) like '" + text5 + "%'";
					}
					if (text4.Equals("ssid"))
					{
						text9 = text9 + " and ssid like '" + text5 + "%'";
					}
					if (text4.Equals("checkcode"))
					{
						text9 = text9 + " and checkcode like '" + text5 + "%'";
					}
				}
				if (!string.IsNullOrEmpty(text6))
				{
					text9 = text9 + " and Inmoney >=" + text6;
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text9;
			int totalCount = this.doh.Count("V_ChargeRecordOfNo");
			string sql = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_ChargeRecordOfNo", "Id", pageSize, num, "desc", text9);
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

		private void ajaxGetActChargeList()
		{
			string text = base.q("keys");
			string text2 = base.q("bank");
			string text3 = base.q("sel");
			string text4 = base.q("u");
			string text5 = base.q("d1");
			string text6 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text7 = "Id in (SELECT Min(Id) FROM [N_UserCharge] where InMoney>=100 and State=1 ";
			if (text5.Trim().Length == 0)
			{
				text5 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
			}
			if (text6.Trim().Length == 0)
			{
				text6 = this.EndTime;
			}
			if (Convert.ToDateTime(text5) > Convert.ToDateTime(text6))
			{
				text5 = text6;
			}
			if (text5.Trim().Length > 0 && text6.Trim().Length > 0)
			{
				string text8 = text7;
				text7 = string.Concat(new string[]
				{
					text8,
					" and STime >='",
					text5,
					"' and STime <'",
					text6,
					"'"
				});
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text7 = text7 + " and BankId=" + text2;
			}
			if (!string.IsNullOrEmpty(text4))
			{
				if (text3.Equals("username"))
				{
					text7 = text7 + " and dbo.f_GetUserName(UserId) like '%" + text4 + "%'";
				}
				else
				{
					text7 = text7 + " and ssid like '%" + text4 + "%'";
				}
			}
			text7 += " group by UserId) and ActState=0  ";
			this.doh.Reset();
			this.doh.ConditionExpress = text7;
			int totalCount = this.doh.Count("V_ChargeRecord");
			string sql = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_ChargeRecord", "Id", pageSize, num, "desc", text7);
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

		private void ajaxGetTranAccList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("sel");
			string text4 = base.q("u");
			string text5 = base.q("money");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text6 = "";
			string text7 = "";
			if (text.Trim().Length == 0)
			{
				text = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = this.EndTime;
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text8 = base.q("id");
			if (!string.IsNullOrEmpty(text8))
			{
				text7 = text7 + " ssid ='" + text8 + "'";
			}
			else
			{
				if (text.Trim().Length > 0 && text2.Trim().Length > 0)
				{
					string text9 = text7;
					text7 = string.Concat(new string[]
					{
						text9,
						" STime >='",
						text,
						"' and STime <'",
						text2,
						"'"
					});
				}
				if (!string.IsNullOrEmpty(text4))
				{
					string text9 = text7;
					text7 = string.Concat(new string[]
					{
						text9,
						" and dbo.f_GetUserName(",
						text3,
						") like '%",
						text4,
						"%'"
					});
				}
				if (!string.IsNullOrEmpty(text5))
				{
					text7 = text7 + " and Inmoney >=" + text5;
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text7;
			int totalCount = this.doh.Count("N_UserChargeLog");
			text6 = text6 + " SELECT  '' as UserName,'' as ToUserName,'0' as [Id],'全部合计' as [ssid],'' as [Type],'' as [UserId],'' as [ToUserId],isnull(sum([MoneyChange]),0) as [MoneyChange],\r\n                        getdate() as [STime],'' as [Remark] FROM [N_UserChargeLog] where " + text7;
			text6 += " union all ";
			text6 += " select * from ( ";
			text6 += SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,dbo.f_GetUserName(ToUserId) as ToUserName,*", "N_UserChargeLog", "Id", pageSize, num, "desc", text7);
			text6 += " ) YouleTable order by Id desc ";
			this.doh.Reset();
			this.doh.SqlCmd = text6;
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

		private void ajaxGetCashList()
		{
			string text = base.q("keys");
			string text2 = base.q("issoft");
			string text3 = base.q("u");
			string text4 = base.q("sel");
			string text5 = base.q("u2");
			string text6 = base.q("sel2");
			string text7 = base.q("d1");
			string text8 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text9 = "";
			string text10 = "";
			if (text7.Trim().Length == 0)
			{
				text7 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
			}
			if (text8.Trim().Length == 0)
			{
				text8 = this.EndTime;
			}
			if (Convert.ToDateTime(text7) > Convert.ToDateTime(text8))
			{
				text7 = text8;
			}
			string text11 = base.q("id");
			if (!string.IsNullOrEmpty(text11))
			{
				text10 = text10 + " ssid ='" + text11 + "'";
			}
			else
			{
				if (text7.Trim().Length > 0 && text8.Trim().Length > 0)
				{
					string text12 = text10;
					text10 = string.Concat(new string[]
					{
						text12,
						" STime >='",
						text7,
						"' and STime <'",
						text8,
						"'"
					});
				}
				if (!string.IsNullOrEmpty(text2))
				{
					text10 = text10 + " and State=" + text2;
				}
				if (!string.IsNullOrEmpty(text3))
				{
					string text12 = text10;
					text10 = string.Concat(new string[]
					{
						text12,
						" and ",
						text4,
						"  like '%",
						text3,
						"%'"
					});
				}
				if (!string.IsNullOrEmpty(text5))
				{
					string text12 = text10;
					text10 = string.Concat(new string[]
					{
						text12,
						" and ",
						text6,
						" >=",
						text5
					});
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text10;
			int totalCount = this.doh.Count("V_UserGetCash");
			text9 = text9 + " SELECT '0' as [Id],'全部合计' as [ssid],'' as [UserId],'' as [UserName],'' as [BankId],'' as [PayMethod],\r\n                        '' as [PayBank],'' as [PayName],'' as [tPayName],'' as [PayAccount],'' as [tPayAccount],isnull(sum([CashMoney]),0) as [CashMoney],\r\n                        '0' as [Money],getdate() as [STime],'-1' as [State],'' as [StateName],getdate() as [STime2],'' as [Msg],'0' as [bet] \r\n                        FROM [V_UserGetCash] where " + text10;
			text9 += " union all ";
			text9 += " select * from ( ";
			text9 += SqlHelp.GetSql0("*", "V_UserGetCash", "Id", pageSize, num, "desc", text10);
			text9 += " ) YouleTable order by Id desc ";
			this.doh.Reset();
			this.doh.SqlCmd = text9;
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

		private void ajaxGetCashCheck()
		{
			string text = base.q("keys");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			string text4 = base.q("u");
			string text5 = base.q("sel");
			string text6 = base.q("u2");
			string text7 = base.q("sel2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text8 = "(State=0 or State=99)";
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id";
			this.doh.AddConditionParameter("@Id", this.AdminId);
			object[] fields = this.doh.GetFields("Sys_Admin", "GroupId,MinCash,MaxCash");
			if (Convert.ToInt32(fields[0]) == 2)
			{
				object obj = text8;
				text8 = string.Concat(new object[]
				{
					obj,
					" and (CashMoney>=",
					fields[1],
					" and CashMoney<",
					fields[2],
					")"
				});
			}
			if (text2.Trim().Length == 0)
			{
				text2 = this.StartTime;
			}
			if (text3.Trim().Length == 0)
			{
				text3 = this.EndTime;
			}
			if (Convert.ToDateTime(text2) > Convert.ToDateTime(text3))
			{
				text2 = text3;
			}
			if (text2.Trim().Length > 0 && text3.Trim().Length > 0)
			{
				string text9 = text8;
				text8 = string.Concat(new string[]
				{
					text9,
					" and STime >='",
					text2,
					"' and STime <'",
					text3,
					"'"
				});
			}
			if (!string.IsNullOrEmpty(text4))
			{
				string text9 = text8;
				text8 = string.Concat(new string[]
				{
					text9,
					" and ",
					text5,
					"  like '%",
					text4,
					"%'"
				});
			}
			if (!string.IsNullOrEmpty(text6))
			{
				string text9 = text8;
				text8 = string.Concat(new string[]
				{
					text9,
					" and ",
					text7,
					" >=",
					text6
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text8;
			int totalCount = this.doh.Count("V_UserGetCash");
			string sql = SqlHelp.GetSql0("*", "V_UserGetCash", "Id", pageSize, num, "desc", text8);
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
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("State", 99);
			int num = this.doh.Update("N_UserGetCash");
			new LogAdminOperDAL().SaveLog(this.AdminId, text, "会员充值", "忽略了Id为" + text + "的提现申请");
			if (num > 0)
			{
				this._response = base.JsonResult(1, "忽略成功");
			}
			else
			{
				this._response = base.JsonResult(0, "忽略失败");
			}
		}

		private void ajaxChargeSetList()
		{
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text = "";
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("Sys_ChargeSet");
			string sql = SqlHelp.GetSql0("*", "Sys_ChargeSet", "Sort", pageSize, num, "asc", text);
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

		private void ajaxChargeSetStates()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField("Sys_ChargeSet", "IsUsed");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsUsed", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Sys_ChargeSet");
			new LogAdminOperDAL().SaveLog(this.AdminId, text, "会员充值", "编辑Id为" + text + "的充值配置启用状态");
			if (num2 > 0)
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
