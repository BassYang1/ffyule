using System;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class PageSavePop : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
		}
	}
}
