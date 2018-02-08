using System;
using System.Data;
using System.Web.UI.HtmlControls;
using Lottery.DAL;
using Lottery.DAL.Flex;

namespace Lottery.AdminFile
{
	public class betZhinfo : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			if (!this.Page.IsPostBack)
			{
				string text = base.Str2Str(base.q("id"));
				this.doh.Reset();
				this.doh.SqlCmd = "select *,dbo.f_GetBetState(State) as StateName2 from V_UserBetZhDetail a where Id=" + text;
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					this.UserName = dataRow["UserName"].ToString();
					this.L_Lottery = dataRow["LotteryName"].ToString();
					this.L_PlayType = dataRow["PlayName"].ToString();
					this.L_IssueNumber = dataRow["IssueNum"].ToString();
					this.L_SingleMoney = Convert.ToDecimal(dataRow["SingleMoney"]).ToString("0.0000") + " 元";
					this.L_RealGet = Convert.ToDecimal(dataRow["RealGet"]).ToString("0.0000") + " 元";
					this.L_Times = dataTable.Rows[0]["Times"].ToString();
					this.L_Total = Convert.ToDecimal(Convert.ToDecimal(dataRow["Total"])).ToString("0.0000");
					decimal num = Convert.ToDecimal(dataRow["Point"]);
					decimal num2 = Convert.ToDecimal(dataRow["Bonus"]);
					this.L_PointMoney = (Convert.ToDecimal(this.L_Total) * num / 100m).ToString("0.0000") + " 元";
					this.L_Bonus = Convert.ToDecimal(dataRow["WinBonus"]).ToString("0.0000") + " 元";
					this.L_Point = Convert.ToDecimal(num2).ToString("0.0000") + "/" + Convert.ToDecimal(num).ToString("0.0000") + " %";
					if (dataRow["PlayCode"].ToString().Contains("3HX"))
					{
						this.L_Point = string.Concat(new string[]
						{
							Convert.ToDecimal(num2 / 2m).ToString("0.0000"),
							"/",
							Convert.ToDecimal(num2).ToString("0.0000"),
							"/",
							Convert.ToDecimal(num).ToString("0.0000"),
							" %"
						});
					}
					this.L_Num = dataRow["Num"].ToString();
					int num3 = Convert.ToInt32(dataRow["State"]);
					this.L_State = dataRow["StateName2"].ToString();
					this.L_STime = dataRow["STime"].ToString();
					this.L_STime2 = dataRow["STime2"].ToString();
					this.L_Pos = dataRow["Pos"].ToString();
					this.L_WinNum = dataRow["WinNum"].ToString();
					if (this.L_Pos != "")
					{
						string text2 = "";
						string[] array = this.L_Pos.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							if (Convert.ToInt32(array[i]) == 1)
							{
								text2 = text2 + "," + i.ToString();
							}
						}
						this.L_Pos = "任选位数：" + text2.Substring(1).Replace("0", "万位").Replace("1", "千位").Replace("2", "百位").Replace("3", "十位").Replace("4", "个位") + "<br/>";
					}
					this.L_Detail = this.L_Pos + BetDetailDAL.GetBetDetail(Convert.ToDateTime(dataRow["STime2"]).ToString("yyyyMMdd"), dataRow["UserId"].ToString(), text);
					if (string.IsNullOrEmpty(this.L_Detail))
					{
						this.L_Detail = this.L_Pos + dataRow["Detail"].ToString();
					}
					this.NumberShow.Visible = false;
					if (num3 >= 2)
					{
						this.NumberShow.Visible = true;
						this.doh.Reset();
						this.doh.ConditionExpress = "Type=@Type and Title=@Title";
						this.doh.AddConditionParameter("@Type", dataRow["LotteryId"].ToString());
						this.doh.AddConditionParameter("@Title", this.L_IssueNumber);
						object field = this.doh.GetField("Sys_LotteryData", "Number");
						this.L_Number = string.Concat(field);
					}
				}
				else
				{
					base.Response.Write("参数错误");
					base.Response.End();
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string L_Bonus;

		public string L_Detail;

		public string L_IssueNumber;

		public string L_Lottery;

		public string L_Num;

		public string L_Number;

		public string L_PlayType;

		public string L_Point;

		public string L_PointMoney;

		public string L_Pos;

		public string L_RealGet;

		public string L_SingleMoney;

		public string L_State;

		public string L_STime;

		public string L_STime2;

		public string L_Times;

		public string L_Total;

		public string UserId;

		public string UserName;

		public string L_WinNum;

		protected HtmlForm form1;

		protected HtmlTableRow NumberShow;
	}
}
