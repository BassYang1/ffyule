using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Utils;

namespace Lottery.WebApp.user
{
	public class usertranacc : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			if (!base.IsPostBack)
			{
				string text = "1";
				if (base.Request.QueryString["id"] != null)
				{
					text = base.Request.QueryString["id"].ToString();
				}
				this.doh.Reset();
				this.doh.ConditionExpress = "id=" + text;
				this.txtId.Text = text;
				object[] fields = this.doh.GetFields("N_User", "UserName,UserCode");
				this.strUserName = string.Concat(fields[0]);
				if (string.Concat(fields[1]).IndexOf("," + this.AdminId + ",") == -1)
				{
					base.FinalMessage("转账的会员不是您的下级不能转账！", "/statics/include/close.htm", 0);
				}
				else
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "Id=" + this.AdminId;
					object[] fields2 = this.doh.GetFields("N_User", "Money,IsTranAcc");
					this.strUserMoney = string.Concat(fields2[0]);
					if (Convert.ToInt32(fields2[1]) == 1)
					{
						base.FinalMessage("您的账号禁止转账！", "/statics/include/close.htm", 0);
					}
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + this.txtId.Text;
			object field = this.doh.GetField("N_User", "UserCode");
			if (string.Concat(field).IndexOf("," + this.AdminId + ",") == -1)
			{
				this.lblMsg.Text = "转账的会员不是您的下级不能转账！";
			}
			else
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "id=@id";
				this.doh.AddConditionParameter("@id", this.AdminId);
				object[] fields = this.doh.GetFields("N_User", "Money,PayPass,IsTranAcc");
				if (fields.Length > 0)
				{
					if (Convert.ToInt32(fields[2]) == 1)
					{
						this.lblMsg.Text = "您的账号禁止转账！";
					}
					else if (Convert.ToDecimal(this.txtMoney.Text) < 1m)
					{
						this.lblMsg.Text = "转账失败,转账金额错误！";
					}
					else if (Convert.ToDecimal(this.txtMoney.Text) > Convert.ToDecimal(fields[0]))
					{
						this.lblMsg.Text = "转账失败,您的可用余额不足";
					}
					else if (!MD5.Last64(MD5.Lower32(this.txtNewPass1.Text.Trim())).Equals(fields[1].ToString()))
					{
						this.lblMsg.Text = "转账失败,您的资金密码错误";
					}
					else
					{
						string type = this.rdo1.Checked ? "0" : "1";
						if (new Lottery.DAL.Flex.UserChargeDAL().SaveUpCharge(type, this.AdminId, this.txtId.Text, Convert.ToDecimal(this.txtMoney.Text)) > 0)
						{
							new LogSysDAL().Save("会员管理", string.Concat(new string[]
							{
								"Id为",
								this.AdminId,
								"的会员转账给Id为",
								this.txtId.Text,
								"的会员！"
							}));
							base.FinalMessage("转账成功", "/statics/include/close.htm", 0);
						}
						else
						{
							base.FinalMessage("转账失败", "/statics/include/close.htm", 0);
						}
					}
				}
			}
		}

		protected HtmlForm form1;

		protected Label lblMsg;

		protected TextBox txtId;

		protected TextBox txtMoney;

		protected RadioButton rdo1;

		protected RadioButton rdo2;

		protected TextBox txtNewPass1;

		protected Button Button1;

		public string strUserName = "";

		public string strUserMoney = "";
	}
}
