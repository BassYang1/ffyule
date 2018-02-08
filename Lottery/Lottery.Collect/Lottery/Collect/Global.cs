using System;
using System.Web;

namespace Lottery.Collect
{
	public class Global : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			TimeData.Run();
		}

		private void Application_End(object sender, EventArgs e)
		{
		}

		private void Application_Error(object sender, EventArgs e)
		{
		}

		protected void Session_Start(object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
		}

		protected void Session_End(object sender, EventArgs e)
		{
		}

		public static HttpContext myContext = HttpContext.Current;
	}
}
