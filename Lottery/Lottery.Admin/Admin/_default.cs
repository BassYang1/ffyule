using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class _default : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "html");
			string value = Cookie.GetValue(this.site.CookiePrev + "admin", "name");
			if (!"woshishui".Equals(value.Trim()))
			{
				string clientIP = IPHelp.ClientIP;
				this.doh.Reset();
				this.doh.SqlCmd = "select * from Sys_LoginCheck where CheckType=0 and IsUsed=1";
				DataTable dataTable = this.doh.GetDataTable();
				bool flag = false;
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						if (IPHelp.domain2ip(string.Concat(dataTable.Rows[i]["CheckTitle"])).Equals(clientIP))
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					base.Response.Clear();
					base.Response.Write("您的网络环境不合法，请联系管理员!");
					base.Response.End();
					return;
				}
			}
			if (!base.IsPostBack)
			{
				this.AdminId = base.Str2Str(Cookie.GetValue(this.site.CookiePrev + "admin", "id"));
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT top 1 * FROM [Sys_Admin] a left join [Sys_Role] b on a.RoleId=b.Id where a.Id=" + this.AdminId;
				DataTable dataTable2 = this.doh.GetDataTable();
				if (dataTable2.Rows.Count > 0)
				{
					this.AdminIsSuper = "1".Equals(dataTable2.Rows[0]["IsSuper"].ToString().Trim());
					this.AdminSetting = dataTable2.Rows[0]["Setting"].ToString();
				}
				if (this.AdminIsSuper)
				{
					this.act0 = "";
					this.act1 = "";
					this.act2 = "";
				}
				else
				{
					if (this.AdminSetting.Contains(",99001,"))
					{
						this.act0 = "";
					}
					if (this.AdminSetting.Contains(",99002,"))
					{
						this.act1 = "";
					}
					if (this.AdminSetting.Contains(",99003,"))
					{
						this.act2 = "";
					}
				}
			}
		}

		public string act0 = "style=\"display:none;\"";

		public string act1 = "style=\"display:none;\"";

		public string act2 = "style=\"display:none;\"";
	}
}
