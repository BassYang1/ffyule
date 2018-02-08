using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
	public static class dtHelp
	{
		public static string DT2JSON(DataTable dt)
		{
			return dtHelp.DT2JSON(dt, 0, "recordcount", "table");
		}

		public static string DT2JSON(DataTable dt, int fromCount)
		{
			return dtHelp.DT2JSON(dt, fromCount, "recordcount", "table");
		}

		public static string DT2JSON(DataTable dt, int fromCount, string totalCountStr, string tbname)
		{
			return dtHelp.DT2JSON(dt, fromCount, "recordcount", "table", true);
		}

		public static string DT2JSON(DataTable dt, int fromCount, string totalCountStr, string tbname, bool formatData)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat(new object[]
			{
				"\"",
				totalCountStr,
				"\":",
				dt.Rows.Count,
				",\"",
				tbname,
				"\": ["
			}));
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("{");
				stringBuilder.Append("\"no\":" + (fromCount + i + 1) + ",");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					if (dt.Columns[j].DataType.Equals(typeof(DateTime)) && dt.Rows[i][j].ToString() != "")
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
							"\""
						}));
					}
					else if (dt.Columns[j].DataType.Equals(typeof(string)))
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
							"\""
						}));
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString(),
							"\""
						}));
					}
				}
				stringBuilder.Append("}");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public static string DT2JSON123(DataTable dtsum, DataTable dt, int fromCount, string totalCountStr, string tbname, bool formatData)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat(new object[]
			{
				"\"",
				totalCountStr,
				"\":",
				dt.Rows.Count,
				",\"",
				tbname,
				"\": ["
			}));
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("{");
				stringBuilder.Append("\"no\":" + (fromCount + i + 1) + ",");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					if (dt.Columns[j].DataType.Equals(typeof(DateTime)) && dt.Rows[i][j].ToString() != "")
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
							"\""
						}));
					}
					else if (dt.Columns[j].DataType.Equals(typeof(string)))
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
							"\""
						}));
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString(),
							"\""
						}));
					}
				}
				stringBuilder.Append("}");
			}
			stringBuilder.Append(",");
			stringBuilder.Append("{");
			stringBuilder.Append("\"no\":111,");
			for (int k = 0; k < dt.Columns.Count; k++)
			{
				if (k > 0)
				{
					stringBuilder.Append(",");
				}
				if (dt.Columns[k].DataType.Equals(typeof(decimal)))
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						"\"",
						dt.Columns[k].ColumnName.ToLower(),
						"\": \"",
						dt.Compute("sum(" + dt.Columns[k].ColumnName.ToLower() + ")", "true").ToString(),
						"\""
					}));
				}
				else if (dt.Columns[k].DataType.Equals(typeof(string)))
				{
					stringBuilder.Append("\"" + dt.Columns[k].ColumnName.ToLower() + "\": \"本页合计\"");
				}
				else
				{
					stringBuilder.Append("\"" + dt.Columns[k].ColumnName.ToLower() + "\": \"0\"");
				}
			}
			stringBuilder.Append("}");
			stringBuilder.Append(",");
			stringBuilder.Append("{");
			stringBuilder.Append("\"no\":112,");
			for (int l = 0; l < dtsum.Columns.Count; l++)
			{
				if (l > 0)
				{
					stringBuilder.Append(",");
				}
				if (dtsum.Columns[l].DataType.Equals(typeof(decimal)))
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						"\"",
						dtsum.Columns[l].ColumnName.ToLower(),
						"\": \"",
						dtsum.Compute("sum(" + dt.Columns[l].ColumnName.ToLower() + ")", "true").ToString(),
						"\""
					}));
				}
				else if (dtsum.Columns[l].DataType.Equals(typeof(string)))
				{
					stringBuilder.Append("\"" + dtsum.Columns[l].ColumnName.ToLower() + "\": \"全部合计\"");
				}
				else
				{
					stringBuilder.Append("\"" + dtsum.Columns[l].ColumnName.ToLower() + "\": \"0\"");
				}
			}
			stringBuilder.Append("}");
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public static List<T> DT2List<T>(DataTable dt)
		{
			if (dt == null)
			{
				return null;
			}
			List<T> list = new List<T>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				T t = (T)((object)Activator.CreateInstance(typeof(T)));
				PropertyInfo[] properties = t.GetType().GetProperties();
				PropertyInfo[] array = properties;
				for (int j = 0; j < array.Length; j++)
				{
					PropertyInfo propertyInfo = array[j];
					int k = 0;
					while (k < dt.Columns.Count)
					{
						if (propertyInfo.Name.ToLower().Equals(dt.Columns[k].ColumnName.ToLower()))
						{
							if (dt.Rows[i][k] == DBNull.Value)
							{
								propertyInfo.SetValue(t, "", null);
								break;
							}
							if (propertyInfo.PropertyType.ToString() == "System.Int32")
							{
								propertyInfo.SetValue(t, int.Parse(dt.Rows[i][k].ToString()), null);
								break;
							}
							if (propertyInfo.PropertyType.ToString() == "System.DateTime")
							{
								propertyInfo.SetValue(t, Convert.ToDateTime(dt.Rows[i][k].ToString()), null);
								break;
							}
							if (propertyInfo.PropertyType.ToString() == "System.Boolean")
							{
								propertyInfo.SetValue(t, Convert.ToBoolean(dt.Rows[i][k].ToString()), null);
								break;
							}
							if (propertyInfo.PropertyType.ToString() == "System.Single")
							{
								propertyInfo.SetValue(t, Convert.ToSingle(dt.Rows[i][k].ToString()), null);
								break;
							}
							if (propertyInfo.PropertyType.ToString() == "System.Double")
							{
								propertyInfo.SetValue(t, Convert.ToDouble(dt.Rows[i][k].ToString()), null);
								break;
							}
							propertyInfo.SetValue(t, dt.Rows[i][k].ToString(), null);
							break;
						}
						else
						{
							k++;
						}
					}
				}
				list.Add(t);
			}
			return list;
		}

		public static string DT2JSONNOHTML(DataTable dt, int fromCount, string totalCountStr, string tbname, bool formatData)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat(new object[]
			{
				"\"",
				totalCountStr,
				"\":",
				dt.Rows.Count,
				",\"",
				tbname,
				"\": ["
			}));
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("{");
				stringBuilder.Append("\"no\":" + (fromCount + i + 1) + ",");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					if (dt.Columns[j].DataType.Equals(typeof(DateTime)) && dt.Rows[i][j].ToString() != "")
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
							"\""
						}));
					}
					else if (dt.Columns[j].DataType.Equals(typeof(string)))
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dtHelp.checkStr(dt.Rows[i][j].ToString()),
							"\""
						}));
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dtHelp.checkStr(dt.Rows[i][j].ToString()),
							"\""
						}));
					}
				}
				stringBuilder.Append("}");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public static string checkStr(string Htmlstring)
		{
			Regex regex = new Regex("<.*?>", RegexOptions.Compiled);
			string text = regex.Replace(Htmlstring, string.Empty);
			return text.Replace("\n", "").Replace("\r", "").Replace("&nbsp;", "").Replace("\t", "");
		}

		public static string DT2JSON(DataTable dt, DataTable dt2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\"total\":" + dt.Rows.Count + ",\"table\": [");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("{");
				stringBuilder.Append("\"no\":" + (i + 1) + ",");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					if (dt.Columns[j].DataType.Equals(typeof(DateTime)) && dt.Rows[i][j].ToString() != "")
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
							"\""
						}));
					}
					else if (dt.Columns[j].DataType.Equals(typeof(string)))
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
							"\""
						}));
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString(),
							"\""
						}));
					}
				}
				stringBuilder.Append(",\"table2\": [");
				DataRow[] array = dt2.Select(string.Concat(new object[]
				{
					"Radio=" + dt.Rows[i]["Id"].ToString()
				}), "Sort asc");
				for (int k = 0; k < array.Length; k++)
				{
					if (k > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("{");
					stringBuilder.Append("\"no\":" + (k + 1) + ",");
					for (int l = 0; l < dt2.Columns.Count; l++)
					{
						if (l > 0)
						{
							stringBuilder.Append(",");
						}
						if (dt2.Columns[l].DataType.Equals(typeof(DateTime)) && array[k][l].ToString() != "")
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								Convert.ToDateTime(array[k][l].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
								"\""
							}));
						}
						else if (dt2.Columns[l].DataType.Equals(typeof(string)))
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
								"\""
							}));
						}
						else
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString(),
								"\""
							}));
						}
					}
					stringBuilder.Append("}");
				}
				stringBuilder.Append("]");
				stringBuilder.Append("}");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public static string DT2JSON2(DataTable dt, DataTable dt2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\"total\":" + dt.Rows.Count + ",\"table\": [");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("{");
				stringBuilder.Append("\"no\":" + (i + 1) + ",");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					if (dt.Columns[j].DataType.Equals(typeof(DateTime)) && dt.Rows[i][j].ToString() != "")
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
							"\""
						}));
					}
					else if (dt.Columns[j].DataType.Equals(typeof(string)))
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
							"\""
						}));
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString(),
							"\""
						}));
					}
				}
				stringBuilder.Append(",\"table2\": [");
				DataRow[] array = dt2.Select(string.Concat(new object[]
				{
					"LotteryId=" + dt.Rows[i]["Ltype"].ToString()
				}), "Id asc");
				for (int k = 0; k < array.Length; k++)
				{
					if (k > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("{");
					stringBuilder.Append("\"no\":" + (k + 1) + ",");
					for (int l = 0; l < dt2.Columns.Count; l++)
					{
						if (l > 0)
						{
							stringBuilder.Append(",");
						}
						if (dt2.Columns[l].DataType.Equals(typeof(DateTime)) && array[k][l].ToString() != "")
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								Convert.ToDateTime(array[k][l].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
								"\""
							}));
						}
						else if (dt2.Columns[l].DataType.Equals(typeof(string)))
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
								"\""
							}));
						}
						else
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString(),
								"\""
							}));
						}
					}
					stringBuilder.Append("}");
				}
				stringBuilder.Append("]");
				stringBuilder.Append("}");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public static string DT2JSON3(DataTable dt, DataTable dt2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\"total\":" + dt.Rows.Count + ",\"table\": [");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("{");
				stringBuilder.Append("\"no\":" + (i + 1) + ",");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					if (dt.Columns[j].DataType.Equals(typeof(DateTime)) && dt.Rows[i][j].ToString() != "")
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
							"\""
						}));
					}
					else if (dt.Columns[j].DataType.Equals(typeof(string)))
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
							"\""
						}));
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString(),
							"\""
						}));
					}
				}
				stringBuilder.Append(",\"table2\": [");
				DataRow[] array = dt2.Select(string.Concat(new object[]
				{
					"Ltype=" + dt.Rows[i]["Ltype"].ToString()
				}), "Id asc");
				for (int k = 0; k < array.Length; k++)
				{
					if (k > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("{");
					stringBuilder.Append("\"no\":" + (k + 1) + ",");
					for (int l = 0; l < dt2.Columns.Count; l++)
					{
						if (l > 0)
						{
							stringBuilder.Append(",");
						}
						if (dt2.Columns[l].DataType.Equals(typeof(DateTime)) && array[k][l].ToString() != "")
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								Convert.ToDateTime(array[k][l].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
								"\""
							}));
						}
						else if (dt2.Columns[l].DataType.Equals(typeof(string)))
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
								"\""
							}));
						}
						else
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString(),
								"\""
							}));
						}
					}
					stringBuilder.Append("}");
				}
				stringBuilder.Append("]");
				stringBuilder.Append("}");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public static string DT2JSON33(DataTable dt, DataTable dt2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\"total\":" + dt.Rows.Count + ",\"table\": [");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("{");
				stringBuilder.Append("\"no\":" + (i + 1) + ",");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					if (dt.Columns[j].DataType.Equals(typeof(DateTime)) && dt.Rows[i][j].ToString() != "")
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
							"\""
						}));
					}
					else if (dt.Columns[j].DataType.Equals(typeof(string)))
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
							"\""
						}));
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString(),
							"\""
						}));
					}
				}
				stringBuilder.Append(",\"table2\": [");
				DataRow[] array = dt2.Select(string.Concat(new object[]
				{
					"TypeId=" + dt.Rows[i]["Ltype"].ToString()
				}), "Id asc");
				for (int k = 0; k < array.Length; k++)
				{
					if (k > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("{");
					stringBuilder.Append("\"no\":" + (k + 1) + ",");
					for (int l = 0; l < dt2.Columns.Count; l++)
					{
						if (l > 0)
						{
							stringBuilder.Append(",");
						}
						if (dt2.Columns[l].DataType.Equals(typeof(DateTime)) && array[k][l].ToString() != "")
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								Convert.ToDateTime(array[k][l].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
								"\""
							}));
						}
						else if (dt2.Columns[l].DataType.Equals(typeof(string)))
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
								"\""
							}));
						}
						else
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString(),
								"\""
							}));
						}
					}
					stringBuilder.Append("}");
				}
				stringBuilder.Append("]");
				stringBuilder.Append("}");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public static string DT2JSON3Table(DataTable dt, DataTable dt2, DataTable dt3)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\"result\" :\"1\",\"total\":" + dt.Rows.Count + ",\"table\": [");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("{\"no\":" + (i + 1) + ",");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					if (dt.Columns[j].DataType.Equals(typeof(DateTime)) && dt.Rows[i][j].ToString() != "")
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
							"\""
						}));
					}
					else if (dt.Columns[j].DataType.Equals(typeof(string)))
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
							"\""
						}));
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString(),
							"\""
						}));
					}
				}
				stringBuilder.Append(",\"table2\": [");
				DataRow[] array = dt2.Select(string.Concat(new object[]
				{
					"TypeId=" + dt.Rows[i]["Ltype"].ToString()
				}), "Sort asc");
				for (int k = 0; k < array.Length; k++)
				{
					if (k > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("{");
					stringBuilder.Append("\"no\":" + (k + 1) + ",");
					for (int l = 0; l < dt2.Columns.Count; l++)
					{
						if (l > 0)
						{
							stringBuilder.Append(",");
						}
						if (dt2.Columns[l].DataType.Equals(typeof(DateTime)) && array[k][l].ToString() != "")
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								Convert.ToDateTime(array[k][l].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
								"\""
							}));
						}
						else if (dt2.Columns[l].DataType.Equals(typeof(string)))
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
								"\""
							}));
						}
						else
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString(),
								"\""
							}));
						}
					}
					stringBuilder.Append(",\"table3\": [");
					DataRow[] array2 = dt3.Select(string.Concat(new object[]
					{
						"Radio=" + dt2.Rows[k]["Id"].ToString()
					}), "Sort asc");
					for (int m = 0; m < array2.Length; m++)
					{
						if (m > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append("{");
						stringBuilder.Append("\"no\":" + (m + 1) + ",");
						for (int n = 0; n < dt3.Columns.Count; n++)
						{
							if (n > 0)
							{
								stringBuilder.Append(",");
							}
							if (dt3.Columns[n].DataType.Equals(typeof(DateTime)) && array2[m][n].ToString() != "")
							{
								stringBuilder.Append(string.Concat(new string[]
								{
									"\"",
									dt3.Columns[n].ColumnName.ToLower(),
									"\": \"",
									Convert.ToDateTime(array2[m][n].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
									"\""
								}));
							}
							else if (dt3.Columns[n].DataType.Equals(typeof(string)))
							{
								stringBuilder.Append(string.Concat(new string[]
								{
									"\"",
									dt3.Columns[n].ColumnName.ToLower(),
									"\": \"",
									array2[m][n].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
									"\""
								}));
							}
							else
							{
								stringBuilder.Append(string.Concat(new string[]
								{
									"\"",
									dt3.Columns[n].ColumnName.ToLower(),
									"\": \"",
									array2[m][n].ToString(),
									"\""
								}));
							}
						}
						stringBuilder.Append("}");
					}
					stringBuilder.Append("]}");
					stringBuilder.Append("}");
				}
				stringBuilder.Append("]}");
			}
			stringBuilder.Append("]}");
			return stringBuilder.ToString();
		}

		public static string DT2JSONAIR(DataTable dt, int fromCount)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("{");
				stringBuilder.Append("'no':'" + (fromCount + i + 1) + "',");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					if (dt.Columns[j].DataType.Equals(typeof(DateTime)) && dt.Rows[i][j].ToString() != "")
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"'",
							dt.Columns[j].ColumnName.ToLower(),
							"': '",
							Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
							"'"
						}));
					}
					else if (dt.Columns[j].DataType.Equals(typeof(string)))
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"'",
							dt.Columns[j].ColumnName.ToLower(),
							"': '",
							dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
							"'"
						}));
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"'",
							dt.Columns[j].ColumnName.ToLower(),
							"': '",
							dt.Rows[i][j].ToString(),
							"'"
						}));
					}
				}
				stringBuilder.Append("}");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public static string DT2JSONAdminLeft(DataTable dt, DataTable dt2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\"total\":" + dt.Rows.Count + ",\"table\": [");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("{");
				stringBuilder.Append("\"no\":" + (i + 1) + ",");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					if (dt.Columns[j].DataType.Equals(typeof(DateTime)) && dt.Rows[i][j].ToString() != "")
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
							"\""
						}));
					}
					else if (dt.Columns[j].DataType.Equals(typeof(string)))
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
							"\""
						}));
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							dt.Columns[j].ColumnName.ToLower(),
							"\": \"",
							dt.Rows[i][j].ToString(),
							"\""
						}));
					}
				}
				stringBuilder.Append(",\"table2\": [");
				DataRow[] array = dt2.Select(string.Concat(new object[]
				{
					"Pid=" + dt.Rows[i]["Id"].ToString()
				}), "Sort asc");
				for (int k = 0; k < array.Length; k++)
				{
					if (k > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("{");
					stringBuilder.Append("\"no\":" + (k + 1) + ",");
					for (int l = 0; l < dt2.Columns.Count; l++)
					{
						if (l > 0)
						{
							stringBuilder.Append(",");
						}
						if (dt2.Columns[l].DataType.Equals(typeof(DateTime)) && array[k][l].ToString() != "")
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								Convert.ToDateTime(array[k][l].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
								"\""
							}));
						}
						else if (dt2.Columns[l].DataType.Equals(typeof(string)))
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>"),
								"\""
							}));
						}
						else
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"\"",
								dt2.Columns[l].ColumnName.ToLower(),
								"\": \"",
								array[k][l].ToString(),
								"\""
							}));
						}
					}
					stringBuilder.Append("}");
				}
				stringBuilder.Append("]");
				stringBuilder.Append("}");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
