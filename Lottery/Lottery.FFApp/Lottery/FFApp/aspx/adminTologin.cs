using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Lottery.DAL;

namespace Lottery.FFApp.aspx
{
	public class adminTologin : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				string text = base.Request.QueryString["id"];
				string sessionId = base.Request.QueryString["cookiess"];
				new UserDAL().ChkAutoLoginWebApp(text.Trim(), sessionId);
				base.Response.Redirect("/");
			}
		}

		protected HtmlForm form1;
	}
}
