using System;
using System.Data;
using System.Text;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Utils;

namespace Lottery.IPhone
{
	public class ajaxBet : UserCenterSession
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
				goto IL_12F;
			case "ajaxGetZHList":
				this.ajaxGetZHList();
				goto IL_12F;
			case "ajaxGetZH":
				this.ajaxGetZH();
				goto IL_12F;
			case "ajaxGetZHInfo":
				this.ajaxGetZHInfo();
				goto IL_12F;
			case "ajaxOper":
				this.ajaxOper();
				goto IL_12F;
			case "ajaxZHIssueNum":
				this.ajaxZHIssueNum();
				goto IL_12F;
			case "ajaxGetBetInfo":
				this.ajaxGetBetInfo();
				goto IL_12F;
			}
			this.DefaultResponse();
			IL_12F:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetList()
		{
			string text = base.q("stime");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			string text4 = base.q("lid");
			string text5 = base.q("pid");
			string text6 = base.q("type");
			string text7 = base.q("state");
			string text8 = base.q("moshi");
			string text9 = base.q("betid");
			string text10 = base.q("u");
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num = base.Str2Int(base.q("flag"), 0);
			if (!string.IsNullOrEmpty(text))
			{
				string text11 = text;
				if (text11 != null)
				{
					if (!(text11 == "1"))
					{
						if (!(text11 == "2"))
						{
							if (!(text11 == "3"))
							{
								if (!(text11 == "4"))
								{
									if (!(text11 == "5"))
									{
										if (text11 == "6")
										{
											text2 = DateTime.Now.ToString("yyyy") + "-01-01 00:00:00";
											text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
										}
									}
									else
									{
										text2 = DateTime.Now.AddMonths(-3).ToString("yyyy-MM") + "-01 00:00:00";
										text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
									}
								}
								else
								{
									text2 = DateTime.Now.ToString("yyyy-MM") + "-01 00:00:00";
									text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
								}
							}
							else
							{
								text2 = DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd") + " 00:00:00";
								text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
							}
						}
						else
						{
							text2 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 00:00:00";
							text3 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59:59";
						}
					}
					else
					{
						text2 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 00:00:00";
						text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
					}
				}
			}
			else
			{
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
			}
			string text12 = "";
			text12 = text12 + " UserId =" + this.AdminId;
			if (!string.IsNullOrEmpty(text10))
			{
				text12 = text12 + " and UserName LIKE '" + text10 + "%'";
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text12 = text12 + " and LotteryId =" + text4;
			}
			if (!string.IsNullOrEmpty(text5))
			{
				text12 = text12 + " and PlayId ='" + text5 + "'";
			}
			if (text2.Trim().Length > 0 && text3.Trim().Length > 0)
			{
				string text13 = text12;
				text12 = string.Concat(new string[]
				{
					text13,
					" and STime2 >='",
					text2,
					"' and STime2 <='",
					text3,
					"'"
				});
			}
			if (!string.IsNullOrEmpty(text7))
			{
				text12 = text12 + " and State =" + text7;
			}
			if (!string.IsNullOrEmpty(text8))
			{
				text12 = text12 + " and SingleMoney ='" + text8 + "'";
			}
			if (!string.IsNullOrEmpty(text9))
			{
				text12 = text12 + " and Id =" + text9;
			}
			string response = "";
			new Lottery.DAL.UserBetDAL().GetListJSON(thispage, pagesize, text12, this.AdminId, ref response);
			this._response = response;
		}

		private void ajaxGetBetInfo()
		{
			string id = base.q("Id");
			string response = "";
			new Lottery.DAL.Flex.UserBetDAL().GetBetInfoJSON(id, this.AdminId, ref response);
			this._response = response;
		}

		private void ajaxGetZHList()
		{
			string text = base.q("stime");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			string text4 = base.q("lid");
			string text5 = base.q("pid");
			string a = base.q("type");
			string text6 = base.q("u");
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num = base.Str2Int(base.q("flag"), 0);
			if (!string.IsNullOrEmpty(text))
			{
				string text7 = text;
				if (text7 != null)
				{
					if (!(text7 == "1"))
					{
						if (!(text7 == "2"))
						{
							if (!(text7 == "3"))
							{
								if (!(text7 == "4"))
								{
									if (!(text7 == "5"))
									{
										if (text7 == "6")
										{
											text2 = DateTime.Now.ToString("yyyy") + "-01-01 00:00:00";
											text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
										}
									}
									else
									{
										text2 = DateTime.Now.AddMonths(-3).ToString("yyyy-MM") + "-01 00:00:00";
										text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
									}
								}
								else
								{
									text2 = DateTime.Now.ToString("yyyy-MM") + "-01 00:00:00";
									text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
								}
							}
							else
							{
								text2 = DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd") + " 00:00:00";
								text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
							}
						}
						else
						{
							text2 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 00:00:00";
							text3 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59:59";
						}
					}
					else
					{
						text2 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 00:00:00";
						text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
					}
				}
			}
			else
			{
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
			}
			string text8 = "";
			if (a == "")
			{
				text8 = text8 + "UserCode like '%" + Strings.PadLeft(this.AdminId) + "%'";
			}
			if (a == "1")
			{
				text8 = text8 + "UserId =" + this.AdminId;
			}
			if (a == "2")
			{
				text8 = text8 + "ParentId =" + this.AdminId;
			}
			if (a == "3")
			{
				string text9 = text8;
				text8 = string.Concat(new string[]
				{
					text9,
					"UserCode like '%",
					Strings.PadLeft(this.AdminId),
					"%' and UserId<>",
					this.AdminId
				});
			}
			if (!string.IsNullOrEmpty(text6))
			{
				text8 = text8 + " and UserName LIKE '%" + text6 + "%'";
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text8 = text8 + " and LotteryId =" + text4;
			}
			if (!string.IsNullOrEmpty(text5))
			{
				text8 = text8 + " and PlayId ='" + text5 + "'";
			}
			if (text2.Trim().Length > 0 && text3.Trim().Length > 0)
			{
				string text9 = text8;
				text8 = string.Concat(new string[]
				{
					text9,
					" and STime >='",
					text2,
					"' and STime <='",
					text3,
					"'"
				});
			}
			string response = "";
			new UserBetZhDAL().GetListJSON_ZH(thispage, pagesize, text8, "", ref response);
			this._response = response;
		}

		private void ajaxGetZH()
		{
			string str = base.Str2Str(base.q("id"));
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			string wherestr = "Id=" + str;
			string response = "";
			new UserBetZhDAL().GetListJSON_ZH(thispage, pagesize, wherestr, "", ref response);
			this._response = response;
		}

		private void ajaxGetZHInfo()
		{
			string str = base.q("id");
			string text = base.q("state");
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			string text2 = "zhId =" + str;
			if (!string.IsNullOrEmpty(text))
			{
				text2 = text2 + " and State =" + text;
			}
			string response = "";
			new UserBetZhDAL().GetListJSON_ZHDetail(thispage, pagesize, text2, "", ref response);
			this._response = response;
		}

		private void ajaxOper()
		{
			string betId = base.f("ids");
			string response = new Lottery.DAL.Flex.UserBetDAL().BetCancel(betId);
			this._response = response;
		}

		private void ajaxZHIssueNum()
		{
			string text = base.q("lid");
			string text2 = base.q("flag");
			DateTime dateTime = base.GetDateTime();
			string str = dateTime.ToString("yyyyMMdd");
			string text3 = dateTime.ToString("HH:mm:ss");
			string text4 = dateTime.ToString("yyyy-MM-dd");
			if (text == "3002" || text == "3003")
			{
				DateTime dateTime2 = Convert.ToDateTime(dateTime.Year.ToString() + "-01-01 20:30:00");
				this.doh.Reset();
				this.doh.SqlCmd = string.Concat(new string[]
				{
					"select datediff(d,'",
					dateTime2.ToString("yyyy-MM-dd HH:mm:ss"),
					"','",
					dateTime.ToString("yyyy-MM-dd HH:mm:ss"),
					"') as d"
				});
				DataTable dataTable = this.doh.GetDataTable();
				int num = Convert.ToInt32(dataTable.Rows[0]["d"]) - 6;
				num++;
				string text5 = dateTime.AddDays(-1.0).ToString("yyyy-MM-dd") + " 20:30:00";
				string text6 = dateTime.ToString("yyyy-MM-dd") + " 20:30:00";
				if (dateTime > Convert.ToDateTime(dateTime.ToString(" 20:30:00")))
				{
					text6 = dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
				}
				else
				{
					num--;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i <= 9; i++)
				{
					string text7 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},";
					text7 = text7.Replace("编号", (i + 1).ToString()).Replace("期号", dateTime.Year.ToString() + Func.AddZero(num + i, 3)).Replace("倍数", "0").Replace("金额", "0.00").Replace("时间", dateTime.AddDays((double)i).ToString("yyyy-MM-dd") + " 20:30:00");
					stringBuilder.Append(text7);
				}
				this._response = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"lotteryid\" :\"",
					text,
					"\",\"totalcount\" :\"10\",\"table\": [",
					stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1),
					"]}"
				});
			}
			else
			{
				if (UserCenterSession.LotteryTime == null)
				{
					UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
				}
				DataRow[] array = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
				{
					"Time >'",
					text3,
					"' and LotteryId=",
					text
				}), "Time asc");
				if (array.Length == 0)
				{
					array = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
					{
						"Time <='",
						text3,
						"' and LotteryId=",
						text
					}), "Time asc");
					str = dateTime.AddDays(1.0).ToString("yyyyMMdd");
				}
				if (dateTime > Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd") + " 00:00:00") && dateTime < Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd") + " 02:00:01"))
				{
					if (text == "1003")
					{
						str = dateTime.AddDays(-1.0).ToString("yyyyMMdd");
					}
				}
				int num2 = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
				{
					"LotteryId=",
					text
				}), "Time asc").Length;
				if (num2 > 120)
				{
					num2 = 120;
				}
				StringBuilder stringBuilder = new StringBuilder();
				this.doh.Reset();
				if (text2.Equals("0"))
				{
					this.doh.SqlCmd = string.Concat(new object[]
					{
						"select top ",
						num2,
						" * from Sys_LotteryTime where lotteryid=",
						text,
						" and sn>=",
						array[0]["Sn"].ToString(),
						"order by Convert(int,sn) asc"
					});
				}
				if (text2.Equals("1"))
				{
					this.doh.SqlCmd = string.Concat(new object[]
					{
						"select top ",
						num2,
						" * from Sys_LotteryTime where lotteryid=",
						text,
						" and sn>",
						array[0]["Sn"].ToString(),
						"order by Convert(int,sn) asc"
					});
				}
				DataTable dataTable2 = this.doh.GetDataTable();
				for (int i = 0; i < dataTable2.Rows.Count; i++)
				{
					string text8 = str + "-" + dataTable2.Rows[i]["sn"].ToString();
					if (text == "1010" || text == "1017" || text == "1011" || text == "1012" || text == "1013" || text == "3004" || text == "3005")
					{
						text8 = string.Concat(new LotteryTimeDAL().GetTsIssueNum(text) + Convert.ToInt32(dataTable2.Rows[i]["sn"].ToString()));
					}
					if (text == "4001")
					{
						if ((DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00") && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:07:01")) || (DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:57:01") && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59")))
						{
							text8 = string.Concat(new LotteryTimeDAL().GetTsIssueNum(text) + 179 + Convert.ToInt32(dataTable2.Rows[i]["sn"].ToString()));
						}
						else
						{
							text8 = string.Concat(new LotteryTimeDAL().GetTsIssueNum(text) + Convert.ToInt32(dataTable2.Rows[i]["sn"].ToString()));
						}
					}
					if (text == "1014" || text == "1015" || text == "1016")
					{
						text8 = text8.Replace("-", "");
					}
					string text9 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},";
					text9 = text9.Replace("编号", (i + 1).ToString()).Replace("期号", text8).Replace("倍数", "0").Replace("金额", "0.00").Replace("时间", dateTime.ToString("yyyy-MM-dd") + " " + dataTable2.Rows[i]["time"]);
					stringBuilder.Append(text9);
				}
				this.doh.Reset();
				this.doh.SqlCmd = string.Concat(new object[]
				{
					"select top ",
					num2 - dataTable2.Rows.Count,
					" * from Sys_LotteryTime where lotteryid=",
					text,
					" order by Convert(int,sn) asc"
				});
				DataTable dataTable3 = this.doh.GetDataTable();
				for (int i = 0; i < dataTable3.Rows.Count; i++)
				{
					string text8 = dateTime.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataTable3.Rows[i]["sn"].ToString();
					if (text == "1010" || text == "3004")
					{
						text8 = string.Concat(new LotteryTimeDAL().GetTsIssueNum(text) + 880 + Convert.ToInt32(dataTable3.Rows[i]["sn"].ToString()));
					}
					if (text == "1012")
					{
						text8 = string.Concat(new LotteryTimeDAL().GetTsIssueNum(text) + 660 + Convert.ToInt32(dataTable3.Rows[i]["sn"].ToString()));
					}
					if (text == "1013")
					{
						text8 = string.Concat(new LotteryTimeDAL().GetTsIssueNum(text) + 203 + Convert.ToInt32(dataTable3.Rows[i]["sn"].ToString()));
					}
					if (text == "4001")
					{
						if ((DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00") && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:07:01")) || (DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:57:01") && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59")))
						{
							text8 = string.Concat(new LotteryTimeDAL().GetTsIssueNum(text) + 179 + Convert.ToInt32(dataTable3.Rows[i]["sn"].ToString()));
						}
						else
						{
							text8 = string.Concat(new LotteryTimeDAL().GetTsIssueNum(text) + Convert.ToInt32(dataTable3.Rows[i]["sn"].ToString()));
						}
					}
					if (text == "1014" || text == "1015" || text == "1016")
					{
						text8 = text8.Replace("-", "");
					}
					string text7 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},";
					text7 = text7.Replace("编号", (i + 1 + dataTable2.Rows.Count).ToString()).Replace("期号", text8).Replace("倍数", "0").Replace("金额", "0.00").Replace("时间", dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " " + dataTable3.Rows[i]["time"]);
					stringBuilder.Append(text7);
				}
				this._response = string.Concat(new object[]
				{
					"{\"result\" :\"1\",\"lotteryid\" :\"",
					text,
					"\",\"totalcount\" :\"",
					num2,
					"\",\"table\": [",
					stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1),
					"]}"
				});
			}
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
