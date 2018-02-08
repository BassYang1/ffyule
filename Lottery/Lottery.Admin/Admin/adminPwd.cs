using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class adminPwd : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id";
			this.doh.AddConditionParameter("@Id", this.AdminId);
			object field = this.doh.GetField("Sys_Admin", "Password");
			if (field != null)
			{
				if (field.ToString().ToLower() == MD5.Last64(MD5.Lower32(this.txtOldPass.Text)))
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "Id=@Id";
					this.doh.AddConditionParameter("@Id", this.AdminId);
					this.doh.AddFieldItem("Password", MD5.Last64(MD5.Lower32(this.txtNewPass2.Text)));
					this.doh.AddFieldItem("IP", Const.GetUserIp);
					this.doh.Update("Sys_Admin");
					new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "修改了管理员的密码");
					base.FinalMessage("密码修改成功", "/admin/close.htm", 0);
				}
				else
				{
					base.FinalMessage("旧密码错误", "/admin/adminPwd2.aspx", 0);
				}
			}
			else
			{
				base.FinalMessage("未登录", "/admin/close.htm", 0);
			}
		}

		protected HtmlForm form1;

		protected TextBox txtOldPass;

		protected TextBox txtNewPass1;

		protected TextBox txtNewPass2;

		protected Button Button1;
	}
}
