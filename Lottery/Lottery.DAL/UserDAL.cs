using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class UserDAL : ComData
	{
		public UserDAL()
		{
			this.site = new conSite().GetSite();
		}

		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, string orderby, string order, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("V_User");
				string sql = SqlHelp.GetSql0("*", "V_User", order, _pagesize, _thispage, orderby, _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
					PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetListJSON2(int _thispage, int _pagesize, string _wherestr1, string orderby, string order, string pid, string uid, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("V_User");
				string sql = SqlHelp.GetSql0(pid + " as pid,*", "V_User", order, _pagesize, _thispage, orderby, _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"nav\" :\"",
					this.GetUserNav(pid, uid),
					"\",\"pagebar\" :\"",
					PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public int Register(string _ParentId, string _UserName, string _Password, decimal _Point)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				string text = MD5.Last64(_Password);
				object[,] array = new object[2, 5];
				array[0, 0] = "ParentId";
				array[0, 1] = "UserName";
				array[0, 2] = "Password";
				array[0, 3] = "Point";
				array[0, 4] = "PayPass";
				array[1, 0] = _ParentId;
				array[1, 1] = _UserName;
				array[1, 2] = text;
				array[1, 3] = _Point;
				array[1, 4] = MD5.Last64(MD5.Lower32("123456"));
				object[,] vFields = array;
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItems(vFields);
				int num = dbOperHandler.Insert("N_User");
				object[,] array2 = new object[2, 2];
				array2[0, 0] = "UserId";
				array2[0, 1] = "Change";
				array2[1, 0] = num;
				array2[1, 1] = 0;
				object[,] vFields2 = array2;
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItems(vFields2);
				dbOperHandler.Insert("N_UserMoneyStatAll");
				result = num;
			}
			return result;
		}

		public string ChkLogin(string _adminname, string _adminpass, int iExpires)
		{
			_adminname = _adminname.Replace("'", "");
			string arg = MD5.Last64(_adminpass);
			string result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("select top 1 Id,Point,IsEnable from N_User with(nolock) where username='{0}' and password='{1}' and isDel=0", _adminname, arg);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 1)
					{
						result = base.JsonResult(0, "您的账户存在未知问题，请于客服联系！");
					}
					else if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 2)
					{
						result = base.JsonResult(0, "对不起，您的网络不稳定，请重新登录！");
					}
					else
					{
						string text = Guid.NewGuid().ToString().Replace("-", "");
						NameValueCollection nameValueCollection = new NameValueCollection();
						nameValueCollection.Add("id", dataTable.Rows[0]["Id"].ToString());
						nameValueCollection.Add("name", _adminname);
						nameValueCollection.Add("cookiess", text);
						nameValueCollection.Add("point", dataTable.Rows[0]["Point"].ToString());
						Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, nameValueCollection, this.site.CookieDomain, this.site.CookiePath);
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "Id=@Id and IsEnable=0";
						dbOperHandler.AddConditionParameter("@Id", dataTable.Rows[0]["Id"].ToString());
						dbOperHandler.AddFieldItem("LastTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
						dbOperHandler.AddFieldItem("IP", IPHelp.ClientIP);
						dbOperHandler.AddFieldItem("sessionId", text);
						dbOperHandler.AddFieldItem("IsOnline", 1);
						dbOperHandler.AddFieldItem("Source", 1);
						dbOperHandler.Update("N_User");
						dbOperHandler.Dispose();
						result = dataTable.Rows[0]["Id"].ToString();
					}
				}
				else
				{
					dbOperHandler.Dispose();
					result = base.JsonResult(0, "会员账号或密码错误！");
				}
			}
			return result;
		}

		public string ChkLoginWebApp(string _adminname, string _adminpass, int iExpires)
		{
			_adminname = _adminname.Replace("'", "");
			string arg = MD5.Last64(_adminpass);
			string result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("select top 1 Id,Point,IsEnable from N_User with(nolock) where username='{0}' and password='{1}' and isDel=0", _adminname, arg);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 1)
					{
						result = base.JsonResult(0, "您的账户存在未知问题，请于客服联系！");
					}
					else if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 2)
					{
						result = base.JsonResult(0, "对不起，您的网络不稳定，请重新登录！");
					}
					else
					{
						string text = Guid.NewGuid().ToString().Replace("-", "");
						NameValueCollection nameValueCollection = new NameValueCollection();
						nameValueCollection.Add("id", dataTable.Rows[0]["Id"].ToString());
						nameValueCollection.Add("name", _adminname);
						nameValueCollection.Add("cookiess", text);
						nameValueCollection.Add("point", dataTable.Rows[0]["Point"].ToString());
						Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, nameValueCollection, this.site.CookieDomain, this.site.CookiePath);
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "Id=@Id and IsEnable=0";
						dbOperHandler.AddConditionParameter("@Id", dataTable.Rows[0]["Id"].ToString());
						dbOperHandler.AddFieldItem("LastTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
						dbOperHandler.AddFieldItem("IP", IPHelp.ClientIP);
						dbOperHandler.AddFieldItem("sessionId", text);
						dbOperHandler.AddFieldItem("IsOnline", 1);
						dbOperHandler.AddFieldItem("Source", 0);
						dbOperHandler.Update("N_User");
						dbOperHandler.Dispose();
						result = dataTable.Rows[0]["Id"].ToString();
					}
				}
				else
				{
					dbOperHandler.Dispose();
					result = base.JsonResult(0, "会员账号或密码错误！");
				}
			}
			return result;
		}

		public string ChkAutoLoginWebApp(string _Id, string _sessionId, int iExpires)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("select top 1 UserName,Point,sessionId from N_User with(nolock) where Id={0}", _Id);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					if (!string.IsNullOrEmpty(string.Concat(dataTable.Rows[0]["sessionId"])))
					{
						NameValueCollection nameValueCollection = new NameValueCollection();
						nameValueCollection.Add("id", _Id);
						nameValueCollection.Add("name", dataTable.Rows[0]["UserName"].ToString());
						nameValueCollection.Add("cookiess", dataTable.Rows[0]["sessionId"].ToString());
						nameValueCollection.Add("point", dataTable.Rows[0]["Point"].ToString());
						Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, nameValueCollection, "www.youle1288.com;youle1288.com;www.youle2888.com;youle2888.com,feifan1188.com,www.feifan1188.com", this.site.CookiePath);
					}
					else
					{
						string text = Guid.NewGuid().ToString().Replace("-", "");
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "Id=@Id";
						dbOperHandler.AddConditionParameter("@Id", _Id);
						dbOperHandler.AddFieldItem("sessionId", text);
						dbOperHandler.Update("N_User");
						dbOperHandler.Dispose();
						NameValueCollection nameValueCollection2 = new NameValueCollection();
						nameValueCollection2.Add("id", _Id);
						nameValueCollection2.Add("name", dataTable.Rows[0]["UserName"].ToString());
						nameValueCollection2.Add("cookiess", text);
						nameValueCollection2.Add("point", dataTable.Rows[0]["Point"].ToString());
						Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, nameValueCollection2, "www.youle1288.com;youle1288.com;www.youle2888.com;youle2888.com,feifan1188.com,www.feifan1188.com", this.site.CookiePath);
					}
				}
			}
			return _Id;
		}

		public string ChkAutoLoginWebApp(string _Id, string _sessionId)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("select top 1 UserName,Point,sessionId from N_User with(nolock) where Id={0}", _Id);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					if (!string.IsNullOrEmpty(string.Concat(dataTable.Rows[0]["sessionId"])))
					{
						NameValueCollection nameValueCollection = new NameValueCollection();
						nameValueCollection.Add("id", _Id);
						nameValueCollection.Add("name", dataTable.Rows[0]["UserName"].ToString());
						nameValueCollection.Add("cookiess", dataTable.Rows[0]["sessionId"].ToString());
						nameValueCollection.Add("point", dataTable.Rows[0]["Point"].ToString());
						Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, nameValueCollection, this.site.CookieDomain, this.site.CookiePath);
					}
					else
					{
						string text = Guid.NewGuid().ToString().Replace("-", "");
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "Id=@Id";
						dbOperHandler.AddConditionParameter("@Id", _Id);
						dbOperHandler.AddFieldItem("sessionId", text);
						dbOperHandler.Update("N_User");
						dbOperHandler.Dispose();
						NameValueCollection nameValueCollection2 = new NameValueCollection();
						nameValueCollection2.Add("id", _Id);
						nameValueCollection2.Add("name", dataTable.Rows[0]["UserName"].ToString());
						nameValueCollection2.Add("cookiess", text);
						nameValueCollection2.Add("point", dataTable.Rows[0]["Point"].ToString());
						Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, nameValueCollection2, this.site.CookieDomain, this.site.CookiePath);
					}
				}
			}
			return _Id;
		}

		public void ChkLogout()
		{
			if (Cookie.GetValue(this.site.CookiePrev + "WebApp") != null)
			{
				Cookie.Del(this.site.CookiePrev + "WebApp", this.site.CookieDomain, this.site.CookiePath);
			}
		}

		public bool Exists(string _wherestr)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr;
				if (dbOperHandler.Exist("N_User"))
				{
					num = 1;
				}
			}
			return num == 1;
		}

		public string GetUserName(string _id)
		{
			string result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT [UserName] FROM [N_User] WHERE [Id]=" + _id;
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					result = dataTable.Rows[0]["UserName"].ToString();
				}
				else
				{
					result = string.Empty;
				}
			}
			return result;
		}

		public bool ChangeUserPassword(string _userid, string _oldPassword, string _newPassword)
		{
			bool result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _userid);
				object field = dbOperHandler.GetField("N_User", "PassWord");
				if (field != null)
				{
					if (field.ToString().ToLower() == MD5.Last64(_oldPassword))
					{
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "id=@id";
						dbOperHandler.AddConditionParameter("@id", _userid);
						dbOperHandler.AddFieldItem("PassWord", MD5.Last64(_newPassword));
						dbOperHandler.AddFieldItem("IP", Const.GetUserIp);
						dbOperHandler.Update("N_User");
						result = true;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public bool ChangePayPassword(string _userid, string _oldPassword, string _newPassword)
		{
			bool result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _userid);
				object field = dbOperHandler.GetField("N_User", "PayPass");
				if (field != null)
				{
					if (field.ToString().ToLower() == MD5.Last64(_oldPassword))
					{
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "id=@id";
						dbOperHandler.AddConditionParameter("@id", _userid);
						dbOperHandler.AddFieldItem("PayPass", MD5.Last64(_newPassword));
						dbOperHandler.AddFieldItem("IP", Const.GetUserIp);
						dbOperHandler.Update("N_User");
						result = true;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public bool SaveUserName(string _userid, string _name)
		{
			bool result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _userid);
				dbOperHandler.AddFieldItem("TrueName", _name);
				if (dbOperHandler.Update("N_User") > 0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public bool SaveEmail(string _userid, string _name)
		{
			bool result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _userid);
				dbOperHandler.AddFieldItem("Email", _name);
				if (dbOperHandler.Update("N_User") > 0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public bool SaveMobile(string _userid, string _name)
		{
			bool result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _userid);
				dbOperHandler.AddFieldItem("Mobile", _name);
				if (dbOperHandler.Update("N_User") > 0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public string UpdateParentId(string Id, string UserName, string Point, string UserGroup, string Code)
		{
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					sqlDataAdapter.SelectCommand.CommandText = "SELECT Id,UserCode,UserGroup,ParentId,point FROM [N_User] where UserName='" + UserName + "'";
					DataTable dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count <= 0)
					{
						string result = "切入账户不存在！";
						return result;
					}
					string text = dataTable.Rows[0]["UserCode"] + Strings.PadLeft(Id);
					if (Convert.ToDecimal(dataTable.Rows[0]["Point"]) / 10m < Convert.ToDecimal(Point))
					{
						string result = "返点高于切入账号的返点！不能切线";
						return result;
					}
					if (Convert.ToDecimal(dataTable.Rows[0]["UserGroup"]) < Convert.ToDecimal(UserGroup))
					{
						string result = "级别高于切入账号的级别！不能切线";
						return result;
					}
					sqlDataAdapter.SelectCommand.CommandText = "SELECT Id FROM [N_User] where UserCode like (select UserCode from N_User where Id=" + Id + ")+'%' and Id<>" + Id;
					DataTable dataTable2 = new DataTable();
					sqlDataAdapter.Fill(dataTable2);
					if (dataTable2.Rows.Count > 0)
					{
						for (int i = 0; i < dataTable2.Rows.Count; i++)
						{
							sqlCommand.CommandText = string.Concat(new string[]
							{
								"update N_User set UserCode=Replace(UserCode,'",
								Code,
								"','",
								text,
								"') where Id=",
								dataTable2.Rows[i]["Id"].ToString()
							});
							sqlCommand.ExecuteNonQuery();
						}
					}
					sqlCommand.CommandText = string.Concat(new object[]
					{
						"update N_User set ParentId=",
						dataTable.Rows[0]["Id"],
						",UserCode='",
						dataTable.Rows[0]["UserCode"],
						Strings.PadLeft(Id),
						"' where Id=",
						Id
					});
					if (sqlCommand.ExecuteNonQuery() > 0)
					{
						string result = "切线成功！";
						return result;
					}
				}
				catch (Exception)
				{
					string result = "切线失败！";
					return result;
				}
			}
			return "";
		}

		public void ChargePoints2(string _id, decimal _money, decimal _points)
		{
			using (new ComData().Doh())
			{
			}
		}

		public int DeleteUser(int userId)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Id=" + userId;
				result = dbOperHandler.Delete("N_User");
			}
			return result;
		}

		public void ClearAllUser()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.AddFieldItem("IsOnline", 0);
				dbOperHandler.AddFieldItem("SessionId", Guid.NewGuid().ToString().Replace("-", ""));
				dbOperHandler.Update("N_User");
			}
		}

		public void UpdateInfo(SqlCommand cmd, int _userId, string _statType, decimal _statValue)
		{
			try
			{
				SqlParameter[] values = new SqlParameter[]
				{
					new SqlParameter("@UserId", _userId),
					new SqlParameter("@statType", _statType),
					new SqlParameter("@statValue", _statValue)
				};
				cmd.CommandText = string.Concat(new string[]
				{
					"update N_User set ",
					_statType,
					"=",
					_statType,
					"+@statValue,updateTime=getdate() where Id=@UserId"
				});
				cmd.Parameters.AddRange(values);
				cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public int DeleteRegLink(int userId)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Id=" + userId;
				result = dbOperHandler.Delete("N_UserRegLink");
			}
			return result;
		}

		public string GetUserNav(string pid, string uid)
		{
			string text = "<span><a href='javascript:void(0);' onclick='ajaxSearchPid(" + uid + ")'>本级</a></span><span>></span>";
			if (pid != "0")
			{
				using (DbOperHandler dbOperHandler = new ComData().Doh())
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = "SELECT UserCode FROM [N_User] WHERE [Id]=" + pid;
					DataTable dataTable = dbOperHandler.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						string[] array = dataTable.Rows[0]["UserCode"].ToString().Replace(",,", ",").Substring(1, dataTable.Rows[0]["UserCode"].ToString().Replace(",,", ",").Length - 2).Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							if (Convert.ToInt32(array[i]) > Convert.ToInt32(uid))
							{
								string text2 = text;
								text = string.Concat(new string[]
								{
									text2,
									"<span><a href='javascript:void(0);' onclick='ajaxSearchPid(",
									array[i],
									")'>",
									this.GetUserName(array[i]),
									"</a></span><span>></span>"
								});
							}
						}
					}
				}
			}
			return text;
		}

		public void UpdateUserCode(string _userid)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select * from N_User where UserCode<>'' and len(Usercode)=" + _userid;
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = "select * from N_User where UserCode='' and ParentId=" + dataTable.Rows[i]["Id"];
						DataTable dataTable2 = dbOperHandler.GetDataTable();
						for (int j = 0; j < dataTable2.Rows.Count; j++)
						{
							dbOperHandler.Reset();
							dbOperHandler.ConditionExpress = "id=@id";
							dbOperHandler.AddConditionParameter("@id", dataTable2.Rows[j]["Id"]);
							dbOperHandler.AddFieldItem("UserCode", string.Concat(new object[]
							{
								dataTable.Rows[i]["UserCode"],
								",",
								dataTable2.Rows[j]["Id"],
								","
							}));
							dbOperHandler.Update("N_User");
						}
					}
				}
			}
		}

		public void Test(string _userid)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("select UserId,isnull(sum(inmoney),0) money from Act_ActiveRecord where CONVERT(varchar(10), STime, 120) = '{0}' group by UserId", _userid);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						dbOperHandler.Reset();
					}
				}
			}
		}

		protected SiteModel site;
	}
}
