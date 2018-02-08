using System;
using System.Data;
using System.Text;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxMoneyStatAll : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			switch (operType)
			{
			case "ajaxGetList":
				this.ajaxGetList();
				goto IL_184;
			case "ajaxGetListTop10":
				this.ajaxGetListTop10();
				goto IL_184;
			case "ajaxGetListDay":
				this.ajaxGetListDay();
				goto IL_184;
			case "ajaxGetListRank":
				this.ajaxGetListRank();
				goto IL_184;
			case "ajaxGetListLottery":
				this.ajaxGetListLottery();
				goto IL_184;
			case "ajaxGetListIuss":
				this.ajaxGetListIuss();
				goto IL_184;
			case "ajaxGetListPlay":
				this.ajaxGetListPlay();
				goto IL_184;
			case "ajaxGetListTeamSale":
				this.ajaxGetListTeamSale();
				goto IL_184;
			case "ajaxGetAllInfo":
				this.ajaxGetAllInfo();
				goto IL_184;
			case "ajaxGetListMonth":
				this.ajaxGetListMonth();
				goto IL_184;
			case "ajaxGetListCheck":
				this.ajaxGetListCheck();
				goto IL_184;
			}
			this.DefaultResponse();
			IL_184:
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
			string text = "";
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("V_UserMoneyStatAllTotal");
			string sql = SqlHelp.GetSql0("*", "V_UserMoneyStatAllTotal", "sort", pageSize, num, "asc", text);
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

		private void ajaxGetListTop10()
		{
			int pageIndex = base.Int_ThisPage();
			int pageSize = 10;
			string text = "";
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int num = this.doh.Count("V_UserMoneyStatAllTop10");
			string sql = SqlHelp.GetSql0("*", "V_UserMoneyStatAllTop10", "total", pageSize, pageIndex, "desc", text);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetListDay()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("code");
			string text4 = base.q("u");
			string text5 = base.q("order");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = Convert.ToDateTime(this.StartTime).AddDays(-30.0).ToString("yyyy-MM-dd");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = Convert.ToDateTime(this.EndTime).AddDays(1.0).ToString("yyyy-MM-dd");
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			if (string.IsNullOrEmpty(text5))
			{
				text5 = "STime";
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
			if (string.IsNullOrEmpty(text3))
			{
				text3 = "0";
			}
			int totalCount;
			DataTable dataTable2;
			if (!string.IsNullOrEmpty(text4))
			{
				if (text3 == "0")
				{
					text7 = text7 + " and dbo.f_GetUserName(UserId) = '" + text4 + "'";
				}
				else
				{
					this.doh.Reset();
					this.doh.SqlCmd = "select top 1 Id from N_User where UserName='" + text4 + "'";
					DataTable dataTable = this.doh.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						object obj = text7;
						text7 = string.Concat(new object[]
						{
							obj,
							" and dbo.f_GetUserCode(UserId) like '%",
							dataTable.Rows[0]["Id"],
							"%'"
						});
					}
					else
					{
						text7 += "1<>1";
					}
				}
				this.doh.Reset();
				this.doh.ConditionExpress = text7;
				totalCount = this.doh.Count("V_UserMoneyStatAllDayOfUser");
				text6 += "select * from (";
				text6 = text6 + "SELECT ' 全部合计' as [STime]\r\n                            ,isnull(sum(Charge),0) Charge\r\n                            ,isnull(sum(getcash),0) getcash\r\n                            ,isnull(sum(bet),0) bet\r\n                            ,isnull(sum(win),0) win\r\n                            ,isnull(sum(Point),0) Point\r\n                            ,isnull(sum(TranAccIn),0) TranAccIn\r\n                            ,isnull(sum(TranAccOut),0) TranAccOut\r\n                            ,isnull(sum(Give),0) Give\r\n                            ,isnull(sum(other),0) other\r\n                            ,isnull(sum(agentFH),0) agentFH\r\n                            ,isnull(sum(total),0) total\r\n                            ,isnull(sum(moneytotal),0) moneytotal\r\n                            FROM [V_UserMoneyStatAllDayOfUser] where " + text7;
				text6 += " union all ";
				text6 += SqlHelp.GetSql0("STime,sum(Charge) as Charge,sum(getcash) as getcash,sum(bet) as bet,sum(win) as win,sum(Point) as Point,sum(TranAccIn) as TranAccIn,sum(TranAccOut) as TranAccOut,sum(Give) as Give,sum(other) as other,sum(agentFH) as agentFH,sum(total) as total,sum(moneytotal) as moneytotal", "V_UserMoneyStatAllDayOfUser", text5, pageSize, num, "desc", text7, "STime");
				text6 += " ) A order by STime desc";
				this.doh.Reset();
				this.doh.SqlCmd = text6;
				dataTable2 = this.doh.GetDataTable();
			}
			else
			{
				this.doh.Reset();
				this.doh.ConditionExpress = text7;
				totalCount = this.doh.Count("V_UserMoneyStatAllDay");
				text6 += "select * from (";
				text6 = text6 + "SELECT ' 全部合计' as [STime]\r\n                            ,isnull(sum(Charge),0) Charge\r\n                            ,isnull(sum(getcash),0) getcash\r\n                            ,isnull(sum(bet),0) bet\r\n                            ,isnull(sum(win),0) win\r\n                            ,isnull(sum(Point),0) Point\r\n                            ,isnull(sum(TranAccIn),0) TranAccIn\r\n                            ,isnull(sum(TranAccOut),0) TranAccOut\r\n                            ,isnull(sum(Give),0) Give\r\n                            ,isnull(sum(other),0) other\r\n                            ,isnull(sum(agentFH),0) agentFH\r\n                            ,isnull(sum(total),0) total\r\n                            ,isnull(sum(moneytotal),0) moneytotal\r\n                            FROM [V_UserMoneyStatAllDay] where " + text7;
				text6 += " union all ";
				text6 += SqlHelp.GetSql0("*", "V_UserMoneyStatAllDay", text5, pageSize, num, "desc", text7);
				text6 += " ) A order by STime desc";
				this.doh.Reset();
				this.doh.SqlCmd = text6;
				dataTable2 = this.doh.GetDataTable();
			}
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable2),
				"}"
			});
			dataTable2.Clear();
			dataTable2.Dispose();
		}

		private void ajaxGetListRank()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("u");
			string text4 = base.q("order");
			string text5 = base.q("orderby");
			int num = base.Int_ThisPage();
			int num2 = base.Str2Int(base.q("pagesize"), 20);
			if (text.Trim().Length == 0)
			{
				text = Convert.ToDateTime(this.StartTime).ToString("yyyy-MM-dd");
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
			if (string.IsNullOrEmpty(text5))
			{
				text5 = "asc";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select * from (");
			stringBuilder.Append(" SELECT userId,dbo.f_GetUserName(userId) as userName,");
			stringBuilder.Append(" (select money from N_User with(nolock) where Id=a.userId) as userMoney,");
			stringBuilder.Append(" (SELECT count(Id) FROM [N_UserBet] where userId=a.userId and (State=3 or State=4) and STime >='{2}' and STime <'{3}') as winNum,");
			stringBuilder.Append(" isnull(sum(Charge),0) as Charge,");
			stringBuilder.Append(" isnull(sum(getcash),0) as getcash,");
			stringBuilder.Append(" (isnull(sum(Bet),0)) as bet,");
			stringBuilder.Append(" isnull(sum(Win),0)as win,");
			stringBuilder.Append(" isnull(sum(Cancellation),0) as Cancellation,");
			stringBuilder.Append(" isnull(sum(Point),0) as Point,");
			stringBuilder.Append(" isnull(sum(Give),0) as tranaccin,");
			stringBuilder.Append(" isnull(sum(Give),0) as tranaccout,");
			stringBuilder.Append(" isnull(sum(Give),0) as Give,");
			stringBuilder.Append(" isnull(sum(other),0) as other,");
			stringBuilder.Append(" isnull(sum(agentFH),0) as agentFH,");
			stringBuilder.Append(" (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total");
			stringBuilder.Append(" ,(isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal");
			stringBuilder.Append(" From N_UserMoneyStatAll a with(nolock)");
			stringBuilder.Append(" Where STime >='{2}' and STime <'{3}'");
			if (!string.IsNullOrEmpty(text3))
			{
				stringBuilder.Append(" and dbo.f_GetUserName(UserId) = '" + text3 + "'");
			}
			stringBuilder.Append(" group by userId ) A");
			stringBuilder.Append(" order by {0} {1}");
			this.doh.Reset();
			this.doh.SqlCmd = string.Format(stringBuilder.ToString(), new object[]
			{
				text4,
				text5,
				text,
				text2
			});
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetListIuss()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("lid");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = Convert.ToDateTime(this.StartTime).ToString("yyyy-MM-dd");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = Convert.ToDateTime(this.EndTime).AddDays(1.0).ToString("yyyy-MM-dd");
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text4 = string.Concat(new string[]
			{
				"STime >='",
				text,
				"' and STime <'",
				text2,
				"' and state<>0 and state<>1"
			});
			int totalCount;
			DataTable dataTable2;
			if (!string.IsNullOrEmpty(text3))
			{
				text4 = text4 + " and LotteryId = " + text3;
				this.doh.Reset();
				this.doh.SqlCmd = "select IssueNum FROM [N_UserBet] where " + text4 + " group by IssueNum";
				DataTable dataTable = this.doh.GetDataTable();
				totalCount = dataTable.Rows.Count;
				string sqlCmd = SqlHelp.GetSqlRow("\r\n                    LotteryId,dbo.f_GetLotteryName(LotteryId) as LotteryName,IssueNum,sum(Total*Times) as bet,\r\n                    sum(WinBonus) as win,sum(-RealGet) as total\r\n                    ,isnull(sum(num),0)  as num\r\n                    ,isnull(sum(winnum),0)  as winnum\r\n                    ,isnull(cast(round(CONVERT(float,isnull(sum(WinBonus),0))*100/CONVERT(float,isnull(sum(Total*Times),1)) ,4) as numeric(9,4)),0) as per\r\n                    ,isnull(sum(PointMoney),0)  as point ", "N_UserBet a", "IssueNum", pageSize, num, "desc", text4, "LotteryId,IssueNum");
				this.doh.Reset();
				this.doh.SqlCmd = sqlCmd;
				dataTable2 = this.doh.GetDataTable();
			}
			else
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "";
				totalCount = this.doh.Count("V_UserMoneyStatAllIuss");
				string sqlCmd = SqlHelp.GetSql0("*", "V_UserMoneyStatAllIuss", "LotteryId", pageSize, num, "asc", "");
				this.doh.Reset();
				this.doh.SqlCmd = sqlCmd;
				dataTable2 = this.doh.GetDataTable();
			}
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable2),
				"}"
			});
			dataTable2.Clear();
			dataTable2.Dispose();
		}

		private void ajaxGetListPlay()
		{
			string text = base.q("lid");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			string text4 = base.q("u");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text2.Trim().Length == 0)
			{
				text2 = Convert.ToDateTime(this.StartTime).ToString("yyyy-MM-dd");
			}
			if (text3.Trim().Length == 0)
			{
				text3 = Convert.ToDateTime(this.EndTime).AddDays(1.0).ToString("yyyy-MM-dd");
			}
			if (Convert.ToDateTime(text2) > Convert.ToDateTime(text3))
			{
				text2 = text3;
			}
			string text5 = "";
			string text6 = string.Concat(new string[]
			{
				"STime2 >='",
				text2,
				"' and STime2 <'",
				text3,
				"' and state>1"
			});
			if (!string.IsNullOrEmpty(text))
			{
				text6 = text6 + " and LotteryId = " + text;
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text6 = text6 + " and Title = '" + text4 + "'";
			}
			this.doh.Reset();
			this.doh.SqlCmd = "select PlayId FROM [N_UserBet] where " + text6 + " group by PlayId";
			DataTable dataTable = this.doh.GetDataTable();
			int count = dataTable.Rows.Count;
			string text7 = (!string.IsNullOrEmpty(text)) ? LotteryUtils.LotteryTitle(Convert.ToInt32(text)) : "全部游戏";
			text5 = text5 + " SELECT '99999' as rowNember,'-1' as [LotteryId],'全部合计' as [LotteryName],'999' as PlayId,'' as Title,\r\n            cast(round(isnull(sum(Total*Times),0),4) as numeric(18,4)) as Bet ,isnull(sum(WinBonus),0) as Win,\r\n            isnull(sum(num),0) as Num,isnull(sum(winnum),0) as WinNum,\r\n            cast(round(CONVERT(float,isnull(sum(WinBonus),0))*100/CONVERT(float,isnull(sum(Total*Times),1)) ,4) as numeric(9,4)) as Per,\r\n            isnull(sum(PointMoney),0) as Point,isnull(sum(-RealGet),0) as total FROM [N_UserBet] where " + text6;
			text5 += " union all ";
			text5 += SqlHelp.GetSqlRow(string.Concat(new string[]
			{
				"'",
				text,
				"' as LotteryId,'",
				text7,
				"' as LotteryName,PlayId,(select titleName from Sys_PlaySmallType where Id=PlayId) as Title,cast(round(isnull(sum(Total*Times),0),4) as numeric(18,4)) as Bet ,isnull(sum(WinBonus),0) as Win,isnull(sum(num),0) as Num,isnull(sum(winnum),0) as WinNum,cast(round(CONVERT(float,isnull(sum(WinBonus),0))*100/CONVERT(float,isnull(sum(Total*Times),1)) ,4) as numeric(9,4)) as Per,isnull(sum(PointMoney),0) as Point,isnull(sum(-RealGet),0) as total"
			}), "N_UserBet", "PlayId", pageSize, num, "asc", text6, "PlayId");
			this.doh.Reset();
			this.doh.SqlCmd = text5;
			DataTable dataTable2 = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, count, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable2),
				"}"
			});
			dataTable2.Clear();
			dataTable2.Dispose();
		}

		private void ajaxGetListLottery()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = Convert.ToDateTime(this.StartTime).ToString("yyyy-MM-dd");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = Convert.ToDateTime(this.EndTime).AddDays(1.0).ToString("yyyy-MM-dd");
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text3 = "";
			string text4 = string.Concat(new string[]
			{
				"STime >='",
				text,
				"' and STime <'",
				text2,
				"' and state>=2"
			});
			this.doh.Reset();
			this.doh.SqlCmd = "select LotteryId FROM [N_UserBet] where " + text4 + " group by LotteryId";
			DataTable dataTable = this.doh.GetDataTable();
			int count = dataTable.Rows.Count;
			text3 = text3 + " SELECT '99999' as rowNember,'9999' as [LotteryId],'全部合计' as [Title],\r\n            cast(round(isnull(sum(Total*Times),0),4) as numeric(18,4)) as Bet ,isnull(sum(WinBonus),0) as Win,\r\n            isnull(sum(num),0) as Num,isnull(sum(winnum),0) as WinNum,\r\n            cast(round(CONVERT(float,isnull(sum(WinBonus),0))*100/CONVERT(float,isnull(sum(Total*Times),1)) ,4) as numeric(9,4)) as Per,\r\n            isnull(sum(PointMoney),0) as Point,isnull(sum(-RealGet),0) as total FROM [N_UserBet] where " + text4;
			text3 += " union all ";
			text3 += SqlHelp.GetSqlRow("LotteryId,(select title from Sys_Lottery where Id=LotteryId) as Title,\r\n            cast(round(isnull(sum(Total*Times),0),4) as numeric(18,4)) as Bet ,isnull(sum(WinBonus),0) as Win,\r\n            isnull(sum(num),0) as Num,isnull(sum(winnum),0) as WinNum,\r\n            cast(round(CONVERT(float,isnull(sum(WinBonus),0))*100/CONVERT(float,isnull(sum(Total*Times),1)) ,4) as numeric(9,4)) as Per,\r\n            isnull(sum(PointMoney),0) as Point,isnull(sum(-RealGet),0) as total", "N_UserBet", "LotteryId", pageSize, num, "asc", text4, "LotteryId");
			this.doh.Reset();
			this.doh.SqlCmd = text3;
			DataTable dataTable2 = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, count, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable2),
				"}"
			});
			dataTable2.Clear();
			dataTable2.Dispose();
		}

		private void ajaxGetListTeamSale()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("group");
			string text4 = base.q("u");
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
			string text5 = string.Concat(new string[]
			{
				"STime>='",
				text,
				"' and STime <'",
				text2,
				"' and "
			});
			string text6 = string.Format("SELECT a.[Id], a.[UserName],a.point as userpoint,\r\n                                                                isnull(sum(Charge),0) as Charge,\r\n                                                                isnull(sum(getcash),0) as getcash,\r\n                                                                isnull(sum(Bet),0)-isnull(sum(Cancellation),0) as bet,\r\n                                                                isnull(sum(Win),0) as Win,\r\n                                                                isnull(sum(b.Point),0) as Point,\r\n                                                                isnull(sum(Give),0) as Give,\r\n                                                                isnull(sum(other),0) as other,\r\n                                                                isnull(sum(agentFH),0) as agentFH,\r\n                                                                isnull(sum(TranAccIn),0) as TranAccIn,\r\n                                                                isnull(sum(TranAccOut),0) as TranAccOut,\r\n                                                                (isnull(sum(Win),0)+isnull(sum(b.Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,\r\n                                                                (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal\r\n                                                                FROM [V_User] a left join [N_UserMoneyStatAll] b on dbo.f_GetUserCode(b.UserId) like '%,'+Convert(varchar(10),a.Id)+',%' ", new object[0]);
			string text7 = text6;
			text6 = string.Concat(new string[]
			{
				text7,
				" where b.STime>='",
				text,
				"' and b.STime <'",
				text2,
				"'"
			});
			if (text4.Trim().Length > 0)
			{
				text6 = text6 + " and UserName = '" + text4 + "'";
			}
			else
			{
				text6 += " and UserName = ''";
			}
			text6 += " group by a.Id,a.UserName,a.point";
			this.doh.Reset();
			this.doh.SqlCmd = text6;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetAllInfo()
		{
			int num = base.Int_ThisPage();
			int num2 = base.Str2Int(base.q("pagesize"), 20);
			string sqlCmd = "SELECT \r\n                            Convert(Varchar(10),getdate(),120) as Date,\r\n                            (SELECT count(Id) FROM [N_User] where IsDel=0 and DateDiff(dd,regTime,getdate())=0) as regToday,\r\n                            (SELECT count(Id) FROM [N_User] where IsDel=0 and DateDiff(dd,regTime,getdate())=1) as regYesterday,\r\n                            (SELECT count(Id) FROM [N_User] where IsDel=0 and DateDiff(MM,regTime,getdate())=0) as regMonth,\r\n                            (SELECT count(Id) FROM [N_User] where IsDel=0) as sum,\r\n                            (SELECT count(Id) FROM [Flex_User] where IsDel=0 and IsOnline=1) as sumOnline,\r\n                            (SELECT count(Id) FROM [Flex_User] where IsDel=0 and IsOnline=1 and Source=1) as sumIosOnline,\r\n                            (SELECT count(Id) FROM [Flex_User] where IsDel=0 and IsOnline=1 and Source=0) as sumPcOnline,\r\n                            Isnull(sum(money),0) as Money\r\n                            FROM [N_User] where IsDel=0";
			this.doh.Reset();
			this.doh.SqlCmd = sqlCmd;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetListMonth()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("code");
			string text4 = base.q("u");
			string text5 = base.q("order");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = Convert.ToDateTime(this.StartTime).AddDays(-30.0).ToString("yyyy-MM-dd");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = Convert.ToDateTime(this.EndTime).AddDays(1.0).ToString("yyyy-MM-dd");
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			if (string.IsNullOrEmpty(text5))
			{
				text5 = "STime";
			}
			string text6 = string.Concat(new string[]
			{
				" STime >='",
				text,
				"' and STime <'",
				text2,
				"'"
			});
			if (string.IsNullOrEmpty(text3))
			{
				text3 = "0";
			}
			int totalCount;
			DataTable dataTable2;
			if (!string.IsNullOrEmpty(text4))
			{
				if (text3 == "0")
				{
					text6 = text6 + " and dbo.f_GetUserName(UserId) = '" + text4 + "'";
				}
				else
				{
					this.doh.Reset();
					this.doh.SqlCmd = "select top 1 Id from N_User where UserName='" + text4 + "'";
					DataTable dataTable = this.doh.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						object obj = text6;
						text6 = string.Concat(new object[]
						{
							obj,
							" and dbo.f_GetUserCode(UserId) like '%",
							dataTable.Rows[0]["Id"],
							"%'"
						});
					}
					else
					{
						text6 += "1<>1";
					}
				}
				this.doh.Reset();
				this.doh.ConditionExpress = text6;
				totalCount = this.doh.Count("V_UserMoneyStatAllMonthOfUser");
				string sql = SqlHelp.GetSql0("*", "V_UserMoneyStatAllMonthOfUser", text5, pageSize, num, "desc", text6);
				this.doh.Reset();
				this.doh.SqlCmd = sql;
				dataTable2 = this.doh.GetDataTable();
			}
			else
			{
				this.doh.Reset();
				this.doh.ConditionExpress = text6;
				totalCount = this.doh.Count("V_UserMoneyStatAllMonth");
				string sql = SqlHelp.GetSql0("*", "V_UserMoneyStatAllMonth", text5, pageSize, num, "desc", text6);
				this.doh.Reset();
				this.doh.SqlCmd = sql;
				dataTable2 = this.doh.GetDataTable();
			}
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable2),
				"}"
			});
			dataTable2.Clear();
			dataTable2.Dispose();
		}

		private void ajaxGetListCheck()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("group");
			string value = base.q("point");
			string text4 = base.q("sel1");
			string value2 = base.q("bet");
			string text5 = base.q("u");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = Convert.ToDateTime(this.StartTime).AddDays(-30.0).ToString("yyyy-MM-dd");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = Convert.ToDateTime(this.EndTime).AddDays(1.0).ToString("yyyy-MM-dd");
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text6 = string.Concat(new string[]
			{
				" STime >='",
				text,
				"' and STime <'",
				text2,
				"'"
			});
			string text7 = "1=1";
			if (!string.IsNullOrEmpty(text3))
			{
				text7 = text7 + "and UserGroup=" + text3;
			}
			if (!string.IsNullOrEmpty(value))
			{
				text7 = text7 + "and point=" + Convert.ToDecimal(value) * 10m;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text7;
			int totalCount = this.doh.Count("Flex_User");
			string text8 = "select * from (";
			text8 += SqlHelp.GetSql0(string.Concat(new string[]
			{
				"'",
				text,
				"' as starttime,'",
				text2,
				"' as endtime,[Id],[UserName],UserGroup,[UserGroupName],[Point],[RegTime],[LastTime],(SELECT isnull(sum([Bet]),0)-isnull(sum(Cancellation),0) FROM [N_UserMoneyStatAll] where ",
				text6,
				" and dbo.f_GetUserCode(UserId) like '%'+dbo.f_User8Code(a.Id)+'%') as Bet"
			}), "Flex_User a", "Id", pageSize, num, "asc", text7);
			text8 += " ) A";
			if (!string.IsNullOrEmpty(value2))
			{
				if (text4.Equals("0"))
				{
					text8 = text8 + " where bet<" + Convert.ToDecimal(value2);
				}
				if (text4.Equals("1"))
				{
					text8 = text8 + " where bet>" + Convert.ToDecimal(value2);
				}
			}
			this.doh.Reset();
			this.doh.SqlCmd = text8;
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
