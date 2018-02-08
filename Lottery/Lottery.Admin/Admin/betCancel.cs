using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class betCancel : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			base.getLotteryDropDownList(ref this.ddlLottery, 0);
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (this.rbo1.Checked)
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "IssueNum='" + this.txtIssue.Text.Trim() + "'";
				if (this.doh.Count("N_UserBet") > 0)
				{
					ListItem selectedItem = this.ddlLottery.SelectedItem;
					new LotteryCheck().Cancel(Convert.ToInt32(selectedItem.Value), this.txtIssue.Text.Trim(), 2);
					new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "对" + this.txtIssue.Text + "期进行撤单");
					base.FinalMessage("撤单成功", "/admin/close.htm", 0);
				}
				else
				{
					base.FinalMessage("该期号不存在投注记录，不能撤单", "/admin/betCancel.aspx", 0);
				}
			}
			else
			{
				base.FinalMessage("请您选中确认撤单选项", "/admin/betCancel.aspx", 0);
			}
		}

		protected HtmlForm form1;

		protected DropDownList ddlLottery;

		protected TextBox txtIssue;

		protected RadioButton rbo1;

		protected RadioButton rbo2;

		protected Button btnSave;
	}
}
