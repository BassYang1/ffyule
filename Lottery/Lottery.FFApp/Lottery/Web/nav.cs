using System;
using Lottery.DAL;

namespace Lottery.Web
{
	public class nav : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			if (!string.IsNullOrEmpty(base.Request.QueryString["id"] ?? ""))
			{
				this.loId = base.Request.QueryString["id"];
			}
			if (!string.IsNullOrEmpty(base.Request.QueryString["tid"] ?? ""))
			{
				this.tId = base.Request.QueryString["tid"];
			}
		}

		public string tId = "1";

		public string loId = "1001";
	}
}
