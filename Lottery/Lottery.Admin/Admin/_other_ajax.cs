using System;
using System.Data;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.Admin
{
	public class _other_ajax : AdminCenter
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this._operType = base.q("oper");
			string operType = this._operType;
			switch (operType)
			{
			case "ajaxAdminToLoginWeb":
				this.ajaxAdminToLoginWeb();
				goto IL_11F;
			case "ajaxGetCurTime":
				this.ajaxGetCurTime();
				goto IL_11F;
			case "ajaxPopInfo":
				this.ajaxPopInfo();
				goto IL_11F;
			case "leftmenu":
				this.GetLeftMenu();
				goto IL_11F;
			case "login":
				this.ajaxLogin();
				goto IL_11F;
			case "logout":
				this.ajaxLogout();
				goto IL_11F;
			case "chkadminpower":
				this.ChkAdminPower();
				goto IL_11F;
			case "ajaxChinese2Pinyin":
				this.ajaxChinese2Pinyin();
				goto IL_11F;
			}
			this.DefaultResponse();
			IL_11F:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			base.Admin_Load("", "json");
			this._response = base.JsonResult(1, "成功登录");
		}

		private void ajaxAdminToLoginWeb()
		{
			string text = base.f("id");
			string text2 = base.f("name");
			string sessionId = base.f("cookieId");
			string text3 = base.f("point");
			int iExpires = 604800;
			string text4 = new UserDAL().ChkAutoLoginWebApp(text.Trim(), sessionId, iExpires);
			this._response = base.JsonResult(1, "");
		}

		private void ajaxGetCurTime()
		{
			this.doh.Reset();
			this.doh.SqlCmd = "select getdate() as Time";
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxPopInfo()
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "State=0 and state<>99";
			int num = this.doh.Count("N_UserGetCash");
			this.doh.Reset();
			this.doh.ConditionExpress = "datediff(minute,ontime ,getdate())<5 and Source=0";
			int num2 = this.doh.Count("N_User");
			this.doh.Reset();
			this.doh.ConditionExpress = "datediff(minute,ontime ,getdate())<5 and Source=1";
			int num3 = this.doh.Count("N_User");
			this._response = string.Concat(new object[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"title\":\"提现请求\",\"usercount\":\"",
				num2,
				"\",\"usercount2\":\"",
				num3,
				"\",\"cashcount\":\"",
				num,
				"\"}"
			});
		}

		private void GetLeftMenu()
		{
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + base.leftMenuJson() + "}";
		}

		private void ajaxLogin()
		{
			string adminname = base.f("name");
			string adminpass = base.f("pass");
			int num = base.Str2Int(base.f("type"), 0);
			int iExpires = 0;
			if (num > 0)
			{
				iExpires = 86400 * num;
			}
			string response = new AdminDAL().ChkAdminLogin(adminname, adminpass, iExpires);
			this._response = response;
		}

		private void ajaxLogout()
		{
			new AdminDAL().ChkAdminLogout();
			this._response = base.JsonResult(1, "成功退出");
		}

		private void ChkAdminPower()
		{
			base.Admin_Load(base.q("power"), "json");
			this._response = base.JsonResult(1, "身份合法");
		}

		private void ajaxChinese2Pinyin()
		{
			base.Admin_Load("", "json");
			int num = base.Str2Int(base.f("t"), 0);
			if (num == 1)
			{
				this._response = base.JsonResult(1, ChineseSpell.MakeSpellCode(base.f("chinese"), "", SpellOptions.TranslateUnknowWordToInterrogation));
			}
			else
			{
				this._response = base.JsonResult(1, ChineseSpell.MakeSpellCode(base.f("chinese"), "", SpellOptions.FirstLetterOnly));
			}
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
