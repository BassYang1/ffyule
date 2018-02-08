using System;
using System.Collections.Specialized;

namespace Lottery.Utils
{
	public class SqlHelp
	{
		public static string GetSql1(string SelectFields, string TblName, int TotalCount, int PageSize, int PageIndex, NameValueCollection Order, string whereStr)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			if (Order.Count > 0)
			{
				string[] allKeys = Order.AllKeys;
				string[] array = allKeys;
				for (int i = 0; i < array.Length; i++)
				{
					string text4 = array[i];
					string text5 = "asc";
					if (Order[text4].ToString() == "asc")
					{
						text5 = "desc";
					}
					int startIndex = text4.IndexOf(".") + 1;
					string text6 = text4.Substring(startIndex);
					text = string.Concat(new string[]
					{
						text,
						text4,
						" ",
						Order[text4],
						","
					});
					text2 = string.Concat(new string[]
					{
						text2,
						text6,
						" ",
						text5,
						","
					});
					text3 = string.Concat(new string[]
					{
						text3,
						text6,
						" ",
						Order[text4],
						","
					});
				}
				text = text.Substring(0, text.Length - 1);
				text2 = text2.Substring(0, text2.Length - 1);
				text3 = text3.Substring(0, text3.Length - 1);
			}
			string text7;
			if (TotalCount > 0 && TotalCount % PageSize > 0 && PageIndex > TotalCount / PageSize)
			{
				text7 = "select * from ( select top {5} {0} from {1} ";
				if (whereStr != "")
				{
					text7 += " where {2} ";
				}
				if (text != "")
				{
					text7 += " order by {4})  as tmp order by {3}";
				}
				text7 = string.Format(text7, new object[]
				{
					SelectFields,
					TblName,
					whereStr,
					text,
					text2,
					TotalCount % PageSize
				});
			}
			else
			{
				text7 = "select * from ( select top {7} * from ( select top {6} {0} from {1} ";
				if (whereStr != "")
				{
					text7 += " where {2} ";
				}
				if (text != "")
				{
					text7 += " order by {3} ) as tmp order by {4} ) as tmp2 order by {5} ";
				}
				text7 = string.Format(text7, new object[]
				{
					SelectFields,
					TblName,
					whereStr,
					text,
					text2,
					text3,
					PageIndex * PageSize,
					PageSize
				});
			}
			return text7;
		}

		public static string GetSql0(string SelectFields, string TblName, string FldName, int PageSize, int PageIndex, string OrderType, string whereStr)
		{
			string text;
			string text2;
			if (OrderType.ToUpper() == "ASC")
			{
				text = "> (SELECT MAX(" + FldName + ")";
				text2 = " ORDER BY " + FldName + " ASC";
			}
			else
			{
				text = "< (SELECT MIN(" + FldName + ")";
				text2 = " ORDER BY " + FldName + " DESC";
			}
			PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
			PageIndex = ((PageIndex == 0) ? 1 : PageIndex);
			string text3;
			if (PageIndex == 1)
			{
				text = "";
				if (whereStr != "")
				{
					text = " Where " + whereStr;
				}
				text3 = string.Concat(new object[]
				{
					"SELECT TOP ",
					PageSize,
					" ",
					SelectFields,
					" From ",
					TblName,
					" with(nolock) ",
					text,
					text2
				});
			}
			else
			{
				text3 = string.Concat(new object[]
				{
					"SELECT TOP ",
					PageSize,
					" ",
					SelectFields,
					" From ",
					TblName,
					" with(nolock)  WHERE ",
					FldName,
					text,
					" From (SELECT TOP ",
					(PageIndex - 1) * PageSize,
					" ",
					FldName,
					" From ",
					TblName,
					" with(nolock) "
				});
				if (whereStr != "")
				{
					text3 = text3 + " Where " + whereStr;
				}
				text3 = text3 + text2 + ") As Tbltemp)";
				if (whereStr != "")
				{
					text3 = text3 + " And " + whereStr;
				}
				text3 += text2;
			}
			return text3;
		}

