using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
	public class IPScaner
	{
		private string GetCountry()
		{
			switch (this.countryFlag)
			{
			case 1:
			case 2:
				this.country = this.GetFlagStr(this.endIpOff + 4L);
				this.local = ((1 == this.countryFlag) ? " " : this.GetFlagStr(this.endIpOff + 8L));
				break;
			default:
				this.country = this.GetFlagStr(this.endIpOff + 4L);
				this.local = this.GetFlagStr(this.objfs.Position);
				break;
			}
			return " ";
		}

		private long GetEndIp()
		{
			this.objfs.Position = this.endIpOff;
			byte[] array = new byte[5];
			this.objfs.Read(array, 0, 5);
			this.endIp = Convert.ToInt64(array[0].ToString()) + Convert.ToInt64(array[1].ToString()) * 256L + Convert.ToInt64(array[2].ToString()) * 256L * 256L + Convert.ToInt64(array[3].ToString()) * 256L * 256L * 256L;
			this.countryFlag = (int)array[4];
			return this.endIp;
		}

		private string GetFlagStr(long offSet)
		{
			byte[] array = new byte[3];
			while (true)
			{
				this.objfs.Position = offSet;
				int num = this.objfs.ReadByte();
				if (num != 1 && num != 2)
				{
					break;
				}
				this.objfs.Read(array, 0, 3);
				if (num == 2)
				{
					this.countryFlag = 2;
					this.endIpOff = offSet - 4L;
				}
				offSet = Convert.ToInt64(array[0].ToString()) + Convert.ToInt64(array[1].ToString()) * 256L + Convert.ToInt64(array[2].ToString()) * 256L * 256L;
			}
			if (offSet < 12L)
			{
				return " ";
			}
			this.objfs.Position = offSet;
			return this.GetStr();
		}

		private long GetStartIp(long recNO)
		{
			long position = this.firstStartIp + recNO * 7L;
			this.objfs.Position = position;
			byte[] array = new byte[7];
			this.objfs.Read(array, 0, 7);
			this.endIpOff = Convert.ToInt64(array[4].ToString()) + Convert.ToInt64(array[5].ToString()) * 256L + Convert.ToInt64(array[6].ToString()) * 256L * 256L;
			this.startIp = Convert.ToInt64(array[0].ToString()) + Convert.ToInt64(array[1].ToString()) * 256L + Convert.ToInt64(array[2].ToString()) * 256L * 256L + Convert.ToInt64(array[3].ToString()) * 256L * 256L * 256L;
			return this.startIp;
		}

		private string GetStr()
		{
			string text = "";
			byte[] array = new byte[2];
			while (true)
			{
				byte b = (byte)this.objfs.ReadByte();
				if (b == 0)
				{
					break;
				}
				if (b > 127)
				{
					byte b2 = (byte)this.objfs.ReadByte();
					array[0] = b;
					array[1] = b2;
					Encoding encoding = Encoding.GetEncoding("GB2312");
					text += encoding.GetString(array);
				}
				else
				{
					text += (char)b;
				}
			}
			return text;
		}

		private string IntToIP(long ip_Int)
		{
			long num = (ip_Int & (long)((long)-16777216)) >> 24;
			if (num < 0L)
			{
				num += 256L;
			}
			long num2 = (ip_Int & 16711680L) >> 16;
			if (num2 < 0L)
			{
				num2 += 256L;
			}
			long num3 = (ip_Int & 65280L) >> 8;
			if (num3 < 0L)
			{
				num3 += 256L;
			}
			long num4 = ip_Int & 255L;
			if (num4 < 0L)
			{
				num4 += 256L;
			}
			return string.Concat(new string[]
			{
				num.ToString(),
				".",
				num2.ToString(),
				".",
				num3.ToString(),
				".",
				num4.ToString()
			});
		}

		public string IPLocation()
		{
			this.QQwry();
			return this.country + this.local;
		}

		public string IPLocation(string dataPath, string ip)
		{
			this.dataPath = dataPath;
			this.ip = ip;
			this.QQwry();
			return this.country + this.local;
		}

		private long IpToInt(string ip)
		{
			char[] separator = new char[]
			{
				'.'
			};
			if (ip.Split(separator).Length == 3)
			{
				ip += ".0";
			}
			string[] array = ip.Split(separator);
			long num = long.Parse(array[0]) * 256L * 256L * 256L;
			long num2 = long.Parse(array[1]) * 256L * 256L;
			long num3 = long.Parse(array[2]) * 256L;
			long num4 = long.Parse(array[3]);
			return num + num2 + num3 + num4;
		}

		private int QQwry()
		{
			string pattern = "(((\\d{1,2})|(1\\d{2})|(2[0-4]\\d)|(25[0-5]))\\.){3}((\\d{1,2})|(1\\d{2})|(2[0-4]\\d)|(25[0-5]))";
			Regex regex = new Regex(pattern);
			if (!regex.Match(this.ip).Success)
			{
				this.errMsg = "IP格式错误";
				return 4;
			}
			long num = this.IpToInt(this.ip);
			int num2 = 0;
			if (num >= this.IpToInt("127.0.0.0") && num <= this.IpToInt("127.255.255.255"))
			{
				this.country = "本机内部环回地址";
				this.local = "";
				num2 = 1;
			}
			else if ((num >= this.IpToInt("0.0.0.0") && num <= this.IpToInt("2.255.255.255")) || (num >= this.IpToInt("64.0.0.0") && num <= this.IpToInt("126.255.255.255")) || (num >= this.IpToInt("58.0.0.0") && num <= this.IpToInt("60.255.255.255")))
			{
				this.country = "网络保留地址";
				this.local = "";
				num2 = 1;
			}
			this.objfs = new FileStream(this.dataPath, FileMode.Open, FileAccess.Read);
			int result;
			try
			{
				this.objfs.Position = 0L;
				byte[] array = new byte[8];
				this.objfs.Read(array, 0, 8);
				this.firstStartIp = (long)((int)array[0] + (int)array[1] * 256 + (int)array[2] * 256 * 256 + (int)array[3] * 256 * 256 * 256);
				this.lastStartIp = (long)((int)array[4] + (int)array[5] * 256 + (int)array[6] * 256 * 256 + (int)array[7] * 256 * 256 * 256);
				long num3 = Convert.ToInt64((double)(this.lastStartIp - this.firstStartIp) / 7.0);
				if (num3 <= 1L)
				{
					this.country = "FileDataError";
					this.objfs.Close();
					result = 2;
				}
				else
				{
					long num4 = num3;
					long num5 = 0L;
					while (num5 < num4 - 1L)
					{
						long num6 = (num4 + num5) / 2L;
						this.GetStartIp(num6);
						if (num == this.startIp)
						{
							num5 = num6;
							break;
						}
						if (num > this.startIp)
						{
							num5 = num6;
						}
						else
						{
							num4 = num6;
						}
					}
					this.GetStartIp(num5);
					this.GetEndIp();
					if (this.startIp <= num && this.endIp >= num)
					{
						this.GetCountry();
						this.local = this.local.Replace("（我们一定要解放台湾！！！）", "");
					}
					else
					{
						num2 = 3;
						this.country = "未知";
						this.local = "";
					}
					this.objfs.Close();
					result = num2;
				}
			}
			catch
			{
				result = 1;
			}
			return result;
		}

		public string Country
		{
			get
			{
				return this.country;
			}
		}

		public string DataPath
		{
			set
			{
				this.dataPath = value;
			}
		}

		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		public string IP
		{
			set
			{
				this.ip = value;
			}
		}

		public string Local
		{
			get
			{
				return this.local;
			}
		}

		private string country;

		private int countryFlag;

		private string dataPath;

		private long endIp;

		private long endIpOff;

		private string errMsg;

		private long firstStartIp;

		private string ip;

		private long lastStartIp;

		private string local;

		private FileStream objfs;

		private long startIp;
	}
}
