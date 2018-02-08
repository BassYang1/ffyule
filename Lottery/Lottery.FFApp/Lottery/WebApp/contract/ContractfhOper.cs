using System;
using Lottery.DAL;

namespace Lottery.WebApp.contract
{
	public class ContractfhOper : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			if (base.Request.QueryString["id"] != null)
			{
				this.userId = base.Request.QueryString["id"].ToString();
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
		}

		public string userId = "0";
	}
}
