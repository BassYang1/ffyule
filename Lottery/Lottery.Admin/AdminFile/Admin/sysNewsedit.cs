using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.AdminFile.Admin
{
	public class sysNewsedit : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			string str = this.txtId.Text = base.Str2Str(base.q("id"));
			if (!base.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from Sys_News where Id=" + str;
				DataTable dataTable = this.doh.GetDataTable();
				this.txtTitle.Text = dataTable.Rows[0]["Title"].ToString();
				this.txtContent.Value = dataTable.Rows[0]["Content"].ToString();
				this.ddlColor.SelectedValue = dataTable.Rows[0]["Color"].ToString();
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + this.txtId.Text;
			this.doh.AddFieldItem("Title", this.txtTitle.Text);
			this.doh.AddFieldItem("Content", this.txtContent.Value);
			this.doh.AddFieldItem("Color", this.ddlColor.SelectedValue);
			this.doh.Update("Sys_News");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "编辑了Id为" + this.txtId.Text + "的系统公告");
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
