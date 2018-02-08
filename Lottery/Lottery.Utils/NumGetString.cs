using System;

namespace Lottery.Utils
{
	public class NumGetString
	{
		public static string NumGetStr(double Num)
		{
			bool flag = false;
			bool flag2 = true;
			string text = "";
			string text2 = "";
			Num = Math.Round(Num, 2);
			if (Num < 0.0)
			{
				return "不转换欠条";
			}
			if (Num > 9999999999999.99)
			{
				return "很难想象谁会有这么多钱！";
			}
			if (Num == 0.0)
			{
				return NumGetString.Ls_ShZ[0];
			}
			if (Num < 1.0)
			{
				flag2 = false;
			}
			string text3 = Num.ToString();
			string text4 = text3;
			if (text4.Contains("."))
			{
				text4 = text3.Substring(0, text3.IndexOf("."));
				text = text3.Substring(text3.IndexOf(".") + 1, text3.Length - text3.IndexOf(".") - 1);
				flag = true;
			}
			if (text == "" || int.Parse(text) <= 0)
			{
				flag = false;
			}
			if (flag2)
			{
				text4 = NumGetString.Reversion_Str(text4);
				for (int i = 0; i < text4.Length; i++)
				{
					string s = text4.Substring(i, 1);
					if (int.Parse(s) != 0)
					{
						text2 = NumGetString.Ls_ShZ[int.Parse(s)] + NumGetString.Ls_DW_Zh[i] + text2;
					}
					else if (i == 0 || i == 4 || i == 8)
					{
						if (text4.Length <= 8 || i != 4)
						{
							text2 = NumGetString.Ls_DW_Zh[i] + text2;
						}
					}
					else if (int.Parse(text4.Substring(i - 1, 1)) != 0)
					{
						text2 = NumGetString.Ls_ShZ[int.Parse(s)] + text2;
					}
				}
				if (!flag)
				{
					return text2 + "整";
				}
			}
			for (int j = 0; j < text.Length; j++)
			{
				string s = text.Substring(j, 1);
				if (int.Parse(s) != 0)
				{
					text2 = text2 + NumGetString.Ls_ShZ[int.Parse(s)] + NumGetString.Ls_DW_X[j];
				}
				else if (j != 1 && flag2)
				{
					text2 += NumGetString.Ls_ShZ[int.Parse(s)];
				}
			}
			return text2;
		}

		public static string LowercaseGetCap(string NumStr, bool Dw)
		{
			string text = "";
			if (NumStr == string.Empty)
			{
				return string.Empty;
			}
			if (Dw)
			{
				NumStr = NumGetString.Reversion_Str(NumStr);
			}
			string result;
			try
			{
				for (int i = 0; i < NumStr.Length; i++)
				{
					string text2 = NumStr.Substring(i, 1);
					if (Dw)
					{
						if (int.Parse(text2) != 0)
						{
							text = NumGetString.Ls_ShZ[int.Parse(text2)] + NumGetString.Num_DW[i] + text;
						}
						else if (i == 0 || i == 4 || i == 8)
						{
							if (text2.Length <= 8 || i != 4)
							{
								text = NumGetString.Num_DW[i] + text;
							}
						}
						else if (int.Parse(NumStr.Substring(i - 1, 1)) != 0)
						{
							text = NumGetString.Ls_ShZ[int.Parse(text2)] + text;
						}
					}
					else
					{
						text += NumGetString.Ls_ShZ[int.Parse(text2)];
					}
				}
				result = text;
			}
			catch (Exception ex)
			{
				result = "转换错误！" + ex.Message;
			}
			return result;
		}

		private static string Reversion_Str(string Rstr)
		{
			char[] array = Rstr.ToCharArray();
			Array.Reverse(array);
			return new string(array);
		}

		private static string[] Ls_ShZ = new string[]
		{
			"零",
			"壹",
			"贰",
			"叁",
			"肆",
			"伍",
			"陆",
			"柒",
			"捌",
			"玖",
			"拾"
		};

		private static string[] Ls_DW_Zh = new string[]
		{
			"元",
			"拾",
			"佰",
			"仟",
			"万",
			"拾",
			"佰",
			"仟",
			"亿",
			"拾",
			"佰",
			"仟",
			"万"
		};

		private static string[] Num_DW = new string[]
		{
			"",
			"拾",
			"佰",
			"仟",
			"万",
			"拾",
			"佰",
			"仟",
			"亿",
			"拾",
			"佰",
			"仟",
			"万"
		};

		private static string[] Ls_DW_X = new string[]
		{
			"角",
			"分"
		};
	}
}
