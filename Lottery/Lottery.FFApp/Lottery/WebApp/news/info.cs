using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.WebApp.news
{
	public class info : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				string str = base.Str2Str(base.q("id"));
				string text = " Id=" + str;
				this.doh.Reset();
				this.doh.ConditionExpress = text;
				string sql = SqlHelp.GetSql0("Substring(Convert(varchar(10),STime,120),6,2) as tmonth,Substring(Convert(varchar(10),STime,120),9,2) as tday,*", "Sys_News", "STime", 1, 1, "desc", text);
				this.doh.Reset();
				this.doh.SqlCmd = sql;
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					this.L_Title = dataRow["Title"].ToString();
					this.L_Month = dataRow["tmonth"].ToString();
					this.L_Day = dataRow["tday"].ToString();
					this.L_Time = dataRow["STime"].ToString();
					this.L_Detail = dataRow["Content"].ToString();
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

		public string L_Title;

		public string L_Month;

		public string L_Day;

		public string L_Time;

		public string L_Detail;
	}
}
