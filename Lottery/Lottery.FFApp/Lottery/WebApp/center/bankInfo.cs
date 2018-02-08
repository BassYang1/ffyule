using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.WebApp.center
{
	public class bankInfo : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			base.getBankDropDownList(ref this.ddlBank, 0);
		}

		protected HtmlForm form1;

		protected DropDownList ddlBank;
	}
}
