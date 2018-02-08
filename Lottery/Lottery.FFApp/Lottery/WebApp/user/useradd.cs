using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.WebApp.user
{
	public class useradd : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			base.getUserGroupDropDownList(ref this.ddlType, 0);
			if (Convert.ToDecimal(this.AdminPoint) < 131m)
			{
				this.lblPoint2.Text = "可分配范围10-" + Convert.ToDecimal(this.AdminPoint) / 10m;
				this.lblPoint3.Text = "可分配范围10-" + Convert.ToDecimal(this.AdminPoint) / 10m;
			}
			else
			{
				this.lblPoint2.Text = "可分配范围10-" + (Convert.ToDecimal(this.AdminPoint) / 10m - Convert.ToDecimal(0.1));
				this.lblPoint3.Text = "可分配范围10-" + (Convert.ToDecimal(this.AdminPoint) / 10m - Convert.ToDecimal(0.1));
			}
		}

		protected HtmlForm form;

		protected DropDownList ddlType;

		protected Label lblPoint2;

		protected Label lblPoint3;
	}
}
