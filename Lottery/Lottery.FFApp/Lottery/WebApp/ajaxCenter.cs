using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.WebApp
{
	public class ajaxCenter : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.CheckFormUrl())
			{
				base.Response.End();
			}
			base.Admin_Load("master", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "ajaxGetUserInfo")
				{
					this.ajaxGetUserInfo();
					goto IL_7C;
				}
				if (operType == "ajaxUserLoginList")
				{
					this.ajaxUserLoginList();
					goto IL_7C;
				}
			}
			this.DefaultResponse();
			IL_7C:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetUserInfo()
		{
			if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
			{
				this.AdminId = Cookie.GetValue(this.site.CookiePrev + "WebApp", "id");
				if (this.AdminId.Length != 0)
				{
					this.doh.Reset();
					this.doh.SqlCmd = "select * from Web_UserInfo where Id=" + this.AdminId;
					DataTable dataTable = this.doh.GetDataTable();
					this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
					dataTable.Clear();
					dataTable.Dispose();
				}
				else
				{
					this._response = "{\"result\" :\"0\",\"returnval\" :\"登录超时，请重新登录！\"}";
				}
			}
			else
			{
				this._response = "{\"result\" :\"0\",\"returnval\" :\"登录超时，请重新登录！\"}";
			}
		}

		private void ajaxUserLoginList()
		{
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num = base.Str2Int(base.q("flag"), 0);
			string wherestr = "UserId=" + this.AdminId;
			string response = "";
			new WebAppListOper().GetUserLoginListJSON(thispage, pagesize, wherestr, ref response);
			this._response = response;
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
