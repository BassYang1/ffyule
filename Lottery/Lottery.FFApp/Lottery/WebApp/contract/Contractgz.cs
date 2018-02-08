using System;
using Lottery.DAL;

namespace Lottery.WebApp.contract
{
	public class Contractgz : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
		}

		public string userId = "0";
	}
}
