using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class userBankDel : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("master", "html");
			string str = this.txtId.Text = base.Str2Str(base.q("id"));
			if (!base.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from N_UserBank where Id=" + str + "order by Id desc";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					this.txtUserId.Text = dataTable.Rows[0]["UserId"].ToString();
					this.doh.Reset();
					this.doh.SqlCmd = "select top 1 * from V_User where Id=" + dataTable.Rows[0]["UserId"].ToString();
					DataTable dataTable2 = this.doh.GetDataTable();
					this.txtId.Text = dataTable2.Rows[0]["Id"].ToString();
					this.txtName.Text = dataTable2.Rows[0]["UserName"].ToString();
					if (string.IsNullOrEmpty(dataTable2.Rows[0]["Question"].ToString()) || string.IsNullOrEmpty(dataTable2.Rows[0]["Answer"].ToString()))
					{
						base.FinalMessage("会员未绑定密保，不能删除！", "close.htm", 0);
					}
					else
					{
						this.txtQuestion.Text = dataTable2.Rows[0]["Question"].ToString();
					}
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtQuestion.Text.Trim()) || string.IsNullOrEmpty(this.txtAnswer.Text.Trim()))
			{
				base.FinalMessage("会员安全答案不正确，不能删除！", "close.htm", 0);
			}
			else
			{
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from N_User where Answer='" + this.txtAnswer.Text.Trim() + "' and Id=" + this.txtUserId.Text;
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "Id=" + this.txtId.Text;
					int num = this.doh.Delete("N_UserBank");
					if (num > 0)
					{
						new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员银行", "删除会员银行信息");
					}
					base.FinalMessage("成功保存", "close.htm", 0);
				}
				else
				{
					base.FinalMessage("会员安全答案不正确，不能删除！", "close.htm", 0);
				}
			}
		}

		protected HtmlForm form1;

		protected TextBox txtName;

		protected TextBox txtId;

		protected TextBox txtUserId;

		protected TextBox txtQuestion;

		protected TextBox txtAnswer;

		protected Label lblmsg;

		protected Button btnSave;
	}
}
