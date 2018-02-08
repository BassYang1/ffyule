using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Lottery.Utils;

namespace Lottery.DBUtility
{
	public abstract class DbOperHandler : IDisposable
	{
		~DbOperHandler()
		{
			this.conn.Close();
		}

		public IDbConnection GetConnection()
		{
			return this.conn;
		}

		public void Reset()
		{
			this.alFieldItems.Clear();
			this.alSqlCmdParameters.Clear();
			this.alConditionParameters.Clear();
			this.ConditionExpress = string.Empty;
			this.SqlCmd = string.Empty;
			this.cmd.Parameters.Clear();
			this.cmd.CommandText = string.Empty;
			this.cmd.CommandType = CommandType.Text;
		}

		public void AddFieldItem(string _fieldName, object _fieldValue)
		{
			for (int i = 0; i < this.alFieldItems.Count; i++)
			{
				if (((DbKeyItem)this.alFieldItems[i]).fieldName == _fieldName)
				{
					throw new ArgumentException(_fieldName + "不能重复赋值!");
				}
			}
			this.alFieldItems.Add(new DbKeyItem(_fieldName, _fieldValue));
		}

		public void AddFieldItems(object[,] _vFields)
		{
			if (!object.Equals(_vFields, null) && _vFields.GetUpperBound(0) == 1 && _vFields.Rank == 2)
			{
				for (int i = 0; i <= _vFields.GetUpperBound(1); i++)
				{
					if (!object.Equals(_vFields[0, i], null))
					{
						this.AddFieldItem(_vFields[0, i].ToString(), _vFields[1, i]);
					}
				}
			}
		}

		public void AddConditionParameter(string _conditionName, object _conditionValue)
		{
			for (int i = 0; i < this.alConditionParameters.Count; i++)
			{
				if (((DbKeyItem)this.alConditionParameters[i]).fieldName == _conditionName)
				{
					throw new ArgumentException("条件参数名\"" + _conditionName + "\"不能重复赋值!");
				}
			}
			this.alConditionParameters.Add(new DbKeyItem(_conditionName, _conditionValue));
		}

		public void AddConditionParameters(object[,] _vParameters)
		{
			if (!object.Equals(_vParameters, null) && _vParameters.GetUpperBound(0) == 1 && _vParameters.Rank == 2)
			{
				for (int i = 0; i <= _vParameters.GetUpperBound(1); i++)
				{
					if (!object.Equals(_vParameters[0, i], null))
					{
						this.AddConditionParameter(_vParameters[0, i].ToString(), _vParameters[1, i]);
					}
				}
			}
		}

		public int Count(string tableName)
		{
			return Convert.ToInt32(this.GetField(tableName, "count(*)", false).ToString());
		}

		public int CountId(string tableName)
		{
			return Convert.ToInt32(this.GetField(tableName, "count(id)", false).ToString());
		}

		public bool Exist(string tableName)
		{
			return this.GetField(tableName, "count(*)", false).ToString() != "0";
		}

		public int MaxId(string tableName)
		{
			return Convert.ToInt32("0" + this.GetField(tableName, "max(id)", false).ToString());
		}

		public int MinValue(string tableName, string fieldName)
		{
			return Convert.ToInt32("0" + this.GetField(tableName, "min(" + fieldName + ")", false).ToString());
		}

		public int MaxValue(string tableName, string fieldName)
		{
			return Convert.ToInt32("0" + this.GetField(tableName, "max(" + fieldName + ")", false).ToString());
		}

		public decimal MaxDecValue(string tableName, string fieldName)
		{
			return Convert.ToDecimal("0" + this.GetField(tableName, "max(" + fieldName + ")", false).ToString());
		}

		protected abstract void GenParameters();

		public int Insert(string _tableName)
		{
			this.tableName = _tableName;
			this.fieldName = string.Empty;
			this.SqlCmd = "insert into [" + this.tableName + "](";
			string text = " values(";
			for (int i = 0; i < this.alFieldItems.Count - 1; i++)
			{
				this.SqlCmd = this.SqlCmd + "[" + ((DbKeyItem)this.alFieldItems[i]).fieldName + "]";
				this.SqlCmd += ",";
				text += "@para";
				text += i.ToString();
				text += ",";
			}
			this.SqlCmd = this.SqlCmd + "[" + ((DbKeyItem)this.alFieldItems[this.alFieldItems.Count - 1]).fieldName + "]";
			this.SqlCmd += ") ";
			text += "@para";
			text += (this.alFieldItems.Count - 1).ToString();
			text += ")";
			this.SqlCmd += text;
			this.cmd.CommandText = this.SqlCmd;
			this.GenParameters();
			this.cmd.ExecuteNonQuery();
			int result = 0;
			try
			{
				this.cmd.CommandText = "select @@identity as id";
				result = Convert.ToInt32(this.cmd.ExecuteScalar());
			}
			catch (Exception)
			{
			}
			return result;
		}

