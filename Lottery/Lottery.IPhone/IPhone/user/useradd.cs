using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.IPhone.user
{
	public class useradd : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			base.getEditDropDownList(ref this.ddlPoint, 0m);
		}

		protected HtmlForm form1;

		protected DropDownList ddlPoint;
	}
}
