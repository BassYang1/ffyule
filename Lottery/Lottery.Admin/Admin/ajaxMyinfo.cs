using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class ajaxMyinfo : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "ajaxCheckBank")
				{
					this.ajaxCheckBank();
					goto IL_A6;
				}
				if (operType == "ajaxBankList")
				{
					this.ajaxBankList();
					goto IL_A6;
				}
				if (operType == "ajaxBankBind")
				{
					this.ajaxBankBind();
					goto IL_A6;
				}
				if (operType == "changepass")
				{
					this.ajaxChangePass();
					goto IL_A6;
				}
				if (operType == "moneypass")
				{
					this.ajaxMoneyPass();
					goto IL_A6;
				}
			}
			this.DefaultResponse();
			IL_A6:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxCheckBank()
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id and PayAccount<>'' and PayName<>''";
			this.doh.AddConditionParameter("@Id", this.AdminId);
			if (this.doh.Exist("N_User"))
			{
				this._response = base.JsonResult(0, "绑定");
			}
			else
			{
				this._response = base.JsonResult(1, "未绑定");
			}
		}

		private void ajaxBankList()
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT [Id],[Bank] FROM [Sys_Bank] where IsUsed=0 ORDER BY Id asc";
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxBankBind()
		{
			string s = base.f("pass");
			string text = base.f("bank");
			string fieldValue = base.f("address");
			string fieldValue2 = base.f("payaccount");
			string fieldValue3 = base.f("payname");
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id";
			this.doh.AddConditionParameter("@Id", text);
			object field = this.doh.GetField("Sys_Bank", "Bank");
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id";
			this.doh.AddConditionParameter("@Id", this.AdminId);
			this.doh.AddFieldItem("PayMethod", text);
			this.doh.AddFieldItem("PayBank", field);
			this.doh.AddFieldItem("PayBankAddress", fieldValue);
			this.doh.AddFieldItem("PayAccount", fieldValue2);
			this.doh.AddFieldItem("PayName", fieldValue3);
			this.doh.AddFieldItem("PayPass", MD5.Last64(s));
			this.doh.AddFieldItem("IP", Const.GetUserIp);
			this.doh.Update("N_User");
			this._response = base.JsonResult(1, "银行绑定成功");
		}

		private void ajaxChangePass()
		{
			string s = base.f("oldpass");
			string s2 = base.f("newpass");
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id";
			this.doh.AddConditionParameter("@Id", this.AdminId);
			object field = this.doh.GetField("Sys_Admin", "Password");
			if (field != null)
			{
				if (field.ToString().ToLower() == MD5.Last64(s))
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "Id=@Id";
					this.doh.AddConditionParameter("@Id", this.AdminId);
					this.doh.AddFieldItem("Password", MD5.Last64(s2));
					this.doh.AddFieldItem("IP", Const.GetUserIp);
					this.doh.Update("Sys_Admin");
					this._response = base.JsonResult(1, "密码修改成功");
				}
				else
				{
					this._response = base.JsonResult(0, "旧密码错误");
				}
			}
			else
			{
				this._response = base.JsonResult(0, "未登录");
			}
		}

		private void ajaxMoneyPass()
		{
			string s = base.f("oldpass");
			string s2 = base.f("newpass");
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id";
			this.doh.AddConditionParameter("@Id", this.AdminId);
			object field = this.doh.GetField("N_User", "PayPass");
			if (field != null)
			{
				if (field.ToString().ToLower() == MD5.Last64(s))
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "Id=@Id";
					this.doh.AddConditionParameter("@Id", this.AdminId);
					this.doh.AddFieldItem("PayPass", MD5.Last64(s2));
					this.doh.AddFieldItem("IP", Const.GetUserIp);
					this.doh.Update("N_User");
					this._response = base.JsonResult(1, "密码修改成功");
				}
				else
				{
					this._response = base.JsonResult(0, "旧密码错误");
				}
			}
			else
			{
				this._response = base.JsonResult(0, "未登录");
			}
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
