using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace Lottery.Utils
{
	public static class jpfileHelp
	{
		public static bool FileCrypt(string oFileName, string EncodeOrDecode, string Password)
		{
			string text = HttpContext.Current.Server.MapPath("~/Bin/tools/jpfile.exe");
			if (!File.Exists(text) || !File.Exists(HttpContext.Current.Server.MapPath(oFileName)))
			{
				return false;
			}
			oFileName = HttpContext.Current.Server.MapPath(oFileName);
			string arguments = string.Concat(new string[]
			{
				EncodeOrDecode,
				" \"",
				oFileName,
				"\" \"",
				Password,
				"\""
			});
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
			return true;
		}

		public static bool FileCrypt(string oFileName, string EncodeOrDecode)
		{
			return jpfileHelp.FileCrypt(oFileName, EncodeOrDecode, "12345678");
		}

		public static bool FileCrypt(string oFileName)
		{
			return jpfileHelp.FileCrypt(oFileName, "-d", "12345678");
		}
	}
}
