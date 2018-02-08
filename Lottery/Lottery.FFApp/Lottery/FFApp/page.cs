using System;
using System.Web.UI;

namespace Lottery.FFApp
{
	public class page : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(base.Request.QueryString["k"] ?? ""))
			{
				this.k = base.Request.QueryString["k"];
			}
		}

		public string k = "b";
	}
}
