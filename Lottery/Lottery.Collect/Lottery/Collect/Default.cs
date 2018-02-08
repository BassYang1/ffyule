using System;
using System.Web.UI;

namespace Lottery.Collect
{
	public class Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Write("程序已经启动，开奖进行中。" + DateTime.Now.ToString());
			base.Response.End();
		}
	}
}
