using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class userMoneyLog : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT   UserId,SUM(MoneyChange) as Money\r\n                         FROM         N_UserMoneyLog\r\n                        WHERE     (MoneyAgo IS NULL)\r\n                        group by UserId\r\n                        order by SUM(MoneyChange) desc";
			DataTable dataTable = this.doh.GetDataTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				new UserTotalTran().MoneyOpers(SsId.MoneyLog, dataTable.Rows[i]["UserId"].ToString(), Convert.ToDecimal(dataTable.Rows[i]["Money"].ToString()), 0, 0, 0, 10, 1, "", "", "流水问题，系统自动补差", "");
			}
		}

		protected HtmlForm form1;

		protected Button btnSave;

		public string url = "";
	}
}
