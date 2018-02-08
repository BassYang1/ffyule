using System;
using System.Web;
using System.Web.UI;

namespace Lottery.DBUtility.UI
{
	public abstract class PageUI : Page
	{
		protected override void OnError(EventArgs e)
		{
			HttpContext current = HttpContext.Current;
			Exception lastError = current.Server.GetLastError();
			string s = string.Concat(new string[]
			{
				"\r\n<pre>Offending URL: ",
				current.Request.Url.ToString(),
				"\r\nSource: ",
				lastError.Source,
				"\r\nMessage: ",
				lastError.Message,
				"\r\nStack trace: ",
				lastError.StackTrace,
				"</pre>"
			});
			current.Response.Write(s);
			current.Server.ClearError();
			base.OnError(e);
		}

		public abstract void ConnectDb();

		protected override void OnInit(EventArgs e)
		{
			base.Unload += new EventHandler(this.Jbpage_Unload);
			base.OnInit(e);
		}

		public void Alert(string msg)
		{
			base.ClientScript.RegisterClientScriptBlock(base.GetType(), "alert", "<script language=\"javascript\">alert('" + msg + "')</script>");
		}

		public void Alert(string name, string msg)
		{
			base.ClientScript.RegisterClientScriptBlock(base.GetType(), name, "<script language=\"javascript\">alert('" + msg + "');</script>");
		}

		public TimeSpan DateDiff(DateTime DateTime1, DateTime DateTime2)
		{
			TimeSpan timeSpan = new TimeSpan(DateTime1.Ticks);
			TimeSpan ts = new TimeSpan(DateTime2.Ticks);
			return timeSpan.Subtract(ts).Duration();
		}

		private void Jbpage_Unload(object sender, EventArgs e)
		{
			if (this.doh != null)
			{
				this.doh.Dispose();
			}
		}

		public DbOperHandler doh;
	}
}