		public static string GetSql0(string SelectFields, string TblName, string FldName, int PageSize, int PageIndex, string OrderType, string whereStr, string groupStr)
		{
			string text;
			string text2;
			if (OrderType.ToUpper() == "ASC")
			{
				text = "> (SELECT MAX(" + FldName + ")";
				text2 = " ORDER BY " + FldName + " ASC";
			}
			else
			{
				text = "< (SELECT MIN(" + FldName + ")";
				text2 = " ORDER BY " + FldName + " DESC";
			}
			PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
			PageIndex = ((PageIndex == 0) ? 1 : PageIndex);
			string text3;
			if (PageIndex == 1)
			{
				text = "";
				if (whereStr != "")
				{
					text = " Where " + whereStr;
				}
				text3 = string.Concat(new object[]
				{
					"SELECT TOP ",
					PageSize,
					" ",
					SelectFields,
					" From ",
					TblName,
					" with(nolock) ",
					text,
					" group by ",
					groupStr,
					" ",
					text2
				});
			}
			else
			{
				text3 = string.Concat(new object[]
				{
					"SELECT TOP ",
					PageSize,
					" ",
					SelectFields,
					" From ",
					TblName,
					" with(nolock) WHERE ",
					FldName,
					text,
					" From (SELECT TOP ",
					(PageIndex - 1) * PageSize,
					" ",
					FldName,
					" From ",
					TblName,
					" with(nolock) "
				});
				if (whereStr != "")
				{
					text3 = text3 + " Where " + whereStr;
				}
				string text4 = text3;
				text3 = string.Concat(new string[]
				{
					text4,
					" group by ",
					groupStr,
					" ",
					text2,
					") As Tbltemp)"
				});
				if (whereStr != "")
				{
					text3 = text3 + " And " + whereStr;
				}
				string text5 = text3;
				text3 = string.Concat(new string[]
				{
					text5,
					" group by ",
					groupStr,
					" ",
					text2
				});
			}
			return text3;
		}

		public static string GetSql0(string SelectFields, string TblNameA, string TblNameB, string FldName, int PageSize, int PageIndex, string OrderType, string joinStr, string whereStr1, string whereStr2)
		{
			string text;
			string text2;
			string str;
			if (OrderType.ToUpper() == "ASC")
			{
				text = "> (SELECT MAX(" + FldName + ")";
				text2 = " ORDER BY A." + FldName + " ASC";
				str = " ORDER BY " + FldName + " ASC";
			}
			else
			{
				text = "< (SELECT MIN(" + FldName + ")";
				text2 = " ORDER BY A." + FldName + " DESC";
				str = " ORDER BY " + FldName + " DESC";
			}
			PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
			PageIndex = ((PageIndex == 0) ? 1 : PageIndex);
			string text3;
			if (PageIndex == 1)
			{
				text = "";
				if (whereStr1 != "")
				{
					text = " WHERE " + whereStr1;
				}
				text3 = string.Concat(new object[]
				{
					"SELECT TOP ",
					PageSize,
					" ",
					SelectFields,
					" FROM [",
					TblNameA,
					"] A  with(nolock) LEFT JOIN [",
					TblNameB,
					"] B  with(nolock) on ",
					joinStr,
					" ",
					text,
					text2
				});
			}
			else
			{
				text3 = string.Concat(new object[]
				{
					"SELECT TOP ",
					PageSize,
					" ",
					SelectFields,
					" FROM [",
					TblNameA,
					"] A  with(nolock) LEFT JOIN [",
					TblNameB,
					"] B  with(nolock) on ",
					joinStr,
					" WHERE A.",
					FldName,
					text,
					" From (SELECT TOP ",
					(PageIndex - 1) * PageSize,
					" ",
					FldName,
					" From [",
					TblNameA,
					"] "
				});
				if (whereStr2 != "")
				{
					text3 = text3 + " Where " + whereStr2;
				}
				text3 = text3 + str + ") As Tbltemp)";
				if (whereStr1 != "")
				{
					text3 = text3 + " And " + whereStr1;
				}
				text3 += text2;
			}
			return text3;
		}

