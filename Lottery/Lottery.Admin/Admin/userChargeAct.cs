using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class userChargeAct : AdminCenter
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
					this.doh.Reset();
					this.doh.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where ActiveType='Charge' and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", this.txtUserId.Text);
					DataTable dataTable = this.doh.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						if (Convert.ToInt32(dataTable.Rows[0]["count"]) > 0)
						{
							this.lblMsg.Text = "已派发，不能继续派发！";
						}
					}
					string act = SsId.Act;
					decimal num = 50m;
					this.doh.Reset();
					this.doh.AddFieldItem("SsId", act);
					this.doh.AddFieldItem("UserId", this.txtUserId.Text);
					this.doh.AddFieldItem("ActiveType", "Charge");
					this.doh.AddFieldItem("ActiveName", "首充佣金");
					this.doh.AddFieldItem("InMoney", num);
					this.doh.AddFieldItem("Remark", "首充佣金");
					this.doh.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					this.doh.AddFieldItem("CheckIp", "后台派发");
					this.doh.AddFieldItem("CheckMachine", "后台派发");
					if (this.doh.Insert("Act_ActiveRecord") > 0)
					{
						this.doh.Reset();
						this.doh.ConditionExpress = "Id=" + this.txtId.Text;
						this.doh.AddFieldItem("ActState", 1);
						int num2 = this.doh.Update("N_UserCharge");
						new UserTotalTran().MoneyOpers(act, this.txtUserId.Text, num, 0, 0, 0, 9, 99, "", "", "首充佣金派发", "");
						new LogAdminOperDAL().SaveLog(this.AdminId, this.txtUserId.Text, "会员管理", "对会员" + this.txtUserId.Text + "首充佣金派发");
						base.FinalMessage("您成功派发首充佣金" + num + "元", "/admin/close.htm", 0);
					}
					else
					{
						this.lblMsg.Text = "首充佣金派发失败！";
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
