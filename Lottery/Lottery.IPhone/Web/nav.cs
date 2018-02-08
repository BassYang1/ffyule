using System;
using System.Data;
using Lottery.DAL;

namespace Lottery.Web
{
	public class nav : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			if (!string.IsNullOrEmpty(base.Request.QueryString["id"] ?? ""))
			{
				this.loId = base.Request.QueryString["id"];
			}
			if (!string.IsNullOrEmpty(base.Request.QueryString["tid"] ?? ""))
			{
				this.tId = base.Request.QueryString["tid"];
			}
			this.doh.Reset();
			this.doh.SqlCmd = "select MinTimes,MaxTimes from Sys_Lottery with(nolock) where Id=" + this.loId;
			DataTable dataTable = this.doh.GetDataTable();
			if (dataTable.Rows.Count > 0)
			{
				this.MinTimes = string.Concat(dataTable.Rows[0]["MinTimes"]);
				this.MaxTimes = string.Concat(dataTable.Rows[0]["MaxTimes"]);
			}
		}

		public string tId = "1";

		public string loId = "1001";

		public string display = "";

		public string MinTimes = "1";

		public string MaxTimes = "1000";
	}
}