		public static string GetSql0(string SelectFields, string TblNameA, string TblNameB, string FldName, int PageSize, int PageIndex, string OrderType, string joinStr, string whereStr1, string whereStr2, string groupStr)
		{
			string text;
			string text2;
			string str;
			if (OrderType.ToUpper() == "ASC")
			{
				text = "> (SELECT MAX(" + FldName + ")";
				text2 = " ORDER BY A." + FldName + " ASC";
				str = " ORDER BY " + FldName + " ASC";
			}
			else
			{
				text = "< (SELECT MIN(" + FldName + ")";
				text2 = " ORDER BY A." + FldName + " DESC";
				str = " ORDER BY " + FldName + " DESC";
			}
			PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
			PageIndex = ((PageIndex == 0) ? 1 : PageIndex);
			string text3;
			if (PageIndex == 1)
			{
				text = "";
				if (whereStr1 != "")
				{
					text = " WHERE " + whereStr1;
				}
				text3 = string.Concat(new object[]
				{
					"SELECT TOP ",
					PageSize,
					" ",
					SelectFields,
					" FROM [",
					TblNameA,
					"] A  with(nolock) LEFT JOIN [",
					TblNameB,
					"] B  with(nolock) on ",
					joinStr,
					" ",
					text,
					" group by ",
					groupStr,
					" ",
					text2
				});
			}
			else
			{
				text3 = string.Concat(new object[]
				{
					"SELECT TOP ",
					PageSize,
					" ",
					SelectFields,
					" FROM [",
					TblNameA,
					"] A  with(nolock) LEFT JOIN [",
					TblNameB,
					"] B  with(nolock) on ",
					joinStr,
					" WHERE A.",
					FldName,
					text,
					" From (SELECT TOP ",
					(PageIndex - 1) * PageSize,
					" ",
					FldName,
					" From [",
					TblNameA,
					"] "
				});
				if (whereStr2 != "")
				{
					text3 = text3 + " Where " + whereStr2;
				}
				text3 = text3 + str + ") As Tbltemp)";
				if (whereStr1 != "")
				{
					text3 = text3 + " And " + whereStr1;
				}
				string text4 = text3;
				text3 = string.Concat(new string[]
				{
					text4,
					" group by ",
					groupStr,
					" ",
					text2
				});
			}
			return text3;
		}

		public static string GetSqlRow(string SelectFields, string TblName, string FldName, int PageSize, int PageIndex, string OrderType, string whereStr)
		{
			PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
			PageIndex = ((PageIndex == 0) ? 1 : PageIndex);
			return string.Concat(new object[]
			{
				"SELECT TOP (",
				PageSize,
				") * FROM (SELECT top(1000) ROW_NUMBER() OVER (ORDER BY ",
				FldName,
				" ",
				OrderType,
				") AS rowNember,",
				SelectFields,
				" FROM ",
				TblName,
				" where ",
				whereStr,
				" order by rowNember) as t WHERE t.rowNember > (",
				PageSize,
				"*(",
				PageIndex,
				"-1)) order by rowNember;"
			});
		}

		public static string GetSqlRow(string SelectFields, string TblName, string FldName, int PageSize, int PageIndex, string OrderType, string whereStr, string groupStr)
		{
			PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
			PageIndex = ((PageIndex == 0) ? 1 : PageIndex);
			return string.Concat(new object[]
			{
				"SELECT TOP (",
				PageSize,
				") * FROM (SELECT top(1000) ROW_NUMBER() OVER (ORDER BY ",
				FldName,
				" ",
				OrderType,
				") AS rowNember,",
				SelectFields,
				" FROM ",
				TblName,
				" where ",
				whereStr,
				" group by ",
				groupStr,
				" order by rowNember) as t WHERE t.rowNember > (",
				PageSize,
				"*(",
				PageIndex,
				"-1)) order by rowNember;"
			});
		}
	}
}
