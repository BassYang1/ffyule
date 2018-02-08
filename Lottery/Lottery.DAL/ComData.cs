using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class ComData
	{
		public DbOperHandler Doh()
		{
			return new SqlDbOperHandler(new SqlConnection(ComData.connectionString));
		}

		public string GetJsonResult(int result, string Message)
		{
			return string.Concat(new object[]
			{
				"[{\"result\":\"",
				result,
				"\",\"message\":\"",
				Message,
				"\"}]"
			});
		}

		public string JsonResult(int result, string Message)
		{
			return string.Concat(new object[]
			{
				"{\"result\":\"",
				result,
				"\",\"message\":\"",
				Message,
				"\"}"
			});
		}

		public string AddZero(int Num, int Len)
		{
			string text = "";
			for (int i = 1; i <= Len; i++)
			{
				text += "0";
			}
			text += Num.ToString();
			return text.Substring(text.Length - Len);
		}

		public static DataTable LotteryTime
		{
			get;
			set;
		}

		protected string ConverTableToJSON(DataTable table)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			if (table != null && table.Rows.Count > 0)
			{
				for (int i = 0; i < table.Rows.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("{");
					for (int j = 0; j < table.Columns.Count; j++)
					{
						if (!string.IsNullOrEmpty(string.Concat(table.Rows[i][j])))
						{
							if (j > 0)
							{
								stringBuilder.Append(",");
							}
							stringBuilder.Append("\"" + table.Columns[j].ColumnName.ToLower() + "\":\"");
							stringBuilder.Append(table.Rows[i][j].ToString() + "\"");
						}
					}
					stringBuilder.Append("}");
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		protected string ConverTableToJSON2(DataTable table)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			if (table != null && table.Rows.Count > 0)
			{
				for (int i = 0; i < table.Rows.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("{");
					for (int j = 0; j < table.Columns.Count; j++)
					{
						if (!string.IsNullOrEmpty(string.Concat(table.Rows[i][j])))
						{
							if (j > 0)
							{
								stringBuilder.Append(",");
							}
							stringBuilder.Append("\"" + table.Columns[j].ColumnName.ToLower() + "\":\"");
							stringBuilder.Append(ComData.NoHTML(table.Rows[i][j].ToString()) + "\"");
						}
					}
					stringBuilder.Append("}");
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		protected string ConverTableToLotteryXML(DataTable dt)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			stringBuilder.Append("<xml rows=\"5\" code=\"ssc\" remain=\"10hrs\">");
			if (dt != null && dt.Rows.Count > 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"<row expect=\"",
						dt.Rows[i]["Title"],
						"\" opencode=\"",
						dt.Rows[i]["NumberAll"],
						"\" opentime=\"",
						dt.Rows[i]["STime"],
						"\" />"
					}));
				}
			}
			stringBuilder.Append("</xml>");
			return stringBuilder.ToString();
		}

		protected string ConverTableToXML(DataTable dt1, DataTable dt2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			stringBuilder.Append("<lotterys>");
			if (dt1 != null && dt1.Rows.Count > 0)
			{
				for (int i = 0; i < dt1.Rows.Count; i++)
				{
					stringBuilder.Append("<lottery>");
					for (int j = 0; j < dt1.Columns.Count; j++)
					{
						if (!string.IsNullOrEmpty(string.Concat(dt1.Rows[i][j])))
						{
							stringBuilder.Append("<" + dt1.Columns[j].ColumnName.ToLower() + ">");
							stringBuilder.Append(dt1.Rows[i][j].ToString().Replace("\n", "").Replace(" ", ""));
							stringBuilder.Append("</" + dt1.Columns[j].ColumnName.ToLower() + ">");
						}
					}
					DataRow[] array = dt2.Select(string.Concat(new object[]
					{
						"Radio=" + dt1.Rows[i]["Id"].ToString()
					}), "Id asc");
					stringBuilder.Append("<plays>");
					for (int k = 0; k < array.Length; k++)
					{
						stringBuilder.Append("<play>");
						for (int l = 0; l < array[k].ItemArray.Length; l++)
						{
							if (!string.IsNullOrEmpty(string.Concat(array[k].ItemArray[l])))
							{
								stringBuilder.Append("<" + dt2.Columns[l].ColumnName.ToLower() + ">");
								stringBuilder.Append(array[k].ItemArray[l].ToString().Replace("\n", "").Replace(" ", ""));
								stringBuilder.Append("</" + dt2.Columns[l].ColumnName.ToLower() + ">");
							}
						}
						stringBuilder.Append("</play>");
					}
					stringBuilder.Append("</plays>");
					stringBuilder.Append("</lottery>");
				}
			}
			stringBuilder.Append("</lotterys>");
			return stringBuilder.ToString();
		}

		protected string ConverTableToXML(DataTable dt)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			stringBuilder.Append("<lotterys>");
			if (dt != null && dt.Rows.Count > 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					stringBuilder.Append("<lottery>");
					for (int j = 0; j < dt.Columns.Count; j++)
					{
						if (!string.IsNullOrEmpty(string.Concat(dt.Rows[i][j])))
						{
							stringBuilder.Append("<" + dt.Columns[j].ColumnName.ToLower() + ">");
							stringBuilder.Append(dt.Rows[i][j].ToString().Replace("\n", "").Replace(" ", ""));
							stringBuilder.Append("</" + dt.Columns[j].ColumnName.ToLower() + ">");
						}
					}
					stringBuilder.Append("</lottery>");
				}
			}
			stringBuilder.Append("</lotterys>");
			return stringBuilder.ToString();
		}

		protected string ConvertDataTableToXML(DataTable xmlDS)
		{
			XmlTextWriter xmlTextWriter = null;
			string result;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Default);
				xmlDS.WriteXml(xmlTextWriter);
				int num = (int)memoryStream.Length;
				byte[] array = new byte[num];
				memoryStream.Seek(0L, SeekOrigin.Begin);
				memoryStream.Read(array, 0, num);
				UTF8Encoding uTF8Encoding = new UTF8Encoding();
				result = uTF8Encoding.GetString(array).Trim();
			}
			catch
			{
				result = string.Empty;
			}
			finally
			{
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Close();
				}
			}
			return result;
		}

		protected string CDataToXml(DataTable dt)
		{
			if (dt != null)
			{
				MemoryStream memoryStream = null;
				XmlTextWriter xmlTextWriter = null;
				try
				{
					memoryStream = new MemoryStream();
					xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Unicode);
					dt.WriteXml(xmlTextWriter);
					int num = (int)memoryStream.Length;
					byte[] array = new byte[num];
					memoryStream.Seek(0L, SeekOrigin.Begin);
					memoryStream.Read(array, 0, num);
					UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
					return unicodeEncoding.GetString(array).Trim();
				}
				catch (Exception ex)
				{
					throw ex;
				}
				finally
				{
					if (xmlTextWriter != null)
					{
						xmlTextWriter.Close();
						memoryStream.Close();
						memoryStream.Dispose();
					}
				}
			}
			return "";
		}

		private DataSet ConvertXMLToDataSet(string xmlData)
		{
			XmlTextReader xmlTextReader = null;
			DataSet result;
			try
			{
				DataSet dataSet = new DataSet();
				StringReader input = new StringReader(xmlData);
				xmlTextReader = new XmlTextReader(input);
				dataSet.ReadXml(xmlTextReader);
				result = dataSet;
			}
			catch (Exception ex)
			{
				string arg_2C_0 = ex.Message;
				result = null;
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
			return result;
		}

		public static string NoHTML(string Htmlstring)
		{
			Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
			Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
			return Htmlstring;
		}

		public static string connectionString = Const.ConnectionString;
	}
}
