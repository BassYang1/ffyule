using System;
using Lottery.DAL;

namespace Lottery.Web.money
{
	public class chargeZfb : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
		}
	}
}
