using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class AdminCenter : AdminBasicPage
	{
		protected void Admin_Load(string powerNum, string pageType)
		{
			if (pageType == "json" && !base.CheckFormUrl())
			{
				base.Response.End();
			}
			if (Cookie.GetValue(this.site.CookiePrev + "admin") != null)
			{
				this.AdminId = base.Str2Str(Cookie.GetValue(this.site.CookiePrev + "admin", "id"));
				this.AdminName = Cookie.GetValue(this.site.CookiePrev + "admin", "name");
				this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "admin", "cookiess");
				if (this.AdminId.Length == 0 || this.AdminName.Length == 0)
				{
					this.showErrMsg("请您登陆系统", pageType);
					return;
				}
				this.doh.Reset();
				this.doh.ConditionExpress = "id=@id";
				this.doh.AddConditionParameter("@id", this.AdminId);
				int num = this.doh.Count("Sys_Admin");
				if (num < 1)
				{
					this.showErrMsg("请您登陆系统", pageType);
					return;
				}
			}
			else
			{
				this.showErrMsg("请您登陆系统", pageType);
			}
		}

		protected void showErrMsg(string msg, string pageType)
		{
			if (pageType != "json")
			{
				base.FinalMessage(msg, "login.aspx", 0);
				return;
			}
			HttpContext.Current.Response.Clear();
			if (!this.AdminIsLogin)
			{
				HttpContext.Current.Response.Write(base.JsonResult(-1, msg));
			}
			else
			{
				HttpContext.Current.Response.Write(base.JsonResult(0, msg));
			}
			HttpContext.Current.Response.End();
		}

		protected string leftMenuJson()
		{
			this.AdminId = base.Str2Str(Cookie.GetValue(this.site.CookiePrev + "admin", "id"));
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT top 1 * FROM [Sys_Admin] a left join [Sys_Role] b on a.RoleId=b.Id where a.Id=" + this.AdminId;
			DataTable dataTable = this.doh.GetDataTable();
			if (dataTable.Rows.Count > 0)
			{
				this.AdminIsSuper = "1".Equals(dataTable.Rows[0]["IsSuper"].ToString().Trim());
				this.AdminSetting = dataTable.Rows[0]["Setting"].ToString();
			}
			else
			{
				this.AdminIsSuper = false;
				this.AdminSetting = "";
			}
			if (this.AdminSetting.Length > 2)
			{
				this.AdminSetting = this.AdminSetting.Substring(1, this.AdminSetting.Length - 2);
			}
			string str = "";
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT * FROM Sys_Menu WHERE IsUsed=0";
			if (!this.AdminIsSuper)
			{
				if (this.AdminSetting.Length > 2)
				{
					DbOperHandler expr_13A = this.doh;
					expr_13A.SqlCmd = expr_13A.SqlCmd + " and Id in (" + this.AdminSetting + ")";
					str = " and Id in (" + this.AdminSetting + ")";
				}
				else
				{
					DbOperHandler expr_178 = this.doh;
					expr_178.SqlCmd += " and Id in (0)";
					str = " and Id in (0)";
				}
			}
			DbOperHandler expr_199 = this.doh;
			expr_199.SqlCmd += " ORDER BY Sort asc";
			DataTable dataTable2 = this.doh.GetDataTable();
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT * FROM Sys_Menu WHERE IsUsed=0 and pId=0";
			if (!this.AdminIsSuper && dataTable2.Rows.Count > 0)
			{
				DbOperHandler expr_1F1 = this.doh;
				expr_1F1.SqlCmd = expr_1F1.SqlCmd + " and Id in (SELECT Pid FROM Sys_Menu WHERE IsUsed=0 " + str + " group by Pid)";
			}
			DbOperHandler expr_212 = this.doh;
			expr_212.SqlCmd += " ORDER BY Sort asc";
			DataTable dataTable3 = this.doh.GetDataTable();
			return dtHelp.DT2JSONAdminLeft(dataTable3, dataTable2);
		}

		protected void getWYBankDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT Id,Bank FROM [Sys_Bank] where IsUsed=0 ORDER BY Id asc";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					return;
				}
				ddlClassId.Items.Add(new ListItem("全部", ""));
				ddlClassId.Items.Add(new ListItem("后台充值", "888"));
				ddlClassId.Items.Add(new ListItem("第三方充值", "999"));
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					ddlClassId.Items.Add(new ListItem(dataTable.Rows[i]["Bank"].ToString(), dataTable.Rows[i]["Id"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getActiveEditDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT [Code],[Name] FROM [Act_ActiveSet] where Isuse=1";
				DbOperHandler expr_31 = this.doh;
				expr_31.SqlCmd += " ORDER BY Id";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					ddlClassId.Items.Add(new ListItem(dataTable.Rows[i]["Name"].ToString(), dataTable.Rows[i]["Code"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getEditDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT [Id],[Point],[Title],[Bonus],[Score],[Times],[Sort] FROM [N_UserLevel]";
				DbOperHandler expr_31 = this.doh;
				expr_31.SqlCmd += " ORDER BY Bonus desc";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					ddlClassId.Items.Add(new ListItem(dataTable.Rows[i]["Bonus"].ToString() + "_" + Convert.ToDecimal(Convert.ToDecimal(dataTable.Rows[i]["Point"]) / 10m).ToString("0.00") + "%", dataTable.Rows[i]["Point"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getRoleDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT [Id],[Name] FROM [Sys_Role] where IsUsed=0";
				DbOperHandler expr_31 = this.doh;
				expr_31.SqlCmd += " ORDER BY Sort";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					ddlClassId.Items.Add(new ListItem(dataTable.Rows[i]["Name"].ToString(), dataTable.Rows[i]["Id"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getAgentDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				ddlClassId.Items.Add(new ListItem("不分红", "0"));
				ddlClassId.Items.Add(new ListItem("一级分红", "1"));
				ddlClassId.Items.Add(new ListItem("二级分红", "2"));
			}
		}

		protected void getGroupDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT [Id],[Name] FROM N_UserGroup ORDER BY Id desc";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					ddlClassId.Items.Add(new ListItem(" " + dataTable.Rows[i]["Name"].ToString(), dataTable.Rows[i]["Id"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getLotteryDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT [Id],[Title] FROM Sys_Lottery ORDER BY Id asc";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					dataTable.Clear();
					dataTable.Dispose();
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					ddlClassId.Items.Add(new ListItem(" " + dataTable.Rows[i]["Title"].ToString(), dataTable.Rows[i]["Id"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getTypeDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				ddlClassId.Items.Add(new ListItem("所有类型", ""));
				ddlClassId.Items.Add(new ListItem("账号充值", "1"));
				ddlClassId.Items.Add(new ListItem("账号提款", "2"));
				ddlClassId.Items.Add(new ListItem("提现失败", "3"));
				ddlClassId.Items.Add(new ListItem("投注扣款", "4"));
				ddlClassId.Items.Add(new ListItem("追号扣款", "5"));
				ddlClassId.Items.Add(new ListItem("追号返款", "6"));
				ddlClassId.Items.Add(new ListItem("游戏返点", "7"));
				ddlClassId.Items.Add(new ListItem("奖金派送", "8"));
				ddlClassId.Items.Add(new ListItem("撤单返款", "9"));
				ddlClassId.Items.Add(new ListItem("充值扣费", "10"));
				ddlClassId.Items.Add(new ListItem("上级充值", "11"));
				ddlClassId.Items.Add(new ListItem("活动礼金", "12"));
				ddlClassId.Items.Add(new ListItem("代理分红", "13"));
				ddlClassId.Items.Add(new ListItem("管理员减扣", "14"));
				ddlClassId.Items.Add(new ListItem("积分兑换", "15"));
			}
		}

		protected string GetContentFile(string TxtFile)
		{
			return DirFile.ReadFile(TxtFile);
		}

		protected string EncryptDES(string encryptString, string encryptKey)
		{
			string result;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
				byte[] keys = this.Keys;
				byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, keys), CryptoStreamMode.Write);
				cryptoStream.Write(bytes2, 0, bytes2.Length);
				cryptoStream.FlushFinalBlock();
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			catch
			{
				result = encryptString;
			}
			return result;
		}

		protected string DecryptDES(string decryptString, string decryptKey)
		{
			string result;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(decryptKey);
				byte[] keys = this.Keys;
				byte[] array = Convert.FromBase64String(decryptString);
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, keys), CryptoStreamMode.Write);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				result = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			catch
			{
				result = decryptString;
			}
			return result;
		}

		public string StrId = "0";

		protected string AdminId = "0";

		protected string AdminName = string.Empty;

		protected string AdminPass = string.Empty;

		protected string AdminSign = string.Empty;

		protected string AdminSetting = string.Empty;

		protected string AdminCookiess = string.Empty;

		protected bool AdminIsLogin;

		protected bool AdminIsFounder;

		protected bool AdminIsSuper;

		protected int AdminGroupId = 1;

		public string StartTime = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 00:00:00";

		public string EndTime = DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd") + " 00:00:00";

		private byte[] Keys = new byte[]
		{
			18,
			52,
			86,
			120,
			144,
			171,
			205,
			239
		};
	}
}
