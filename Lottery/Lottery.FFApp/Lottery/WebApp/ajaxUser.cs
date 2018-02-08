using System;
using System.Configuration;
using System.Data;
using Lottery.Collect;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Utils;

namespace Lottery.WebApp
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
				goto IL_283;
			case "moneypass":
				this.ajaxChangeMoneyPwd();
				goto IL_283;
			case "ajaxGetList":
				this.ajaxGetList();
				goto IL_283;
			case "ajaxGetTotalList":
				this.ajaxGetTotalList();
				goto IL_283;
			case "ajaxGetTeamTotalList":
				this.ajaxGetTeamTotalList();
				goto IL_283;
			case "ajaxGetTeamType":
				this.ajaxGetTeamType();
				goto IL_283;
			case "ajaxRegiter":
				this.ajaxRegiter();
				goto IL_283;
			case "ajaxGetRegStrList":
				this.ajaxGetRegStrList();
				goto IL_283;
			case "ajaxRegStr":
				this.ajaxRegStr();
				goto IL_283;
			case "ajaxRegStrAll":
				this.ajaxRegStrAll();
				goto IL_283;
			case "ajaxVerify":
				this.ajaxVerify();
				goto IL_283;
			case "ajaxVerifyExist":
				this.ajaxVerifyExist();
				goto IL_283;
			case "saveTrueName":
				this.saveTrueName();
				goto IL_283;
			case "saveEmail":
				this.saveEmail();
				goto IL_283;
			case "saveMobile":
				this.saveMobile();
				goto IL_283;
			case "ajaxGetFKListOnLine":
				this.ajaxGetFKListOnLine();
				goto IL_283;
			case "ajaxGetFKProListSub":
				this.ajaxGetFKProListSub();
				goto IL_283;
			case "ajaxGetUserGroupList":
				this.ajaxGetUserGroupList();
				goto IL_283;
			case "ajaxGetUserPointList":
				this.ajaxGetUserPointList();
				goto IL_283;
			case "GetUserJson":
				this.GetUserJson();
				goto IL_283;
			}
			this.DefaultResponse();
			IL_283:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		public void GetUserJson()
		{
			if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
			{
				string value = Cookie.GetValue(this.site.CookiePrev + "WebApp", "id");
				string text = Public.GetUserJson(Convert.ToInt32(value)).Replace("[", "").Replace("]", "");
				if (!string.IsNullOrEmpty(text))
				{
					this._response = "{\"result\":\"1\",\"table\": [" + text + "]}";
				}
				else
				{
					this._response = "{\"result\":\"0\",\"table\": []}";
				}
			}
		}

		private void ajaxRegiter()
		{
			string text = base.f("type");
			string userName = base.f("name");
			string password = base.f("pwd");
			string text2 = base.f("point");
			if (Convert.ToInt32(text) == 2)
			{
				if (Convert.ToDouble(text2) != 130.0)
				{
					this._response = base.GetJsonResult2(0, "直属只能开13.0的账号！");
					return;
				}
			}
			if (Convert.ToInt32(text) == 3)
			{
				if (Convert.ToDouble(text2) != 130.0)
				{
					this._response = base.GetJsonResult2(0, "特权直属只能开13.0的账号！");
					return;
				}
			}
			if (Convert.ToInt32(text) == 4)
			{
				if (Convert.ToDouble(text2) != 130.0)
				{
					this._response = base.GetJsonResult2(0, "招商只能开13.0的账号！");
					return;
				}
			}
			this.doh.Reset();
			this.doh.SqlCmd = "select UserGroup,Point as Upoint from N_User with(nolock) where Id=" + this.AdminId;
			DataTable dataTable = this.doh.GetDataTable();
			if (Convert.ToDouble(dataTable.Rows[0]["Upoint"]) > 130.0)
			{
				if (Convert.ToDouble(text2) >= Convert.ToDouble(dataTable.Rows[0]["Upoint"]))
				{
					this._response = base.GetJsonResult2(0, "您的账号不能开平级账号！");
					return;
				}
			}
			else
			{
				if (Convert.ToDouble(text2) > Convert.ToDouble(dataTable.Rows[0]["Upoint"]))
				{
					this._response = base.GetJsonResult2(0, "返点不能大于您的返点！");
					return;
				}
				if (Convert.ToInt32(text) < 2)
				{
					string sqlCmd = string.Format("SELECT *,(select count(*) from N_User where ParentID={0} AND UserGroup<2 and Point=a.Point*10) as regNums \r\n                            From [N_UserPointQuota] a with(nolock)  \r\n                            Where [Point]={1}", this.AdminId, dataTable.Rows[0]["Upoint"].ToString());
					this.doh.Reset();
					this.doh.SqlCmd = sqlCmd;
					DataTable dataTable2 = this.doh.GetDataTable();
					if (dataTable2.Rows.Count > 0)
					{
						if (Convert.ToDouble(dataTable2.Rows[0]["ChildNums"]) <= Convert.ToDouble(dataTable2.Rows[0]["RegNums"]))
						{
							this._response = base.GetJsonResult2(0, "您选择的返点平级配额不足！");
							return;
						}
					}
				}
			}
			string sqlCmd2 = string.Format("SELECT *,(select count(*) from N_User where ParentID={0} AND UserGroup=a.[toGroup]) as regNums \r\n                            From [N_UserGroupQuota] a with(nolock)  \r\n                            Where [Group]={1} and [ToGroup]={2}", this.AdminId, dataTable.Rows[0]["UserGroup"].ToString(), text);
			this.doh.Reset();
			this.doh.SqlCmd = sqlCmd2;
			DataTable dataTable3 = this.doh.GetDataTable();
			if (dataTable3.Rows.Count > 0)
			{
				if (Convert.ToDouble(dataTable3.Rows[0]["ChildNums"]) <= Convert.ToDouble(dataTable3.Rows[0]["RegNums"]))
				{
					this._response = base.GetJsonResult2(0, "您选择的开户类型配额不足！");
					return;
				}
			}
			this._response = new Lottery.DAL.Flex.UserDAL().Register(this.AdminId, text, userName, password, text2);
		}

		private void ajaxGetList()
		{
			string text = base.q("username");
			string text2 = base.q("money1");
			string text3 = base.q("money2");
			string text4 = base.q("online").Replace(",", "");
			int num = base.Str2Int(base.q("gId"), 0);
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text5 = base.q("Id");
			string text6 = base.q("group");
			string text7 = "dbo.f_GetUserCode(Id) like '%" + Strings.PadLeft(this.AdminId) + "%' and Id<>" + this.AdminId;
			if (text.Trim().Length > 0)
			{
				text7 = text7 + " and UserName LIKE '" + text + "%'";
			}
			else if (!string.IsNullOrEmpty(text5))
			{
				text7 = "parentId=" + text5;
			}
			else
			{
				text7 = "parentId=" + this.AdminId;
			}
			if (!string.IsNullOrEmpty(text6))
			{
				text7 = text7 + " and UserGroup <" + text6;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text7 = text7 + " and Money <=" + text2;
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text7 = text7 + " and Money >=" + text3;
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text7 = text7 + " and IsOnline =" + text4;
			}
			string response = "";
			new WebAppListOper().GetUserListJSON(this.AdminId, thispage, pagesize, text7, "asc", "Id", ref response);
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
			int num = base.Int_ThisPage();
			int num2 = base.Str2Int(base.q("pagesize"), 20);
			string text = base.q("flag");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			string text4 = "dbo.f_GetUserCode(userId) like '%" + Strings.PadLeft(this.AdminId) + "%'";
			if (text2.Trim().Length == 0)
			{
				text2 = DateTime.Now.AddDays(-3.0).ToString("yyyy-MM-dd") + " 00:00:00";
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
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					" and STime >='",
					text2,
					"' and STime <='",
					text3,
					"'"
				});
			}
			string sqlCmd = "SELECT TOP 1 isnull(sum(Charge),0) as Charge,\r\n                                    isnull(sum(getcash),0) as getcash, \r\n                                    isnull(sum(bet),0)-isnull(sum(Cancellation),0) as bet ,\r\n                                    isnull(sum(win),0) as win,\r\n                                    isnull(sum(Point),0) as Point,\r\n                                    isnull(sum(Give),0) as Give,\r\n                                    isnull(sum(other),0) as other,  \r\n                                    isnull(sum(-total),0) as total \r\n                    From N_UserMoneyStatAll with(nolock)  \r\n                    Where " + text4;
			this.doh.Reset();
			this.doh.SqlCmd = sqlCmd;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetTeamType()
		{
			string sqlCmd = string.Format("SELECT \r\n                            Convert(varchar(10),STime,120) as STime,\r\n                            isnull(sum(Charge),0) as Charge,\r\n                            isnull(sum(getcash),0) as getcash, \r\n                            isnull(sum(Bet),0)-isnull(sum(Cancellation),0) as bet ,\r\n                            isnull(sum(Win),0) as win,\r\n                            isnull(sum(Point),0) as Point,\r\n                            isnull(sum(Give),0) as Give,\r\n                            isnull(sum(Change),0) as Change,\r\n                            isnull(sum(AgentFH),0) as AgentFH, \r\n                            (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-(isnull(sum(Bet),0)) as total,\r\n                            (SELECT isnull(sum(Times*total),0) FROM [N_UserBet] with(nolock) where state=0 and Convert(varchar(10),STime,120)=Convert(varchar(10),a.STime,120)) as betno\r\n                            FROM [N_UserMoneyStatAll] a with(nolock)\r\n                            where dbo.f_GetUserCode(userId) like '%{0}%' and Id<>{1} and convert(varchar(7),STime,120)=convert(varchar(7),getdate(),120) \r\n                            group by Convert(varchar(10),STime,120)", Strings.PadLeft(this.AdminId), this.AdminId);
			this.doh.Reset();
			this.doh.SqlCmd = sqlCmd;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetUserGroupList()
		{
			int num = base.Int_ThisPage();
			int num2 = base.Str2Int(base.q("pagesize"), 20);
			this.doh.Reset();
			this.doh.SqlCmd = "select UserGroup from N_User with(nolock) where Id=" + this.AdminId;
			DataTable dataTable = this.doh.GetDataTable();
			string text = "[Group]=" + dataTable.Rows[0]["UserGroup"];
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("N_UserGroupQuota");
			string sql = SqlHelp.GetSql0(string.Format("row_number() over (order by ToGroup desc) as rowid\r\n                ,case [Group] when 0 then '会员' when 1 then '代理' when 2 then '直属' when 3 then '特权直属' when 4 then '招商' when 5 then '主管' when 6 then '管理' end as GroupName\r\n                ,case [ToGroup] when 0 then '会员' when 1 then '代理' when 2 then '直属' when 3 then '特权直属' when 4 then '招商' when 5 then '主管' when 6 then '管理' end as ToGroupName\r\n                ,(select count(*) from N_User where ParentID={0} AND UserGroup=a.[ToGroup]) as regNums,*", this.AdminId), "[N_UserGroupQuota] a", "ToGroup", num2, num, "desc", text);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable2 = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(80, "js", 2, totalCount, num2, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable2, num2 * (num - 1)),
				"}"
			});
			dataTable2.Clear();
			dataTable2.Dispose();
		}

		private void ajaxGetUserPointList()
		{
			int num = base.Int_ThisPage();
			int num2 = base.Str2Int(base.q("pagesize"), 20);
			this.doh.Reset();
			this.doh.SqlCmd = "select Point*0.1 as Upoint from N_User with(nolock) where Id=" + this.AdminId;
			DataTable dataTable = this.doh.GetDataTable();
			string text = "[Point]=" + dataTable.Rows[0]["Upoint"];
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("N_UserPointQuota");
			string sql = SqlHelp.GetSql0(string.Format("row_number() over (order by Point desc) as rowid\r\n                ,(select count(*) from N_User where ParentID={0} AND UserGroup<2 and Point=a.Point*10) as regNums,*", this.AdminId), "[N_UserPointQuota] a", "Point", num2, num, "desc", text);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable2 = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(80, "js", 2, totalCount, num2, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable2, num2 * (num - 1)),
				"}"
			});
			dataTable2.Clear();
			dataTable2.Dispose();
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
				PageBar.GetPageBar(80, "js", 2, totalCount, num2, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable, num2 * (num - 1)),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxRegStr()
		{
			string text = base.f("point");
			string yxTime = base.f("yxtime");
			string times = base.f("times");
			string encryptKey = ConfigurationManager.AppSettings["DesKey"].ToString();
			string url = ConfigurationManager.AppSettings["RootUrl"].ToString() + "/register.aspx?u=" + base.EncryptDES(this.AdminId + "@" + text, encryptKey).Replace("+", "@");
			new UserRegLinkDAL().SaveUserRegLink(this.AdminId, Convert.ToDecimal(text), yxTime, times, url);
			this._response = base.JsonResult(1, "注册链接全部生成成功！");
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

		private void saveTrueName()
		{
			string name = base.f("name");
			if (new Lottery.DAL.UserDAL().SaveUserName(this.AdminId, name))
			{
				new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员绑定真实姓名！");
				this._response = base.JsonResult(1, "真实姓名绑定成功");
			}
			else
			{
				this._response = base.JsonResult(0, "真实姓名绑定失败");
			}
		}

		private void saveEmail()
		{
			string name = base.f("name");
			if (new Lottery.DAL.UserDAL().SaveEmail(this.AdminId, name))
			{
				new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员绑定邮箱！");
				this._response = base.JsonResult(1, "邮箱绑定成功");
			}
			else
			{
				this._response = base.JsonResult(0, "邮箱绑定失败");
			}
		}

		private void saveMobile()
		{
			string name = base.f("name");
			if (new Lottery.DAL.UserDAL().SaveMobile(this.AdminId, name))
			{
				new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员绑定手机！");
				this._response = base.JsonResult(1, "手机绑定成功");
			}
			else
			{
				this._response = base.JsonResult(0, "手机绑定失败");
			}
		}

		private void ajaxGetFKListOnLine()
		{
			string text = base.q("username");
			string text2 = base.q("money1");
			string text3 = base.q("money2");
			string text4 = base.q("online").Replace(",", "");
			int num = base.Str2Int(base.q("gId"), 0);
			int num2 = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num3 = base.Str2Int(base.q("flag"), 0);
			string text5 = base.q("Id");
			string text6 = "IsOnline=1 and dbo.f_GetUserCode(Id) like '%" + Strings.PadLeft(this.AdminId) + "%' and Id<>" + this.AdminId;
			if (text.Trim().Length > 0)
			{
				text6 = text6 + " and UserName LIKE '" + text + "%'";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text6 = text6 + " and Money <=" + text2;
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text6 = text6 + " and Money >=" + text3;
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text6 = text6 + " and IsOnline =" + text4;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text6;
			int totalCount = this.doh.Count("Flex_User");
			string text7 = "";
			this.doh.Reset();
			this.doh.SqlCmd = SqlHelp.GetSql0("Id,UserCode", "Flex_User", "ID", pageSize, num2, "asc", text6);
			DataTable dataTable = this.doh.GetDataTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				text7 += string.Format("select *,'" + this.ajaxGetUserNames(dataTable.Rows[i]["UserCode"].ToString(), this.AdminId) + "' as usercodes from Flex_User where Id=" + dataTable.Rows[i]["Id"].ToString(), new object[0]);
				if (i != dataTable.Rows.Count - 1)
				{
					text7 += " union all ";
				}
			}
			if (!string.IsNullOrEmpty(text7))
			{
				this.doh.Reset();
				this.doh.SqlCmd = text7;
				dataTable = this.doh.GetDataTable();
				this._response = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
					PageBar.GetPageBar(80, "js", 2, totalCount, pageSize, num2, "javascript:ajaxList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON(dataTable),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
			else
			{
				this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"recordcount\":0,\"table\": []}";
			}
		}

		public void ajaxGetFKProListSub()
		{
			string text = base.q("d1") + " 00:00:00";
			string text2 = base.q("d2") + " 23:59:59";
			string text3 = base.q("id");
			string text4 = base.q("u");
			string value = base.q("tid");
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
			string text5 = string.Concat(new string[]
			{
				" STime >='",
				text,
				"' and STime <='",
				text2,
				"'"
			});
			bool flag = true;
			if (string.IsNullOrEmpty(text3))
			{
				if (!string.IsNullOrEmpty(text4.Trim()))
				{
					this.doh.Reset();
					this.doh.SqlCmd = "select Id,usercode from N_User where UserName='" + text4 + "'";
					DataTable dataTable = this.doh.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						if (dataTable.Rows[0]["usercode"].ToString().Contains(this.AdminId))
						{
							text3 = dataTable.Rows[0]["Id"].ToString();
						}
						else
						{
							text3 = "-1";
							flag = false;
						}
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					text3 = this.AdminId;
					flag = true;
				}
			}
			if (flag)
			{
				int num3 = 0;
				string text6 = string.Format("select {1} as totalcount, {0} as UserID,\r\n                                            (select Convert(varchar(10),cast(round([Point]/10.0,2) as numeric(5,2))) from N_User with(nolock) where Id={0} ) as userpoint,\r\n                                            dbo.f_GetUserName({0}) as userName,\r\n                                            (select isnull(sum(money),0) from N_User with(nolock) where Id = {0}) as money,\r\n                                            isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0) Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,\r\n                                            (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,\r\n                                            (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal\r\n                                            from Flex_UserMoneyStatAll b with(nolock)\r\n                                            where {2} and UserId={0}", text3, num3, text5);
				text6 += " union all ";
				this.doh.Reset();
				this.doh.ConditionExpress = " ParentId = " + text3;
				num3 = this.doh.Count("N_User");
				this.doh.Reset();
				this.doh.SqlCmd = SqlHelp.GetSql0("Id,UserName,Money,Point", "N_User", "ID", pageSize, num, "asc", " ParentId = " + text3);
				DataTable dataTable = this.doh.GetDataTable();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					string text7 = text5 + " and UserCode like '%" + Strings.PadLeft(dataTable.Rows[i]["Id"].ToString()) + "%'";
					if (!string.IsNullOrEmpty(value))
					{
						this.doh.Reset();
						this.doh.ConditionExpress = string.Concat(new object[]
						{
							"STime >='",
							text,
							"' and STime <='",
							text2,
							"' and UserId=",
							dataTable.Rows[i]["Id"],
							" and (Charge<>0 or GetCash<>0 or Bet<>0 or win<>0 or point<>0 or give<>0)"
						});
						int num4 = this.doh.Count("Flex_UserMoneyStatAll");
						if (num4 > 0)
						{
							text6 += string.Format("select {0} as totalcount, {1} as UserID,\r\n                                            Convert(varchar(10),cast(round({2}/10.0,2) as numeric(5,2))) as userpoint,\r\n                                            '{3}' as userName,\r\n                                            isnull(sum({4}),0)  as money,\r\n                                            isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0)  Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,\r\n                                            (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,\r\n                                            (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal\r\n                                            from Flex_UserMoneyStatAll b with(nolock)\r\n                                            where {5}", new object[]
							{
								num3,
								dataTable.Rows[i]["Id"].ToString(),
								dataTable.Rows[i]["Point"].ToString(),
								dataTable.Rows[i]["UserName"].ToString(),
								dataTable.Rows[i]["Money"].ToString(),
								text7
							});
							text6 += " union all ";
						}
					}
					else
					{
						text6 += string.Format("select {0} as totalcount, {1} as UserID,\r\n                                            Convert(varchar(10),cast(round({2}/10.0,2) as numeric(5,2))) as userpoint,\r\n                                            '{3}' as userName,\r\n                                                                                        (select isnull(sum(money),0) from N_User with(nolock) where UserCode like '%,{1},%') as money,\r\n                                            isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0)  Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,\r\n                                            (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,\r\n                                            (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal\r\n                                            from Flex_UserMoneyStatAll b with(nolock)\r\n                                            where {5}", new object[]
						{
							num3,
							dataTable.Rows[i]["Id"].ToString(),
							dataTable.Rows[i]["Point"].ToString(),
							dataTable.Rows[i]["UserName"].ToString(),
							dataTable.Rows[i]["Money"].ToString(),
							text7
						});
						text6 += " union all ";
					}
				}
				text6 += string.Format("select {2} as totalcount, '-1' as UserID,'合计' as userpoint,'' as userName,\r\n                                            (select isnull(sum(money),0) from N_User with(nolock) where UserCode like '%,{0},%') as money,\r\n                                            isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0)  Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,\r\n                                            (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,\r\n                                            (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal\r\n                                            FROM Flex_UserMoneyStatAll b with(nolock) where {1}", text3, text5 + " and UserCode like '%" + Strings.PadLeft(text3) + "%'", num3);
				this.doh.Reset();
				this.doh.SqlCmd = text6;
				dataTable = this.doh.GetDataTable();
				this._response = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
					PageBar.GetPageBar(80, "js", 2, num3, pageSize, num, "javascript:ajaxList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON(dataTable),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
			else
			{
				this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"recordcount\":0,\"table\": []}";
			}
		}

		public string ajaxGetUserNames(string ucode, string Id)
		{
			string text = "";
			ucode = ucode.Substring(ucode.IndexOf(Id) - 1);
			string[] array = ucode.Replace(",,", "_").Replace(",", "").Split(new char[]
			{
				'_'
			});
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i]))
					{
						this.doh.Reset();
						this.doh.ConditionExpress = "Id=" + array[i];
						text = text + this.doh.GetField("N_User", "UserName") + "->";
					}
				}
				text = text.Substring(0, text.Length - 2);
			}
			else
			{
				text = "---";
			}
			return text;
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
