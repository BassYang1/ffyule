using System;
using System.Data;
using Lottery.DAL;

namespace Lottery.WebApp.contract
{
	public class MyAgentfhlist : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			if (!base.IsPostBack)
			{
				DateTime now = DateTime.Now;
				if (now.Day >= 1 && now.Day <= 15)
				{
					this.d1 = now.ToString("yyyy-MM") + "-01 00:00:00";
					this.d2 = now.ToString("yyyy-MM") + "-16 00:00:00";
				}
				if (now.Day >= 16 && now.Day <= 31)
				{
					this.d1 = now.ToString("yyyy-MM") + "-16 00:00:00";
					this.d2 = now.AddMonths(1).ToString("yyyy-MM") + "-01 00:00:00";
				}
				this.doh.Reset();
				this.doh.SqlCmd = string.Format("SELECT \r\n                        (isnull(sum(Bet),0)-isnull(sum(Cancellation),0)) as Bet,\r\n                        isnull(sum(Bet),0)-(isnull(sum(Win),0)+isnull(sum(Give),0)+isnull(sum(Change),0)+isnull(sum(Cancellation),0)+isnull(sum(Point),0)) as Loss\r\n                        FROM [N_UserMoneyStatAll] with(nolock)\r\n                        where (STime>='{0}' and STime<'{1}') and dbo.f_GetUserCode(UserId) like '%'+dbo.f_User8Code({2})+'%'", this.d1, this.d2, this.AdminId);
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					this.Bet = dataTable.Rows[0]["Bet"].ToString();
					this.Loss = dataTable.Rows[0]["Loss"].ToString();
				}
				this.doh.Reset();
				this.doh.SqlCmd = string.Format("SELECT top 1 UserGroup from N_User where Id=" + this.AdminId, new object[0]);
				dataTable = this.doh.GetDataTable();
				this.doh.Reset();
				if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 4)
				{
					this.doh.SqlCmd = string.Format("select top 1 Group3 from Act_Day15FHSet with(nolock) where GroupId=4 and IsUsed=0 and {0}>=MinMoney*10000 order by MinMoney desc", this.Loss);
				}
				else if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 3)
				{
					this.doh.SqlCmd = string.Format("select top 1 Group3 from Act_Day15FHSet with(nolock) where GroupId=3 and IsUsed=0 and {0}>=MinMoney*150000 order by MinMoney desc", this.Bet);
				}
				else
				{
					this.doh.SqlCmd = string.Format("SELECT b.money as Group3 FROM [N_UserContract] a left join [N_UserContractDetail] b on a.Id=b.UcId where Type=1 and userId={0} and {1}>=MinMoney*150000 order by MinMoney desc", this.AdminId, this.Bet);
				}
				dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					this.Per = dataTable.Rows[0]["Group3"].ToString();
				}
				this.HyNum = "无限制";
				this.Money = Convert.ToDecimal(Convert.ToDecimal(this.Loss) * Convert.ToDecimal(this.Per) / 100m).ToString("0.0000");
			}
		}

		public string d1 = "0";

		public string d2 = "0";

		public string Bet = "0";

		public string Loss = "0";

		public string Per = "0";

		public string HyNum = "0";

		public string LxNum = "0";

		public string Money = "0";
	}
}
