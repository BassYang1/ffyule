using System;

namespace Lottery.Utils
{
	public static class Int
	{
		public static int PageCount(int TotalCount, int PageSize)
		{
			if (TotalCount == 0)
			{
				return 1;
			}
			if (TotalCount % PageSize != 0)
			{
				return TotalCount / PageSize + 1;
			}
			return TotalCount / PageSize;
		}

		public static int Max(int int1, int int2)
		{
			if (int1 <= int2)
			{
				return int2;
			}
			return int1;
		}

		public static int Min(int int1, int int2)
		{
			if (int1 >= int2)
			{
				return int2;
			}
			return int1;
		}

		public static int ExactlyDivisible(double x, double y, bool ending)
		{
			double num = x / y;
			if (!ending)
			{
				return Convert.ToInt32(num);
			}
			return Convert.ToInt32(num - x % y / y);
		}
	}
}