		public int Update(string _tableName)
		{
			this.tableName = _tableName;
			this.fieldName = string.Empty;
			this.SqlCmd = "UPDATE [" + this.tableName + "] SET ";
			for (int i = 0; i < this.alFieldItems.Count - 1; i++)
			{
				this.SqlCmd = this.SqlCmd + "[" + ((DbKeyItem)this.alFieldItems[i]).fieldName + "]";
				this.SqlCmd += "=";
				this.SqlCmd += "@para";
				this.SqlCmd += i.ToString();
				this.SqlCmd += ",";
			}
			this.SqlCmd = this.SqlCmd + "[" + ((DbKeyItem)this.alFieldItems[this.alFieldItems.Count - 1]).fieldName + "]";
			this.SqlCmd += "=";
			this.SqlCmd += "@para";
			this.SqlCmd += (this.alFieldItems.Count - 1).ToString();
			if (this.ConditionExpress != string.Empty)
			{
				this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
			}
			this.cmd.CommandText = this.SqlCmd;
			this.GenParameters();
			return this.cmd.ExecuteNonQuery();
		}

		public int ExecuteSqlNonQuery()
		{
			this.cmd.CommandText = this.SqlCmd;
			this.GenParameters();
			return this.cmd.ExecuteNonQuery();
		}

