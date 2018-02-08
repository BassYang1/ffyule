using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;

namespace Lottery.Utils
{
	public static class DirFile
	{
		public static void CreateDir(string dir)
		{
			if (dir.Length == 0)
			{
				return;
			}
			if (!Directory.Exists(HttpContext.Current.Server.MapPath(dir)))
			{
				Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dir));
			}
		}

		public static void CreateFolder(string folderPath)
		{
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}
		}

		public static void DeleteDir(string dir)
		{
			if (dir.Length == 0)
			{
				return;
			}
			if (Directory.Exists(HttpContext.Current.Server.MapPath(dir)))
			{
				Directory.Delete(HttpContext.Current.Server.MapPath(dir), true);
			}
		}

		public static bool FileExists(string file)
		{
			return File.Exists(HttpContext.Current.Server.MapPath(file));
		}

		public static string ReadFile(string file)
		{
			if (!DirFile.FileExists(file))
			{
				return "";
			}
			string result;
			try
			{
				StreamReader streamReader = new StreamReader(HttpContext.Current.Server.MapPath(file), Encoding.UTF8);
				string text = streamReader.ReadToEnd();
				streamReader.Close();
				result = text;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public static void SaveFile(string TxtStr, string tempDir)
		{
			DirFile.SaveFile(TxtStr, tempDir, true);
		}

		public static void SaveFile(string TxtStr, string tempDir, bool noBom)
		{
			try
			{
				DirFile.CreateDir(DirFile.GetFolderPath(true, tempDir));
				StreamWriter streamWriter;
				if (noBom)
				{
					streamWriter = new StreamWriter(HttpContext.Current.Server.MapPath(tempDir), false, new UTF8Encoding(false));
				}
				else
				{
					streamWriter = new StreamWriter(HttpContext.Current.Server.MapPath(tempDir), false, Encoding.UTF8);
				}
				streamWriter.Write(TxtStr);
				streamWriter.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static void CopyFile(string file1, string file2, bool overwrite)
		{
			if (File.Exists(HttpContext.Current.Server.MapPath(file1)))
			{
				if (overwrite)
				{
					File.Copy(HttpContext.Current.Server.MapPath(file1), HttpContext.Current.Server.MapPath(file2), true);
					return;
				}
				if (!File.Exists(HttpContext.Current.Server.MapPath(file2)))
				{
					File.Copy(HttpContext.Current.Server.MapPath(file1), HttpContext.Current.Server.MapPath(file2));
				}
			}
		}

		public static void DeleteFile(string file)
		{
			if (file.Length == 0)
			{
				return;
			}
			if (File.Exists(HttpContext.Current.Server.MapPath(file)))
			{
				File.Delete(HttpContext.Current.Server.MapPath(file));
			}
		}

		public static string GetFolderPath(string filePath)
		{
			return DirFile.GetFolderPath(false, filePath);
		}

		public static string GetFolderPath(bool isUrl, string filePath)
		{
			if (isUrl)
			{
				return filePath.Substring(0, filePath.LastIndexOf("/") + 1);
			}
			return filePath.Substring(0, filePath.LastIndexOf("\\") + 1);
		}

		public static string GetFileName(string filePath)
		{
			return DirFile.GetFileName(false, filePath);
		}

		public static string GetFileName(bool isUrl, string filePath)
		{
			if (isUrl)
			{
				return filePath.Substring(filePath.LastIndexOf("/") + 1, filePath.Length - filePath.LastIndexOf("/") - 1);
			}
			return filePath.Substring(filePath.LastIndexOf("\\") + 1, filePath.Length - filePath.LastIndexOf("\\") - 1);
		}

		public static string GetFileExt(string filePath)
		{
			return filePath.Substring(filePath.LastIndexOf(".") + 1, filePath.Length - filePath.LastIndexOf(".") - 1).ToLower();
		}

		public static void CopyDir(string OldDir, string NewDir)
		{
			DirectoryInfo oldDirectory = new DirectoryInfo(OldDir);
			DirectoryInfo newDirectory = new DirectoryInfo(NewDir);
			DirFile.CopyDir(oldDirectory, newDirectory);
		}

		private static void CopyDir(DirectoryInfo OldDirectory, DirectoryInfo NewDirectory)
		{
			string text = NewDirectory.FullName + "\\" + OldDirectory.Name;
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			FileInfo[] files = OldDirectory.GetFiles();
			FileInfo[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				FileInfo fileInfo = array[i];
				File.Copy(fileInfo.FullName, text + "\\" + fileInfo.Name, true);
			}
			DirectoryInfo[] directories = OldDirectory.GetDirectories();
			DirectoryInfo[] array2 = directories;
			for (int j = 0; j < array2.Length; j++)
			{
				DirectoryInfo oldDirectory = array2[j];
				DirectoryInfo newDirectory = new DirectoryInfo(text);
				DirFile.CopyDir(oldDirectory, newDirectory);
			}
		}

		public static void DelDir(string OldDir)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(OldDir);
			directoryInfo.Delete(true);
		}

		public static void CopyAndDelDir(string OldDirectory, string NewDirectory)
		{
			DirFile.CopyDir(OldDirectory, NewDirectory);
			DirFile.DelDir(OldDirectory);
		}

		public static bool DownloadFile(HttpRequest _Request, HttpResponse _Response, string _fullPath, long _speed)
		{
			string fileName = DirFile.GetFileName(false, _fullPath);
			try
			{
				FileStream fileStream = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				BinaryReader binaryReader = new BinaryReader(fileStream);
				try
				{
					_Response.AddHeader("Accept-Ranges", "bytes");
					_Response.Buffer = false;
					long length = fileStream.Length;
					long num = 0L;
					double num2 = 10240.0;
					int millisecondsTimeout = (int)Math.Floor(1000.0 * num2 / (double)_speed) + 1;
					if (_Request.Headers["Range"] != null)
					{
						_Response.StatusCode = 206;
						string[] array = _Request.Headers["Range"].Split(new char[]
						{
							'=',
							'-'
						});
						num = Convert.ToInt64(array[1]);
					}
					_Response.AddHeader("Content-Length", (length - num).ToString());
					_Response.AddHeader("Connection", "Keep-Alive");
					_Response.ContentType = "application/octet-stream";
					_Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
					binaryReader.BaseStream.Seek(num, SeekOrigin.Begin);
					int num3 = (int)Math.Floor((double)(length - num) / num2) + 1;
					for (int i = 0; i < num3; i++)
					{
						if (_Response.IsClientConnected)
						{
							_Response.BinaryWrite(binaryReader.ReadBytes(int.Parse(num2.ToString())));
							Thread.Sleep(millisecondsTimeout);
						}
						else
						{
							i = num3;
						}
					}
				}
				catch
				{
					bool result = false;
					return result;
				}
				finally
				{
					binaryReader.Close();
					fileStream.Close();
				}
			}
			catch
			{
				bool result = false;
				return result;
			}
			return true;
		}
	}
}
