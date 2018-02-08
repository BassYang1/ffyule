using System;
using System.IO;
using System.Net;
using System.Text;

namespace Lottery.Utils
{
	public static class HttpHelp
	{
		public static string Get_Http(string url, int timeout, Encoding EnCodeType)
		{
			string result = string.Empty;
			if (url.Length < 10)
			{
				return "$UrlIsFalse$";
			}
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.Timeout = timeout;
				httpWebRequest.Method = "Get";
				WebResponse response = httpWebRequest.GetResponse();
				Stream responseStream = response.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, EnCodeType);
				result = streamReader.ReadToEnd();
				responseStream.Close();
				streamReader.Close();
			}
			catch (Exception)
			{
				result = "$GetFalse$";
			}
			return result;
		}

		public static string Post_Http(string url, string postData, string encodeType)
		{
			string result = null;
			try
			{
				Encoding encoding = Encoding.GetEncoding(encodeType);
				byte[] bytes = encoding.GetBytes(postData);
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.Timeout = 19600;
				httpWebRequest.Method = "POST";
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				httpWebRequest.ContentLength = (long)bytes.Length;
				Stream requestStream = httpWebRequest.GetRequestStream();
				requestStream.Write(bytes, 0, bytes.Length);
				requestStream.Close();
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.Default);
				result = streamReader.ReadToEnd();
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}
	}
}
