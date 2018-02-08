using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxProfitloss : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "ajaxGetProList")
				{
					this.ajaxGetProList();
					goto IL_90;
				}
				if (operType == "ajaxGetProListTeam")
				{
					this.ajaxGetProListTeam();
					goto IL_90;
				}
				if (operType == "ajaxGetProReturnListTeam")
				{
					this.ajaxGetProReturnListTeam();
					goto IL_90;
				}
				if (operType == "ajaxUserDetail")
				{
					this.ajaxUserDetail();
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

		private void ajaxGetProList()
		{
			string text = base.q("u");
			string text2 = base.q("Id");
			string text3 = base.q("d1");
			string text4 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text5 = "";
			if (text.Trim().Length > 0)
			{
				text5 = text5 + " UserName LIKE '%" + text + "%'";
			}
			else if (!string.IsNullOrEmpty(text2))
			{
				text5 = text5 + " ParentId=" + text2;
			}
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
				text6 = text6 + dataTable.Rows[i]["Id"].ToString() + ",";
			}
			DataTable dt = new DataTable();
			if (text6.Length > 1)
			{
				text6 = text6.Substring(0, text6.Length - 1);
				dt = this.GetUserMoneyStat(text3, text4, text6);
			}
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dt),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
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
				decimal d3 = 0m;
				decimal d4 = 0m;
				decimal num2 = 0m;
				decimal d5 = 0m;
				decimal d6 = 0m;
				decimal d7 = 0m;
				decimal num3 = 0m;
				decimal num4 = 0m;
				decimal d8 = 0m;
				decimal num5 = 0m;
				decimal num6 = 0m;
				decimal d9 = 0m;
				string userMoneyStatSql = this.GetUserMoneyStatSql(d1, d2, array[i]);
				this.doh.Reset();
				this.doh.SqlCmd = userMoneyStatSql;
				DataTable dataTable2 = this.doh.GetDataTable();
				for (int j = 0; j < dataTable2.Rows.Count; j++)
				{
					num = Convert.ToDecimal(dataTable2.Rows[j]["money"].ToString());
					d3 = Convert.ToDecimal(dataTable2.Rows[j]["Charge"].ToString());
					d4 = Convert.ToDecimal(dataTable2.Rows[j]["GetCash"].ToString());
					num2 = Convert.ToDecimal(dataTable2.Rows[j]["Bet"].ToString());
					d5 = Convert.ToDecimal(dataTable2.Rows[j]["Point"].ToString());
					d6 = Convert.ToDecimal(dataTable2.Rows[j]["Win"].ToString());
					d7 = Convert.ToDecimal(dataTable2.Rows[j]["Cancellation"].ToString());
					num3 = Convert.ToDecimal(dataTable2.Rows[j]["TranAccIn"].ToString());
					num4 = Convert.ToDecimal(dataTable2.Rows[j]["TranAccOut"].ToString());
					d8 = Convert.ToDecimal(dataTable2.Rows[j]["Give"].ToString());
					num5 = Convert.ToDecimal(dataTable2.Rows[j]["AgentFH"].ToString());
					num6 = Convert.ToDecimal(dataTable2.Rows[j]["Other"].ToString());
					d9 = Convert.ToDecimal(dataTable2.Rows[j]["Change"].ToString());
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow["Id"] = array[i];
				dataRow["userId"] = dataTable2.Rows[0]["Id"].ToString();
				dataRow["userName"] = dataTable2.Rows[0]["userName"].ToString();
				dataRow["chindcount"] = dataTable2.Rows[0]["chindcount"].ToString();
				dataRow["money"] = num.ToString();
				dataRow["Charge"] = d3.ToString();
				dataRow["GetCash"] = d4.ToString();
				dataRow["Bet"] = (num2 - d7).ToString();
				dataRow["Point"] = d5.ToString();
				dataRow["Win"] = d6.ToString();
				dataRow["Cancellation"] = d7.ToString();
				dataRow["TranAccIn"] = num3.ToString();
				dataRow["TranAccOut"] = num4.ToString();
				dataRow["Give"] = d8.ToString();
				dataRow["AgentFH"] = num5.ToString();
				dataRow["Other"] = num6.ToString();
				dataRow["Change"] = d9.ToString();
				dataRow["Total"] = string.Concat(d6 + d8 + d9 + d7 + d5 - num2);
				dataRow["MoneyTotal"] = string.Concat(d3 - d4);
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
				") as userName,(select count(*) from N_User with(nolock) where parentId=",
				userId,
				") as chindcount,(select money from N_User with(nolock) where Id=",
				userId,
				") as money,isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0) Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.AgentFH),0) AgentFH,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change FROM N_UserMoneyStatAll b where STime>='",
				d1,
				"' and STime <'",
				d2,
				"' and (UserId=",
				userId,
				")"
			});
		}

		private void ajaxGetProListTeam()
		{
			string text = base.q("u");
			string text2 = base.q("Id");
			string text3 = base.q("d1");
			string text4 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text5 = "isDel=0";
			if (text.Trim().Length > 0)
			{
				text5 = text5 + " and UserName LIKE '%" + text + "%'";
			}
			else if (string.IsNullOrEmpty(text2))
			{
				text5 += " and ParentId=0";
				this.Session["return"] = 0;
			}
			else
			{
				text5 = text5 + " and ParentId=" + text2;
				this.Session["return"] = text2;
			}
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
				text6 = text6 + dataTable.Rows[i]["Id"].ToString() + ",";
			}
			DataTable dt = new DataTable();
			if (text6.Length > 1)
			{
				text6 = text6.Substring(0, text6.Length - 1);
				dt = this.GetUserMoneyStatTeam(text3, text4, text6);
			}
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dt),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetProReturnListTeam()
		{
			string text = base.q("u");
			string text2 = base.q("Id");
			string text3 = base.q("d1");
			string text4 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text5 = "isDel=0";
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", string.Concat(this.Session["return"]));
			object obj = this.doh.GetField("N_User", "ParentId");
			obj = (string.IsNullOrEmpty(string.Concat(obj)) ? 0 : obj);
			if (!string.IsNullOrEmpty(string.Concat(obj)))
			{
				text5 = text5 + " and ParentId=" + obj;
				this.Session["return"] = obj;
			}
			if (text.Trim().Length > 0)
			{
				text5 = text5 + " and UserName LIKE '%" + text + "%'";
			}
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
				text6 = text6 + dataTable.Rows[i]["Id"].ToString() + ",";
			}
			DataTable dt = new DataTable();
			if (text6.Length > 1)
			{
				text6 = text6.Substring(0, text6.Length - 1);
				dt = this.GetUserMoneyStatTeam(text3, text4, text6);
			}
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"",
				PageBar.GetPageBar(3, "js", 2, totalCount, pageSize, num, "javascript:ajaxGetProReturnListTeam(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dt),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private DataTable GetUserMoneyStatTeam(string d1, string d2, string userId)
		{
			DataTable dataTable = this.CreatDataTable();
			string[] array = userId.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				decimal num = 0m;
				decimal d3 = 0m;
				decimal num2 = 0m;
				decimal num3 = 0m;
				decimal num4 = 0m;
				decimal d4 = 0m;
				decimal num5 = 0m;
				decimal d5 = 0m;
				decimal d6 = 0m;
				decimal num6 = 0m;
				decimal d7 = 0m;
				decimal d8 = 0m;
				decimal num7 = 0m;
				string userMoneyStatTeamSql = this.GetUserMoneyStatTeamSql(d1, d2, array[i]);
				this.doh.Reset();
				this.doh.SqlCmd = userMoneyStatTeamSql;
				DataTable dataTable2 = this.doh.GetDataTable();
				for (int j = 0; j < dataTable2.Rows.Count; j++)
				{
					num = Convert.ToDecimal(dataTable2.Rows[j]["money"].ToString());
					d3 += Convert.ToDecimal(dataTable2.Rows[j]["Charge"].ToString());
					num2 += Convert.ToDecimal(dataTable2.Rows[j]["GetCash"].ToString());
					num3 += Convert.ToDecimal(dataTable2.Rows[j]["Bet"].ToString());
					num4 += Convert.ToDecimal(dataTable2.Rows[j]["Point"].ToString());
					d4 += Convert.ToDecimal(dataTable2.Rows[j]["Win"].ToString());
					num5 += Convert.ToDecimal(dataTable2.Rows[j]["Cancellation"].ToString());
					d5 += Convert.ToDecimal(dataTable2.Rows[j]["TranAccIn"].ToString());
					d6 += Convert.ToDecimal(dataTable2.Rows[j]["TranAccOut"].ToString());
					num6 += Convert.ToDecimal(dataTable2.Rows[j]["Give"].ToString());
					d7 += Convert.ToDecimal(dataTable2.Rows[j]["AgentFH"].ToString());
					d8 += Convert.ToDecimal(dataTable2.Rows[j]["Other"].ToString());
					num7 += Convert.ToDecimal(dataTable2.Rows[j]["Change"].ToString());
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow["Id"] = array[i];
				dataRow["userId"] = dataTable2.Rows[0]["Id"].ToString();
				dataRow["userName"] = dataTable2.Rows[0]["userName"].ToString();
				dataRow["chindcount"] = dataTable2.Rows[0]["chindcount"].ToString();
				dataRow["money"] = num.ToString();
				dataRow["Charge"] = d3.ToString();
				dataRow["GetCash"] = num2.ToString();
				dataRow["Bet"] = (num3 - num5).ToString();
				dataRow["Point"] = num4.ToString();
				dataRow["Win"] = d4.ToString();
				dataRow["Cancellation"] = num5.ToString();
				dataRow["TranAccIn"] = d5.ToString();
				dataRow["TranAccOut"] = d6.ToString();
				dataRow["Give"] = num6.ToString();
				dataRow["AgentFH"] = d7.ToString();
				dataRow["Other"] = d8.ToString();
				dataRow["Change"] = num7.ToString();
				dataRow["Total"] = string.Concat(d4 + num6 + num7 + num5 + num4 - num3);
				dataRow["MoneyTotal"] = string.Concat(d3 - num2);
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		private string GetUserMoneyStatTeamSql(string d1, string d2, string userId)
		{
			string text = " and dbo.f_GetUserCode(UserId) like '%" + Strings.PadLeft(userId) + "%'";
			return string.Concat(new string[]
			{
				"SELECT ",
				userId,
				" as Id,(select userName from N_User with(nolock) where Id=",
				userId,
				") as userName,(select count(*) from N_User with(nolock) where parentId=",
				userId,
				") as chindcount,(select money from N_User with(nolock) where Id=",
				userId,
				") as money,isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0) Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.AgentFH),0) AgentFH,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change FROM N_UserMoneyStatAll b where STime>='",
				d1,
				"' and STime<'",
				d2,
				"' ",
				text
			});
		}

		private void ajaxUserDetail()
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
			string arg = string.Concat(new string[]
			{
				"STime>='",
				text,
				"' and STime <'",
				text2,
				"'"
			});
			string text5 = string.Format("SELECT a.[Id]\r\n                                            ,[UserName]\r\n                                            ,[Money]\r\n                                            ,a.[Point] as userpoint\r\n                                            ,isnull(sum(Charge),0) as Charge\r\n                                            ,isnull(sum(getcash),0)  as getcash\r\n                                            ,isnull(sum(Bet),0)-isnull(sum(Cancellation),0)  as bet\r\n                                            ,isnull(sum(Win),0)  as Win\r\n                                            ,isnull(sum(b.Point),0)  as Point\r\n                                            ,isnull(sum(Give),0)  as Give\r\n                                            ,isnull(sum(agentFH),0) as agentFH\r\n                                            ,isnull(sum(other),0)  as other\r\n                                            ,(isnull(sum(Win),0)+isnull(sum(b.Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0)  as total\r\n                                            ,isnull(sum(Charge),0)-isnull(sum(getcash),0)  as moneytotal\r\n                                            ,(SELECT count(*) FROM [N_UserMoneyStatAll] with(nolock) where dbo.f_GetUserCode(UserId) like '%,'+Convert(varchar(10),a.id)+',%' and Convert(varchar(10),STime,120)=Convert(varchar(10),getdate(),120) and Bet-Cancellation>0) as YxNum\r\n                                            ,(select count(*) from N_User where dbo.f_GetUserCode(id) like '%,'+Convert(varchar(10),a.id)+',%' and IsOnline=1) as OnLineNum\r\n                                            ,(select count(*) from N_User where dbo.f_GetUserCode(id) like '%,'+Convert(varchar(10),a.id)+',%' and Point=129) as point129\r\n                                            ,(select count(*) from N_User where dbo.f_GetUserCode(id) like '%,'+Convert(varchar(10),a.id)+',%' and Point=128) as point128\r\n                                            ,(select count(*) from N_User where dbo.f_GetUserCode(id) like '%,'+Convert(varchar(10),a.id)+',%' and Point=127) as point127\r\n                                            ,(select count(*) from N_User where dbo.f_GetUserCode(id) like '%,'+Convert(varchar(10),a.id)+',%' and Point=126) as point126\r\n                                            FROM [V_User] a left join [N_UserMoneyStatAll] b on dbo.f_GetUserCode(b.UserId) like '%,'+Convert(varchar(10),a.id)+',%'\r\n                                            where {0}", arg);
			if (text4.Trim().Length > 0)
			{
				text5 = text5 + " and UserName like '%" + text4 + "%'";
			}
			else
			{
				text5 += " and UserName = ''";
			}
			text5 += " group by a.[Id],[UserName],[Money],a.[Point]";
			this.doh.Reset();
			this.doh.SqlCmd = text5;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private DataTable CreatDataTable()
		{
			return new DataTable
			{
				Columns = 
				{
					"Id",
					"userId",
					"userName",
					"chindcount",
					"money",
					"Charge",
					"GetCash",
					"Bet",
					"Point",
					"Win",
					"Cancellation",
					"TranAccIn",
					"TranAccOut",
					"Give",
					"Other",
					"AgentFH",
					"Change",
					"Total",
					"MoneyTotal"
				}
			};
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
