using System;
using System.IO;
using System.Web;

namespace Lottery.EMWeb
{
	public class ajaxuploadhandler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			HttpPostedFile httpPostedFile = context.Request.Files[0];
			string text = "/statics/temp/";
			int contentLength = httpPostedFile.ContentLength;
			int num = 512000;
			string s = "-1";
			if (contentLength <= num)
			{
				byte[] buffer = new byte[contentLength];
				httpPostedFile.InputStream.Read(buffer, 0, contentLength);
				MemoryStream memoryStream = new MemoryStream(buffer);
				string text2 = HttpContext.Current.Server.MapPath(text);
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				string str = ajaxuploadhandler.CreateIdCode() + ".txt";
				httpPostedFile.SaveAs(text2 + str);
				s = text + str;
				memoryStream.Close();
			}
			context.Response.Write(s);
		}

		public static string CreateIdCode()
		{
			return (DateTime.Now.ToUniversalTime() - Convert.ToDateTime("1970-01-01")).TotalMilliseconds.ToString("0");
		}

		public bool IsReusable
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
