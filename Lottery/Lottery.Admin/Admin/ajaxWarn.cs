using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxWarn : AdminCenter
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
			case "ajaxWarnCount":
				this.ajaxWarnCount();
				goto IL_162;
			case "ajaxBetOfWinWarn":
				this.ajaxBetOfWinWarn();
				goto IL_162;
			case "ajaxBetOfPointWarn":
				this.ajaxBetOfPointWarn();
				goto IL_162;
			case "ajaxStatOfRealWarn":
				this.ajaxStatOfRealWarn();
				goto IL_162;
			case "ajaxStatOfActiveWarn":
				this.ajaxStatOfActiveWarn();
				goto IL_162;
			case "ajaxStatOfFhWarn":
				this.ajaxStatOfFhWarn();
				goto IL_162;
			case "ajaxBetOfYLLWarn":
				this.ajaxBetOfYLLWarn();
				goto IL_162;
			case "ajaxUserOfIpWarn":
				this.ajaxUserOfIpWarn();
				goto IL_162;
			case "ajaxGetCashWarn":
				this.ajaxGetCashWarn();
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

		private void ajaxWarnCount()
		{
			this.doh.Reset();
			this.doh.SqlCmd = "select * from V_WarnCount";
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxBetOfWinWarn()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("lid");
			string text4 = base.q("pid");
			string text5 = base.q("u");
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
			this.doh.Reset();
			this.doh.ConditionExpress = "id=1";
			object field = this.doh.GetField("Sys_Info", "WarnTotal");
			decimal num3 = string.IsNullOrEmpty(string.Concat(field)) ? 0m : Convert.ToDecimal(field.ToString());
			string text6 = "State=3 and WinBonus>=" + num3;
			if (!string.IsNullOrEmpty(text5))
			{
				text6 = text6 + " and dbo.f_GetUserName(UserId) LIKE '%" + text5 + "%'";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text6 = text6 + " and LotteryId =" + text3;
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text6 = text6 + " and PlayId ='" + text4 + "'";
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text7 = text6;
				text6 = string.Concat(new string[]
				{
					text7,
					" and STime2 >='",
					text,
					"' and STime2 <'",
					text2,
					"'"
				});
			}
			string text8 = base.q("order");
			if (!string.IsNullOrEmpty(text8))
			{
				if (text8.Equals("bet"))
				{
					text8 = "Times*Total";
				}
			}
			else
			{
				text8 = "winbonus";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text6;
			int totalCount = this.doh.Count("N_UserBet");
			string sql = SqlHelp.GetSql0("Id,UserId,dbo.f_GetUserName(UserId) as UserName,UserMoney,PlayId,dbo.f_GetPlayName(PlayId) as PlayName,PlayCode,LotteryId,dbo.f_GetLotteryName(LotteryId) as LotteryName,IssueNum,SingleMoney,Times,Num,DX,DS,Times*Total as Total,Point,PointMoney,Bonus,WinNum,WinBonus,RealGet,Pos,STime,STime2,IsOpen,State,IsDelay,IsWin,STime9", "N_UserBet", text8, pageSize, num, "desc", text6);
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

		private void ajaxBetOfPointWarn()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("lid");
			string text4 = base.q("pid");
			string text5 = base.q("u");
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
			this.doh.Reset();
			this.doh.ConditionExpress = "id=1";
			object field = this.doh.GetField("Sys_Info", "PointWarnTotal");
			decimal num3 = string.IsNullOrEmpty(string.Concat(field)) ? 0m : Convert.ToDecimal(field.ToString());
			string text6 = "PointMoney>=" + num3;
			if (!string.IsNullOrEmpty(text5))
			{
				text6 = text6 + " and dbo.f_GetUserName(UserId) LIKE '%" + text5 + "%'";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text6 = text6 + " and LotteryId =" + text3;
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text6 = text6 + " and PlayId ='" + text4 + "'";
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text7 = text6;
				text6 = string.Concat(new string[]
				{
					text7,
					" and STime2 >='",
					text,
					"' and STime2 <'",
					text2,
					"'"
				});
			}
			string text8 = base.q("order");
			if (!string.IsNullOrEmpty(text8))
			{
				if (text8.Equals("bet"))
				{
					text8 = "Times*Total";
				}
			}
			else
			{
				text8 = "PointMoney";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text6;
			int totalCount = this.doh.Count("N_UserBet");
			string sql = SqlHelp.GetSql0("Id,UserId,dbo.f_GetUserName(UserId) as UserName,UserMoney,PlayId,dbo.f_GetPlayName(PlayId) as PlayName,PlayCode,LotteryId,dbo.f_GetLotteryName(LotteryId) as LotteryName,IssueNum,SingleMoney,Times,Num,DX,DS,Times*Total as Total,Point,PointMoney,Bonus,WinNum,WinBonus,RealGet,Pos,STime,STime2,IsOpen,State,IsDelay,IsWin,STime9", "N_UserBet", text8, pageSize, num, "desc", text6);
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

		private void ajaxStatOfRealWarn()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("u");
			string text4 = base.q("order");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = Convert.ToDateTime(this.StartTime).AddDays(0.0).ToString("yyyy-MM-dd");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = Convert.ToDateTime(this.EndTime).AddDays(1.0).ToString("yyyy-MM-dd");
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			if (string.IsNullOrEmpty(text4))
			{
				text4 = "total";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = "id=1";
			object field = this.doh.GetField("Sys_Info", "RealWarnTotal");
			decimal num3 = string.IsNullOrEmpty(string.Concat(field)) ? 0m : Convert.ToDecimal(field.ToString());
			string text5 = string.Concat(new object[]
			{
				" STime >=Convert(varchar(10),'",
				text,
				"',120)  and STime <Convert(varchar(10),'",
				text2,
				"',120) and total>=",
				num3
			});
			if (!string.IsNullOrEmpty(text3))
			{
				text5 = text5 + " and dbo.f_GetUserName(UserId) = '" + text3 + "'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text5;
			int totalCount = this.doh.Count("V_UserMoneyStatAllDayOfUser");
			string sql = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_UserMoneyStatAllDayOfUser", text4, pageSize, num, "desc", text5);
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

		private void ajaxStatOfActiveWarn()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("u");
			string text4 = base.q("order");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = Convert.ToDateTime(this.StartTime).AddDays(0.0).ToString("yyyy-MM-dd");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = Convert.ToDateTime(this.EndTime).AddDays(1.0).ToString("yyyy-MM-dd");
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			if (string.IsNullOrEmpty(text4))
			{
				text4 = "Give";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = "id=1";
			object field = this.doh.GetField("Sys_Info", "ActiveWarnTotal");
			decimal num3 = string.IsNullOrEmpty(string.Concat(field)) ? 0m : Convert.ToDecimal(field.ToString());
			string text5 = string.Concat(new object[]
			{
				" STime >=Convert(varchar(10),'",
				text,
				"',120)  and STime <Convert(varchar(10),'",
				text2,
				"',120) and Give>=",
				num3
			});
			if (!string.IsNullOrEmpty(text3))
			{
				text5 = text5 + " and dbo.f_GetUserName(UserId) = '" + text3 + "'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text5;
			int totalCount = this.doh.Count("V_UserMoneyStatAllDayOfUser");
			string sql = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_UserMoneyStatAllDayOfUser", text4, pageSize, num, "desc", text5);
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

		private void ajaxStatOfFhWarn()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("u");
			string text4 = base.q("order");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = Convert.ToDateTime(this.StartTime).AddDays(0.0).ToString("yyyy-MM-dd");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = Convert.ToDateTime(this.EndTime).AddDays(1.0).ToString("yyyy-MM-dd");
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			if (string.IsNullOrEmpty(text4))
			{
				text4 = "STime";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = "id=1";
			object field = this.doh.GetField("Sys_Info", "FhWarnTotal");
			decimal num3 = string.IsNullOrEmpty(string.Concat(field)) ? 0m : Convert.ToDecimal(field.ToString());
			string text5 = string.Concat(new object[]
			{
				" STime >=Convert(varchar(10),'",
				text,
				"',120)  and STime <Convert(varchar(10),'",
				text2,
				"',120) and Other>=",
				num3
			});
			if (!string.IsNullOrEmpty(text3))
			{
				text5 = text5 + " and dbo.f_GetUserName(UserId) = '" + text3 + "'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text5;
			int totalCount = this.doh.Count("V_UserMoneyStatAllDayOfUser");
			string sql = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_UserMoneyStatAllDayOfUser", text4, pageSize, num, "desc", text5);
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

		private void ajaxBetOfYLLWarn()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("lid");
			string text4 = base.q("pid");
			string text5 = base.q("u");
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
			this.doh.Reset();
			this.doh.ConditionExpress = "id=1";
			object field = this.doh.GetField("Sys_Info", "YLLWarnTotal");
			decimal d = string.IsNullOrEmpty(string.Concat(field)) ? 0m : Convert.ToDecimal(field.ToString());
			string text6 = "State=3 and WinBonus>=(Times*Total)*" + d / 100m;
			if (!string.IsNullOrEmpty(text5))
			{
				text6 = text6 + " and dbo.f_GetUserName(UserId) LIKE '%" + text5 + "%'";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text6 = text6 + " and LotteryId =" + text3;
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text6 = text6 + " and PlayId ='" + text4 + "'";
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text7 = text6;
				text6 = string.Concat(new string[]
				{
					text7,
					" and STime2 >='",
					text,
					"' and STime2 <'",
					text2,
					"'"
				});
			}
			string text8 = base.q("order");
			if (!string.IsNullOrEmpty(text8))
			{
				if (text8.Equals("bet"))
				{
					text8 = "Times*Total";
				}
			}
			else
			{
				text8 = "STime2";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text6;
			int totalCount = this.doh.Count("V_UserBet");
			string sql = SqlHelp.GetSql0("*", "V_UserBet", text8, pageSize, num, "desc", text6);
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

		private void ajaxUserOfIpWarn()
		{
			string text = base.q("ip");
			string text2 = base.q("uname");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text3 = "isDel=0 and Ip in(select Ip from N_User with(nolock) where isDel=0 group by Ip having count(Ip)>1)";
			string fldName = "Id";
			if (!string.IsNullOrEmpty(text2))
			{
				text3 = text3 + " and UserName like '%" + text2.Trim() + "%'";
				fldName = "UserName";
			}
			if (!string.IsNullOrEmpty(text))
			{
				text3 = text3 + " and ip like '%" + text.Trim() + "%'";
				fldName = "Ip";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text3;
			int totalCount = this.doh.Count("V_User");
			string sql = SqlHelp.GetSql0("*", "V_User", fldName, pageSize, num, "asc", text3);
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

		private void ajaxGetCashWarn()
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
			this.doh.Reset();
			this.doh.ConditionExpress = "id=1";
			object field = this.doh.GetField("Sys_Info", "GetCashWarnTotal");
			decimal num3 = string.IsNullOrEmpty(string.Concat(field)) ? 0m : Convert.ToDecimal(field.ToString());
			string text8 = "(State=0 or State=99) and CashMoney>(SELECT isnull(sum(Charge),0) FROM [N_UserMoneyStatAll] where UserId=V_UserGetCash.UserId)*" + num3;
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
					" like '%",
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

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
