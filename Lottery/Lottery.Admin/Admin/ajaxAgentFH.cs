using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxAgentFH : AdminCenter
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
			case "ajaxGetDetailList":
				this.ajaxGetDetailList();
				goto IL_148;
			case "ajaxDetailStates":
				this.ajaxDetailStates();
				goto IL_148;
			case "ajaxGetAgentFHList":
				this.ajaxGetProListSub();
				goto IL_148;
			case "ajaxGetAgentFHRecord":
				this.ajaxGetAgentFHRecord();
				goto IL_148;
			case "ajaxGetAgent1List":
				this.ajaxGetAgent1List();
				goto IL_148;
			case "ajaxAgent1States":
				this.ajaxAgent1States();
				goto IL_148;
			case "ajaxGetAgent2List":
				this.ajaxGetAgent2List();
				goto IL_148;
			case "ajaxAgent2States":
				this.ajaxAgent2States();
				goto IL_148;
			}
			this.DefaultResponse();
			IL_148:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetDetailList()
		{
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text = "";
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("Act_AgentFHDetail");
			string sql = SqlHelp.GetSql0("*", "Act_AgentFHDetail", "Id", pageSize, num, "asc", text);
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

		private void ajaxDetailStates()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField("Act_AgentFHDetail", "IsUsed");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsUsed", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Act_AgentFHDetail");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxGetProListSub()
		{
			this.doh.Reset();
			this.doh.SqlCmd = "select * from N_User with(nolock) where IsDel=0 and AgentId<>0 order by Id asc";
			DataTable dataTable = this.doh.GetDataTable();
			string text = "";
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				text = text + "," + dataTable.Rows[i]["Id"].ToString();
			}
			DataTable dt = new DataTable();
			if (text.Length > 1)
			{
				text = text.Substring(1, text.Length - 1);
				dt = this.GetUserMoneyStatSub(text);
			}
			this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dt) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private DataTable GetUserMoneyStatSub(string userId)
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
				decimal d = 0m;
				decimal num3 = 0m;
				decimal num4 = 0m;
				decimal num5 = 0m;
				decimal num6 = 0m;
				decimal num7 = 0m;
				string userMoneyStatSubSql = this.GetUserMoneyStatSubSql(array[i]);
				this.doh.Reset();
				this.doh.SqlCmd = userMoneyStatSubSql;
				DataTable dataTable2 = this.doh.GetDataTable();
				for (int j = 0; j < dataTable2.Rows.Count; j++)
				{
					num += Convert.ToDecimal(dataTable2.Rows[j]["Bet"].ToString());
					num2 += Convert.ToDecimal(dataTable2.Rows[j]["Point"].ToString());
					d += Convert.ToDecimal(dataTable2.Rows[j]["Win"].ToString());
					num3 += Convert.ToDecimal(dataTable2.Rows[j]["Cancellation"].ToString());
					num4 += Convert.ToDecimal(dataTable2.Rows[j]["Give"].ToString());
					num5 += Convert.ToDecimal(dataTable2.Rows[j]["Other"].ToString());
					num6 += Convert.ToDecimal(dataTable2.Rows[j]["AdminAddDed"].ToString());
					num7 += Convert.ToDecimal(dataTable2.Rows[j]["Change"].ToString());
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow["Id"] = array[i];
				dataRow["userName"] = dataTable2.Rows[0]["userName"].ToString();
				dataRow["agentId"] = dataTable2.Rows[0]["agentId"].ToString();
				dataRow["Bet"] = (num - num3).ToString();
				dataRow["Total"] = string.Concat(d + num4 + num7 + num5 + num3 + num2 - num - num6);
				dataRow["AgentFHMoney"] = (string.IsNullOrEmpty(string.Concat(dataTable2.Rows[0]["AgentFHMoney"])) ? 0m : Convert.ToDecimal(dataTable2.Rows[0]["AgentFHMoney"]));
				dataRow["STime"] = dataTable2.Rows[0]["STime"].ToString();
				dataRow["Point"] = num2.ToString();
				dataRow["Win"] = d.ToString();
				dataRow["Cancellation"] = num3.ToString();
				dataRow["Give"] = num4.ToString();
				dataRow["Other"] = num5.ToString();
				dataRow["AdminAddDed"] = num6.ToString();
				dataRow["Change"] = num7.ToString();
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		private string GetUserMoneyStatSubSql(string userId)
		{
			return string.Concat(new string[]
			{
				"SELECT ",
				userId,
				" as Id,(select userName from N_User with(nolock) where Id=",
				userId,
				") as userName,(select agentId from N_User with(nolock) where Id=",
				userId,
				") as agentId,(select top 1 Inmoney from Act_AgentFHRecord where UserId=",
				userId,
				" order by Id desc) as AgentFHMoney,(SELECT [STime] FROM [Act_AgentFHSet]) as STime,isnull(sum(b.Bet),0) Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.ChargeDeduct),0) ChargeDeduct,isnull(sum(b.ChargeUp),0) ChargeUp,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change FROM N_UserMoneyStatAll b with(nolock)  where (STime between (SELECT max([STime]) FROM [Act_AgentFHSet]) and getdate()) and (UserId in (SELECT [Id] FROM [N_User] where UserCode like '%",
				Strings.PadLeft(userId),
				"%') or UserId=",
				userId,
				")"
			});
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
						"agentId",
						typeof(int)
					},
					{
						"AgentFHMoney",
						typeof(decimal)
					},
					{
						"STime",
						typeof(string)
					},
					{
						"Bet",
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
						"Give",
						typeof(decimal)
					},
					{
						"Other",
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

		private void ajaxGetAgentFHRecord()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("code");
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
			string text5 = "";
			string text6 = "1=1";
			if (!string.IsNullOrEmpty(text4))
			{
				text6 = text6 + " and UserName LIKE '%" + text4 + "%'";
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text7 = text6;
				text6 = string.Concat(new string[]
				{
					text7,
					" and STime >='",
					text,
					"' and STime <'",
					text2,
					"'"
				});
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text6 = text6 + " and AgentId=" + text3;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text6;
			int totalCount = this.doh.Count("V_AgentFHRecord");
			text5 = text5 + " SELECT '0' as [Id],'0' as [UserId],'全部合计' as [username],'0' as [AgentId],'-' as [GroupName],isnull(sum(contractcount),0) as contractcount,max([StartTime]) as [StartTime],max([EndTime]) as [EndTime],isnull(sum([Bet]),0) as [Bet],isnull(sum([Total]),0) as [Total],0 as [Per],isnull(sum([InMoney]),0) as [InMoney],getdate() as [STime],'-' as [Remark]\r\n                    FROM [V_AgentFHRecord] where " + text6;
			text5 += " union all ";
			text5 += " select * from ( ";
			text5 += SqlHelp.GetSql0("*", "V_AgentFHRecord", "id", pageSize, num, "desc", text6);
			text5 += " ) YouleTable order by Id desc ";
			this.doh.Reset();
			this.doh.SqlCmd = text5;
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

		private void ajaxGetAgent1List()
		{
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text = "";
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("Act_Agent1");
			string sql = SqlHelp.GetSql0("*", "Act_Agent1", "Id", pageSize, num, "asc", text);
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

		private void ajaxAgent1States()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField("Act_Agent1", "IsUsed");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsUsed", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Act_Agent1");
			if (num2 > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private void ajaxGetAgent2List()
		{
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text = "";
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("Act_Agent2");
			string sql = SqlHelp.GetSql0("*", "Act_Agent2", "Id", pageSize, num, "asc", text);
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

		private void ajaxAgent2States()
		{
			string text = base.f("id");
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", text);
			object field = this.doh.GetField("Act_Agent2", "IsUsed");
			int num = Convert.ToInt32(field);
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + text;
			this.doh.AddFieldItem("IsUsed", (num == 0) ? 1 : 0);
			int num2 = this.doh.Update("Act_Agent2");
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
