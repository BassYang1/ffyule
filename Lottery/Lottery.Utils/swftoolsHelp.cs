using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Lottery.Utils
{
	public static class swftoolsHelp
	{
		public static bool PDF2SWF(string pdfPath, string swfPath)
		{
			return swftoolsHelp.PDF2SWF(pdfPath, swfPath, 1, swftoolsHelp.GetPageCount(HttpContext.Current.Server.MapPath(pdfPath)), 80);
		}

		public static bool PDF2SWF(string pdfPath, string swfPath, int page)
		{
			return swftoolsHelp.PDF2SWF(pdfPath, swfPath, 1, page, 80);
		}

		public static bool PDF2SWF(string pdfPath, string swfPath, int beginpage, int endpage, int photoQuality)
		{
			string text = HttpContext.Current.Server.MapPath("~/Bin/tools/pdf2swf-0.9.1.exe");
			pdfPath = HttpContext.Current.Server.MapPath(pdfPath);
			swfPath = HttpContext.Current.Server.MapPath(swfPath);
			if (!File.Exists(text) || !File.Exists(pdfPath))
			{
				return false;
			}
			if (File.Exists(swfPath))
			{
				File.Delete(swfPath);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" \"" + pdfPath + "\"");
			stringBuilder.Append(" -o \"" + swfPath + "\"");
			stringBuilder.Append(" -s flashversion=9");
			if (endpage > swftoolsHelp.GetPageCount(pdfPath))
			{
				endpage = swftoolsHelp.GetPageCount(pdfPath);
			}
			stringBuilder.Append(string.Concat(new object[]
			{
				" -p \"",
				beginpage,
				"-",
				endpage,
				"\""
			}));
			stringBuilder.Append(" -j " + photoQuality);
			string arguments = stringBuilder.ToString();
			Process process = new Process();
			process.StartInfo.FileName = text;
			process.StartInfo.Arguments = arguments;
			process.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath("~/Bin/");
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.CreateNoWindow = false;
			process.Start();
			process.BeginErrorReadLine();
			process.WaitForExit();
			process.Close();
			process.Dispose();
			return File.Exists(swfPath);
		}

		public static int GetPageCount(string pdfPath)
		{
			byte[] array = File.ReadAllBytes(pdfPath);
			if (array == null)
			{
				return -1;
			}
			if (array.Length <= 0)
			{
				return -1;
			}
			string @string = Encoding.Default.GetString(array);
			Regex regex = new Regex("/Type\\s*/Page[^s]");
			MatchCollection matchCollection = regex.Matches(@string);
			return matchCollection.Count;
		}
	}
}
