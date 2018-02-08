using System;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
	public class LotterySum
	{
		public static int IsDS(int SumNumber)
		{
			if (SumNumber % 2 == 0)
			{
				return 1;
			}
			return -1;
		}

		public static int IsDX(int SumNumber, int ComNum, int IsDsDx)
		{
			if (SumNumber == ComNum)
			{
				return IsDsDx;
			}
			if (SumNumber > ComNum)
			{
				return 1;
			}
			return -1;
		}

		public static string ShowDsStr(int DS)
		{
			switch (DS)
			{
			case -1:
				return "<font color='#FF6600'>单</font>";
			case 1:
				return "<font color='#33FF00'>双</font>";
			}
			return "";
		}

		public static string ShowDxDsStr(int DX, int DS, int IsShow)
		{
			string text = "";
			if (DX == -1)
			{
				text = "小";
			}
			if (DX == 1)
			{
				text = "大";
			}
			if (DX == 0 && IsShow == 1)
			{
				text = "和";
			}
			if (DS == -1)
			{
				text += " 单";
			}
			if (DS == 1)
			{
				text += " 双";
			}
			return text;
		}

		public static string ShowDxStr(int DX, int IsShow)
		{
			switch (DX)
			{
			case -1:
				return "<font color='#FF6600'>小</font>";
			case 1:
				return "<font color='#33FF00'>大</font>";
			}
			if (IsShow == 1)
			{
				return "<font color='Red'>和</font>";
			}
			return "";
		}

		public static string ShowHappy10Num(string Number)
		{
			string str = "<table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
			str += "<tr>";
			string text = "";
			string[] array = Regex.Split(Number, ",");
			for (int i = 0; i < array.Length; i++)
			{
				if (text == "")
				{
					text = "<td class=\"Sf_Num\">" + array[i] + "</td>";
				}
				else
				{
					text = text + "<td class=\"Sf_Num_s\">&nbsp;</td><td class=\"Sf_Num\">" + array[i] + "</td>";
				}
			}
			return str + text + "</tr></table>";
		}

		public static string ShowHappy8Num(string Number)
		{
			string str = "<table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
			str += "<tr>";
			string text = "";
			string[] array = Regex.Split(Number, ",");
			for (int i = 0; i < array.Length; i++)
			{
				if (text == "")
				{
					text = "<td class=\"KL_Num\">" + array[i] + "</td>";
				}
				else
				{
					text = text + "<td class=\"KL_Num_s\">&nbsp;</td><td class=\"KL_Num\">" + array[i] + "</td>";
				}
			}
			return str + text + "</tr></table>";
		}

		public static int SumNumber(string Num)
		{
			int num = 0;
			string[] array = Num.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				num += Convert.ToInt32(array[i]);
			}
			return num;
		}
	}
}
