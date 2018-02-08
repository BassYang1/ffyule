using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class adminPower : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("master", "");
			this.id = base.Str2Str(base.q("id"));
			this.hfAdminId.Value = this.id;
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + this.id;
			string text = this.doh.GetField("Sys_Role", "Setting").ToString();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<table cellspacing=\"0\" cellpadding=\"0\" class=\"formtable\">");
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT * FROM Sys_Menu WHERE IsUsed=0 and pId=0 ORDER BY id";
			DataTable dataTable = this.doh.GetDataTable();
			string str = string.Empty;
			string str2 = string.Empty;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				str = dataTable.Rows[i]["Id"].ToString();
				str2 = dataTable.Rows[i]["Name"].ToString();
				stringBuilder.Append("<tr><th>" + str2 + "</th>");
				stringBuilder.Append("<td>");
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT * FROM Sys_Menu WHERE IsUsed=0 and pId=" + str + " ORDER BY id";
				DataTable dataTable2 = this.doh.GetDataTable();
				for (int j = 0; j < dataTable2.Rows.Count; j++)
				{
					string str3 = dataTable2.Rows[j]["Id"].ToString();
					string str4 = dataTable2.Rows[j]["Name"].ToString();
					stringBuilder.Append("<input type=checkbox class='checkbox' name=\"admin_power\" value=\"" + str3 + "\"");
					if (text.Contains("," + str3 + ","))
					{
						stringBuilder.Append(" checked");
					}
					stringBuilder.Append("> <span style='margin-right:10px;'>" + str4 + "</span>");
				}
				stringBuilder.Append("</td></tr>");
			}
			stringBuilder.Append("<tr><th>通知区域</th>");
			stringBuilder.Append("<td>");
			stringBuilder.Append("<input type=checkbox class='checkbox' name=\"admin_power\" value=\"99001\"");
			if (text.Contains(",99001,"))
			{
				stringBuilder.Append(" checked");
			}
			stringBuilder.Append("> <span style='margin-right:10px;'>提现提示</span>");
			stringBuilder.Append("<input type=checkbox class='checkbox' name=\"admin_power\" value=\"99002\"");
			if (text.Contains(",99001,"))
			{
				stringBuilder.Append(" checked");
			}
			stringBuilder.Append("> <span style='margin-right:10px;'>警告提示</span>");
			stringBuilder.Append("<input type=checkbox class='checkbox' name=\"admin_power\" value=\"99003\"");
			if (text.Contains(",99003,"))
			{
				stringBuilder.Append(" checked");
			}
			stringBuilder.Append("> <span style='margin-right:10px;'>活动提示</span>");
			stringBuilder.Append("</td></tr>");
			stringBuilder.Append("</table>");
			this.ltAdminSetting.Text = stringBuilder.ToString();
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			string fieldValue = ",";
			if (base.Request.Form["admin_power"] != null)
			{
				fieldValue = "," + base.Request.Form["admin_power"].ToString() + ",";
			}
			this.id = this.hfAdminId.Value.ToString();
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + this.id;
			this.doh.AddFieldItem("Setting", fieldValue);
			this.doh.Update("Sys_Role");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "编辑了Id为" + this.id + "的角色权限");
			base.FinalMessage("正确保存!", "close.htm", 0);
		}

		protected HtmlForm form1;

		protected Literal ltAdminSetting;

		protected Button btnSave;

		protected HiddenField hfAdminId;

		private string id = "0";
	}
}
