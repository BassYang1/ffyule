using System;
using System.Web;
using System.Web.UI.HtmlControls;
using Lottery.DAL;

namespace Lottery.Web.temp
{
	public class WebSite : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			new SiteDAL().CreateSiteConfig();
			new SiteDAL().CreateSiteFiles();
			HttpContext.Current.Application["Lottery"] = null;
			base.Response.Write("更新完成：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		protected HtmlForm form1;
	}
}
