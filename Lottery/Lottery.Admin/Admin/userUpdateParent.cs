using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class userUpdateParent : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("master", "html");
			string str = this.txtId.Text = base.Str2Str(base.q("id"));
			if (!base.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from N_User with(nolock) where Id=" + str;
				DataTable dataTable = this.doh.GetDataTable();
				this.txtName.Text = dataTable.Rows[0]["UserName"].ToString();
				this.txtPoint.Text = Convert.ToDecimal(Convert.ToDecimal(dataTable.Rows[0]["Point"].ToString()) / 10m).ToString("0.00");
				this.txtGroup.Text = dataTable.Rows[0]["UserGroup"].ToString();
				this.txtCode.Text = dataTable.Rows[0]["UserCode"].ToString();
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			string pageMsg = new UserDAL().UpdateParentId(this.txtId.Text, this.txtToName.Text, this.txtPoint.Text, this.txtGroup.Text, this.txtCode.Text);
			new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员切线", "对会员" + this.txtName.Text + "进行切线，切到" + this.txtToName.Text);
			base.FinalMessage(pageMsg, "close.htm", 0);
		}

		protected HtmlForm form1;

		protected TextBox txtName;

		protected TextBox txtPoint;

		protected TextBox txtGroup;

		protected TextBox txtToName;

		protected TextBox txtId;

		protected TextBox txtCode;

		protected Button Button1;
	}
}
