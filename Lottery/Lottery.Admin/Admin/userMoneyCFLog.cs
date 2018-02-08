using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lottery.DAL;

namespace Lottery.Admin
{
	public class userMoneyCFLog : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			string sqlCmd = string.Format("select UserId from [Act_ActiveRecord] \r\n\twhere CONVERT(varchar(10),STime,120)='{0}'\r\n\tgroup by UserId", this.TextBox1.Text);
			this.doh.Reset();
			this.doh.SqlCmd = sqlCmd;
			DataTable dataTable = this.doh.GetDataTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				this.doh.Reset();
				int num = this.doh.ExecuteSql(string.Format("Insert into N_UserMoneyStatAll(UserId,[Charge],STime) values ({0},0,'{1} 00:00:00')", dataTable.Rows[i]["UserId"].ToString(), this.TextBox1.Text));
			}
		}

		public string url = "";

		protected HtmlForm form1;

		protected Button btnSave;

		protected TextBox TextBox1;
	}
}
