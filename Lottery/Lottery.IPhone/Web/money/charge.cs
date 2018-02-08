using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Web.money
{
	public class charge : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			base.getZXChargeSetDropDownList(ref this.ddlLine, 0);
			base.getZXBankDropDownList(ref this.ddlBank, 0);
		}

		protected HtmlForm form1;

		protected DropDownList ddlLine;

		protected DropDownList ddlBank;
	}
}
