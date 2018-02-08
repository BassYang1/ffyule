using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Lottery.DAL;

namespace Lottery.WebApp.plus
{
	public class auto : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string text = base.Request.QueryString["Id"];
			string text2 = base.Request.QueryString["SessionId"];
			int iExpires = 604800;
			string text3 = new UserDAL().ChkAutoLoginWebApp(text.Trim(), text2.Trim(), iExpires);
			base.Response.Redirect("http://localhost:50092/Index.aspx");
		}

		protected HtmlForm form1;
	}
}
