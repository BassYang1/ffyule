using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class useradd : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			base.getEditDropDownList(ref this.ddlPoint, 0);
			base.getGroupDropDownList(ref this.ddlGroup, 0);
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			string text = this.txtAdminPass1.Text;
			if (text == "")
			{
				text = "123456";
			}
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT Id FROM [N_User] WHERE [UserName]='" + this.txtAdminName.Text + "'";
			if (this.doh.GetDataTable().Rows.Count > 0)
			{
				base.FinalMessage("用户名重复", "", 1);
			}
			ListItem selectedItem = this.ddlPoint.SelectedItem;
			int num = new UserDAL().Register("0", this.txtAdminName.Text, MD5.Lower32(text), Convert.ToDecimal(selectedItem.Value));
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + num;
			this.doh.AddFieldItem("UserGroup", this.ddlGroup.SelectedValue);
			this.doh.AddFieldItem("UserCode", Strings.PadLeft(num.ToString()));
			if (this.doh.Update("N_User") > 0)
			{
				new LogAdminOperDAL().SaveLog(this.AdminId, string.Concat(num), "会员管理", "添加了会员" + this.txtAdminName.Text);
				if (this.ddlGroup.SelectedValue == "0")
				{
					this.doh.Reset();
					this.doh.SqlCmd = "select Id,Point from N_User with(nolock) where Id=" + num + " and IsEnable=0 and IsDel=0";
					DataTable dataTable = this.doh.GetDataTable();
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						this.doh.Reset();
						this.doh.SqlCmd = "SELECT [Point] FROM [N_UserLevel] where Point>=125.00 and Point<=" + Convert.ToDecimal(dataTable.Rows[i]["Point"]) + " order by [Point] desc";
						DataTable dataTable2 = this.doh.GetDataTable();
						for (int j = 0; j < dataTable2.Rows.Count; j++)
						{
							if (Convert.ToDecimal(dataTable2.Rows[j]["Point"]) == Convert.ToDecimal(dataTable.Rows[i]["Point"]))
							{
								new UserQuotaDAL().SaveUserQuota(dataTable.Rows[i]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[j]["Point"]) / 10m, Convert.ToInt32(this.txtAdminQuota2.Text));
							}
							else
							{
								new UserQuotaDAL().SaveUserQuota(dataTable.Rows[i]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[j]["Point"]) / 10m, Convert.ToInt32(this.txtAdminQuota.Text));
							}
						}
						new LogAdminOperDAL().SaveLog(this.AdminId, "0", "会员管理", "自动生成了Id为" + dataTable.Rows[i]["Id"] + "的会员的配额");
					}
				}
			}
			base.FinalMessage("操作成功", "/admin/close.htm", 0);
		}

		protected HtmlForm form1;

		protected DropDownList ddlGroup;

		protected DropDownList ddlPoint;

		protected TextBox txtAdminName;

		protected TextBox txtId;

		protected TextBox txtAdminPass1;

		protected TextBox txtAdminPass2;

		protected TextBox txtAdminQuota;

		protected TextBox txtAdminQuota2;

		protected Button Button1;
	}
}
