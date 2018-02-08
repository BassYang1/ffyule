using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class activeNewsEdit : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			if (!base.IsPostBack)
			{
				string str = this.txtId.Text = base.Str2Str(base.q("id"));
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from Act_ActiveSet where Id=" + str;
				DataTable dataTable = this.doh.GetDataTable();
				this.lblName.Text = dataTable.Rows[0]["Name"].ToString();
				this.txtTitle.Text = dataTable.Rows[0]["Title"].ToString();
				this.txtContent.Text = dataTable.Rows[0]["Content"].ToString();
				this.txtRemark.Value = dataTable.Rows[0]["Remark"].ToString();
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + this.txtId.Text;
			this.doh.AddFieldItem("Title", this.txtTitle.Text);
			this.doh.AddFieldItem("Content", this.txtContent.Text);
			this.doh.AddFieldItem("Remark", this.txtRemark.Value);
			this.doh.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			this.doh.Update("Act_ActiveSet");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "添加了活动公告！");
			base.FinalMessage("操作成功", "/admin/close.htm", 0);
		}

		protected HtmlForm form1;

		protected Label lblName;

		protected TextBox txtTitle;

		protected TextBox txtId;

		protected TextBox txtContent;

		protected HtmlTextArea txtRemark;

		protected Button Button1;
	}
}
