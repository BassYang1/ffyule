using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class userUpdatePoint : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("master", "html");
			string str = this.txtId.Text = base.Str2Str(base.q("id"));
			base.getEditDropDownList(ref this.ddlPoint, 0);
			base.getGroupDropDownList(ref this.ddlGroup, 0);
			if (!base.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "select top 1 * from V_User with(nolock) where Id=" + str;
				DataTable dataTable = this.doh.GetDataTable();
				this.txtName.Text = dataTable.Rows[0]["UserName"].ToString();
				this.txtPoint.Text = dataTable.Rows[0]["Point"].ToString();
				this.ddlGroup.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["UserGroup"]).ToString();
				this.ddlPoint.SelectedValue = dataTable.Rows[0]["UPoint"].ToString();
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			ListItem selectedItem = this.ddlGroup.SelectedItem;
			ListItem selectedItem2 = this.ddlPoint.SelectedItem;
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=" + this.txtId.Text;
			this.doh.AddFieldItem("Point", Convert.ToDecimal(selectedItem2.Value));
			this.doh.AddFieldItem("UserGroup", selectedItem.Value);
			int num = this.doh.Update("N_User");
			if (num > 0)
			{
				new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员返点", "修改了" + this.txtName.Text + "的返点，类型信息");
			}
			base.FinalMessage("成功保存", "close.htm", 0);
		}

		protected HtmlForm form1;

		protected TextBox txtName;

		protected TextBox txtId;

		protected TextBox txtPoint;

		protected DropDownList ddlPoint;

		protected DropDownList ddlGroup;

		protected Button Button1;
	}
}
