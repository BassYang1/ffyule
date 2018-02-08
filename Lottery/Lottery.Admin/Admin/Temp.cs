using System;
using System.Configuration;
using System.Web.UI;

namespace Lottery.Admin
{
	public class Temp : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.strRootUrl = ConfigurationManager.AppSettings["RootUrl"].ToString();
			this.strIphoneUrl = ConfigurationManager.AppSettings["IphoneUrl"].ToString();
		}

		public string strRootUrl = "";

		public string strIphoneUrl = "";
	}
}
