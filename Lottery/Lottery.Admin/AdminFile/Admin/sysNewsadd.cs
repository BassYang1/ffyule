using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.AdminFile.Admin
{
	public class sysNewsadd : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.AddFieldItem("Title", this.txtTitle.Text);
			this.doh.AddFieldItem("Content", this.txtContent.Value);
			this.doh.AddFieldItem("Color", this.ddlColor.SelectedValue);
			this.doh.AddFieldItem("STime", DateTime.Now);
			this.doh.AddFieldItem("IsUsed", 1);
			this.doh.Insert("Sys_News");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "添加了" + this.txtTitle.Text + "系统公告");
			base.FinalMessage("操作成功", "/admin/close.htm", 0);
		}

		protected HtmlForm form1;

		protected TextBox txtTitle;

		protected TextBox txtId;

		protected HtmlTextArea txtContent;

		protected DropDownList ddlColor;

		protected Button btnSave;
	}
}
