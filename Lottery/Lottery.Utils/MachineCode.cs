using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Lottery.Utils
{
	public static class MachineCode
	{
		public static void setIntCode()
		{
			for (int i = 1; i < MachineCode.intCode.Length; i++)
			{
				MachineCode.intCode[i] = i % 9;
			}
		}

		public static string GetDiskVolumeSerialNumber()
		{
			new ManagementClass("Win32_NetworkAdapterConfiguration");
			ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
			managementObject.Get();
			return managementObject.GetPropertyValue("VolumeSerialNumber").ToString();
		}

		public static string getCpu()
		{
			string result = null;
			ManagementClass managementClass = new ManagementClass("win32_Processor");
			ManagementObjectCollection instances = managementClass.GetInstances();
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ManagementObject managementObject = (ManagementObject)enumerator.Current;
					result = managementObject.Properties["Processorid"].Value.ToString();
				}
			}
			return result;
		}

		public static string getMNum()
		{
			string text = MachineCode.getCpu() + MachineCode.GetDiskVolumeSerialNumber();
			return text.Substring(0, 24);
		}

		public static string getRNum()
		{
			MachineCode.setIntCode();
			string text = MachineCode.getCpu() + MachineCode.GetDiskVolumeSerialNumber() + MachineCode.EncryptDES("mobstermobstermobstermobstermobstermobstermobstermobster", "shuangseq");
			for (int i = 1; i < MachineCode.Charcode.Length; i++)
			{
				MachineCode.Charcode[i] = Convert.ToChar(text.Substring(i - 1, 1));
			}
			for (int j = 1; j < MachineCode.intNumber.Length; j++)
			{
				MachineCode.intNumber[j] = MachineCode.intCode[Convert.ToInt32(MachineCode.Charcode[j])] + Convert.ToInt32(MachineCode.Charcode[j]);
			}
			string text2 = "";
			for (int k = 1; k < MachineCode.intNumber.Length; k++)
			{
				if (MachineCode.intNumber[k] >= 48 && MachineCode.intNumber[k] <= 57)
				{
					text2 += Convert.ToChar(MachineCode.intNumber[k]).ToString();
				}
				else if (MachineCode.intNumber[k] >= 65 && MachineCode.intNumber[k] <= 90)
				{
					text2 += Convert.ToChar(MachineCode.intNumber[k]).ToString();
				}
				else if (MachineCode.intNumber[k] >= 97 && MachineCode.intNumber[k] <= 122)
				{
					text2 += Convert.ToChar(MachineCode.intNumber[k]).ToString();
				}
				else if (MachineCode.intNumber[k] > 122)
				{
					text2 += Convert.ToChar(MachineCode.intNumber[k] - 10).ToString();
				}
				else
				{
					text2 += Convert.ToChar(MachineCode.intNumber[k] - 9).ToString();
				}
			}
			return text2.ToUpper();
		}

		public static string getRNum(string str)
		{
			MachineCode.setIntCode();
			string text = str + MachineCode.EncryptDES("mobstermobstermobstermobstermobstermobstermobstermobster", "shuangseq");
			for (int i = 1; i < MachineCode.Charcode.Length; i++)
			{
				MachineCode.Charcode[i] = Convert.ToChar(text.Substring(i - 1, 1));
			}
			for (int j = 1; j < MachineCode.intNumber.Length; j++)
			{
				MachineCode.intNumber[j] = MachineCode.intCode[Convert.ToInt32(MachineCode.Charcode[j])] + Convert.ToInt32(MachineCode.Charcode[j]);
			}
			string text2 = "";
			for (int k = 1; k < MachineCode.intNumber.Length; k++)
			{
				if (MachineCode.intNumber[k] >= 48 && MachineCode.intNumber[k] <= 57)
				{
					text2 += Convert.ToChar(MachineCode.intNumber[k]).ToString();
				}
				else if (MachineCode.intNumber[k] >= 65 && MachineCode.intNumber[k] <= 90)
				{
					text2 += Convert.ToChar(MachineCode.intNumber[k]).ToString();
				}
				else if (MachineCode.intNumber[k] >= 97 && MachineCode.intNumber[k] <= 122)
				{
					text2 += Convert.ToChar(MachineCode.intNumber[k]).ToString();
				}
				else if (MachineCode.intNumber[k] > 122)
				{
					text2 += Convert.ToChar(MachineCode.intNumber[k] - 10).ToString();
				}
				else
				{
					text2 += Convert.ToChar(MachineCode.intNumber[k] - 9).ToString();
				}
			}
			return text2.ToUpper();
		}

		public static string EncryptDES(string encryptString, string encryptKey)
		{
			byte[] array = new byte[]
			{
				18,
				52,
				86,
				120,
				144,
				171,
				205,
				239
			};
			string result;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
				byte[] rgbIV = array;
				byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
				cryptoStream.Write(bytes2, 0, bytes2.Length);
				cryptoStream.FlushFinalBlock();
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			catch
			{
				result = encryptString;
			}
			return result;
		}

		public static int[] intCode = new int[127];

		public static int[] intNumber = new int[64];

		public static char[] Charcode = new char[64];
	}
}
