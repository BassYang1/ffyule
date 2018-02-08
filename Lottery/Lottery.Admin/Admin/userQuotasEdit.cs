using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class userQuotasEdit : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			string text = this.txtId.Text = base.Str2Str(base.q("id"));
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (this.rbo1.Checked)
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "id=" + this.txtId.Text;
				this.doh.AddFieldItem("CheckTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				this.doh.AddFieldItem("State", 1);
				this.doh.Update("N_UserQuotas");
				this.doh.Reset();
				this.doh.ConditionExpress = "id=" + this.txtId.Text;
				object field = this.doh.GetField("N_UserQuotas", "UserId");
				if (this.rbo3.Checked)
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "id=" + field;
					object field2 = this.doh.GetField("N_User", "ParentId");
					this.doh.Reset();
					this.doh.SqlCmd = "select Id from N_User with(nolock) where ParentId=" + field;
					DataTable dataTable = this.doh.GetDataTable();
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
					}
				}
				if (this.rbo4.Checked)
				{
					this.doh.Reset();
					this.doh.SqlCmd = "select Id from N_User with(nolock) where ParentId=" + field;
					DataTable dataTable = this.doh.GetDataTable();
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
					}
				}
				if (this.rbo5.Checked)
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "id=" + field;
					object[] fields = this.doh.GetFields("N_User", "ParentId,Money");
				}
				if (this.rbo6.Checked)
				{
				}
			}
			if (this.rbo2.Checked)
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "id=" + this.txtId.Text;
				this.doh.AddFieldItem("CheckTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				this.doh.AddFieldItem("State", 2);
				this.doh.Update("N_UserQuotas");
			}
			new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员管理", "同意了Id为" + this.txtId.Text + "的会员回收申请");
			base.FinalMessage("操作成功", "/admin/close.htm", 0);
		}

		protected HtmlForm form1;

		protected RadioButton rbo1;

		protected RadioButton rbo2;

		protected TextBox txtId;

		protected RadioButton rbo3;

		protected RadioButton rbo4;

		protected RadioButton rbo5;

		protected RadioButton rbo6;

		protected Button btnSave;
	}
}
