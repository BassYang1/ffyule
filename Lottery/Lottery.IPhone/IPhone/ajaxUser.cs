using System;
using System.Configuration;
using System.Data;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Utils;

namespace Lottery.IPhone
{
	public class ajaxUser : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("master", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			switch (operType)
			{
			case "changepass":
				this.ajaxChangeUserPwd();
				goto IL_184;
			case "moneypass":
				this.ajaxChangeMoneyPwd();
				goto IL_184;
			case "ajaxGetList":
				this.ajaxGetList();
				goto IL_184;
			case "ajaxGetTotalList":
				this.ajaxGetTotalList();
				goto IL_184;
			case "ajaxGetTeamTotalList":
				this.ajaxGetTeamTotalList();
				goto IL_184;
			case "ajaxGetTeamType":
				this.ajaxGetTeamType();
				goto IL_184;
			case "ajaxRegiter":
				this.ajaxRegiter();
				goto IL_184;
			case "ajaxGetRegStrList":
				this.ajaxGetRegStrList();
				goto IL_184;
			case "ajaxRegStrAll":
				this.ajaxRegStrAll();
				goto IL_184;
			case "ajaxVerify":
				this.ajaxVerify();
				goto IL_184;
			case "ajaxVerifyExist":
				this.ajaxVerifyExist();
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

		private void ajaxRegiter()
		{
			string userGroup = base.f("type");
			string userName = base.f("name");
			string password = base.f("pwd");
			string point = base.f("point");
			this._response = new Lottery.DAL.Flex.UserDAL().Register(this.AdminId, userGroup, userName, password, point);
		}

		private void ajaxGetList()
		{
			string text = base.q("keys");
			string text2 = base.q("moneymin");
			string text3 = base.q("moneymax");
			string text4 = base.q("pointmin");
			string text5 = base.q("pointmax");
			string text6 = base.q("orderby");
			string text7 = base.q("order");
			string text8 = base.q("group");
			string text9 = base.q("online");
			int num = base.Str2Int(base.q("gId"), 0);
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text10;
			if (text.Trim().Length > 0)
			{
				text10 = "dbo.f_GetUserCode(Id) like '%" + Strings.PadLeft(this.AdminId) + "%'";
				text10 = text10 + " and UserName LIKE '%" + text + "%'";
			}
			else
			{
				text10 = "parentId=" + this.AdminId;
			}
			if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
			{
				string text11 = text10;
				text10 = string.Concat(new string[]
				{
					text11,
					" and Money >=",
					text2,
					" and Money<=",
					text3
				});
			}
			if (!string.IsNullOrEmpty(text4) && !string.IsNullOrEmpty(text5))
			{
				string text11 = text10;
				text10 = string.Concat(new string[]
				{
					text11,
					" and Convert(decimal(18, 2),Replace(Point,'%','')) >=",
					text4,
					" and Convert(decimal(18, 2),Replace(Point,'%',''))<=",
					text5
				});
			}
			if (!string.IsNullOrEmpty(text8))
			{
				text10 = text10 + " and usergroup =" + text8;
			}
			if (!string.IsNullOrEmpty(text9))
			{
				text10 = text10 + " and IsOnline =" + text9;
			}
			if (string.IsNullOrEmpty(text6))
			{
				text6 = "desc";
			}
			if (string.IsNullOrEmpty(text7))
			{
				text7 = "Id";
			}
			string response = "";
			new Lottery.DAL.UserDAL().GetListJSON(thispage, pagesize, text10, text6, text7, ref response);
			this._response = response;
		}

		private void ajaxGetTotalList()
		{
			int pageIndex = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			string whereStr = "userId =" + this.AdminId;
			string sql = SqlHelp.GetSql0("[sort],\r\n                                                    [Name],\r\n                                                    isnull(sum(Charge),0) as Charge,\r\n                                                    isnull(sum(getcash),0) as getcash, \r\n                                                    isnull(sum(bet),0) as bet ,\r\n                                                    isnull(sum(win),0) as win,\r\n                                                    isnull(sum(Point),0) as Point,\r\n                                                    isnull(sum(Give),0) as Give,\r\n                                                    isnull(sum(Change),0) as Change,\r\n                                                    isnull(sum(AgentFH),0) as AgentFH, \r\n                                                    isnull(sum(total),0) as total,\r\n                                                    isnull(sum(betno),0) as betno", "V_UserMoneyStatAllUserTotal", "sort", pageSize, pageIndex, "asc", whereStr, "Name,sort");
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetTeamTotalList()
		{
			int pageIndex = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			string text = base.q("flag");
			string text2 = "dbo.f_GetUserCode(userId) like '%" + Strings.PadLeft(this.AdminId) + "%'";
			string text3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 00:00:00";
			string text4 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
			if (string.IsNullOrEmpty(text))
			{
				text = "1";
			}
			text2 = text2 + " and sort=" + text;
			string sql = SqlHelp.GetSql0("[sort],\r\n                                                    isnull(sum(Charge),0) as Charge,\r\n                                                    isnull(sum(getcash),0) as getcash, \r\n                                                    isnull(sum(bet),0) as bet ,\r\n                                                    isnull(sum(win),0) as win,\r\n                                                    isnull(sum(Point),0) as Point,\r\n                                                    isnull(sum(Give),0) as Give,\r\n                                                    isnull(sum(other),0) as other,  \r\n                                                    isnull(sum(-total),0) as total,\r\n                                                    isnull(sum(moneytotal),0) as moneytotal", "V_UserMoneyStatAllUserTotal", "sort", pageSize, pageIndex, "asc", text2, "sort");
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetTeamType()
		{
			string sqlCmd = string.Format("SELECT \r\n                            Convert(varchar(10),STime,120) as STime,\r\n                            isnull(sum(Charge),0) as Charge,\r\n                            isnull(sum(getcash),0)-isnull(sum(GetCashErr),0) as getcash, \r\n                            isnull(sum(Bet),0)+isnull(sum(BetChase),0)-isnull(sum(WinChase),0)-isnull(sum(Cancellation),0) as bet ,\r\n                            isnull(sum(Win),0) as win,\r\n                            isnull(sum(Point),0) as Point,\r\n                            isnull(sum(Give),0) as Give,\r\n                            isnull(sum(Change),0) as Change,\r\n                            isnull(sum(AgentFH),0) as AgentFH, \r\n                            (isnull(sum(WinChase),0)+isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(AgentFH),0)+isnull(sum(Cancellation),0))-(isnull(sum(Bet),0)+isnull(sum(BetChase),0)) as total,\r\n                            (SELECT isnull(sum(Times*total),0) FROM [N_UserBet] with(nolock) where state=0 and Convert(varchar(10),STime,120)=Convert(varchar(10),a.STime,120)) as betno\r\n                            FROM [N_UserMoneyStatAll] a with(nolock)\r\n                            where dbo.f_GetUserCode(userId) like '%{0}%' and Id<>{1} and convert(varchar(7),STime,120)=convert(varchar(7),getdate(),120) \r\n                            group by Convert(varchar(10),STime,120)", Strings.PadLeft(this.AdminId), this.AdminId);
			this.doh.Reset();
			this.doh.SqlCmd = sqlCmd;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetRegStrList()
		{
			int num = base.Int_ThisPage();
			int num2 = base.Str2Int(base.q("pagesize"), 20);
			string text = "UserId=" + this.AdminId;
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("N_UserRegLink");
			string sql = SqlHelp.GetSql0("row_number() over (order by Point desc) as rowid,*", "N_UserRegLink", "Point", num2, num, "desc", text);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(6, "js", 2, totalCount, num2, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable, num2 * (num - 1)),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxRegStrAll()
		{
			this.doh.Reset();
			this.doh.SqlCmd = "select Point from N_User with(nolock) where Id=" + this.AdminId;
			DataTable dataTable = this.doh.GetDataTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT Point FROM [N_UserLevel] where point>=100 and Point<" + Convert.ToDecimal(dataTable.Rows[i]["Point"]) + " order by [Point] desc";
				DataTable dataTable2 = this.doh.GetDataTable();
				for (int j = 0; j < dataTable2.Rows.Count; j++)
				{
					string encryptKey = ConfigurationManager.AppSettings["DesKey"].ToString();
					string url = ConfigurationManager.AppSettings["RootUrl"].ToString() + "/register.aspx?u=" + base.EncryptDES(this.AdminId + "@" + dataTable2.Rows[j]["Point"].ToString(), encryptKey).Replace("+", "@");
					new UserRegLinkDAL().SaveUserRegLink(this.AdminId, Convert.ToDecimal(dataTable2.Rows[j]["Point"]) / 10m, url);
				}
			}
			new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员生成注册链接！");
			this._response = base.JsonResult(1, "注册链接全部生成成功！");
		}

		private void ajaxChangeUserPwd()
		{
			string oldPassword = base.f("oldpass");
			string newPassword = base.f("newpass");
			if (new Lottery.DAL.UserDAL().ChangeUserPassword(this.AdminId, oldPassword, newPassword))
			{
				new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员修改登录密码！");
				this._response = base.JsonResult(1, "密码修改成功");
			}
			else
			{
				this._response = base.JsonResult(0, "旧密码错误");
			}
		}

		private void ajaxChangeMoneyPwd()
		{
			string oldPassword = base.f("oldpass");
			string newPassword = base.f("newpass");
			if (new Lottery.DAL.UserDAL().ChangePayPassword(this.AdminId, oldPassword, newPassword))
			{
				new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员修改银行密码！");
				this._response = base.JsonResult(1, "密码修改成功");
			}
			else
			{
				this._response = base.JsonResult(0, "旧密码错误");
			}
		}

		private void ajaxVerifyExist()
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id and Question<>'' and Answer<>''";
			this.doh.AddConditionParameter("@Id", this.AdminId);
			if (this.doh.Exist("N_User"))
			{
				this._response = base.JsonResult(1, "验证信息绑定成功");
			}
			else
			{
				this._response = base.JsonResult(0, "验证信息绑定失败");
			}
		}

		private void ajaxVerify()
		{
			string fieldValue = base.f("question");
			string fieldValue2 = base.f("answer");
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id";
			this.doh.AddConditionParameter("@Id", this.AdminId);
			this.doh.AddFieldItem("Question", fieldValue);
			this.doh.AddFieldItem("Answer", fieldValue2);
			this.doh.Update("N_User");
			new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员绑定验证信息！");
			this._response = base.JsonResult(1, "验证信息绑定成功");
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
