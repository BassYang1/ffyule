using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.WebApp
{
	public class ajaxProfitloss : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "ajaxGetPointList")
				{
					this.ajaxGetPointList();
					goto IL_90;
				}
				if (operType == "ajaxGetProList")
				{
					this.ajaxGetProList();
					goto IL_90;
				}
				if (operType == "ajaxGetProListSub")
				{
					this.ajaxGetProListSub();
					goto IL_90;
				}
				if (operType == "ajaxGetProListId")
				{
					this.ajaxGetProListId();
					goto IL_90;
				}
			}
			this.DefaultResponse();
			IL_90:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetProListId()
		{
			string text = base.q("keys");
			string text2 = base.q("d1") + " 00:00:00";
			string text3 = base.q("d2") + " 23:59:59";
			string text4 = base.q("id");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text5 = "IsDel=0";
			if (!string.IsNullOrEmpty(text4))
			{
				text5 = text5 + " and ParentId =" + text4;
			}
			else
			{
				text5 += " and ParentId =-1";
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
			this.doh.Reset();
			this.doh.ConditionExpress = text5;
			int totalCount = this.doh.Count("N_User");
			string sql = SqlHelp.GetSql0("[Id]", "N_User a", "Id", pageSize, num, "asc", text5);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			string text6 = "";
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				text6 = text6 + "," + dataTable.Rows[i]["Id"].ToString();
			}
			DataTable dt = new DataTable();
			if (text6.Length > 1)
			{
				text6 = text6.Substring(1, text6.Length - 1);
				dt = this.GetUserMoneyStat(text2, text3, text6);
				this.doh.Reset();
				this.doh.SqlCmd = "select Id from N_User with(nolock) where " + text5;
				DataTable dataTable2 = this.doh.GetDataTable();
				string text7 = "";
				for (int i = 0; i < dataTable2.Rows.Count; i++)
				{
					text7 = text7 + "," + dataTable2.Rows[i]["Id"].ToString();
				}
				DataTable dtsum = new DataTable();
				if (text7.Length > 1)
				{
					text7 = text7.Substring(1, text7.Length - 1);
					dtsum = this.GetUserMoneyStat(text2, text3, text7);
				}
				this._response = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"",
					PageBar.GetPageBar(6, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON123(dtsum, dt, 0, "recordcount", "table", true),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
			else
			{
				this._response = "{\"result\" :\"0\",\"returnval\" :\"加载完成\"}";
			}
		}

		private void ajaxGetProList()
		{
			string text = base.q("type");
			if (string.IsNullOrEmpty(text) || text == "0")
			{
				string text2 = base.q("stime");
				string text3 = base.q("keys");
				string text4 = base.q("d1") + " 00:00:00";
				string text5 = base.q("d2") + " 23:59:59";
				string text6 = base.q("id");
				int num = base.Int_ThisPage();
				int pageSize = base.Str2Int(base.q("pagesize"), 20);
				int num2 = base.Str2Int(base.q("flag"), 0);
				string text7 = "IsDel=0";
				if (!string.IsNullOrEmpty(text6))
				{
					text7 = text7 + " and ParentId =" + text6;
				}
				else if (text3.Trim().Length > 0)
				{
					string text8 = text7;
					text7 = string.Concat(new string[]
					{
						text8,
						" and UserCode like '%",
						Strings.PadLeft(this.AdminId),
						"%' and UserName LIKE '%",
						text3,
						"%'"
					});
				}
				else
				{
					text7 = text7 + " and ParentId =" + this.AdminId;
				}
				if (!string.IsNullOrEmpty(text2))
				{
					string text9 = text2;
					if (text9 != null)
					{
						if (!(text9 == "1"))
						{
							if (!(text9 == "2"))
							{
								if (!(text9 == "3"))
								{
									if (!(text9 == "4"))
									{
										if (!(text9 == "5"))
										{
											if (text9 == "6")
											{
												text4 = DateTime.Now.ToString("yyyy") + "-01-01 00:00:00";
												text5 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
											}
										}
										else
										{
											text4 = DateTime.Now.AddMonths(-3).ToString("yyyy-MM") + "-01 00:00:00";
											text5 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
										}
									}
									else
									{
										text4 = DateTime.Now.ToString("yyyy-MM") + "-01 00:00:00";
										text5 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
									}
								}
								else
								{
									text4 = DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd") + " 00:00:00";
									text5 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
								}
							}
							else
							{
								text4 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 00:00:00";
								text5 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59:59";
							}
						}
						else
						{
							text4 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 00:00:00";
							text5 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
						}
					}
				}
				else
				{
					if (text4.Trim().Length == 0)
					{
						text4 = this.StartTime;
					}
					if (text5.Trim().Length == 0)
					{
						text5 = this.EndTime;
					}
					if (Convert.ToDateTime(text4) > Convert.ToDateTime(text5))
					{
						text4 = text5;
					}
				}
				this.doh.Reset();
				this.doh.ConditionExpress = text7;
				int totalCount = this.doh.Count("N_User");
				string sql = SqlHelp.GetSql0("[Id]", "N_User a", "Id", pageSize, num, "asc", text7);
				this.doh.Reset();
				this.doh.SqlCmd = sql;
				DataTable dataTable = this.doh.GetDataTable();
				string text10 = "";
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					text10 = text10 + "," + dataTable.Rows[i]["Id"].ToString();
				}
				DataTable userMoneyStat = this.GetUserMoneyStat(text4, text5, this.AdminId);
				if (text10.Length > 1)
				{
					text10 = text10.Substring(1, text10.Length - 1);
					if (num == 1)
					{
						userMoneyStat = this.GetUserMoneyStat(text4, text5, this.AdminId + "," + text10);
					}
					else
					{
						userMoneyStat = this.GetUserMoneyStat(text4, text5, text10);
					}
				}
				this.doh.Reset();
				this.doh.SqlCmd = "select Id from N_User with(nolock) where " + text7;
				DataTable dataTable2 = this.doh.GetDataTable();
				string text11 = "";
				for (int i = 0; i < dataTable2.Rows.Count; i++)
				{
					text11 = text11 + "," + dataTable2.Rows[i]["Id"].ToString();
				}
				DataTable userMoneyStat2 = this.GetUserMoneyStat(text4, text5, this.AdminId);
				if (text11.Length > 1)
				{
					text11 = text11.Substring(1, text11.Length - 1);
					userMoneyStat2 = this.GetUserMoneyStat(text4, text5, this.AdminId + "," + text11);
				}
				this._response = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"",
					PageBar.GetPageBar(6, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON123(userMoneyStat2, userMoneyStat, 0, "recordcount", "table", true),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
			else
			{
				this.ajaxGetProListSub();
			}
		}

		private DataTable GetUserMoneyStat(string d1, string d2, string userId)
		{
			DataTable dataTable = this.CreatDataTable();
			string[] array = userId.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				decimal num = 0m;
				decimal num2 = 0m;
				decimal num3 = 0m;
				decimal d3 = 0m;
				decimal d4 = 0m;
				decimal num4 = 0m;
				decimal d5 = 0m;
				decimal d6 = 0m;
				decimal d7 = 0m;
				decimal d8 = 0m;
				decimal d9 = 0m;
				decimal d10 = 0m;
				decimal num5 = 0m;
				decimal num6 = 0m;
				decimal d11 = 0m;
				decimal d12 = 0m;
				decimal num7 = 0m;
				decimal d13 = 0m;
				string userMoneyStatSql = this.GetUserMoneyStatSql(d1, d2, array[i]);
				this.doh.Reset();
				this.doh.SqlCmd = userMoneyStatSql;
				DataTable dataTable2 = this.doh.GetDataTable();
				for (int j = 0; j < dataTable2.Rows.Count; j++)
				{
					num = Convert.ToDecimal(dataTable2.Rows[j]["money"].ToString());
					num2 = Convert.ToDecimal(dataTable2.Rows[j]["childnum"].ToString());
					num3 = Convert.ToDecimal(dataTable2.Rows[j]["Charge"].ToString());
					d3 = Convert.ToDecimal(dataTable2.Rows[j]["GetCash"].ToString());
					d4 = Convert.ToDecimal(dataTable2.Rows[j]["GetCashErr"].ToString());
					num4 = Convert.ToDecimal(dataTable2.Rows[j]["Bet"].ToString());
					d5 = Convert.ToDecimal(dataTable2.Rows[j]["Betno"].ToString());
					d6 = Convert.ToDecimal(dataTable2.Rows[j]["BetChase"].ToString());
					d7 = Convert.ToDecimal(dataTable2.Rows[j]["WinChase"].ToString());
					d8 = Convert.ToDecimal(dataTable2.Rows[j]["Point"].ToString());
					d9 = Convert.ToDecimal(dataTable2.Rows[j]["Win"].ToString());
					d10 = Convert.ToDecimal(dataTable2.Rows[j]["Cancellation"].ToString());
					num5 = Convert.ToDecimal(dataTable2.Rows[j]["ChargeDeduct"].ToString());
					num6 = Convert.ToDecimal(dataTable2.Rows[j]["ChargeUp"].ToString());
					d11 = Convert.ToDecimal(dataTable2.Rows[j]["Give"].ToString());
					d12 = Convert.ToDecimal(dataTable2.Rows[j]["AgentFH"].ToString());
					num7 = Convert.ToDecimal(dataTable2.Rows[j]["AdminAddDed"].ToString());
					d13 = Convert.ToDecimal(dataTable2.Rows[j]["Change"].ToString());
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow["Id"] = array[i];
				dataRow["userName"] = dataTable2.Rows[0]["userName"].ToString();
				dataRow["money"] = num.ToString();
				dataRow["childnum"] = num2.ToString();
				dataRow["Charge"] = num3.ToString();
				dataRow["GetCash"] = (d3 - d4).ToString();
				dataRow["GetCashErr"] = d4.ToString();
				dataRow["Bet"] = (num4 + d6 - d7 - d10 - d5).ToString();
				dataRow["BetChase"] = d6.ToString();
				dataRow["WinChase"] = d7.ToString();
				dataRow["Point"] = d8.ToString();
				dataRow["Win"] = d9.ToString();
				dataRow["Cancellation"] = d10.ToString();
				dataRow["ChargeDeduct"] = num5.ToString();
				dataRow["ChargeUp"] = num6.ToString();
				dataRow["Give"] = d11.ToString();
				dataRow["AgentFH"] = d12.ToString();
				dataRow["AdminAddDed"] = num7.ToString();
				dataRow["Change"] = d13.ToString();
				dataRow["Total"] = string.Concat(d9 + d7 + d11 + d13 + d12 + d10 + d8 + d5 - num4 - d6);
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		private string GetUserMoneyStatSql(string d1, string d2, string userId)
		{
			return string.Concat(new string[]
			{
				"SELECT ",
				userId,
				" as Id,(select userName from N_User with(nolock) where Id=",
				userId,
				") as userName,(select money from N_User with(nolock) where Id=",
				userId,
				") as money,(SELECT isnull(sum(Times*total),0) FROM [N_UserBet] with(nolock) where state=0 and (Stime2 >'",
				d1,
				"' and STime2<'",
				d2,
				"') and UserId =",
				userId,
				") as Betno,(select count(*) from N_User with(nolock) where ParentId=",
				userId,
				") as childnum,isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.GetCashErr),0) GetCashErr,isnull(sum(b.Bet),0) Bet,isnull(sum(b.BetChase),0) BetChase,isnull(sum(b.WinChase),0) WinChase,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.ChargeDeduct),0) ChargeDeduct,isnull(sum(b.ChargeUp),0) ChargeUp,isnull(sum(b.Give),0) Give,isnull(sum(b.AgentFH),0) AgentFH,isnull(sum(b.AdminAddDed),0) AdminAddDed,isnull(sum(b.Change),0) Change FROM N_UserMoneyStatAll b with(nolock)  where STime>='",
				d1,
				"' and STime<='",
				d2,
				"' and (UserId=",
				userId,
				")"
			});
		}

		private void ajaxGetProListSub()
		{
			string text = base.q("stime");
			string text2 = base.q("keys");
			string text3 = base.q("d1") + " 00:00:00";
			string text4 = base.q("d2") + " 23:59:59";
			string text5 = base.q("id");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text6 = "IsDel=0";
			if (!string.IsNullOrEmpty(text5))
			{
				text6 = text6 + " and ParentId =" + text5;
			}
			else if (text2.Trim().Length > 0)
			{
				string text7 = text6;
				text6 = string.Concat(new string[]
				{
					text7,
					" and UserCode like '%",
					Strings.PadLeft(this.AdminId),
					"%' and UserName LIKE '%",
					text2,
					"%'"
				});
			}
			else
			{
				text6 = text6 + " and ParentId =" + this.AdminId;
			}
			if (!string.IsNullOrEmpty(text))
			{
				string text8 = text;
				if (text8 != null)
				{
					if (!(text8 == "1"))
					{
						if (!(text8 == "2"))
						{
							if (!(text8 == "3"))
							{
								if (!(text8 == "4"))
								{
									if (!(text8 == "5"))
									{
										if (text8 == "6")
										{
											text3 = DateTime.Now.ToString("yyyy") + "-01-01 00:00:00";
											text4 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
										}
									}
									else
									{
										text3 = DateTime.Now.AddMonths(-3).ToString("yyyy-MM") + "-01 00:00:00";
										text4 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
									}
								}
								else
								{
									text3 = DateTime.Now.ToString("yyyy-MM") + "-01 00:00:00";
									text4 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
								}
							}
							else
							{
								text3 = DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd") + " 00:00:00";
								text4 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
							}
						}
						else
						{
							text3 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 00:00:00";
							text4 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59:59";
						}
					}
					else
					{
						text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 00:00:00";
						text4 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
					}
				}
			}
			else
			{
				if (text3.Trim().Length == 0)
				{
					text3 = this.StartTime;
				}
				if (text4.Trim().Length == 0)
				{
					text4 = this.EndTime;
				}
				if (Convert.ToDateTime(text3) > Convert.ToDateTime(text4))
				{
					text3 = text4;
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text6;
			int totalCount = this.doh.Count("N_User");
			string sql = SqlHelp.GetSql0("[Id]", "N_User a", "Id", pageSize, num, "asc", text6);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			string text9 = "";
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				text9 = text9 + "," + dataTable.Rows[i]["Id"].ToString();
			}
			DataTable userMoneyStatSub = this.GetUserMoneyStatSub(text3, text4, this.AdminId);
			if (text9.Length > 1)
			{
				text9 = text9.Substring(1, text9.Length - 1);
				if (num == 1)
				{
					userMoneyStatSub = this.GetUserMoneyStatSub(text3, text4, this.AdminId + "," + text9);
				}
				else
				{
					userMoneyStatSub = this.GetUserMoneyStatSub(text3, text4, text9);
				}
			}
			this.doh.Reset();
			this.doh.SqlCmd = "select Id from N_User with(nolock) where " + text6;
			DataTable dataTable2 = this.doh.GetDataTable();
			string text10 = "";
			for (int i = 0; i < dataTable2.Rows.Count; i++)
			{
				text10 = text10 + "," + dataTable2.Rows[i]["Id"].ToString();
			}
			DataTable userMoneyStatSub2 = this.GetUserMoneyStatSub(text3, text4, this.AdminId);
			if (text10.Length > 1)
			{
				text10 = text10.Substring(1, text10.Length - 1);
				userMoneyStatSub2 = this.GetUserMoneyStatSub(text3, text4, this.AdminId + "," + text10);
			}
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"",
				PageBar.GetPageBar(6, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON123(userMoneyStatSub2, userMoneyStatSub, 0, "recordcount", "table", true),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private DataTable GetUserMoneyStatSub(string d1, string d2, string userId)
		{
			DataTable dataTable = this.CreatDataTable();
			string[] array = userId.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				decimal num = 0m;
				decimal num2 = 0m;
				decimal d3 = 0m;
				decimal d4 = 0m;
				decimal num3 = 0m;
				decimal num4 = 0m;
				decimal num5 = 0m;
				decimal num6 = 0m;
				decimal num7 = 0m;
				decimal num8 = 0m;
				decimal d5 = 0m;
				decimal num9 = 0m;
				decimal d6 = 0m;
				decimal d7 = 0m;
				decimal num10 = 0m;
				decimal num11 = 0m;
				decimal d8 = 0m;
				decimal num12 = 0m;
				string userMoneyStatSubSql = this.GetUserMoneyStatSubSql(d1, d2, array[i]);
				this.doh.Reset();
				this.doh.SqlCmd = userMoneyStatSubSql;
				DataTable dataTable2 = this.doh.GetDataTable();
				for (int j = 0; j < dataTable2.Rows.Count; j++)
				{
					num = Convert.ToDecimal(dataTable2.Rows[j]["money"].ToString());
					num2 = Convert.ToDecimal(dataTable2.Rows[j]["childnum"].ToString());
					d3 += Convert.ToDecimal(dataTable2.Rows[j]["Charge"].ToString());
					d4 += Convert.ToDecimal(dataTable2.Rows[j]["GetCash"].ToString());
					num3 += Convert.ToDecimal(dataTable2.Rows[j]["GetCashErr"].ToString());
					num4 += Convert.ToDecimal(dataTable2.Rows[j]["Bet"].ToString());
					num5 += Convert.ToDecimal(dataTable2.Rows[j]["Betno"].ToString());
					num6 += Convert.ToDecimal(dataTable2.Rows[j]["BetChase"].ToString());
					num7 += Convert.ToDecimal(dataTable2.Rows[j]["WinChase"].ToString());
					num8 += Convert.ToDecimal(dataTable2.Rows[j]["Point"].ToString());
					d5 += Convert.ToDecimal(dataTable2.Rows[j]["Win"].ToString());
					num9 += Convert.ToDecimal(dataTable2.Rows[j]["Cancellation"].ToString());
					d6 += Convert.ToDecimal(dataTable2.Rows[j]["ChargeDeduct"].ToString());
					d7 += Convert.ToDecimal(dataTable2.Rows[j]["ChargeUp"].ToString());
					num10 += Convert.ToDecimal(dataTable2.Rows[j]["Give"].ToString());
					num11 += Convert.ToDecimal(dataTable2.Rows[j]["AgentFH"].ToString());
					d8 += Convert.ToDecimal(dataTable2.Rows[j]["AdminAddDed"].ToString());
					num12 += Convert.ToDecimal(dataTable2.Rows[j]["Change"].ToString());
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow["Id"] = array[i];
				dataRow["userName"] = dataTable2.Rows[0]["userName"].ToString();
				dataRow["money"] = num.ToString();
				dataRow["childnum"] = num2.ToString();
				dataRow["Charge"] = d3.ToString();
				dataRow["GetCash"] = (d4 - num3).ToString();
				dataRow["GetCashErr"] = num3.ToString();
				dataRow["Bet"] = (num4 + num6 - num7 - num9 - num5).ToString();
				dataRow["BetChase"] = num6.ToString();
				dataRow["WinChase"] = num7.ToString();
				dataRow["Point"] = num8.ToString();
				dataRow["Win"] = d5.ToString();
				dataRow["Cancellation"] = num9.ToString();
				dataRow["ChargeDeduct"] = d6.ToString();
				dataRow["ChargeUp"] = d7.ToString();
				dataRow["Give"] = num10.ToString();
				dataRow["AgentFH"] = num11.ToString();
				dataRow["AdminAddDed"] = d8.ToString();
				dataRow["Change"] = num12.ToString();
				dataRow["Total"] = string.Concat(d5 + num7 + num10 + num12 + num11 + num9 + num8 + num5 - num4 - num6);
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		private string GetUserMoneyStatSubSql(string d1, string d2, string userId)
		{
			return string.Concat(new string[]
			{
				"SELECT ",
				userId,
				" as Id,(select userName from N_User with(nolock) where Id=",
				userId,
				") as userName,(select money from N_User with(nolock) where Id=",
				userId,
				") as money,(SELECT isnull(sum(Times*total),0) FROM [N_UserBet] with(nolock) where state=0 and (Stime2 >'",
				d1,
				"' and STime2<'",
				d2,
				"') and (UserId in (SELECT [Id] FROM [N_User] where UserCode like '%",
				Strings.PadLeft(userId),
				"%') or UserId=",
				userId,
				")) as Betno,(select count(*) from N_User with(nolock) where ParentId=",
				userId,
				") as childnum,isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.GetCashErr),0) GetCashErr,isnull(sum(b.Bet),0) Bet,isnull(sum(b.BetChase),0) BetChase,isnull(sum(b.WinChase),0) WinChase,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.ChargeDeduct),0) ChargeDeduct,isnull(sum(b.ChargeUp),0) ChargeUp,isnull(sum(b.Give),0) Give,isnull(sum(b.AgentFH),0) AgentFH,isnull(sum(b.AdminAddDed),0) AdminAddDed,isnull(sum(b.Change),0) Change FROM N_UserMoneyStatAll b with(nolock)  where STime>='",
				d1,
				"' and STime<='",
				d2,
				"' and (UserId in (SELECT [Id] FROM [N_User] where UserCode like '%",
				Strings.PadLeft(userId),
				"%') or UserId=",
				userId,
				")"
			});
		}

		private void ajaxGetPointList()
		{
			string text = base.q("d1") + " 00:00:00";
			string text2 = base.q("d2") + " 23:59:59";
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			string text3 = "UserId =" + this.AdminId;
			if (text.Trim().Length == 0)
			{
				text = DateTime.Now.ToString("yyyy-MM") + "-01";
			}
			if (text2.Trim().Length == 0)
			{
				text2 = this.EndTime;
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text4 = text3;
				text3 = string.Concat(new string[]
				{
					text4,
					" and STime >='",
					text.Substring(0, 10),
					"' and STime <='",
					text2.Substring(0, 10),
					"'"
				});
			}
			string response = "";
			new UserMoneyStatDAL().GetListJSON(thispage, pagesize, text3, ref response);
			this._response = response;
		}

		private DataTable CreatDataTable()
		{
			return new DataTable
			{
				Columns = 
				{
					{
						"Id",
						typeof(int)
					},
					{
						"userName",
						typeof(string)
					},
					{
						"money",
						typeof(decimal)
					},
					{
						"childnum",
						typeof(decimal)
					},
					{
						"Charge",
						typeof(decimal)
					},
					{
						"GetCash",
						typeof(decimal)
					},
					{
						"GetCashErr",
						typeof(decimal)
					},
					{
						"Bet",
						typeof(decimal)
					},
					{
						"BetChase",
						typeof(decimal)
					},
					{
						"WinChase",
						typeof(decimal)
					},
					{
						"Point",
						typeof(decimal)
					},
					{
						"Win",
						typeof(decimal)
					},
					{
						"Cancellation",
						typeof(decimal)
					},
					{
						"ChargeDeduct",
						typeof(decimal)
					},
					{
						"ChargeUp",
						typeof(decimal)
					},
					{
						"Give",
						typeof(decimal)
					},
					{
						"AgentFH",
						typeof(decimal)
					},
					{
						"AdminAddDed",
						typeof(decimal)
					},
					{
						"Change",
						typeof(decimal)
					},
					{
						"Total",
						typeof(decimal)
					}
				}
			};
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
