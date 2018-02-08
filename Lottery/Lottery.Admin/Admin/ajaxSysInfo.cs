using System;
using System.Data;
using System.Web;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxSysInfo : AdminCenter
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
				if (operType == "ajaxGetList")
				{
					this.ajaxGetList();
					goto IL_7C;
				}
				if (operType == "ajaxUpdate")
				{
					this.ajaxUpdate();
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

		private void ajaxGetList()
		{
			this.doh.Reset();
			this.doh.SqlCmd = "select top 1 * from Sys_Info";
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxUpdate()
		{
			string[] allKeys = HttpContext.Current.Request.Form.AllKeys;
			this.doh.Reset();
			this.doh.ConditionExpress = "id=1";
			for (int i = 0; i < allKeys.Length; i++)
			{
				string text = base.f(allKeys[i]);
				if (!string.IsNullOrEmpty(text))
				{
					this.doh.AddFieldItem(allKeys[i], text);
				}
			}
			int num = this.doh.Update("Sys_Info");
			new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "修改了常规设置");
			if (num > 0)
			{
				this._response = base.JsonResult(1, "设置成功");
			}
			else
			{
				this._response = base.JsonResult(0, "设置失败");
			}
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
