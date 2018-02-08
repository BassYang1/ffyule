using System;
using System.Data;
using System.Web;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL.Flex
{
	public class UserDAL : ComData
	{
		public void ClearSession()
		{
			Cookie.Del("UserId");
			Cookie.Del("UserName");
			Cookie.Del("UserPoint");
			Cookie.Del("SessionId");
		}

		public string UserInfo(string UserId, string SessionId)
		{
			if (UserId != "0" && SessionId != "0")
			{
				using (DbOperHandler dbOperHandler = new ComData().Doh())
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT '1' as result,Id,Money,IsDel,IsEnable,sessionId\r\n                                    ,Convert(varchar(10),cast(round(Point/10.0,2) as numeric(10,2))) as Point\r\n                                    ,'0' as email\r\n                                    ,'0' as notice\r\n                                    FROM [N_User] a where Id={0} and sessionId='{1}'", UserId, SessionId);
					DataTable dataTable = dbOperHandler.GetDataTable();
					string result;
					if (dataTable.Rows.Count <= 0)
					{
						dbOperHandler.Dispose();
						result = base.GetJsonResult(0, "登陆已超时，请您重新登陆");
						return result;
					}
					if (dataTable.Rows[0]["IsDel"].Equals("1") || dataTable.Rows[0]["IsEnable"].Equals("1"))
					{
						result = base.GetJsonResult(0, "您的账户存在未知问题，请于客服联系！");
						return result;
					}
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = "Id=@Id";
					dbOperHandler.AddConditionParameter("@Id", UserId);
					dbOperHandler.AddFieldItem("ontime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					dbOperHandler.AddFieldItem("IsOnline", 1);
					dbOperHandler.Update("N_User");
					dbOperHandler.Dispose();
					result = base.ConverTableToJSON(dataTable);
					return result;
				}
			}
			return base.GetJsonResult(0, "登陆已超时，请您重新登陆！");
		}

		public string GetEmailCount(string UserId)
		{
			if (UserId != "0")
			{
				using (DbOperHandler dbOperHandler = new ComData().Doh())
				{
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = "IsDelReceive=0 and IsRead=0 and ReceiveId=" + UserId;
					int num = dbOperHandler.CountId("N_UserEmail");
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = "IsRead=0 and UserId=" + UserId;
					int num2 = dbOperHandler.CountId("N_UserMessage");
					return base.GetJsonResult(1, string.Concat(num + num2));
				}
			}
			return base.GetJsonResult(0, "0");
		}

		public string Login(string UserName, string UserPass)
		{
			UserName = UserName.ToLower().Replace("'", "");
			UserPass = MD5.Last64(MD5.Lower32(UserPass));
			string text = Guid.NewGuid().ToString().Replace("-", "");
			string result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 '1' as result,a.Id,ParentId,UserGroup,Convert(varchar(10),cast(round(Point/10.0,2) as numeric(10,2))) as Point,\r\n                                            UserName,Money,'{0}' as SessionId,LastTime,OnTime,IP,a.IsEnable,IsGetCash,IsBet,IsTranAcc,EnableSeason,LoginId,\r\n                                            case when b.Id is null then '0' else '1' end as IsBank,'0' as email,'0' as notice \r\n                                            FROM N_User a left join N_UserBank b on a.Id=b.UserId\r\n                                            where username='{1}' and password='{2}' and isDel=0", text, UserName, UserPass);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 1)
					{
						result = base.GetJsonResult(0, "您的账户存在未知问题，请于客服联系！");
					}
					else if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 2)
					{
						result = base.GetJsonResult(0, "对不起，您的网络不稳定，请重新登录！！");
					}
					else
					{
						this.ClearSession();
						Cookie.SetObj("UserId", dataTable.Rows[0]["Id"].ToString());
						Cookie.SetObj("UserName", UserName);
						Cookie.SetObj("UserPoint", dataTable.Rows[0]["Point"].ToString());
						Cookie.SetObj("SessionId", text);
						string clientIP = IPHelp.ClientIP;
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "Id=@Id";
						dbOperHandler.AddConditionParameter("@Id", dataTable.Rows[0]["Id"].ToString());
						dbOperHandler.AddFieldItem("LastTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
						dbOperHandler.AddFieldItem("ontime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
						dbOperHandler.AddFieldItem("IP", clientIP);
						dbOperHandler.AddFieldItem("sessionId", text);
						dbOperHandler.AddFieldItem("IsOnline", 1);
						dbOperHandler.AddFieldItem("Source", 0);
						dbOperHandler.Update("N_User");
						dbOperHandler.Dispose();
						IPScaner iPScaner = new IPScaner();
						iPScaner.DataPath = HttpContext.Current.Server.MapPath("Data/qqwry.dat");
						iPScaner.IP = clientIP;
						string address = iPScaner.IPLocation() + iPScaner.ErrMsg;
						string browser = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
						string oSNameByUserAgent = this.GetOSNameByUserAgent(HttpContext.Current.Request.UserAgent);
						new LogUserLoginDAL().Save(dataTable.Rows[0]["Id"].ToString(), address, browser, oSNameByUserAgent, clientIP);
						result = base.ConverTableToJSON(dataTable);
					}
				}
				else
				{
					dbOperHandler.Dispose();
					result = base.GetJsonResult(0, "登录失败，用户名或密码错误！");
				}
			}
			return result;
		}

		public string Register(string _ParentId, string _UserGroup, string _UserName, string _Password, string _Point)
		{
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "SELECT Id FROM [N_User] WHERE [UserName]='" + _UserName.ToLower() + "'";
				if (dbOperHandler.GetDataTable().Rows.Count > 0)
				{
					jsonResult = base.GetJsonResult(0, "账号已存在，请更换一个账号！");
				}
				else
				{
					string text = MD5.Last64(MD5.Lower32(_Password));
					object[,] array = new object[2, 6];
					array[0, 0] = "ParentId";
					array[0, 1] = "UserGroup";
					array[0, 2] = "UserName";
					array[0, 3] = "Password";
					array[0, 4] = "Point";
					array[0, 5] = "PayPass";
					array[1, 0] = _ParentId;
					array[1, 1] = _UserGroup;
					array[1, 2] = _UserName.ToLower();
					array[1, 3] = text;
					array[1, 4] = _Point;
					array[1, 5] = MD5.Last64(MD5.Lower32("123456"));
					object[,] vFields = array;
					dbOperHandler.Reset();
					dbOperHandler.AddFieldItems(vFields);
					int num = dbOperHandler.Insert("N_User");
					if (num > 0)
					{
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "id=@id";
						dbOperHandler.AddConditionParameter("@id", _ParentId);
						object field = dbOperHandler.GetField("N_User", "UserCode");
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "id=" + num;
						dbOperHandler.AddFieldItem("UserCode", field + Strings.PadLeft(num.ToString()));
						dbOperHandler.Update("N_User");
						object[,] array2 = new object[2, 2];
						array2[0, 0] = "UserId";
						array2[0, 1] = "Change";
						array2[1, 0] = num;
						array2[1, 1] = 0;
						object[,] vFields2 = array2;
						dbOperHandler.Reset();
						dbOperHandler.AddFieldItems(vFields2);
						dbOperHandler.Insert("N_UserMoneyStatAll");
						jsonResult = base.GetJsonResult(1, "添加会员成功！");
					}
					else
					{
						jsonResult = base.GetJsonResult(0, "添加会员失败！");
					}
				}
			}
			return jsonResult;
		}

		public void getUserPointListJson(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", UserId);
				object field = dbOperHandler.GetField("N_User", "Point");
				dbOperHandler.SqlCmd = "SELECT point,Convert(varchar(10),cast(round([Point]/10.0,2) as numeric(5,2)))+'%' as title FROM [N_UserLevel] where point>=100 and point<" + field;
				DbOperHandler expr_4B = dbOperHandler;
				expr_4B.SqlCmd += " ORDER BY Bonus desc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void getUserUpPointListJson(string UserId, string MinPoint, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", UserId);
				object field = dbOperHandler.GetField("N_User", "Point");
				dbOperHandler.SqlCmd = string.Concat(new object[]
				{
					"SELECT point,Convert(varchar(10),cast(round([Point]/10.0,2) as numeric(5,2)))+'%' as title FROM [N_UserLevel] where point<",
					field,
					" and point>=",
					Convert.ToDouble(MinPoint.Replace("%", "")) * 10.0
				});
				DbOperHandler expr_88 = dbOperHandler;
				expr_88.SqlCmd += " ORDER BY Bonus asc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void getSysBankJson(ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.SqlCmd = "SELECT Id,Bank from Sys_Bank where IsGetCash=0 and IsUsed=0";
				DbOperHandler expr_17 = dbOperHandler;
				expr_17.SqlCmd += " ORDER BY Id asc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void getChargeSysBankJson(ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.SqlCmd = "SELECT Id,Bank from Sys_Bank where IsCharge=0 and IsUsed=0";
				DbOperHandler expr_17 = dbOperHandler;
				expr_17.SqlCmd += " ORDER BY id asc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void getChargeSetJson(string Type, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.SqlCmd = "SELECT Id,MerName from Sys_ChargeSet where IsUsed=0";
				DbOperHandler expr_17 = dbOperHandler;
				expr_17.SqlCmd += " ORDER BY sort asc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void getChargeSysBankByIdJson(string Id, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.SqlCmd = "SELECT * from Sys_Bank where Id=" + Id;
				DbOperHandler expr_1D = dbOperHandler;
				expr_1D.SqlCmd += " ORDER BY Id asc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void getChargeSetByIdJson(string Id, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.SqlCmd = "SELECT * from Sys_ChargeSet where Id=" + Id;
				DbOperHandler expr_1D = dbOperHandler;
				expr_1D.SqlCmd += " ORDER BY Id asc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void getSysBankBinByIdJson(string BankId, string BankBin, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.SqlCmd = string.Concat(new string[]
				{
					"SELECT top 1 * from Sys_BankBinInfo where BankId=",
					BankId,
					" and BankBin='",
					BankBin,
					"'"
				});
				DbOperHandler expr_3F = dbOperHandler;
				expr_3F.SqlCmd += " ORDER BY Id asc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetListJSON(int page, int PSize, string whereStr, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = whereStr;
				int num = dbOperHandler.Count("Flex_User");
				string sql = SqlHelp.GetSql0(num + " as totalcount,row_number() over (order by Id asc) as rowid,*", "Flex_User", "Id", PSize, page, "asc", whereStr);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetListOnlineJSON(int page, int PSize, string whereStr, string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = whereStr + " and UserCode like '%," + UserId + ",%'";
				int num = dbOperHandler.Count("N_User");
				string sql = SqlHelp.GetSql0(num + " as totalcount,ID,UserName,UserCode,Money,LastTime", "N_User", "Id", PSize, page, "asc", whereStr + " and UserCode like '%," + UserId + ",%'");
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				string text = "";
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						string text2 = string.Empty;
						string text3 = dataTable.Rows[i]["UserCode"].ToString().Replace(",,", "_").Replace(",", "");
						text3 = text3.Substring(text3.IndexOf(UserId));
						string[] array = text3.Split(new char[]
						{
							'_'
						});
						if (array.Length > 0)
						{
							for (int j = 0; j < array.Length; j++)
							{
								if (!string.IsNullOrEmpty(array[j]))
								{
									dbOperHandler.Reset();
									dbOperHandler.ConditionExpress = "Id=" + array[j];
									text2 = text2 + dbOperHandler.GetField("N_User", "UserName") + ">";
								}
							}
							text2 = text2.Substring(0, text2.Length - 1);
							if (i != 0)
							{
								text += " union all ";
							}
							object obj = text;
							text = string.Concat(new object[]
							{
								obj,
								" select  ",
								num,
								" as totalcount,row_number() over (order by Id asc) as rowid,ID,UserName,UserCode,Money,'",
								text2,
								"' as CodeName,LastTime from N_User  where  Id=",
								dataTable.Rows[i]["Id"]
							});
						}
					}
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = text;
					dataTable = dbOperHandler.GetDataTable();
				}
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetUserInfoJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select * from V_UserInfo where Id=" + UserId;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetMoneyJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select Money from N_User where Id=" + UserId;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetMsgJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 1 Id,title,Msg from N_UserMessage with(nolock) where IsRead=0 and UserId=" + UserId + " order by Id desc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = "Id=@Id";
					dbOperHandler.AddConditionParameter("@Id", dataTable.Rows[0]["Id"].ToString());
					dbOperHandler.AddFieldItem("IsRead", "1");
					dbOperHandler.Update("N_UserMessage");
					_jsonstr = base.ConverTableToJSON(dataTable);
				}
				else
				{
					_jsonstr = "[{\"title\":\"0\",\"msg\":\"0\"}]";
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public int UpdateInfo(string UserId, string Question, string Answer)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=" + UserId;
				dbOperHandler.AddFieldItem("Question", Question);
				dbOperHandler.AddFieldItem("Answer", Answer);
				result = dbOperHandler.Update("N_User");
			}
			return result;
		}

		public int UpdatePoint(string UserId, string Point)
		{
			int result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=" + UserId;
				dbOperHandler.AddFieldItem("Point", Point);
				result = dbOperHandler.Update("N_User");
			}
			return result;
		}

		public string UpdateInfo(string UserId, string QQ, string Email, string Mobile)
		{
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=" + UserId;
				dbOperHandler.AddFieldItem("QQ", QQ);
				dbOperHandler.AddFieldItem("Email", Email);
				dbOperHandler.AddFieldItem("Mobile", Mobile);
				if (dbOperHandler.Update("N_User") > 0)
				{
					jsonResult = base.GetJsonResult(1, "基本信息保存成功！");
				}
				else
				{
					jsonResult = base.GetJsonResult(0, "基本信息保存失败！");
				}
			}
			return jsonResult;
		}

		public string ChangeUserPassword(string _userid, string _oldPassword, string _newPassword)
		{
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _userid);
				object field = dbOperHandler.GetField("N_User", "PassWord");
				if (field != null)
				{
					if (field.ToString().ToLower() == MD5.Last64(MD5.Lower32(_oldPassword)))
					{
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "id=@id";
						dbOperHandler.AddConditionParameter("@id", _userid);
						dbOperHandler.AddFieldItem("PassWord", MD5.Last64(MD5.Lower32(_newPassword)));
						dbOperHandler.AddFieldItem("IP", Const.GetUserIp);
						if (dbOperHandler.Update("N_User") > 0)
						{
							jsonResult = base.GetJsonResult(1, "登录密码修改成功！");
						}
						else
						{
							jsonResult = base.GetJsonResult(0, "登录密码修改失败！");
						}
					}
					else
					{
						jsonResult = base.GetJsonResult(0, "原登录密码错误！");
					}
				}
				else
				{
					jsonResult = base.GetJsonResult(0, "原登录密码错误！");
				}
			}
			return jsonResult;
		}

		public string ChangePayPassword(string _userid, string _oldPassword, string _newPassword)
		{
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _userid);
				object field = dbOperHandler.GetField("N_User", "PayPass");
				if (field != null)
				{
					if (field.ToString().ToLower() == MD5.Last64(MD5.Lower32(_oldPassword)))
					{
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "id=@id";
						dbOperHandler.AddConditionParameter("@id", _userid);
						dbOperHandler.AddFieldItem("PayPass", MD5.Last64(MD5.Lower32(_newPassword)));
						dbOperHandler.AddFieldItem("IP", Const.GetUserIp);
						if (dbOperHandler.Update("N_User") > 0)
						{
							jsonResult = base.GetJsonResult(1, "取款密码修改成功！");
						}
						else
						{
							jsonResult = base.GetJsonResult(0, "取款密码修改失败！");
						}
					}
					else
					{
						jsonResult = base.GetJsonResult(0, "原取款密码错误！");
					}
				}
				else
				{
					jsonResult = base.GetJsonResult(0, "原取款密码错误！");
				}
			}
			return jsonResult;
		}

		public string ClearUserPassword(string _userid, string Password)
		{
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", _userid);
				dbOperHandler.AddFieldItem("PassWord", MD5.Last64(MD5.Lower32(Password)));
				dbOperHandler.AddFieldItem("IP", Const.GetUserIp);
				if (dbOperHandler.Update("N_User") > 0)
				{
					jsonResult = base.GetJsonResult(1, "重置密码成功！");
				}
				else
				{
					jsonResult = base.GetJsonResult(0, "重置密码失败！");
				}
			}
			return jsonResult;
		}

		public string UserTranAcc(string Type, string UserId, string ToUserId, string Money, string PassWord)
		{
			if (Convert.ToDecimal(Money) < 0m)
			{
				return base.GetJsonResult(0, "转账金额不正确！");
			}
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", UserId);
				object[] fields = dbOperHandler.GetFields("N_User", "Money,PayPass");
				if (fields.Length > 0)
				{
					if (Convert.ToDecimal(Money) > Convert.ToDecimal(fields[0]))
					{
						jsonResult = base.GetJsonResult(0, "您的可用余额不足");
					}
					else if (!MD5.Last64(MD5.Lower32(PassWord)).Equals(fields[1].ToString()))
					{
						jsonResult = base.GetJsonResult(0, "您的取款密码错误");
					}
					else if (new UserChargeDAL().SaveUpCharge(Type, UserId, ToUserId, Convert.ToDecimal(Money)) > 0)
					{
						new LogSysDAL().Save("会员管理", string.Concat(new string[]
						{
							"Id为",
							UserId,
							"的会员转账给Id为",
							ToUserId,
							"的会员！"
						}));
						jsonResult = base.GetJsonResult(1, "转账成功！");
					}
					else
					{
						jsonResult = base.GetJsonResult(0, "转账失败！");
					}
				}
				else
				{
					jsonResult = base.GetJsonResult(0, "账号出现问题，请您重新登陆！");
				}
			}
			return jsonResult;
		}

		public void GetUserNameJSON(string UserName, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select '1' as result,Id,UserName,Question,Answer from N_User where UserName='" + UserName + "'";
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					_jsonstr = base.ConverTableToJSON(dataTable);
				}
				else
				{
					_jsonstr = base.GetJsonResult(0, "用户不存在！");
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetLoginListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int num = dbOperHandler.Count("Log_UserLogin");
				string sql = SqlHelp.GetSql0(num + " as totalcount,row_number() over (order by Id desc) as rowid,dbo.f_GetUserName(UserId) as UserName,*", "Log_UserLogin", "loginTime", _pagesize, _thispage, "desc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON2(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		private string GetOSNameByUserAgent(string userAgent)
		{
			string result;
			if (userAgent.Contains("NT 10.0"))
			{
				result = "Windows 10";
			}
			else if (userAgent.Contains("NT 6.3"))
			{
				result = "Windows 8.1";
			}
			else if (userAgent.Contains("NT 6.2"))
			{
				result = "Windows 8";
			}
			else if (userAgent.Contains("NT 6.1"))
			{
				result = "Windows 7";
			}
			else if (userAgent.Contains("NT 6.1"))
			{
				result = "Windows 7";
			}
			else if (userAgent.Contains("NT 6.0"))
			{
				result = "Windows Vista/Server 2008";
			}
			else if (userAgent.Contains("NT 5.2"))
			{
				if (userAgent.Contains("64"))
				{
					result = "Windows XP";
				}
				else
				{
					result = "Windows Server 2003";
				}
			}
			else if (userAgent.Contains("NT 5.1"))
			{
				result = "Windows XP";
			}
			else if (userAgent.Contains("NT 5"))
			{
				result = "Windows 2000";
			}
			else if (userAgent.Contains("NT 4"))
			{
				result = "Windows NT4";
			}
			else if (userAgent.Contains("Me"))
			{
				result = "Windows Me";
			}
			else if (userAgent.Contains("98"))
			{
				result = "Windows 98";
			}
			else if (userAgent.Contains("95"))
			{
				result = "Windows 95";
			}
			else if (userAgent.Contains("Mac"))
			{
				result = "Mac";
			}
			else if (userAgent.Contains("Unix"))
			{
				result = "UNIX";
			}
			else if (userAgent.Contains("Linux"))
			{
				result = "Linux";
			}
			else if (userAgent.Contains("SunOS"))
			{
				result = "SunOS";
			}
			else
			{
				result = HttpContext.Current.Request.Browser.Platform;
			}
			return result;
		}
	}
}
