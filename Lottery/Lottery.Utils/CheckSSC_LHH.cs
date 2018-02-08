using System;

namespace Lottery.Utils
{
	public static class CheckSSC_LHH
	{
		public static int P_LHH(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			CheckNumber.Split(new char[]
			{
				'_'
			});
			int num2 = 0;
			int num3 = 0;
			if (Pos == "WQ")
			{
				num2 = Convert.ToInt32(array[0]);
				num3 = Convert.ToInt32(array[1]);
			}
			if (Pos == "WB")
			{
				num2 = Convert.ToInt32(array[0]);
				num3 = Convert.ToInt32(array[2]);
			}
			if (Pos == "WS")
			{
				num2 = Convert.ToInt32(array[0]);
				num3 = Convert.ToInt32(array[3]);
			}
			if (Pos == "WG")
			{
				num2 = Convert.ToInt32(array[0]);
				num3 = Convert.ToInt32(array[4]);
			}
			if (Pos == "QB")
			{
				num2 = Convert.ToInt32(array[1]);
				num3 = Convert.ToInt32(array[2]);
			}
			if (Pos == "QS")
			{
				num2 = Convert.ToInt32(array[1]);
				num3 = Convert.ToInt32(array[3]);
			}
			if (Pos == "QG")
			{
				num2 = Convert.ToInt32(array[1]);
				num3 = Convert.ToInt32(array[4]);
			}
			if (Pos == "BS")
			{
				num2 = Convert.ToInt32(array[2]);
				num3 = Convert.ToInt32(array[3]);
			}
			if (Pos == "BG")
			{
				num2 = Convert.ToInt32(array[2]);
				num3 = Convert.ToInt32(array[4]);
			}
			if (Pos == "SG")
			{
				num2 = Convert.ToInt32(array[3]);
				num3 = Convert.ToInt32(array[4]);
			}
			string value = "";
			if (num2 > num3)
			{
				value = "龙";
			}
			if (num2 == num3)
			{
				value = "和";
			}
			if (num2 < num3)
			{
				value = "虎";
			}
			if (CheckNumber.IndexOf(value) != -1)
			{
				num++;
			}
			return num;
		}
	}
}