		public bool DropTable(string _tableName)
		{
			bool result;
			try
			{
				this.cmd.CommandText = "drop table [" + _tableName + "]";
				this.cmd.ExecuteNonQuery();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public bool ExistTable(string _tableName)
		{
			bool result;
			try
			{
				this.cmd.CommandText = "select top 1 * from [" + _tableName + "]";
				this.cmd.ExecuteNonQuery();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public object GetField(string _tableName, string _fieldName, bool _isField)
		{
			this.tableName = _tableName;
			this.fieldName = _fieldName;
			if (_isField)
			{
				this.SqlCmd = string.Concat(new string[]
				{
					"select [",
					this.fieldName,
					"] from [",
					this.tableName,
					"] with(nolock) "
				});
			}
			else
			{
				this.SqlCmd = string.Concat(new string[]
				{
					"select ",
					this.fieldName,
					" from [",
					this.tableName,
					"] with(nolock) "
				});
			}
			if (this.ConditionExpress != string.Empty)
			{
				this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
			}
			this.cmd.CommandText = this.SqlCmd;
			this.GenParameters();
			object obj = this.cmd.ExecuteScalar();
			if (obj == null)
			{
				obj = string.Empty;
			}
			return obj;
		}

		public object GetField(string _tableName, string _fieldName)
		{
			return this.GetField(_tableName, _fieldName, true);
		}

		public object[] GetFields(string _tableName, string _fieldNames)
		{
			this.SqlCmd = string.Concat(new string[]
			{
				"select ",
				_fieldNames,
				" from ",
				_tableName,
				" with(nolock) "
			});
			if (this.ConditionExpress != string.Empty)
			{
				this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
			}
			this.cmd.CommandText = this.SqlCmd;
			this.GenParameters();
			DataSet dataSet = new DataSet();
			this.da.SelectCommand = this.cmd;
			this.da.Fill(dataSet);
			DataTable dataTable = dataSet.Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				object[] array = new object[dataTable.Columns.Count];
				for (int i = 0; i < dataTable.Columns.Count; i++)
				{
					array[i] = dataTable.Rows[0][i];
				}
				return array;
			}
			return null;
		}

		public int GetCount(string _tableName, string _fieldName)
		{
			this.tableName = _tableName;
			this.fieldName = _fieldName;
			this.SqlCmd = string.Concat(new string[]
			{
				"select count(",
				this.fieldName,
				") from [",
				this.tableName,
				"] with(nolock) "
			});
			if (this.ConditionExpress != string.Empty)
			{
				this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
			}
			this.cmd.CommandText = this.SqlCmd;
			this.GenParameters();
			return (int)this.cmd.ExecuteScalar();
		}

		public DataTable GetDataTable()
		{
			DataSet dataSet = this.GetDataSet();
			return dataSet.Tables[0];
		}

		public DataSet GetDataSet()
		{
			this.alConditionParameters.Clear();
			this.ConditionExpress = string.Empty;
			this.cmd.CommandText = this.SqlCmd;
			this.GenParameters();
			DataSet dataSet = new DataSet();
			this.da.SelectCommand = this.cmd;
			this.da.Fill(dataSet);
			return dataSet;
		}

		public int Add(string _tableName, string _fieldName)
		{
			return this.Add(_tableName, _fieldName, 1);
		}

		public int Add(string _tableName, string _fieldName, int _num)
		{
			this.tableName = _tableName;
			this.fieldName = _fieldName;
			int num = Convert.ToInt32("0" + this.GetField(this.tableName, this.fieldName));
			num += _num;
			this.cmd.Parameters.Clear();
			this.cmd.CommandText = string.Empty;
			this.AddFieldItem(_fieldName, num);
			this.Update(this.tableName);
			return num;
		}

		public decimal Add(string _tableName, string _fieldName, decimal _num)
		{
			this.tableName = _tableName;
			this.fieldName = _fieldName;
			decimal num = Convert.ToDecimal("0" + this.GetField(this.tableName, this.fieldName));
			num += _num;
			this.cmd.Parameters.Clear();
			this.cmd.CommandText = string.Empty;
			this.AddFieldItem(_fieldName, num);
			this.Update(this.tableName);
			return num;
		}

		public int Deduct(string _tableName, string _fieldName)
		{
			return this.Deduct(_tableName, _fieldName, 1);
		}

		public decimal Deduct(string _tableName, string _fieldName, decimal _num)
		{
			this.tableName = _tableName;
			this.fieldName = _fieldName;
			decimal num = Convert.ToDecimal("0" + this.GetField(this.tableName, this.fieldName));
			if (num > 0m)
			{
				num -= _num;
				if (num < 0m)
				{
					num = 0m;
				}
			}
			this.cmd.Parameters.Clear();
			this.cmd.CommandText = string.Empty;
			this.AddFieldItem(_fieldName, num);
			this.Update(this.tableName);
			return num;
		}

		public decimal Deduct2(string _tableName, string _fieldName, decimal _num)
		{
			this.tableName = _tableName;
			this.fieldName = _fieldName;
			decimal num = Convert.ToDecimal("0" + this.GetField(this.tableName, this.fieldName));
			num -= _num;
			this.cmd.Parameters.Clear();
			this.cmd.CommandText = string.Empty;
			this.AddFieldItem(_fieldName, num);
			this.Update(this.tableName);
			return num;
		}

		public int Deduct(string _tableName, string _fieldName, int _num)
		{
			this.tableName = _tableName;
			this.fieldName = _fieldName;
			int num = Convert.ToInt32("0" + this.GetField(this.tableName, this.fieldName));
			if (num > 0)
			{
				num -= _num;
				if (num < 0)
				{
					num = 0;
				}
			}
			this.cmd.Parameters.Clear();
			this.cmd.CommandText = string.Empty;
			this.AddFieldItem(_fieldName, num);
			this.Update(this.tableName);
			return num;
		}

		public int Sum(string _tableName, string _fieldName)
		{
			this.tableName = _tableName;
			this.fieldName = _fieldName;
			return Convert.ToInt32("0" + this.GetField(this.tableName, "sum(" + this.fieldName + ")", false));
		}

		public int Delete(string _tableName)
		{
			this.tableName = _tableName;
			this.SqlCmd = "delete from [" + this.tableName + "]";
			if (this.ConditionExpress != string.Empty)
			{
				this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
			}
			this.cmd.CommandText = this.SqlCmd;
			this.GenParameters();
			return this.cmd.ExecuteNonQuery();
		}

		public int Audit(string _tableName, string _fieldName)
		{
			this.tableName = _tableName;
			this.fieldName = _fieldName;
			this.SqlCmd = string.Concat(new string[]
			{
				"UPDATE [",
				this.tableName,
				"] SET [",
				this.fieldName,
				"]=1-",
				this.fieldName
			});
			if (this.ConditionExpress != string.Empty)
			{
				this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
			}
			this.cmd.CommandText = this.SqlCmd;
			this.GenParameters();
			return this.cmd.ExecuteNonQuery();
		}

		public DataRow GetSP_Row(string ProcedureName)
		{
			this.cmd.CommandText = ProcedureName;
			this.cmd.CommandType = CommandType.StoredProcedure;
			this.GenParameters();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
			sqlDataAdapter.SelectCommand = (SqlCommand)this.cmd;
			DataTable dataTable = new DataTable();
			sqlDataAdapter.Fill(dataTable);
			sqlDataAdapter.Dispose();
			if (dataTable.Rows.Count <= 0)
			{
				return null;
			}
			return dataTable.Rows[0];
		}

		public DataRowCollection GetSP_Rows(string ProcedureName)
		{
			this.cmd.CommandText = ProcedureName;
			this.cmd.CommandType = CommandType.StoredProcedure;
			this.GenParameters();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
			sqlDataAdapter.SelectCommand = (SqlCommand)this.cmd;
			DataTable dataTable = new DataTable();
			sqlDataAdapter.Fill(dataTable);
			sqlDataAdapter.Dispose();
			return dataTable.Rows;
		}

		public int ExecuteSql(string SqlCmd)
		{
			this.cmd.CommandText = SqlCmd;
			this.cmd.ExecuteNonQuery();
			int result = 0;
			try
			{
				this.cmd.CommandText = "select @@identity as id";
				result = Convert.ToInt32(this.cmd.ExecuteScalar());
			}
			catch (Exception)
			{
			}
			return result;
		}

		public void ExecuteProcActive(string ProcedureName)
		{
			string connectionString = Const.ConnectionString;
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			sqlConnection.Open();
			new SqlCommand(ProcedureName, sqlConnection)
			{
				CommandType = CommandType.StoredProcedure
			}.ExecuteNonQuery();
			sqlConnection.Close();
		}

		public object[] ExecuteProc(string ProcedureName, string userId)
		{
			object[] array = new object[3];
			string connectionString = Const.ConnectionString;
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(ProcedureName, sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Add("@userId", SqlDbType.VarChar, 100).Value = userId;
			sqlCommand.Parameters.Add("@ownbet", SqlDbType.VarChar, 100);
			sqlCommand.Parameters["@ownbet"].Direction = ParameterDirection.Output;
			sqlCommand.ExecuteNonQuery();
			array[0] = sqlCommand.Parameters["@ownbet"].Value;
			sqlConnection.Close();
			return array;
		}

		public object ExecuteProcAuto(string ProcedureName, string userId)
		{
			string connectionString = Const.ConnectionString;
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(ProcedureName, sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Add("@userId", SqlDbType.VarChar, 100).Value = userId;
			sqlCommand.Parameters.Add("@output", SqlDbType.VarChar, 100);
			sqlCommand.Parameters["@output"].Direction = ParameterDirection.Output;
			sqlCommand.ExecuteNonQuery();
			object value = sqlCommand.Parameters["@output"].Value;
			sqlConnection.Close();
			return value;
		}

		public object ExecuteProc_Active1(string ProcedureName, string userId, string CheckIp, string CheckMachine)
		{
			string connectionString = Const.ConnectionString;
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(ProcedureName, sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Add("@userId", SqlDbType.VarChar, 100).Value = userId;
			sqlCommand.Parameters.Add("@CheckIp", SqlDbType.VarChar, 100).Value = CheckIp;
			sqlCommand.Parameters.Add("@CheckMachine", SqlDbType.VarChar, 100).Value = CheckMachine;
			sqlCommand.Parameters.Add("@output", SqlDbType.VarChar, 100);
			sqlCommand.Parameters["@output"].Direction = ParameterDirection.Output;
			sqlCommand.ExecuteNonQuery();
			object value = sqlCommand.Parameters["@output"].Value;
			sqlConnection.Close();
			return value;
		}

		public int ExecuteProcUserOpers(string ssId, string userId, decimal userMoney, decimal statMoney, string statType, int logLotteryId, int logPlayId, int logSysId, int logCode, int logIsSoft, string messageTitle, string messageContent, string reMark)
		{
			SqlConnection sqlConnection = new SqlConnection(Const.ConnectionString);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("Global_UserOperTran", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Add("@SsId", SqlDbType.VarChar, 50).Value = ssId;
			sqlCommand.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = userId;
			sqlCommand.Parameters.Add("@UserMoney", SqlDbType.Decimal, 18).Value = userMoney;
			sqlCommand.Parameters.Add("@StatMoney", SqlDbType.Decimal, 18).Value = statMoney;
			sqlCommand.Parameters.Add("@StatType", SqlDbType.VarChar, 20).Value = statType;
			sqlCommand.Parameters.Add("@LogLotteryId", SqlDbType.Int, 8).Value = logLotteryId;
			sqlCommand.Parameters.Add("@LogPlayId", SqlDbType.Int, 8).Value = logPlayId;
			sqlCommand.Parameters.Add("@LogSysId", SqlDbType.Int, 8).Value = logSysId;
			sqlCommand.Parameters.Add("@LogCode", SqlDbType.Int, 8).Value = logCode;
			sqlCommand.Parameters.Add("@LogIsSoft", SqlDbType.Int, 8).Value = logIsSoft;
			sqlCommand.Parameters.Add("@LogReMark", SqlDbType.VarChar, 200).Value = reMark;
			sqlCommand.Parameters.Add("@MessageTitle", SqlDbType.VarChar, 50).Value = messageTitle;
			sqlCommand.Parameters.Add("@MessageContent", SqlDbType.VarChar, 200).Value = messageContent;
			sqlCommand.Parameters.Add("@output", SqlDbType.VarChar, 200);
			sqlCommand.Parameters["@output"].Direction = ParameterDirection.Output;
			sqlCommand.ExecuteNonQuery();
			object value = sqlCommand.Parameters["@output"].Value;
			sqlConnection.Close();
			return Convert.ToInt32(value);
		}

		public object ExecuteProcUserOpers(string ssId, string userId, decimal userMoney, decimal statMoney, string statType, int logLotteryId, int logPlayId, int logSysId, int logCode, int logIsSoft, string messageTitle, string messageContent, string reMark, SqlCommand cmd)
		{
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "Global_UserOperTran";
			cmd.Parameters.Add("@SsId", SqlDbType.VarChar, 50).Value = ssId;
			cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = userId;
			cmd.Parameters.Add("@UserMoney", SqlDbType.Decimal, 18).Value = userMoney;
			cmd.Parameters.Add("@StatMoney", SqlDbType.Decimal, 18).Value = statMoney;
			cmd.Parameters.Add("@StatType", SqlDbType.VarChar, 20).Value = statType;
			cmd.Parameters.Add("@LogLotteryId", SqlDbType.Int, 8).Value = logLotteryId;
			cmd.Parameters.Add("@LogPlayId", SqlDbType.Int, 8).Value = logPlayId;
			cmd.Parameters.Add("@LogSysId", SqlDbType.Int, 8).Value = logSysId;
			cmd.Parameters.Add("@LogCode", SqlDbType.Int, 8).Value = logCode;
			cmd.Parameters.Add("@LogIsSoft", SqlDbType.Int, 8).Value = logIsSoft;
			cmd.Parameters.Add("@LogReMark", SqlDbType.VarChar, 200).Value = reMark;
			cmd.Parameters.Add("@MessageTitle", SqlDbType.VarChar, 50).Value = messageTitle;
			cmd.Parameters.Add("@MessageContent", SqlDbType.VarChar, 200).Value = messageContent;
			cmd.Parameters.Add("@output", SqlDbType.VarChar, 200);
			cmd.Parameters["@output"].Direction = ParameterDirection.Output;
			cmd.ExecuteNonQuery();
			return cmd.Parameters["@output"].Value;
		}

		public void Dispose()
		{
			this.conn.Close();
			this.conn.Dispose();
		}

		public DatabaseType dbType;

		public string ConditionExpress = string.Empty;

		public string SqlCmd = string.Empty;

		protected string tableName = string.Empty;

		protected string fieldName = string.Empty;

		protected IDbConnection conn;

		protected IDbCommand cmd;

		protected IDbDataAdapter da;

		protected ArrayList alFieldItems = new ArrayList(10);

		protected ArrayList alSqlCmdParameters = new ArrayList(5);

		protected ArrayList alConditionParameters = new ArrayList(5);
	}
}
