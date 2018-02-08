using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class betUpdate : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			if (!this.Page.IsPostBack)
			{
				string text = base.Str2Str(base.q("id"));
				string text2 = " Id=" + text;
				this.doh.Reset();
				this.doh.ConditionExpress = text2;
				this.txtId.Text = text;
				string sql = SqlHelp.GetSql0("Id,UserId,dbo.f_GetUserName(UserId) as UserName,dbo.f_GetUserCode(UserId) as UserCode,UserMoney,PlayId,dbo.f_GetPlayName(PlayId) as PlayName,PlayCode,LotteryId,dbo.f_GetLotteryName(LotteryId) as LotteryName,IssueNum,SingleMoney,Times,Num,Detail,DX,DS,Times*Total as Total,Point,PointMoney,Bonus,WinNum,WinBonus,RealGet,Pos,STime,STime2,IsOpen,State,dbo.f_GetBetState(State) as StateName,IsDelay,IsWin,STime9", "N_UserBet", "STime2", 999, 1, "desc", text2);
				this.doh.Reset();
				this.doh.SqlCmd = sql;
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					this.UserName = dataRow["UserName"].ToString();
					this.L_Lottery = dataRow["LotteryName"].ToString();
					this.L_PlayType = dataRow["PlayName"].ToString();
					this.L_IssueNumber = dataRow["IssueNum"].ToString();
					this.L_SingleMoney = Convert.ToDecimal(dataRow["SingleMoney"]).ToString("0.00") + " 元";
					this.L_RealGet = Convert.ToDecimal(dataRow["RealGet"]).ToString("0.00") + " 元";
					this.L_Times = dataRow["Times"].ToString();
					this.L_Total = Convert.ToDecimal(dataRow["Total"]).ToString("0.00");
					decimal num = Convert.ToDecimal(dataRow["Point"]);
					this.L_PointMoney = (Convert.ToDecimal(this.L_Total) * num / 1000m).ToString("0.00") + " 元";
					this.L_Bonus = Convert.ToDecimal(dataRow["WinBonus"]).ToString("0.00") + " 元";
					decimal num2 = Convert.ToDecimal(dataRow["Bonus"]);
					this.L_Point = Convert.ToDecimal(num2).ToString("0.00") + "/" + Convert.ToDecimal(num).ToString("0.00") + " %";
					if (dataRow["PlayCode"].ToString().Contains("3HX"))
					{
						this.L_Point = string.Concat(new string[]
						{
							Convert.ToDecimal(num2 / 2m).ToString("0.00"),
							"/",
							Convert.ToDecimal(num2).ToString("0.00"),
							"/",
							Convert.ToDecimal(num).ToString("0.00"),
							" %"
						});
					}
					this.L_Num = dataRow["Num"].ToString();
					int num3 = Convert.ToInt32(dataRow["State"]);
					this.L_State = dataRow["StateName"].ToString();
					this.L_STime = dataRow["STime"].ToString();
					this.L_STime2 = dataRow["STime2"].ToString();
					this.txtDetail.Text = dataRow["Detail"].ToString().Replace("#", " # ");
					this.L_Pos = dataRow["Pos"].ToString();
					if (this.L_Pos != "")
					{
						string text3 = "";
						string[] array = this.L_Pos.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							if (Convert.ToInt32(array[i]) == 1)
							{
								text3 = text3 + "," + i.ToString();
							}
						}
						this.L_Pos = text3.Substring(1);
					}
					if (this.L_Pos != "")
					{
					}
					this.NumberShow.Visible = false;
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

		protected void btnSave_Click(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id";
			this.doh.AddConditionParameter("@Id", this.txtId.Text);
			this.doh.AddFieldItem("Detail", this.txtDetail.Text);
			this.doh.Update("N_UserBet");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "对" + this.txtId.Text + "的订单进行改单");
			base.FinalMessage("改单成功，请选中记录点击选中派奖进行手动派奖", "/admin/close.htm", 0);
		}

		protected HtmlForm form1;

		protected HtmlTableRow NumberShow;

		protected TextBox txtDetail;

		protected TextBox txtId;

		protected Button btnSave;

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
	}
}
