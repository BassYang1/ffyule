using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.AdminFile.Admin
{
	public class lotteryDataAdd : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			base.getLotteryDropDownList(ref this.ddlType, 0);
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			int num = Convert.ToInt32(this.ddlType.SelectedValue);
			string text = this.txtNumber.Text;
			string numberAll = text.Replace("+", ",").Replace(" ", ",");
			if (num == 1010 || num == 1011 || num == 1012 || num == 1013 || num == 1014 || num == 1015 || num == 1016 || num == 1017)
			{
				text = text.Replace("+", ",").Replace(" ", ",");
				string[] array = text.Split(new char[]
				{
					','
				});
				if (array.Length >= 20)
				{
					int num2 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3])) % 10;
					int num3 = (Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6]) + Convert.ToInt32(array[7])) % 10;
					int num4 = (Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11])) % 10;
					int num5 = (Convert.ToInt32(array[12]) + Convert.ToInt32(array[13]) + Convert.ToInt32(array[14]) + Convert.ToInt32(array[15])) % 10;
					int num6 = (Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
					text = string.Concat(new object[]
					{
						num2,
						",",
						num3,
						",",
						num4,
						",",
						num5,
						",",
						num6
					});
				}
				else
				{
					base.FinalMessage("开奖号码不正确！", "/admin/close.htm", 0);
				}
			}
			string[] array2 = text.Split(new char[]
			{
				','
			});
			if (array2.Length > 10)
			{
				base.FinalMessage("开奖号码不正确！", "/admin/close.htm", 0);
			}
			else
			{
				if (new LotteryDataDAL().Add(num, this.txtTitle.Text.Trim(), text, this.txtOpenTime.Text, numberAll))
				{
					LotteryCheck.RunOper(Convert.ToInt32(this.ddlType.SelectedValue), this.txtTitle.Text.Trim());
				}
				new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "添加了" + this.txtTitle.Text + "开奖号码");
				base.FinalMessage("操作成功", "/admin/close.htm", 0);
			}
		}

		protected HtmlForm form1;

		protected DropDownList ddlType;

		protected TextBox txtTitle;

		protected TextBox txtNumber;

		protected TextBox txtOpenTime;

		protected Button btnSave;
	}
}
