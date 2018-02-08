using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class userUpdatePoints : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("master", "html");
			this.txtId.Text = base.q("id");
			base.getEditDropDownList(ref this.ddlPoint, 0);
			base.getGroupDropDownList(ref this.ddlGroup, 0);
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			ListItem selectedItem = this.ddlGroup.SelectedItem;
			ListItem selectedItem2 = this.ddlPoint.SelectedItem;
			string[] array = this.txtId.Text.Split(new char[]
			{
				','
			});
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "Id=" + array[i];
					this.doh.AddFieldItem("Point", Convert.ToDecimal(selectedItem2.Value));
					this.doh.AddFieldItem("UserGroup", selectedItem.Value);
					this.doh.Update("N_User");
					new LogAdminOperDAL().SaveLog(this.AdminId, array[i], "会员返点", "修改了" + array[i] + "的返点，类型信息");
				}
			}
			base.FinalMessage("成功保存", "close.htm", 0);
		}

		protected HtmlForm form1;

		protected TextBox txtId;

		protected DropDownList ddlPoint;

		protected DropDownList ddlGroup;

		protected Button Button1;
	}
}
