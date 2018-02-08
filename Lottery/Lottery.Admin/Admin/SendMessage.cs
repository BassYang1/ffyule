using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class SendMessage : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (this.ddlType.SelectedValue == "1")
			{
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1000 Id from V_User with(nolock) where IsOnline=1 order by Id asc";
				DataTable dataTable = this.doh.GetDataTable();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					new UserMessageDAL().Save(dataTable.Rows[i]["Id"].ToString(), "即时信息", this.txtMessage.Text);
				}
			}
			if (this.ddlType.SelectedValue == "2")
			{
				if (string.IsNullOrEmpty(this.txtName.Text))
				{
					base.FinalMessage("会员账号不能为空", "/admin/SendMessage.aspx", 0);
					return;
				}
				this.doh.Reset();
				this.doh.ConditionExpress = "UserName=@UserName";
				this.doh.AddConditionParameter("@UserName", this.txtName.Text);
				object field = this.doh.GetField("N_User", "Id");
				if (string.IsNullOrEmpty(string.Concat(field)))
				{
					base.FinalMessage("会员账号不存在", "/admin/SendMessage.aspx", 0);
					return;
				}
				new UserMessageDAL().Save(field.ToString(), "即时信息", this.txtMessage.Text);
			}
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "发送即时信息");
			base.FinalMessage("信息发送成功", "/admin/close.htm", 0);
		}

		protected HtmlForm form1;

		protected DropDownList ddlType;

		protected TextBox txtName;

		protected TextBox txtMessage;

		protected Button btnSave;
	}
}
