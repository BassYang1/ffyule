using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.IPhone.user
{
	public class userupdate : UserCenterSession
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
				this.doh.ConditionExpress = "id=@id";
				this.doh.AddConditionParameter("@id", text);
				object[] fields = this.doh.GetFields("N_User", "UserName,Point");
				this.txtUserId.Text = text;
				this.txtUserName.Text = fields[0].ToString();
				base.getEditDropDownList(ref this.ddlPoint, Convert.ToDecimal(string.Concat(fields[1])), 0);
				this.ddlPoint.SelectedValue = string.Concat(fields[1]);
				this.pointBefore = Convert.ToDecimal(string.Concat(fields[1]));
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT top 1 ParentId FROM [N_User] where Id=" + this.txtUserId.Text;
			DataTable dataTable = this.doh.GetDataTable();
			if (dataTable.Rows.Count > 0)
			{
				if (!this.AdminId.Equals(dataTable.Rows[0]["ParentId"].ToString()))
				{
					base.FinalMessage("不是您的直属下级不能修改其返点！", "/statics/html/close.htm", 0);
					return;
				}
			}
			this.doh.Reset();
			this.doh.ConditionExpress = "id=@id";
			this.doh.AddConditionParameter("@id", dataTable.Rows[0]["ParentId"].ToString());
			object field = this.doh.GetField("N_User", "Point");
			ListItem selectedItem = this.ddlPoint.SelectedItem;
			if (this.pointBefore >= Convert.ToDecimal(selectedItem.Value) || Convert.ToDecimal(selectedItem.Value) > Convert.ToDecimal(field))
			{
				base.FinalMessage("下属返点只能升不能降而且不能大于您的返点！", "/statics/html/close.htm", 0);
			}
			else
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "id=" + this.txtUserId.Text;
				this.doh.AddFieldItem("Point", Convert.ToDecimal(selectedItem.Value));
				this.doh.Update("N_User");
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT [Point] FROM [N_UserLevel] where Point>=125.00 and Point<=" + Convert.ToDecimal(selectedItem.Value) + " order by [Point] desc";
				DataTable dataTable2 = this.doh.GetDataTable();
				for (int i = 0; i < dataTable2.Rows.Count; i++)
				{
					if (!new UserQuotaDAL().Exists(string.Concat(new object[]
					{
						"UserId=",
						this.txtUserId.Text,
						" and UserLevel=",
						Convert.ToDecimal(dataTable2.Rows[i]["Point"]) / 10m
					})))
					{
						new UserQuotaDAL().SaveUserQuota(this.txtUserId.Text, Convert.ToDecimal(dataTable2.Rows[i]["Point"]) / 10m, 0);
					}
				}
				base.FinalMessage("返点修改成功", "/statics/html/close.htm", 0);
			}
		}

		protected HtmlForm form1;

		protected TextBox txtUserName;

		protected TextBox txtUserId;

		protected DropDownList ddlPoint;

		protected Button Button1;

		private decimal pointBefore = 0m;
	}
}
