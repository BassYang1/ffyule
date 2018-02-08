using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxUser : AdminCenter
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
				goto IL_261;
			case "ajaxGetReturnList":
				this.ajaxGetReturnList();
				goto IL_261;
			case "ajaxGetFKList":
				this.ajaxGetFKList();
				goto IL_261;
			case "ajaxGetFKListOnLine":
				this.ajaxGetFKListOnLine();
				goto IL_261;
			case "ajaxGetFKProListSub":
				this.ajaxGetFKProListSub();
				goto IL_261;
			case "ajaxGetQuotasList":
				this.ajaxGetQuotasList();
				goto IL_261;
			case "ajaxDel":
				this.ajaxDel();
				goto IL_261;
			case "ajaxAllDel":
				this.ajaxAllDel();
				goto IL_261;
			case "ajaxDel2":
				this.ajaxDel2();
				goto IL_261;
			case "ajaxAllDel2":
				this.ajaxAllDel2();
				goto IL_261;
			case "ajaxStates":
				this.ajaxStates();
				goto IL_261;
			case "ajaxUpdatePwd":
				this.ajaxUpdatePwd();
				goto IL_261;
			case "ajaxCheckUserName":
				this.ajaxCheckUserName();
				goto IL_261;
			case "clearLoginRecord":
				this.clearLoginRecord();
				goto IL_261;
			case "ajaxGetDelList":
				this.ajaxGetDelList();
				goto IL_261;
			case "ajaxDelStates":
				this.ajaxDelStates();
				goto IL_261;
			case "ajaxOnline":
				this.ajaxOnline();
				goto IL_261;
			case "ajaxAllOnline":
				this.ajaxAllOnline();
				goto IL_261;
			}
			this.DefaultResponse();
			IL_261:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxCheckUserName()
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "username=@username";
			this.doh.AddConditionParameter("@username", base.q("txtUserName"));
			if (this.doh.Exist("N_User"))
			{
				this._response = base.JsonResult(0, "此账号已存在，不能添加");
			}
			else
			{
				this._response = base.JsonResult(1, "帐号不存在，可以添加");
			}
		}

		private void ajaxGetList()
		{
			string value = base.q("ip");
			string text = base.q("group");
			string text2 = base.q("online");
			string text3 = base.q("isenable");
			string text4 = base.q("nologin");
			string text5 = base.q("money1");
			string text6 = base.q("money2");
			string value2 = base.q("sel2");
			string text7 = base.q("uname");
			string text8 = base.q("id");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text9 = "isDel=0";
			string fldName = "Id";
			if (!string.IsNullOrEmpty(text))
			{
				text9 = text9 + " and UserGroup =" + text;
				fldName = "Id";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text9 = text9 + " and IsOnline =" + text2;
				fldName = "Id";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text9 = text9 + " and isenable =" + text3;
				fldName = "Id";
			}
			if (!string.IsNullOrEmpty(text5) && !string.IsNullOrEmpty(text6))
			{
				string text10 = text9;
				text9 = string.Concat(new string[]
				{
					text10,
					" and Money >= ",
					text5,
					" and Money<=",
					text6
				});
				fldName = "Money";
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text9 = text9 + " and datediff(day, OnTime, getdate()) >= " + text4;
				fldName = "datediff(day, OnTime, getdate())";
			}
			if (!string.IsNullOrEmpty(value))
			{
				text9 += " and Ip in(select Ip from N_User with(nolock) group by Ip having count(Ip)>1)";
				fldName = "Ip";
			}
			if (!string.IsNullOrEmpty(text8))
			{
				text9 = text9 + " and ParentId=" + text8;
			}
			else if (!string.IsNullOrEmpty(text7.Trim()))
			{
				if ("1".Equals(value2))
				{
					text9 = text9 + " and UserName = '" + text7.Trim() + "'";
					fldName = "UserName";
				}
				if ("2".Equals(value2))
				{
					text9 = text9 + " and Id = '" + text7.Trim() + "'";
					fldName = "Id";
				}
				if ("3".Equals(value2))
				{
					object obj = text9;
					text9 = string.Concat(new object[]
					{
						obj,
						" and UPoint = '",
						Convert.ToDecimal(text7.Trim()) * 10m,
						"'"
					});
					fldName = "Point";
				}
				if ("4".Equals(value2))
				{
					text9 = text9 + " and Id in (select UserId from N_UserBank where PayAccount like '%" + text7.Trim() + "%')";
					fldName = "Id";
				}
				if ("5".Equals(value2))
				{
					text9 = text9 + " and Id in (select UserId from N_UserBank where PayName like '%" + text7.Trim() + "%')";
					fldName = "Id";
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text9;
			int totalCount = this.doh.Count("V_User");
			string sql = SqlHelp.GetSql0("*", "V_User", fldName, pageSize, num, "asc", text9);
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

		private void ajaxGetReturnList()
		{
			string text = base.q("money1");
			string text2 = base.q("money2");
			string text3 = base.q("score1");
			string text4 = base.q("score2");
			string text5 = base.q("ucode");
			string text6 = base.q("uname");
			string text7 = base.q("id");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text8 = "isDel=0";
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", string.Concat(this.Session["return"]));
			object obj = this.doh.GetField("N_User", "ParentId");
			obj = (string.IsNullOrEmpty(string.Concat(obj)) ? 0 : obj);
			if (!string.IsNullOrEmpty(string.Concat(obj)))
			{
				text8 = text8 + " and ParentId=" + obj;
				this.Session["return"] = obj;
			}
			if (!string.IsNullOrEmpty(text6))
			{
				text8 = text8 + " and (UserName LIKE '%" + text6 + "%')";
			}
			if (!string.IsNullOrEmpty(text5))
			{
				text8 = text8 + " and len(UserCode) = " + text5;
			}
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				string text9 = text8;
				text8 = string.Concat(new string[]
				{
					text9,
					" and Money >= ",
					text,
					" and Money<=",
					text2
				});
			}
			if (!string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4))
			{
				string text9 = text8;
				text8 = string.Concat(new string[]
				{
					text9,
					" and Score >= ",
					text3,
					" and Score<=",
					text4
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text8;
			int totalCount = this.doh.Count("N_User");
			string sql = SqlHelp.GetSql0("*", "V_User", "Id", pageSize, num, "asc", text8);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxGetReturnList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetFKList()
		{
			string value = base.q("ip");
			string text = base.q("group");
			string text2 = base.q("online");
			string value2 = base.q("sel1");
			string text3 = base.q("money1");
			string text4 = base.q("money2");
			string value3 = base.q("sel2");
			string text5 = base.q("uname");
			string text6 = base.q("id");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text7 = "isDel=0 ";
			string fldName = "Id";
			if (!string.IsNullOrEmpty(text))
			{
				text7 = text7 + " and UserGroup =" + text;
				fldName = "Id";
			}
			if (!string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4))
			{
				if ("1".Equals(value2))
				{
					string text8 = text7;
					text7 = string.Concat(new string[]
					{
						text8,
						" and Money >= ",
						text3,
						" and Money<=",
						text4
					});
					fldName = "Money";
				}
				if ("2".Equals(value2))
				{
					string text8 = text7;
					text7 = string.Concat(new string[]
					{
						text8,
						" and Score >= ",
						text3,
						" and Score<=",
						text4
					});
					fldName = "Score";
				}
				if ("3".Equals(value2))
				{
					string text8 = text7;
					text7 = string.Concat(new string[]
					{
						text8,
						" and datediff(day, OnTime, getdate()) >= ",
						text3,
						" and datediff(day, OnTime, getdate())<=",
						text4
					});
					fldName = "datediff(day, OnTime, getdate())";
				}
			}
			if (!string.IsNullOrEmpty(value))
			{
				text7 += " and Ip in(select Ip from N_User with(nolock) group by Ip having count(Ip)>1)";
				fldName = "Ip";
			}
			if (!string.IsNullOrEmpty(text6))
			{
				text7 = text7 + " and ParentId=" + text6;
			}
			else if (!string.IsNullOrEmpty(text5.Trim()))
			{
				if ("1".Equals(value3))
				{
					text7 = text7 + " and ParentId = (select Id from N_User where UserName = '" + text5.Trim() + "')";
					fldName = "UserName";
				}
			}
			else
			{
				text7 += " and UserName=''";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text7;
			int totalCount = this.doh.Count("N_User");
			string sql = SqlHelp.GetSql0("*", "V_User", fldName, pageSize, num, "asc", text7);
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

		private void ajaxGetFKListOnLine()
		{
			string value = base.q("ip");
			string text = base.q("group");
			string text2 = base.q("online");
			string value2 = base.q("sel1");
			string text3 = base.q("money1");
			string text4 = base.q("money2");
			string value3 = base.q("sel2");
			string text5 = base.q("uname");
			string text6 = base.q("id");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text7 = "isDel=0 ";
			string fldName = "Id";
			if (!string.IsNullOrEmpty(text))
			{
				text7 = text7 + " and UserGroup =" + text;
				fldName = "Id";
			}
			if (!string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4))
			{
				if ("1".Equals(value2))
				{
					string text8 = text7;
					text7 = string.Concat(new string[]
					{
						text8,
						" and Money >= ",
						text3,
						" and Money<=",
						text4
					});
					fldName = "Money";
				}
				if ("2".Equals(value2))
				{
					string text8 = text7;
					text7 = string.Concat(new string[]
					{
						text8,
						" and Score >= ",
						text3,
						" and Score<=",
						text4
					});
					fldName = "Score";
				}
				if ("3".Equals(value2))
				{
					string text8 = text7;
					text7 = string.Concat(new string[]
					{
						text8,
						" and datediff(day, OnTime, getdate()) >= ",
						text3,
						" and datediff(day, OnTime, getdate())<=",
						text4
					});
					fldName = "datediff(day, OnTime, getdate())";
				}
			}
			if (!string.IsNullOrEmpty(value))
			{
				text7 += " and Ip in(select Ip from N_User with(nolock) group by Ip having count(Ip)>1)";
				fldName = "Ip";
			}
			if (!string.IsNullOrEmpty(text6))
			{
				text7 = text7 + " and ParentId=" + text6;
				text7 += " and IsOnline =1";
			}
			else if (!string.IsNullOrEmpty(text5.Trim()))
			{
				if ("1".Equals(value3))
				{
					text7 = text7 + " and ParentId = (select Id from N_User where UserName = '" + text5.Trim() + "')";
					text7 += " and IsOnline =1";
					fldName = "UserName";
				}
			}
			else
			{
				text7 += " and UserName=''";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text7;
			int totalCount = this.doh.Count("N_User");
			string sql = SqlHelp.GetSql0("*", "V_User", fldName, pageSize, num, "asc", text7);
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

		public void ajaxGetFKProListSub()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("id");
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
			string text5 = string.Concat(new string[]
			{
				" STime >='",
				text,
				"' and STime <'",
				text2,
				"'"
			});
			bool flag = true;
			if (string.IsNullOrEmpty(text3))
			{
				if (!string.IsNullOrEmpty(text4.Trim()))
				{
					this.doh.Reset();
					this.doh.SqlCmd = "select Id from N_User where UserName='" + text4 + "'";
					DataTable dataTable = this.doh.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						text3 = dataTable.Rows[0]["Id"].ToString();
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					text3 = "-1";
					flag = false;
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
					text6 += string.Format("select {0} as totalcount, {1} as UserID,\r\n                                            Convert(varchar(10),cast(round({2}/10.0,2) as numeric(5,2))) as userpoint,\r\n                                            '{3}' as userName,\r\n                                            (select isnull(sum(money),0) from N_User with(nolock) where UserCode like '%,{1},%') as money,\r\n                                            isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0)  Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,\r\n                                            (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,\r\n                                            (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal\r\n                                            from Flex_UserMoneyStatAll b with(nolock)\r\n                                            where {5}", new object[]
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
				text6 += string.Format("select {2} as totalcount, '-1' as UserID,'合计' as userpoint,'' as userName,\r\n                                            (select isnull(sum(money),0) from N_User with(nolock) where UserCode like '%,{0},%') as money,\r\n                                            isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0)  Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,\r\n                                            (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,\r\n                                            (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal\r\n                                            FROM Flex_UserMoneyStatAll b with(nolock) where {1}", text3, text5 + " and UserCode like '%" + Strings.PadLeft(text3) + "%'", num3);
				this.doh.Reset();
				this.doh.SqlCmd = text6;
				dataTable = this.doh.GetDataTable();
				this._response = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
					PageBar.GetPageBar(3, "js", 2, num3, pageSize, num, "javascript:ajaxList(<#page#>);"),
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

		private void ajaxGetQuotasList()
		{
			string text = base.q("uname");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text2 = "";
			if (!string.IsNullOrEmpty(text))
			{
				text2 = text2 + " UserName LIKE '%" + text + "%'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("V_UserQuotasList");
			string sql = SqlHelp.GetSql0("*", "V_UserQuotasList", "Id", pageSize, num, "desc", text2);
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

		private void ajaxDel()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			this.doh.AddFieldItem("isDel", 1);
			int num = this.doh.Update("N_User");
			if (num > 0)
			{
				new LogAdminOperDAL().SaveLog(this.AdminId, text, "会员管理", "删除了Id为" + text + "的会员");
				this._response = base.JsonResult(1, "删除成功");
			}
			else
			{
				this._response = base.JsonResult(0, "删除失败");
			}
		}

		private void ajaxAllDel()
		{
			string text = base.f("ids");
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "id=@id";
				this.doh.AddConditionParameter("@id", array[i]);
				this.doh.AddFieldItem("isDel", 1);
				this.doh.AddFieldItem("IsOnline", 0);
				this.doh.AddFieldItem("SessionId", Guid.NewGuid().ToString().Replace("-", ""));
				int num = this.doh.Update("N_User");
				new LogAdminOperDAL().SaveLog(this.AdminId, array[i], "会员管理", "删除了Id为" + array[i] + "的会员");
			}
			this._response = base.JsonResult(1, "删除成功");
		}

		private void ajaxDel2()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			int num = this.doh.Delete("N_User");
			if (num > 0)
			{
				new LogAdminOperDAL().SaveLog(this.AdminId, text, "会员管理", "彻底删除了Id为" + text + "的会员");
				this._response = base.JsonResult(1, "删除成功");
			}
			else
			{
				this._response = base.JsonResult(0, "删除失败");
			}
		}

		private void ajaxAllDel2()
		{
			string text = base.f("ids");
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "id=@id";
				this.doh.AddConditionParameter("@id", array[i]);
				int num = this.doh.Delete("N_User");
				new LogAdminOperDAL().SaveLog(this.AdminId, array[i], "会员管理", "彻底删除Id为" + array[i] + "的会员");
			}
			this._response = base.JsonResult(1, "删除成功");
		}

		private void ajaxStates()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField("N_User", "IsEnable");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsEnable", (num == 0) ? 1 : 0);
			this.doh.AddFieldItem("IsOnline", 0);
			this.doh.AddFieldItem("SessionId", Guid.NewGuid().ToString().Replace("-", ""));
			int num2 = this.doh.Update("N_User");
			new LogAdminOperDAL().SaveLog(this.AdminId, text, "会员管理", "锁定了Id为" + text + "的会员");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxOnline()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsOnline", 0);
			this.doh.AddFieldItem("SessionId", Guid.NewGuid().ToString().Replace("-", ""));
			int num = this.doh.Update("N_User");
			new LogAdminOperDAL().SaveLog(this.AdminId, text, "会员管理", "强制Id为" + text + "的会员下线");
			if (num > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxAllOnline()
		{
			string text = base.f("ids");
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "id=" + array[i];
				this.doh.AddFieldItem("IsOnline", 0);
				this.doh.AddFieldItem("SessionId", Guid.NewGuid().ToString().Replace("-", ""));
				int num = this.doh.Update("N_User");
				new LogAdminOperDAL().SaveLog(this.AdminId, array[i], "会员管理", "强制Id为" + array[i] + "的会员下线");
			}
			this._response = base.JsonResult(1, "下线成功");
		}

		private void ajaxUpdatePwd()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			this.doh.AddConditionParameter("@Password", MD5.Last64(MD5.Lower32("123456")));
			int num = this.doh.Update("N_User");
			new LogAdminOperDAL().SaveLog(this.AdminId, text, "会员管理", "重置Id为" + text + "的会员的密码为123456");
			if (num > 0)
			{
				this._response = base.JsonResult(1, "重置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "重置失败");
			}
		}

		private void clearLoginRecord()
		{
			new LogSysDAL().DeleteUserLogs();
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "会员管理", "清空会员登陆日志");
			this._response = base.JsonResult(1, "成功清空");
		}

		private void ajaxGetDelList()
		{
			string text = base.q("ucode");
			string text2 = base.q("uname");
			string text3 = base.q("id");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text4 = "isDel=1";
			if (!string.IsNullOrEmpty(text3))
			{
				text4 = text4 + " and ParentId=" + text3;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text4 = text4 + " and (UserName LIKE '%" + text2 + "%')";
			}
			if (!string.IsNullOrEmpty(text))
			{
				text4 = text4 + " and len(UserCode) = " + text;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("N_User");
			string sql = SqlHelp.GetSql0("*", "V_User", "Id", pageSize, num, "asc", text4);
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

		private void ajaxDelStates()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsDel", 0);
			int num = this.doh.Update("N_User");
			new LogAdminOperDAL().SaveLog(this.AdminId, text, "会员管理", "恢复了Id为" + text + "的会员");
			if (num > 0)
			{
				this._response = base.JsonResult(1, "恢复成功");
			}
			else
			{
				this._response = base.JsonResult(0, "恢复失败");
			}
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
