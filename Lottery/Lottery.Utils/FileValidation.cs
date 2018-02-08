using System;
using System.IO;
using System.Text;
using System.Web;

namespace Lottery.Utils
{
	public static class FileValidation
	{
		public static bool IsAllowedExtension(HttpPostedFile oFile, FileExtension[] fileEx)
		{
			int contentLength = oFile.ContentLength;
			byte[] buffer = new byte[contentLength];
			oFile.InputStream.Read(buffer, 0, contentLength);
			MemoryStream memoryStream = new MemoryStream(buffer);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			string text = "";
			try
			{
				text = binaryReader.ReadByte().ToString();
				text += binaryReader.ReadByte().ToString();
			}
			catch
			{
			}
			binaryReader.Close();
			memoryStream.Close();
			for (int i = 0; i < fileEx.Length; i++)
			{
				FileExtension fileExtension = fileEx[i];
				if (int.Parse(text) == (int)fileExtension)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsSecureUploadPhoto(HttpPostedFile oFile)
		{
			bool flag = false;
			string a = Path.GetExtension(oFile.FileName).ToLower();
			string[] array = new string[]
			{
				".gif",
				".png",
				".jpeg",
				".jpg",
				".bmp"
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (a == array[i])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return true;
			}
			FileExtension[] fileEx = new FileExtension[]
			{
				FileExtension.BMP,
				FileExtension.GIF,
				FileExtension.JPG,
				FileExtension.PNG
			};
			return FileValidation.IsAllowedExtension(oFile, fileEx);
		}

		public static bool IsSecureUpfilePhoto(string photoFile)
		{
			bool flag = false;
			string a = "Yes";
			string a2 = Path.GetExtension(photoFile).ToLower();
			string[] array = new string[]
			{
				".gif",
				".png",
				".jpeg",
				".jpg",
				".bmp"
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (a2 == array[i])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return true;
			}
			StreamReader streamReader = new StreamReader(photoFile, Encoding.Default);
			string text = streamReader.ReadToEnd();
			streamReader.Close();
			string text2 = "request|<script|.getfolder|.createfolder|.deletefolder|.createdirectory|.deletedirectory|.saveas|wscript.shell|script.encode|server.|.createobject|execute|activexobject|language=";
			string[] array2 = text2.Split(new char[]
			{
				'|'
			});
			for (int j = 0; j < array2.Length; j++)
			{
				string value = array2[j];
				if (text.ToLower().IndexOf(value) != -1)
				{
					File.Delete(photoFile);
					a = "No";
					break;
				}
			}
			return a == "Yes";
		}
	}
}
