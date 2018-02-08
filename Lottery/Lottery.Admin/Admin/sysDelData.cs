using System;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class sysDelData : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
		}
	}
}
