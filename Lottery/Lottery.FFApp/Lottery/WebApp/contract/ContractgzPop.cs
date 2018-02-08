using System;
using System.Data;
using Lottery.DAL;

namespace Lottery.WebApp.contract
{
	public class ContractgzPop : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["id"] != null)
				{
					this.userId = base.Request.QueryString["id"].ToString();
				}
				this.doh.Reset();
				this.doh.SqlCmd = string.Format("SELECT top 1 UserGroup from N_User where Id=" + this.AdminId, new object[0]);
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 4)
					{
						this.maxAdminPer = "1";
					}
					else if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 3)
					{
						this.maxAdminPer = "2";
					}
					else if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 2)
					{
						this.maxAdminPer = "11";
					}
					else
					{
						this.maxAdminPer = "1";
					}
				}
			}
		}

		public string userId = "0";

		public string maxAdminPer = "0";
	}
}
