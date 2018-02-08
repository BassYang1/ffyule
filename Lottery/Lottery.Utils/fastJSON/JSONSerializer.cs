using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace Lottery.Utils.fastJSON
{
	internal class JSONSerializer
	{
		public static string ToJSON(object obj)
		{
			return new JSONSerializer().ConvertToJSON(obj);
		}

		internal string ConvertToJSON(object obj)
		{
			this.WriteValue(obj);
			return this._output.ToString();
		}

		private void WriteValue(object obj)
		{
			if (obj == null)
			{
				this._output.Append("null");
				return;
			}
			if (obj is sbyte || obj is byte || obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong || obj is decimal || obj is double || obj is float)
			{
				this._output.Append(Convert.ToString(obj, NumberFormatInfo.InvariantInfo));
				return;
			}
			if (obj is bool)
			{
				this._output.Append(obj.ToString().ToLower());
				return;
			}
			if (obj is char || obj is Enum || obj is Guid || obj is string)
			{
				this.WriteString(obj.ToString());
				return;
			}
			if (obj is DateTime)
			{
				this._output.Append("\"");
				this._output.Append(((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss"));
				this._output.Append("\"");
				return;
			}
			if (obj is DataSet)
			{
				this.WriteDataset((DataSet)obj);
				return;
			}
			if (obj is byte[])
			{
				this.WriteByteArray((byte[])obj);
				return;
			}
			if (obj is IDictionary)
			{
				this.WriteDictionary((IDictionary)obj);
				return;
			}
			if (obj is Array || obj is IList || obj is ICollection)
			{
				this.WriteArray((IEnumerable)obj);
				return;
			}
			this.WriteObject(obj);
		}

		private void WriteByteArray(byte[] bytes)
		{
			this.WriteString(Convert.ToBase64String(bytes));
		}

		private void WriteDataset(DataSet ds)
		{
			this._output.Append("{");
			this.WritePair("$schema", ds.GetXmlSchema());
			this._output.Append(",");
			foreach (DataTable dataTable in ds.Tables)
			{
				this._output.Append("\"");
				this._output.Append(dataTable.TableName);
				this._output.Append("\":[");
				foreach (DataRow dataRow in dataTable.Rows)
				{
					this._output.Append("{");
					foreach (DataColumn dataColumn in dataRow.Table.Columns)
					{
						this.WritePair(dataColumn.ColumnName, dataRow[dataColumn]);
					}
					this._output.Append("}");
				}
				this._output.Append("]");
			}
			this._output.Append("}");
		}

		private void WriteObject(object obj)
		{
			this._output.Append("{");
			Type type = obj.GetType();
			this.WritePair("$type", type.AssemblyQualifiedName);
			this._output.Append(",");
			List<Getters> getters = JSON.Instance.GetGetters(type);
			foreach (Getters current in getters)
			{
				this.WritePair(current.Name, current.Getter(obj));
			}
			this._output.Append("}");
		}

		private void WritePair(string name, string value)
		{
			this.WriteString(name);
			this._output.Append(":");
			this.WriteString(value);
		}

		private void WritePair(string name, object value)
		{
			this.WriteString(name);
			this._output.Append(":");
			this.WriteValue(value);
		}

		private void WriteArray(IEnumerable array)
		{
			this._output.Append("[");
			bool flag = false;
			foreach (object current in array)
			{
				if (flag)
				{
					this._output.Append(',');
				}
				this.WriteValue(current);
				flag = true;
			}
			this._output.Append("]");
		}

		private void WriteDictionary(IDictionary dic)
		{
			this._output.Append("[");
			bool flag = false;
			foreach (DictionaryEntry dictionaryEntry in dic)
			{
				if (flag)
				{
					this._output.Append(",");
				}
				this._output.Append("{");
				this.WritePair("k", dictionaryEntry.Key);
				this._output.Append(",");
				this.WritePair("v", dictionaryEntry.Value);
				this._output.Append("}");
				flag = true;
			}
			this._output.Append("]");
		}

		private void WriteString(string s)
		{
			this._output.Append('"');
			int i = 0;
			while (i < s.Length)
			{
				char c = s[i];
				char c2 = c;
				switch (c2)
				{
				case '\t':
					this._output.Append("\\t");
					break;
				case '\n':
					this._output.Append("\\n");
					break;
				case '\v':
				case '\f':
					goto IL_A6;
				case '\r':
					this._output.Append("\\r");
					break;
				default:
					if (c2 != '"' && c2 != '\\')
					{
						goto IL_A6;
					}
					this._output.Append("\\");
					this._output.Append(c);
					break;
				}
				IL_EE:
				i++;
				continue;
				IL_A6:
				if (c >= ' ' && c < '\u0080')
				{
					this._output.Append(c);
					goto IL_EE;
				}
				this._output.Append("\\u");
				StringBuilder arg_E8_0 = this._output;
				int num = (int)c;
				arg_E8_0.Append(num.ToString("X4"));
				goto IL_EE;
			}
			this._output.Append('"');
		}

		private readonly StringBuilder _output = new StringBuilder();
	}
}
