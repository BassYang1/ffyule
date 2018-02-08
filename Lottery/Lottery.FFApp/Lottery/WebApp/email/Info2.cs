using System;
using System.Data;
using Lottery.DAL;

namespace Lottery.WebApp.email
{
	public class Info2 : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			if (!this.Page.IsPostBack)
			{
				string text = this.L_Id = base.Str2Str(base.q("id"));
				string sqlCmd = "select dbo.f_GetUserName(SendId) as SendName,dbo.f_GetUserName(ReceiveId) as ReceiveName,* from N_UserEmail where Id=" + text;
				this.doh.Reset();
				this.doh.SqlCmd = sqlCmd;
				DataTable dataTable = this.doh.GetDataTable();
				new UserEmailDAL().UpdateState(text);
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					this.L_Time = dataRow["STime"].ToString();
					this.L_SendName = dataRow["SendName"].ToString();
					this.L_ReceiveName = dataRow["ReceiveName"].ToString();
					this.L_Title = dataRow["Title"].ToString();
					this.L_Contents = dataRow["Contents"].ToString();
				}
				else
				{
					base.Response.Write("参数错误");
					base.Response.End();
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string L_Id;

		public string L_Time;

		public string L_SendName;

		public string L_ReceiveName;

		public string L_Title;

		public string L_Contents;
	}
}
