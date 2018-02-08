using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.WebApp.user
{
	public class userindex : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			this.doh.Reset();
			this.doh.ConditionExpress = "usercode like '%" + Strings.PadLeft(this.AdminId) + "%' and Id<>" + this.AdminId;
			this.UserSum = string.Concat(this.doh.Count("N_User"));
			this.doh.Reset();
			this.doh.ConditionExpress = "ParentId=" + this.AdminId;
			this.UserZsSum = string.Concat(this.doh.Count("N_User"));
			this.doh.Reset();
			this.doh.ConditionExpress = "IsOnline=1 and usercode like '%" + Strings.PadLeft(this.AdminId) + "%'";
			this.UserOnLineSum = string.Concat(this.doh.Count("Flex_User"));
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT isnull(sum(money),0) as money FROM [N_User] with(nolock) where usercode like '%" + Strings.PadLeft(this.AdminId) + "%' and Id<>" + this.AdminId;
			DataTable dataTable = this.doh.GetDataTable();
			this.UserMoneySum = Convert.ToDecimal(dataTable.Rows[0]["money"].ToString()).ToString("0.00");
		}

		public string UserSum = "0";

		public string UserZsSum = "0";

		public string UserOnLineSum = "0";

		public string UserMoneySum = "0";
	}
}
