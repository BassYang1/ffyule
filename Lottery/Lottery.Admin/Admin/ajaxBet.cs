using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxBet : AdminCenter
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
				goto IL_1B6;
			case "ajaxGetListOfMissing":
				this.ajaxGetListOfMissing();
				goto IL_1B6;
			case "ajaxGetZHList":
				this.ajaxGetZHList();
				goto IL_1B6;
			case "ajaxGetZH":
				this.ajaxGetZH();
				goto IL_1B6;
			case "ajaxGetZHInfo":
				this.ajaxGetZHInfo();
				goto IL_1B6;
			case "ajaxCancelTitle":
				this.ajaxCancelTitle();
				goto IL_1B6;
			case "ajaxCancelTitleOfNo":
				this.ajaxCancelTitleOfNo();
				goto IL_1B6;
			case "ajaxBetCanel":
				this.ajaxBetCanel();
				goto IL_1B6;
			case "ajaxBetCheat":
				this.ajaxBetCheat();
				goto IL_1B6;
			case "ajaxOper":
				this.ajaxOper();
				goto IL_1B6;
			case "ajaxPaiJiangBetId":
				this.ajaxPaiJiangBetId();
				goto IL_1B6;
			case "ajaxBetOpers":
				this.ajaxBetOpers();
				goto IL_1B6;
			}
			this.DefaultResponse();
			IL_1B6:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxPaiJiangBetId()
		{
			int num = base.Str2Int(base.q("flag"), 0);
			string text = base.f("ids");
			string[] array = text.Split(new char[]
			{
				','
			});
			string text2 = "";
			for (int i = 0; i < array.Length; i++)
			{
				text2 += new LotteryCheck().RunOfBetId(array[i]);
			}
			if (string.IsNullOrEmpty(text2))
			{
				this._response = base.JsonResult(1, "派奖成功");
			}
			else
			{
				this._response = base.JsonResult(0, text2);
			}
		}

		private void ajaxCancelTitle()
		{
			int lotteryId = base.Str2Int(base.q("flag"), 0);
			string text = base.f("ids");
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				new LotteryCheck().Cancel(lotteryId, array[i], 2);
			}
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "管理员对" + text + "进行撤单！");
			this._response = base.JsonResult(1, "撤单成功");
		}

		private void ajaxCancelTitleOfNo()
		{
			int lotteryId = base.Str2Int(base.q("flag"), 0);
			string text = base.f("ids");
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				new LotteryCheck().Cancel(lotteryId, array[i], 0);
			}
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "管理员对" + text + "进行撤单！");
			this._response = base.JsonResult(1, "撤单成功");
		}

		private void ajaxBetCanel()
		{
			int num = base.Str2Int(base.q("flag"), 0);
			string text = base.f("ids");
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				new LotteryCheck().CancelOfBetId(array[i]);
			}
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "管理员对" + text + "进行撤单！");
			this._response = base.JsonResult(1, "撤单成功");
		}

		private void ajaxBetCheat()
		{
			int num = base.Str2Int(base.q("flag"), 0);
			string text = base.f("ids");
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				new UserBetDAL().BetCheat(array[i].ToString());
			}
			this._response = base.JsonResult(1, "加入改单列表成功，请到待修改订单中修改");
		}

		private void ajaxGetList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("state");
			string text4 = base.q("lid");
			string text5 = base.q("pid");
			string text6 = base.q("sel");
			string text7 = base.q("u");
			string text8 = base.q("IsCheat");
			string value = base.q("yc");
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
			string text9 = "";
			string text10 = string.Concat(new string[]
			{
				" STime2 >='",
				text,
				"' and STime2 <'",
				text2,
				"'"
			});
			string text11 = base.q("id");
			if (!string.IsNullOrEmpty(text11))
			{
				text10 = text10 + " and ssid ='" + text11 + "'";
			}
			else
			{
				if (!string.IsNullOrEmpty(text7))
				{
					text7 = text7.Trim();
					if (text6.Equals("username"))
					{
						text10 = text10 + " and UserName = '" + text7 + "'";
					}
					else if (text6.Equals("IssueNum"))
					{
						text10 = text10 + " and IssueNum = '" + text7 + "'";
					}
					else
					{
						text10 = text10 + " and ssid = '" + text7 + "'";
					}
				}
				if (!string.IsNullOrEmpty(text4))
				{
					text10 = text10 + " and LotteryId =" + text4;
				}
				if (!string.IsNullOrEmpty(text5))
				{
					text10 = text10 + " and PlayId =" + text5;
				}
				if (!string.IsNullOrEmpty(text3))
				{
					text10 = text10 + " and state =" + text3;
				}
				if (!string.IsNullOrEmpty(text8))
				{
					text10 = text10 + " and IsCheat=" + text8 + " and State=0";
				}
				if (!string.IsNullOrEmpty(value))
				{
					text10 += " and WarnState='异常'";
				}
			}
			string text12 = base.q("order");
			if (!string.IsNullOrEmpty(text12))
			{
				if (text12.Equals("bet"))
				{
					text12 = "Times*Total";
				}
			}
			else
			{
				text12 = "Id";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text10;
			int totalCount = this.doh.Count("V_UserBet");
			text9 += SqlHelp.GetSql0("*", "V_UserBet", text12, pageSize, num, "desc", text10);
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

		private void ajaxGetListOfMissing()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("lid");
			string text4 = base.q("pid");
			string text5 = base.q("sel");
			string text6 = base.q("u");
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
			string text7 = "State=0 and " + new UserBetDAL().GetWQWhere();
			if (!string.IsNullOrEmpty(text6))
			{
				if (text5.Equals("username"))
				{
					text7 = text7 + " and dbo.f_GetUserName(UserId) like '%" + text6 + "%'";
				}
				else
				{
					text7 = text7 + " and ssid like '%" + text6 + "%'";
				}
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text7 = text7 + " and LotteryId =" + text3;
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text7 = text7 + " and PlayId ='" + text4 + "'";
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text8 = text7;
				text7 = string.Concat(new string[]
				{
					text8,
					" and STime2 >='",
					text,
					"' and STime2 <'",
					text2,
					"'"
				});
			}
			string text9 = base.q("order");
			if (!string.IsNullOrEmpty(text9))
			{
				if (text9.Equals("bet"))
				{
					text9 = "Times*Total";
				}
			}
			else
			{
				text9 = "stime2";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text7;
			int totalCount = this.doh.Count("N_UserBet");
			string sql = SqlHelp.GetSql0("Id,ssid,UserId,dbo.f_GetUserName(UserId) as UserName,UserMoney,PlayId,dbo.f_GetPlayName(PlayId) as PlayName,PlayCode,LotteryId,dbo.f_GetLotteryName(LotteryId) as LotteryName,IssueNum,SingleMoney,Times,Num,DX,DS,cast(round(Times*Total,4) as numeric(15,4)) as Total,Point,PointMoney,Bonus,WinNum,WinBonus,RealGet,Pos,STime,STime2,IsOpen,State,IsDelay,IsWin,STime9", "N_UserBet", text9, pageSize, num, "desc", text7);
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

		private void ajaxGetZHList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("lid");
			string text4 = base.q("pid");
			string text5 = base.q("sel");
			string text6 = base.q("u");
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
			string text7 = "1=1";
			string text8 = base.q("id");
			if (!string.IsNullOrEmpty(text8))
			{
				text7 = text7 + " and ssid ='" + text8 + "'";
			}
			else
			{
				if (!string.IsNullOrEmpty(text6))
				{
					if (text5.Equals("username"))
					{
						text7 = text7 + " and dbo.f_GetUserName(UserId) like '%" + text6 + "%'";
					}
					else
					{
						text7 = text7 + " and ssid like '%" + text6 + "%'";
					}
				}
				if (!string.IsNullOrEmpty(text3))
				{
					text7 = text7 + " and LotteryId =" + text3;
				}
				if (!string.IsNullOrEmpty(text4))
				{
					text7 = text7 + " and PlayId ='" + text4 + "'";
				}
				if (text.Trim().Length > 0 && text2.Trim().Length > 0)
				{
					string text9 = text7;
					text7 = string.Concat(new string[]
					{
						text9,
						" and STime >='",
						text,
						"' and STime <'",
						text2,
						"'"
					});
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text7;
			int totalCount = this.doh.Count("V_UserBetZh");
			string sql = SqlHelp.GetSql0("*", "V_UserBetZh", "STime", pageSize, num, "desc", text7);
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

		private void ajaxGetZH()
		{
			string str = base.Str2Str(base.q("id"));
			int pageIndex = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			string text = "Id=" + str;
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int num = this.doh.Count("V_UserBetZh");
			string sql = SqlHelp.GetSql0("*", "V_UserBetZh", "STime", pageSize, pageIndex, "desc", text);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetZHInfo()
		{
			string str = base.q("id");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			string text = "zhid =" + str;
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("V_UserBetZhDetail");
			string sql = SqlHelp.GetSql0("*", "V_UserBetZhDetail", "Id", pageSize, num, "asc", text);
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

		private void ajaxOper()
		{
			string text = base.f("ids");
			int num = new UserBetZhDAL().BetCancel(text);
			if (num == 1)
			{
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "管理员对" + text + "追号进行终止追号！");
				this._response = base.JsonResult(1, "操作成功！");
			}
			else
			{
				this._response = base.JsonResult(0, "操作失败！");
			}
		}

		private void ajaxBetOpers()
		{
			string text = base.f("flag");
			string text2 = base.f("loid");
			string text3 = base.f("Issue");
			if (text.Trim().Equals("1"))
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "IssueNum='" + text3.Trim() + "'";
				if (this.doh.Count("N_UserBet") > 0)
				{
					new LotteryCheck().Cancel(Convert.ToInt32(text2.Trim()), text3.Trim(), 0);
					new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "对" + text3.Trim() + "期进行撤单");
					this._response = base.JsonResult(1, "操作成功！");
				}
				else
				{
					this._response = base.JsonResult(0, "该期号不存在投注记录！");
				}
			}
			if (text.Trim().Equals("2"))
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "IssueNum='" + text3.Trim() + "'";
				if (this.doh.Count("N_UserBet") > 0)
				{
					new LotteryCheck().Cancel(Convert.ToInt32(text2.Trim()), text3.Trim(), 2);
					new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "对" + text3.Trim() + "期进行撤单");
					this._response = base.JsonResult(1, "操作成功！");
				}
				else
				{
					this._response = base.JsonResult(0, "该期号不存在投注记录！");
				}
			}
			if (text.Trim().Equals("3"))
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "IssueNum='" + text3.Trim() + "'";
				if (this.doh.Count("N_UserBet") > 0)
				{
					new LotteryCheck().CancelToNoOfTitle(text2.Trim(), text3.Trim());
					new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "对" + text3.Trim() + "期进行撤回到未开奖");
					this._response = base.JsonResult(1, "操作成功！");
				}
				else
				{
					this._response = base.JsonResult(0, "该期号不存在投注记录！");
				}
			}
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
