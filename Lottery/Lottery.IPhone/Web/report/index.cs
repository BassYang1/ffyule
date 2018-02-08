using System;
using System.Data;
using Lottery.DAL;

namespace Lottery.Web.report
{
	public class index : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT AgentId FROM [N_User] where Id=" + this.AdminId;
			DataTable dataTable = this.doh.GetDataTable();
			if (dataTable.Rows.Count > 0)
			{
				if (Convert.ToInt32(dataTable.Rows[0]["AgentId"]) != 0)
				{
					this.act1 = "";
				}
			}
		}

		public string act1 = "style=\"display:none;\"";
	}
}
