using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class userTrueNameEdit : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("master", "html");
			string text = this.txtId.Text = base.Str2Str(base.q("id"));
			if (!base.IsPostBack)
			{
				this.txtId.Text = text;
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from V_User where Id=" + text;
				DataTable dataTable = this.doh.GetDataTable();
				this.txtName.Text = dataTable.Rows[0]["UserName"].ToString();
				this.txtTrueName.Text = dataTable.Rows[0]["TrueName"].ToString();
				if (string.IsNullOrEmpty(dataTable.Rows[0]["Question"].ToString()) || string.IsNullOrEmpty(dataTable.Rows[0]["Answer"].ToString()))
				{
					base.FinalMessage("会员未绑定密保，不能修改！", "close.htm", 0);
				}
				else
				{
					this.txtQuestion.Text = dataTable.Rows[0]["Question"].ToString();
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtQuestion.Text.Trim()) || string.IsNullOrEmpty(this.txtAnswer.Text.Trim()))
			{
				base.FinalMessage("会员安全答案不正确，不能修改！", "close.htm", 0);
			}
			else
			{
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from N_User where Answer='" + this.txtAnswer.Text.Trim() + "' and Id=" + this.txtId.Text;
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "Id=" + this.txtId.Text;
					this.doh.AddFieldItem("TrueName", this.txtTrueName.Text);
					int num = this.doh.Update("N_User");
					if (num > 0)
					{
						new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员银行", "修改真实姓名");
					}
					base.FinalMessage("成功保存", "close.htm", 0);
				}
				else
				{
					base.FinalMessage("会员安全答案不正确，不能修改！", "close.htm", 0);
				}
			}
		}

		protected HtmlForm form1;

		protected TextBox txtName;

		protected TextBox txtId;

		protected TextBox txtQuestion;

		protected TextBox txtAnswer;

		protected TextBox txtTrueName;

		protected Label lblmsg;

		protected Button btnSave;
	}
}
