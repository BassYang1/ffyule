using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Lottery.Utils
{
	public static class ExecuteSqlBlock
	{
		public static bool Go(string dbType, string connectionString, string pathToScriptFile)
		{
			StreamReader streamReader = null;
			Stream stream = null;
			if (!File.Exists(pathToScriptFile))
			{
				return false;
			}
			bool result;
			try
			{
				stream = File.OpenRead(pathToScriptFile);
				streamReader = new StreamReader(stream, Encoding.UTF8);
				if (dbType == "0")
				{
					using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
					{
						using (OleDbCommand oleDbCommand = new OleDbCommand())
						{
							oleDbConnection.Open();
							oleDbCommand.Connection = oleDbConnection;
							oleDbCommand.CommandType = CommandType.Text;
							string commandText;
							while ((commandText = ExecuteSqlBlock.ReadNextStatementFromStream(streamReader)) != null)
							{
								oleDbCommand.CommandText = commandText;
								oleDbCommand.ExecuteNonQuery();
							}
						}
						goto IL_F9;
					}
				}
				using (SqlConnection sqlConnection = new SqlConnection(connectionString))
				{
					using (SqlCommand sqlCommand = new SqlCommand())
					{
						sqlConnection.Open();
						sqlCommand.Connection = sqlConnection;
						sqlCommand.CommandTimeout = 180;
						sqlCommand.CommandType = CommandType.Text;
						string commandText;
						while ((commandText = ExecuteSqlBlock.ReadNextStatementFromStream(streamReader)) != null)
						{
							sqlCommand.CommandText = commandText;
							sqlCommand.ExecuteNonQuery();
						}
					}
				}
				IL_F9:
				result = true;
			}
			catch
			{
				result = false;
			}
			finally
			{
				streamReader.Close();
				streamReader.Dispose();
				stream.Close();
				stream.Dispose();
			}
			return result;
		}

		private static string ReadNextStatementFromStream(StreamReader _reader)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (true)
			{
				string text = _reader.ReadLine();
				if (text == null)
				{
					break;
				}
				if (text.TrimEnd(new char[0]).ToUpper() == "GO")
				{
					goto IL_4E;
				}
				stringBuilder.AppendFormat("{0}\r\n", text);
			}
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return null;
			IL_4E:
			return stringBuilder.ToString();
		}
	}
}
