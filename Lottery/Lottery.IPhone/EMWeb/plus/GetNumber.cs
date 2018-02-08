using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using Lottery.DAL;

namespace Lottery.EMWeb.plus
{
	public class GetNumber : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.ContentType = "text/html; charset=utf-8";
			string text = base.Request.QueryString["lid"].ToString();
			string text2 = base.Request.QueryString["callback"].ToString();
			string html = GetNumber.GetHtml(string.Concat(new string[]
			{
				this.strNumberUrl,
				"/Data/GetJsonData.aspx?lid=",
				text,
				"&callback=",
				text2
			}));
			base.Response.Write(html);
		}

		public static string GetHtml(string Url)
		{
			string result = "";
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
				httpWebRequest.Method = "GET";
				httpWebRequest.UserAgent = "MSIE";
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
				result = streamReader.ReadToEnd();
			}
			catch
			{
				new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
			}
			return result;
		}

		private string strNumberUrl = ConfigurationManager.AppSettings["NumberUrl"].ToString();
	}
}
