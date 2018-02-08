using System;

namespace Lottery.Utils
{
	public class Func
	{
		public static string Addstr(string Old, string New)
		{
			if (Old == "")
			{
				return New;
			}
			return Old + "," + New;
		}

		public static string AddZero(int Num, int Len)
		{
			string text = "";
			for (int i = 1; i <= Len; i++)
			{
				text += "0";
			}
			text += Num.ToString();
			return text.Substring(text.Length - Len);
		}

		public static string Delstr(string Old, string delStr)
		{
			string text = "";
			string[] array = Old.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != delStr)
				{
					text = Func.Addstr(text, array[i]);
				}
			}
			return text;
		}

		public static string ExistUser(string Users, string Users2)
		{
			if (Users2 == "")
			{
				return Users;
			}
			string[] array = Users.Split(new char[]
			{
				','
			});
			string text = "";
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text2 = array2[i];
				if (!Func.IsExistUser(Users2, text2))
				{
					if (text == "")
					{
						text = text2;
					}
					else
					{
						text = text + "," + text2;
					}
				}
			}
			return text;
		}

		public static int GetLenNum(string str)
		{
			return str.Length;
		}

		public static int GetLettoryNum(string Lettory)
		{
			if (Lettory.IndexOf(",") == -1)
			{
				return 0;
			}
			string[] array = Lettory.Split(new char[]
			{
				'|'
			});
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					','
				});
				int num2 = 1;
				for (int j = 0; j < array2.Length; j++)
				{
					num2 *= Func.GetLenNum(array2[j]);
				}
				num += num2;
			}
			return num;
		}

		public static int GetLottoryNumZ3(string Lettory)
		{
			string[] array = Lettory.Split(new char[]
			{
				'|'
			});
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				num += Func.GetLenNum(array[i]) * (Func.GetLenNum(array[i]) - 1) * 3;
			}
			return num;
		}

		public static int GetLottoryNumZ6(string Lettory)
		{
			string[] array = Lettory.Split(new char[]
			{
				'|'
			});
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				int num2 = 0;
				switch (array[i].Length)
				{
				case 3:
					num2 = 6;
					break;
				case 4:
					num2 = 24;
					break;
				case 5:
					num2 = 60;
					break;
				case 6:
					num2 = 120;
					break;
				case 7:
					num2 = 210;
					break;
				case 8:
					num2 = 336;
					break;
				case 9:
					num2 = 504;
					break;
				case 10:
					num2 = 720;
					break;
				}
				num += num2;
			}
			return num;
		}

		public static string GetOSNameByUserAgent(string userAgent)
		{
			string result = "未知";
			if (userAgent.Contains("NT 6.0"))
			{
				return "Windows Vista/Server 2008";
			}
			if (userAgent.Contains("NT 6.1"))
			{
				return "Windows 7";
			}
			if (userAgent.Contains("NT 5.2"))
			{
				return "Windows Server 2003";
			}
			if (userAgent.Contains("NT 5.1"))
			{
				return "Windows XP";
			}
			if (userAgent.Contains("NT 5"))
			{
				return "Windows 2000";
			}
			if (userAgent.Contains("NT 4"))
			{
				return "Windows NT4";
			}
			if (userAgent.Contains("Me"))
			{
				return "Windows Me";
			}
			if (userAgent.Contains("98"))
			{
				return "Windows 98";
			}
			if (userAgent.Contains("95"))
			{
				return "Windows 95";
			}
			if (userAgent.Contains("Mac"))
			{
				return "Mac";
			}
			if (userAgent.Contains("Unix"))
			{
				return "UNIX";
			}
			if (userAgent.Contains("Linux"))
			{
				return "Linux";
			}
			if (userAgent.Contains("SunOS"))
			{
				result = "SunOS";
			}
			return result;
		}

		public static bool IsDateTime(string source)
		{
			bool result;
			try
			{
				Convert.ToDateTime(source);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool IsExistUser(string Users, string UserId)
		{
			bool result = true;
			if (("," + Users + ",").IndexOf("," + UserId + ",") == -1)
			{
				result = false;
			}
			return result;
		}

		public static string MoneyColor(decimal money)
		{
			if (money < 0m)
			{
				return "<font color='#339900'>" + money.ToString("0.00") + "</font>";
			}
			if (money > 0m)
			{
				return "<font color='#CCFF00'>+" + money.ToString("0.00") + "</font>";
			}
			return "<font color='#000000'>" + money.ToString("0.00") + "</font>";
		}

		public static string NumberPos(string Pos)
		{
			string text = "";
			string[] array = "万位,千位,百位,十位,个位".Split(new char[]
			{
				','
			});
			string[] array2 = Pos.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array2.Length; i++)
			{
				string value = array2[i];
				if (text == "")
				{
					text = array[Convert.ToInt32(value)];
				}
				else
				{
					text = text + "，" + array[Convert.ToInt32(value)];
				}
			}
			return text;
		}

		public static int SearchStrNum(string strs, string str)
		{
			int num = 0;
			for (int i = 0; i < strs.Length; i++)
			{
				if (strs.Substring(i, 1) == str)
				{
					num++;
				}
			}
			return num;
		}

		public static string ShowMoney(decimal Money)
		{
			if (Money > 0m)
			{
				return "<font color='#FF3300'>" + Money.ToString("0.00") + "</font>";
			}
			if (Money < 0m)
			{
				return "<font color='#00FF00'>" + Money.ToString("0.00") + "</font>";
			}
			return "<font color='#000000'>" + Money.ToString("0.00") + "</font>";
		}

		public static string ShowMoneyPT(decimal Money)
		{
			return "<font color='#000000'>" + Money.ToString("0.00") + "</font>";
		}

		public static string ShowMoneyCheck(decimal Money)
		{
			if (Money > 0m)
			{
				return "<font color='#FF3300'>" + Money.ToString("0.00") + "(盈)</font>";
			}
			if (Money < 0m)
			{
				return "<font color='#00FF00'>" + Money.ToString("0.00") + "(亏)</font>";
			}
			return "<font color='#000000'>" + Money.ToString("0.00") + "</font>";
		}

		public static string UserCode(int UserId, string ParentCode)
		{
			return ParentCode + Func.UserCodeLength(UserId);
		}

		public static string UserCodeLength(int UserId)
		{
			string text = "00000000" + UserId.ToString();
			return text.Substring(text.Length - 8);
		}
	}
}
