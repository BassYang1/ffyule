using System;
using Lottery.DAL;

namespace Lottery.IPhone.money
{
	public class getcash : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
		}
	}
}
