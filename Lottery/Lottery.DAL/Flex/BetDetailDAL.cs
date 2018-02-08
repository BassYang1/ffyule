using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL.Flex
{
	public static class BetDetailDAL
	{
		public static void SetBetDetail(string STime, string UserId, string BetId, string Detail)
		{
			BetDetailDAL.SaveContentFile(Detail, string.Concat(new string[]
			{
				STime,
				"\\",
				UserId,
				"\\",
				BetId,
				".js"
			}));
		}

		public static string GetBetDetail2(string STime, string UserId, string BetId)
		{
			if (string.IsNullOrEmpty(BetDetailDAL.ReadContentFile(string.Concat(new string[]
			{
				STime,
				"\\",
				UserId,
				"\\",
				BetId,
				".js"
			}))))
			{
				string text = "";
				using (DbOperHandler dbOperHandler = new ComData().Doh())
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = "select [Detail] from N_UserBet where Id=" + BetId;
					DataTable dataTable = dbOperHandler.GetDataTable();
					text = string.Concat(dataTable.Rows[0]["Detail"]);
					dataTable.Clear();
					dataTable.Dispose();
				}
				return BetDetailDAL.ReadContentFile(string.Concat(new string[]
				{
					STime,
					"\\",
					UserId,
					"\\",
					text,
					".js"
				}));
			}
			return BetDetailDAL.ReadContentFile(string.Concat(new string[]
			{
				STime,
				"\\",
				UserId,
				"\\",
				BetId,
				".js"
			}));
		}

		public static string GetBetDetail(string STime, string UserId, string BetId)
		{
			if (string.IsNullOrEmpty(BetDetailDAL.ReadContentFile(string.Concat(new string[]
			{
				STime,
				"\\",
				UserId,
				"\\",
				BetId,
				".js"
			}))))
			{
				string text = "";
				using (DbOperHandler dbOperHandler = new ComData().Doh())
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = "select [Detail] from N_UserBet where Id=" + BetId;
					DataTable dataTable = dbOperHandler.GetDataTable();
					text = string.Concat(dataTable.Rows[0]["Detail"]);
					dataTable.Clear();
					dataTable.Dispose();
				}
				return BetDetailDAL.ReadContentFile(string.Concat(new string[]
				{
					STime,
					"\\",
					UserId,
					"\\",
					text,
					".js"
				}));
			}
			return BetDetailDAL.ReadContentFile(string.Concat(new string[]
			{
				STime,
				"\\",
				UserId,
				"\\",
				BetId,
				".js"
			}));
		}

		public static void SetYouleDetail(string STime, string UserId, string BetId, string Detail)
		{
			BetDetailDAL.SaveContentFile(Detail, string.Concat(new string[]
			{
				STime,
				"\\",
				UserId,
				"\\",
				BetId,
				".js"
			}));
		}

		public static string GetYouleDetail(string STime, string UserId, string BetId)
		{
			Random random = new Random();
			return HtmlOperate.GetHtml(string.Concat(new object[]
			{
				"http://192.168.0.51:999/",
				STime,
				"/",
				UserId,
				"/",
				BetId,
				".js?",
				random.Next(1, 1000)
			}));
		}

		public static bool connectState(string path)
		{
			return BetDetailDAL.connectState(path, "", "");
		}

		public static bool connectState(string path, string userName, string passWord)
		{
			bool result = false;
			Process process = new Process();
			try
			{
				process.StartInfo.FileName = "cmd.exe";
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardInput = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.CreateNoWindow = true;
				process.Start();
				string value = string.Concat(new string[]
				{
					"net use ",
					path,
					" /User:",
					userName,
					" ",
					passWord,
					" /PERSISTENT:YES"
				});
				process.StandardInput.WriteLine(value);
				process.StandardInput.WriteLine("exit");
				while (!process.HasExited)
				{
					process.WaitForExit(1000);
				}
				string text = process.StandardError.ReadToEnd();
				process.StandardError.Close();
				if (!string.IsNullOrEmpty(text))
				{
					throw new Exception(text);
				}
				result = true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				process.Close();
				process.Dispose();
			}
			return result;
		}

		public static void ReadFiles(string path)
		{
			try
			{
				using (StreamReader streamReader = new StreamReader(path))
				{
					string value;
					while ((value = streamReader.ReadLine()) != null)
					{
						Console.WriteLine(value);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(ex.Message);
			}
		}

		public static void WriteFiles(string path)
		{
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(path))
				{
					streamWriter.Write("This is the ");
					streamWriter.WriteLine("header for the file.");
					streamWriter.WriteLine("-------------------");
					streamWriter.Write("The date is: ");
					streamWriter.WriteLine(DateTime.Now);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(ex.Message);
			}
		}

		private static string ReadContentFile2(string fileName)
		{
			string result;
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo("\\\\192.168.0.51\\Bets");
				string path = directoryInfo.ToString() + "\\" + fileName;
				using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
				{
					string text = streamReader.ReadToEnd();
					result = text;
				}
			}
			catch (Exception ex)
			{
				new LogExceptionDAL().Save("派奖异常", ex.Message);
				result = "";
			}
			return result;
		}

		private static bool FileExists(string file)
		{
			return File.Exists(file);
		}

		private static string ReadContentFile(string fileName)
		{
			string text = BetDetailDAL.Folder + fileName;
			if (!BetDetailDAL.FileExists(text))
			{
				return "";
			}
			string result;
			try
			{
				StreamReader streamReader = new StreamReader(text, Encoding.UTF8);
				string text2 = streamReader.ReadToEnd();
				streamReader.Close();
				result = text2;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		private static void SaveContentFile(string TxtStr, string fileName)
		{
			BetDetailDAL.SaveContentFile(TxtStr, fileName, "2");
		}

		private static void SaveContentFile(string TxtStr, string fileName, string Edcode)
		{
			string text = BetDetailDAL.Folder + fileName;
			Encoding encoding = Encoding.Default;
			if (Edcode != null)
			{
				if (!(Edcode == "3"))
				{
					if (!(Edcode == "2"))
					{
						if (Edcode == "1")
						{
							encoding = Encoding.GetEncoding("GB2312");
						}
					}
					else
					{
						encoding = Encoding.UTF8;
					}
				}
				else
				{
					encoding = Encoding.Unicode;
				}
			}
			DirFile.CreateFolder(DirFile.GetFolderPath(false, text));
			StreamWriter streamWriter = new StreamWriter(text, false, encoding);
			streamWriter.Write(TxtStr);
			streamWriter.Close();
		}

		public static void WriteFilesOfWangluo(string path, string content)
		{
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(path))
				{
					streamWriter.WriteLine(content);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private static string Folder = "D:\\Bets\\";
	}
}
