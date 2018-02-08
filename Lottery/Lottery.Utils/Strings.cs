using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
	public static class Strings
	{
		public static string EncryptStr(string rs)
		{
			byte[] array = new byte[rs.Length];
			for (int i = 0; i <= rs.Length - 1; i++)
			{
				array[i] = (byte)((byte)rs[i] + 1);
			}
			rs = "";
			for (int j = array.Length - 1; j >= 0; j--)
			{
				string arg_48_0 = rs;
				char c = (char)array[j];
				rs = arg_48_0 + c.ToString();
			}
			return rs;
		}

		public static string DecryptStr(string rs)
		{
			byte[] array = new byte[rs.Length];
			for (int i = 0; i <= rs.Length - 1; i++)
			{
				array[i] = (byte)((byte)rs[i] - 1);
			}
			rs = "";
			for (int j = array.Length - 1; j >= 0; j--)
			{
				string arg_48_0 = rs;
				char c = (char)array[j];
				rs = arg_48_0 + c.ToString();
			}
			return rs;
		}

		public static string Escape(string str)
		{
			if (str == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				if (char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == '/' || c == '\\' || c == '.')
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append(Uri.HexEscape(c));
				}
			}
			return stringBuilder.ToString();
		}

		public static string UnEscape(string str)
		{
			if (str == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			int num = 0;
			while (num != length)
			{
				if (Uri.IsHexEncoding(str, num))
				{
					stringBuilder.Append(Uri.HexUnescape(str, ref num));
				}
				else
				{
					stringBuilder.Append(str[num++]);
				}
			}
			return stringBuilder.ToString();
		}

		public static string PadLeft(string inputString)
		{
			return "," + inputString + ",";
		}

		public static string Left(string inputString, int len)
		{
			if (inputString.Length < len)
			{
				return inputString;
			}
			return inputString.Substring(0, len);
		}

		public static string Right(string inputString, int len)
		{
			if (inputString.Length < len)
			{
				return inputString;
			}
			return inputString.Substring(inputString.Length - len, len);
		}

		public static string CutString(string inputString, int len)
		{
			ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
			int num = 0;
			string text = "";
			byte[] bytes = aSCIIEncoding.GetBytes(inputString);
			for (int i = 0; i < bytes.Length; i++)
			{
				if (bytes[i] == 63)
				{
					num += 2;
				}
				else
				{
					num++;
				}
				try
				{
					text += inputString.Substring(i, 1);
				}
				catch
				{
					break;
				}
				if (num >= len)
				{
					break;
				}
			}
			return text;
		}

		public static string RemoveSpaceStr(string original)
		{
			return Regex.Replace(original, "\\s{2,}", " ");
		}

		public static string ToSummary(string Htmlstring)
		{
			string original = Strings.NoHTML(Htmlstring);
			return Strings.RemoveSpaceStr(original).Replace("[Jumbot_PageBreak]", " ");
		}

		public static string NoHTML(string Htmlstring)
		{
			Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&ldquo;", "“", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&rdquo;", "”", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
			Htmlstring = Htmlstring.Replace("<", "&lt;");
			Htmlstring = Htmlstring.Replace(">", "&gt;");
			return Htmlstring;
		}

		public static string ReplaceEx(string original, string pattern, string replacement)
		{
			int length;
			int num = length = 0;
			string text = original.ToUpper();
			string value = pattern.ToUpper();
			int val = original.Length / pattern.Length * (replacement.Length - pattern.Length);
			char[] array = new char[original.Length + Math.Max(0, val)];
			int num2;
			while ((num2 = text.IndexOf(value, num)) != -1)
			{
				for (int i = num; i < num2; i++)
				{
					array[length++] = original[i];
				}
				for (int j = 0; j < replacement.Length; j++)
				{
					array[length++] = replacement[j];
				}
				num = num2 + pattern.Length;
			}
			if (num == 0)
			{
				return original;
			}
			for (int k = num; k < original.Length; k++)
			{
				array[length++] = original[k];
			}
			return new string(array, 0, length);
		}

		public static string HtmlEncode(string theString)
		{
			theString = theString.Replace(">", "&gt;");
			theString = theString.Replace("<", "&lt;");
			theString = theString.Replace("  ", " &nbsp;");
			theString = theString.Replace("\"", "&quot;");
			theString = theString.Replace("'", "&#39;");
			theString = theString.Replace("\r\n", "<br/> ");
			return theString;
		}

		public static string HtmlDecode(string theString)
		{
			theString = theString.Replace("&gt;", ">");
			theString = theString.Replace("&lt;", "<");
			theString = theString.Replace(" &nbsp;", "  ");
			theString = theString.Replace("&quot;", "\"");
			theString = theString.Replace("&#39;", "'");
			theString = theString.Replace("<br/> ", "\r\n");
			theString = theString.Replace("&mdash;", "—");
			return theString;
		}

		public static string ToMoney(double _value)
		{
			return string.Format("{0:F2}", _value);
		}

		public static string ToMoney(string _value)
		{
			return string.Format("{0:F2}", Convert.ToDouble(_value));
		}

		public static string ToMoney(int _value)
		{
			return string.Format("{0:F2}", Convert.ToDouble(_value));
		}

		public static string ToSBC(string input)
		{
			char[] array = input.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == ' ')
				{
					array[i] = '\u3000';
				}
				else if (array[i] < '\u007f')
				{
					array[i] += 'ﻠ';
				}
			}
			return new string(array);
		}

		public static string ToDBC(string input)
		{
			char[] array = input.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == '\u3000')
				{
					array[i] = ' ';
				}
				else if (array[i] > '＀' && array[i] < '｟')
				{
					array[i] -= 'ﻠ';
				}
			}
			return new string(array);
		}

		public static string SimpleLineSummary(string theString)
		{
			theString = theString.Replace("&gt;", "");
			theString = theString.Replace("&lt;", "");
			theString = theString.Replace(" &nbsp;", "  ");
			theString = theString.Replace("&quot;", "\"");
			theString = theString.Replace("&#39;", "'");
			theString = theString.Replace("<br/> ", "\r\n");
			theString = theString.Replace("\"", "");
			theString = theString.Replace("\t", " ");
			theString = theString.Replace("\r", " ");
			theString = theString.Replace("\n", " ");
			theString = Regex.Replace(theString, "\\s{2,}", " ");
			return theString;
		}

		public static string UBB2HTML(string content)
		{
			content = Regex.Replace(content, "\\[b\\](.+?)\\[/b\\]", "<b>$1</b>", RegexOptions.IgnoreCase);
			content = Regex.Replace(content, "\\[i\\](.+?)\\[/i\\]", "<i>$1</i>", RegexOptions.IgnoreCase);
			content = Regex.Replace(content, "\\[u\\](.+?)\\[/u\\]", "<u>$1</u>", RegexOptions.IgnoreCase);
			content = Regex.Replace(content, "\\[p\\](.+?)\\[/p\\]", "<p>$1</p>", RegexOptions.IgnoreCase);
			content = Regex.Replace(content, "\\[align=left\\](.+?)\\[/align\\]", "<align='left'>$1</align>", RegexOptions.IgnoreCase);
			content = Regex.Replace(content, "\\[align=center\\](.+?)\\[/align\\]", "<align='center'>$1</align>", RegexOptions.IgnoreCase);
			content = Regex.Replace(content, "\\[align=right\\](.+?)\\[/align\\]", "<align='right'>$1</align>", RegexOptions.IgnoreCase);
			content = Regex.Replace(content, "\\[url=(?<url>.+?)]\\[/url]", "<a href='${url}' target=_blank>${url}</a>", RegexOptions.IgnoreCase);
			content = Regex.Replace(content, "\\[url=(?<url>.+?)](?<name>.+?)\\[/url]", "<a href='${url}' target=_blank>${name}</a>", RegexOptions.IgnoreCase);
			content = Regex.Replace(content, "\\[quote](?<text>.+?)\\[/quote]", "<div class=\"quote\">${text}</div>", RegexOptions.IgnoreCase);
			content = Regex.Replace(content, "\\[img](?<img>.+?)\\[/img]", "<a href='${img}' target=_blank><img src='${img}' alt=''/></a>", RegexOptions.IgnoreCase);
			return content;
		}

		public static string Html2Js(string source)
		{
			return string.Format("document.write(\"{0}\");", string.Join("\");\r\ndocument.write(\"", source.Replace("\\", "\\\\").Replace("/", "\\/").Replace("'", "\\'").Replace("\"", "\\\"").Split(new char[]
			{
				'\r',
				'\n'
			}, StringSplitOptions.RemoveEmptyEntries)));
		}

		public static string Html2JsStr(string source)
		{
			return string.Format("{0}", string.Join(" ", source.Replace("\\", "\\\\").Replace("/", "\\/").Replace("'", "\\'").Replace("\"", "\\\"").Replace("\t", "").Split(new char[]
			{
				'\r',
				'\n'
			}, StringSplitOptions.RemoveEmptyEntries)));
		}

		public static string FilterSymbol(string theString)
		{
			string[] array = new string[]
			{
				"'",
				"\"",
				"\r",
				"\n",
				"<",
				">",
				"(",
				")",
				"{",
				"}",
				"%",
				"?",
				",",
				".",
				"=",
				"+",
				"-",
				"_",
				";",
				"|",
				"[",
				"]",
				"&",
				"/"
			};
			for (int i = 0; i < array.Length; i++)
			{
				theString = theString.Replace(array[i], string.Empty);
			}
			return theString;
		}

		public static string DelSymbol(string theString)
		{
			string[] array = new string[]
			{
				"'",
				"\"",
				"\r",
				"\n",
				"<",
				">",
				"%",
				"?",
				"=",
				"-",
				"_",
				"|",
				"[",
				"]",
				"&",
				"/"
			};
			for (int i = 0; i < array.Length; i++)
			{
				theString = theString.Replace(array[i], string.Empty);
			}
			return theString;
		}

		public static string SafetyTitle(string theString)
		{
			string[] array = new string[]
			{
				"'",
				";",
				"\"",
				"\r",
				"\n"
			};
			for (int i = 0; i < array.Length; i++)
			{
				theString = theString.Replace(array[i], string.Empty);
			}
			return theString;
		}

		public static string SafetyQueryS(string theString)
		{
			string[] array = new string[]
			{
				"'",
				";",
				"\"",
				"\r",
				"\n",
				"<",
				">"
			};
			for (int i = 0; i < array.Length; i++)
			{
				theString = theString.Replace(array[i], string.Empty);
			}
			return theString;
		}

		public static string SafetyLikeValue(string theString)
		{
			string[] array = new string[]
			{
				"'",
				";",
				"\"",
				"\r",
				"\n",
				"%",
				"-",
				"[",
				"]",
				"(",
				")"
			};
			for (int i = 0; i < array.Length; i++)
			{
				theString = theString.Replace(array[i], string.Empty);
			}
			return theString;
		}

		public static string[] GetRegValue(string HtmlCode, string RegexString, string GroupKey, bool RightToLeft)
		{
			Regex regex;
			if (RightToLeft)
			{
				regex = new Regex(RegexString, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.RightToLeft);
			}
			else
			{
				regex = new Regex(RegexString, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			}
			MatchCollection matchCollection = regex.Matches(HtmlCode);
			string[] array = new string[matchCollection.Count];
			for (int i = 0; i < matchCollection.Count; i++)
			{
				array[i] = matchCollection[i].Groups[GroupKey].Value;
			}
			return array;
		}

		public static string AttributeValue(string HtmlTag, string AttributeName)
		{
			string text = HtmlTag.StartsWith(AttributeName + "=") ? "(.{0})" : "([\"'\\s\\|:]{1})";
			string regexString = string.Concat(new string[]
			{
				text,
				AttributeName,
				"=(\"|')(?<",
				AttributeName,
				">.*?[^\\\\]{1})(\\2)"
			});
			string[] regValue = Strings.GetRegValue(HtmlTag, regexString, AttributeName, false);
			if (regValue.Length > 0)
			{
				return regValue[0].ToString();
			}
			return "";
		}

		public static string DateStringFromNow(DateTime dt)
		{
			TimeSpan timeSpan = DateTime.Now - dt;
			if (timeSpan.TotalDays > 60.0)
			{
				return dt.ToShortDateString();
			}
			if (timeSpan.TotalDays > 30.0)
			{
				return "1个月前";
			}
			if (timeSpan.TotalDays > 14.0)
			{
				return "2周前";
			}
			if (timeSpan.TotalDays > 7.0)
			{
				return "1周前";
			}
			if (timeSpan.TotalDays > 1.0)
			{
				return string.Format("{0}天前", (int)Math.Floor(timeSpan.TotalDays));
			}
			if (timeSpan.TotalHours > 1.0)
			{
				return string.Format("{0}小时前", (int)Math.Floor(timeSpan.TotalHours));
			}
			if (timeSpan.TotalMinutes > 1.0)
			{
				return string.Format("{0}分钟前", (int)Math.Floor(timeSpan.TotalMinutes));
			}
			if (timeSpan.TotalSeconds >= 1.0)
			{
				return string.Format("{0}秒前", (int)Math.Floor(timeSpan.TotalSeconds));
			}
			return "1秒前";
		}

		public static ArrayList GetHtmls(string sHtml, string strStart, string strEnd)
		{
			return Strings.getArray(sHtml, strStart, strEnd);
		}

		public static ArrayList GetHtmls(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
		{
			return Strings.getArray(sHtml, strStart, strEnd, getStart, getEnd);
		}

		public static string GetHtml(string sHtml, string strStart, string strEnd)
		{
			return Strings.getResult(sHtml, strStart, strEnd);
		}

		public static string GetHtml(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
		{
			return Strings.getResult(sHtml, strStart, strEnd, getStart, getEnd);
		}

		private static string enReplaceStr(string str)
		{
			if (str == null || str == "")
			{
				return "superstring_空值";
			}
			return str.Replace("\r", "superstring_回车").Replace("\n", "superstring_换行").Replace("\"", "superstring_双引").Replace("\\", "superstring_反斜");
		}

		private static string deReplaceStr(string str)
		{
			return str.Replace("superstring_回车", "\r").Replace("superstring_换行", "\n").Replace("superstring_双引", "\"").Replace("superstring_反斜", "\\").Replace("superstring_空值", "").Replace("superstring_空头", "").Replace("superstring_空尾", "");
		}

		private static ArrayList getArray(string sHtml, string strStart, string strEnd)
		{
			return Strings.getArray(sHtml, strStart, strEnd, false, false);
		}

		private static ArrayList getArray(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
		{
			if (strEnd == null || strEnd == "")
			{
				sHtml += "superstring_空尾";
				strEnd = "superstring_空尾";
			}
			if (strStart == null || strStart == "")
			{
				sHtml = "superstring_空头" + sHtml;
				strStart = "superstring_空头";
			}
			ArrayList arrayList = new ArrayList();
			Regex regex = new Regex(Strings.RegexStr(Strings.enReplaceStr(strStart), Strings.enReplaceStr(strEnd)), RegexOptions.Multiline | RegexOptions.Singleline);
			MatchCollection matchCollection = regex.Matches(Strings.enReplaceStr(sHtml));
			for (int i = 0; i < matchCollection.Count; i++)
			{
				string text = Strings.deReplaceStr(matchCollection[i].Value);
				if (getStart)
				{
					text = strStart + text;
				}
				if (getEnd)
				{
					text += strEnd;
				}
				arrayList.Add(text);
			}
			return arrayList;
		}

		private static string getResult(string sHtml, string strStart, string strEnd)
		{
			return Strings.getResult(sHtml, strStart, strEnd, false, false);
		}

		private static string getResult(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
		{
			if (strEnd == null || strEnd == "")
			{
				sHtml += "superstring_空尾";
				strEnd = "superstring_空尾";
			}
			if (strStart == null || strStart == "")
			{
				sHtml = "superstring_空头" + sHtml;
				strStart = "superstring_空头";
			}
			Regex regex = new Regex(Strings.RegexStr(Strings.enReplaceStr(strStart), Strings.enReplaceStr(strEnd)), RegexOptions.Multiline | RegexOptions.Singleline);
			string text = Strings.deReplaceStr(regex.Match(Strings.enReplaceStr(sHtml)).Value);
			if (getStart)
			{
				text = strStart + text;
			}
			if (getEnd)
			{
				text += strEnd;
			}
			return text;
		}

		private static string RegexStr(string strStart, string strEnd)
		{
			string text = strStart;
			string text2 = strEnd;
			for (int i = 0; i < Strings.aryChar.Length; i++)
			{
				text = text.Replace(Strings.aryChar[i], "\\" + Strings.aryChar[i]);
				text2 = text2.Replace(Strings.aryChar[i], "\\" + Strings.aryChar[i]);
			}
			return string.Concat(new string[]
			{
				"(?<=(",
				text,
				"))[.\\s\\S]*?(?=(",
				text2,
				"))"
			});
		}

		public static string[] aryChar = new string[]
		{
			"\\",
			"^",
			"$",
			"{",
			"}",
			"[",
			"]",
			".",
			"(",
			")",
			"*",
			"+",
			"?",
			"!",
			"#",
			"|"
		};
	}
}
