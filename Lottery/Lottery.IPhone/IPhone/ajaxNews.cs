using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.IPhone
{
	public class ajaxNews : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "ajaxGetNewsList")
				{
					this.ajaxGetNewsList();
					goto IL_A6;
				}
				if (operType == "ajaxGetNewsContent")
				{
					this.ajaxGetNewsContent();
					goto IL_A6;
				}
				if (operType == "ajaxGetNewsTop1")
				{
					this.ajaxGetNewsTop1();
					goto IL_A6;
				}
				if (operType == "ajaxHistoryTop5")
				{
					this.ajaxHistoryTop5();
					goto IL_A6;
				}
				if (operType == "ajaxGetSscList")
				{
					this.ajaxGetSscList();
					goto IL_A6;
				}
			}
			this.DefaultResponse();
			IL_A6:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetNewsList()
		{
			string text = base.q("issoft");
			string text2 = base.q("d1");
			string text3 = base.q("d2");
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num = base.Str2Int(base.q("flag"), 0);
			string wherestr = "IsUsed =1";
			string response = "";
			new NewsDAL().GetListJSON(thispage, pagesize, wherestr, ref response);
			this._response = response;
		}

		private void ajaxGetNewsContent()
		{
			string str = base.q("id");
			string wherestr = "id =" + str;
			string text = "";
			new NewsDAL().GetListJSON(wherestr, ref text);
			this._response = text.Replace("<br/>", "");
		}

		private void ajaxGetNewsTop1()
		{
			string text = "";
			new NewsDAL().GetListJSON_Top1(ref text);
			this._response = text.Replace("<br/>", "");
		}

		private void ajaxHistoryTop5()
		{
			string text = base.q("lid");
			this.doh.Reset();
			if (text == "23")
			{
				this.doh.SqlCmd = "SELECT [IssueNum] as title,(select number from Sys_LotteryData where title=a.IssueNum) as number FROM [N_UserBet] a where lotteryId=23 and UserId=" + this.AdminId + " group by a.IssueNum order by a.IssueNum desc";
			}
			else
			{
				this.doh.SqlCmd = "SELECT TOP 5 [Title],[Number] FROM [Sys_LotteryData] with(nolock) where Type=" + text + " order by replace([Title],'-','') desc";
			}
			DataTable dataTable = this.doh.GetDataTable();
			string text2 = "\"recordcount\":1,\"table\": [信息列表]";
			string text3 = "";
			int num = 1;
			foreach (DataRow dataRow in dataTable.Rows)
			{
				string text4 = string.Concat(new object[]
				{
					"{\"no\":",
					num,
					",\"title\": \"",
					dataRow["Title"].ToString(),
					"\","
				});
				if (!string.IsNullOrEmpty(string.Concat(dataRow["Number"])))
				{
					string[] array = dataRow["Number"].ToString().Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						object obj = text4;
						text4 = string.Concat(new object[]
						{
							obj,
							"\"ball",
							i + 1,
							"\": \"",
							array[i],
							"\","
						});
					}
					text4 = text4.Substring(0, text4.Length - 1) + "}";
				}
				text3 = text3 + text4 + ",";
				num++;
			}
			text2 = text2.Replace("信息列表", text3.Substring(0, (text3.Length > 1) ? (text3.Length - 1) : 0));
			this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + text2 + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetSscList()
		{
			string text = base.q("type");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (string.IsNullOrEmpty(text))
			{
				text = "1";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@UserId";
			this.doh.AddConditionParameter("@UserId", this.AdminId);
			object field = this.doh.GetField("N_User", "Point");
			if (text == "3")
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "Point=@Point";
				this.doh.AddConditionParameter("@Point", field);
				field = this.doh.GetField("N_UserLevel", "DpPoint");
			}
			string text2 = "flag=0 and LotteryId=" + text;
			this.doh.Reset();
			this.doh.ConditionExpress = text2;
			int totalCount = this.doh.Count("Sys_PlaySmallType");
			string sql = SqlHelp.GetSql0("row_number() over (order by Sort asc) as rowid,Convert(decimal(10,2),MinBonus+" + Convert.ToDecimal(field) + "*PosBonus*2) as ownMaxBonus,*,(select Title from Sys_PlayBigType where Id=a.Radio) as bigtitle", "Sys_PlaySmallType a", "Sort", pageSize, num, "asc", text2);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(6, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
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
