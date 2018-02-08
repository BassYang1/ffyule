using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class userChargeEdit : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			string str = this.txtId.Text = base.Str2Str(base.q("id"));
			this.doh.Reset();
			this.doh.SqlCmd = "select top 1 *,dbo.f_GetUserName(UserID) as UserName from N_UserCharge where Id=" + str;
			DataTable dataTable = this.doh.GetDataTable();
			this.txtUserId.Text = dataTable.Rows[0]["UserId"].ToString();
			this.lblUserName.Text = dataTable.Rows[0]["UserName"].ToString();
			this.lblPayMoney.Text = dataTable.Rows[0]["InMoney"].ToString();
			this.lblSsid.Text = dataTable.Rows[0]["SsId"].ToString();
			this.txtTime.Text = dataTable.Rows[0]["STime"].ToString();
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id";
			this.doh.AddConditionParameter("@Id", this.AdminId);
			object field = this.doh.GetField("Sys_Admin", "Password");
			if (field != null)
			{
				if (field.ToString().ToLower() == MD5.Last64(MD5.Lower32(this.txtPwd.Text)))
				{
					if (!new SFTDAL().Exists("PayRequestId ='" + this.lblSsid.Text + "'"))
					{
						double num = Convert.ToDouble(this.lblPayMoney.Text);
						if (new SFTDAL().SavePayInfo(this.txtUserId.Text, "9999", this.lblSsid.Text, this.lblPayMoney.Text, num.ToString(), this.txtTime.Text, DateTime.Now.ToString("yyyyMMddHHmmss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "成功") > 0)
						{
							new LogAdminOperDAL().SaveLog(this.AdminId, this.txtUserId.Text, "会员充值", string.Concat(new string[]
							{
								"对",
								this.lblSsid.Text,
								"进行补单，金额",
								this.lblPayMoney.Text,
								"元"
							}));
							base.FinalMessage("操作成功", "/admin/close.htm", 0);
						}
						else
						{
							this.lblMsg.Text = "补单失败！";
						}
					}
					else
					{
						this.lblMsg.Text = "补单失败，此订单已到账！";
					}
				}
				else
				{
					this.lblMsg.Text = "安全密码错误！";
				}
			}
		}

		protected HtmlForm form1;

		protected Label lblSsid;

		protected Label lblUserName;

		protected Label lblPayMoney;

		protected TextBox txtPwd;

		protected Label lblMsg;

		protected TextBox txtId;

		protected TextBox txtUserId;

		protected TextBox txtTime;

		protected Button btnSave;

		public string url = "";
	}
}
