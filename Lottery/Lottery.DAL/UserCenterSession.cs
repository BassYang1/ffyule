using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class UserCenterSession : BasicPage
	{
		protected void Admin_Load(string powerNum, string pageType)
		{
			if (this.site.WebIsOpen.ToString().Equals("1"))
			{
				this.showErrMsg(this.site.WebCloseSeason.ToString(), pageType);
				return;
			}
			this.chkPower(powerNum, pageType);
		}

		protected void chkPower(string s, string pageType)
		{
			if (pageType == "json" && !base.CheckFormUrl())
			{
				base.Response.End();
				return;
			}
			if (!this.IsPower(s))
			{
				this.showErrMsg("您还没有登录，请先登陆", pageType);
			}
		}

		protected bool IsPower(string s)
		{
			bool result = false;
			if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
			{
				this.AdminId = base.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
				this.AdminName = Cookie.GetValue(this.site.CookiePrev + "WebApp", "name");
				this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "WebApp", "cookiess");
				this.AdminPoint = Cookie.GetValue(this.site.CookiePrev + "WebApp", "point");
				if (this.AdminId != "0")
				{
					result = true;
				}
			}
			return result;
		}

		public void UserInfo(ref string _jsonstr)
		{
			if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") == null)
			{
				_jsonstr = "{\"result\" :\"0\",\"Message\" :\"登陆已超时，请您重新登陆\"}";
				return;
			}
			this.AdminId = base.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
			this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "WebApp", "cookiess");
			if (!(this.AdminId != "0") || !(this.AdminCookiess != ""))
			{
				_jsonstr = "{\"result\" :\"0\",\"Message\" :\"登陆已超时，请您重新登陆\"}";
				return;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + this.AdminId;
			this.doh.AddFieldItem("OnTime", DateTime.Now.ToString());
			this.doh.Update("N_User");
			this.doh.Reset();
			this.doh.SqlCmd = "select top 1 UserName,Point,Money,Pic,IsDel,IsEnable,sessionId from N_User where Id=" + this.AdminId;
			DataTable dataTable = this.doh.GetDataTable();
			if (dataTable.Rows.Count <= 0)
			{
				_jsonstr = "{\"result\" :\"0\",\"Message\" :\"登陆已超时，请您重新登陆\"}";
				return;
			}
			if (Convert.ToInt32(dataTable.Rows[0]["IsDel"]) != 0)
			{
				_jsonstr = "{\"result\" :\"0\",\"Message\" :\"您的账号异常，请联系客服\"}";
				return;
			}
			if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"]) != 0)
			{
				_jsonstr = "{\"result\" :\"0\",\"Message\" :\"您的账号异常，请联系客服\"}";
				return;
			}
			if (!this.AdminCookiess.Equals(dataTable.Rows[0]["sessionId"].ToString()))
			{
				_jsonstr = "{\"result\" :\"0\",\"Message\" :\"登陆已超时，请您重新登陆\"}";
				return;
			}
			int num = 0;
			string text = Convert.ToDouble(dataTable.Rows[0]["Money"]).ToString("0.00").PadLeft(10, '0').Replace(".", "");
			_jsonstr = string.Concat(new object[]
			{
				"{\"result\" :\"1\",\"AdminId\" :\"",
				this.AdminId,
				"\",\"AdminName\" :\"",
				dataTable.Rows[0]["UserName"],
				"\",\"AdminMoney\" :\"",
				dataTable.Rows[0]["Money"],
				"\",\"Money\" :\"",
				text,
				"\",\"emailcount\" :\"",
				num,
				"\"}"
			});
		}

		public void chkLogin()
		{
			if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
			{
				this.AdminId = base.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
				this.AdminName = Cookie.GetValue(this.site.CookiePrev + "WebApp", "name");
				this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "WebApp", "cookiess");
				if (this.AdminId.Length != 0 && this.AdminName.Length != 0)
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "id=@id and sessionId=@cookiess";
					this.doh.AddConditionParameter("@id", this.AdminId);
					this.doh.AddConditionParameter("@cookiess", this.AdminCookiess);
					object[] fields = this.doh.GetFields("N_User", "PassWord,sessionId,Id,Money,Score,Pic,Point");
					if (fields != null)
					{
						this.AdminIsLogin = true;
						this.AdminMoney = fields[3].ToString();
						this.AdminScore = fields[4].ToString();
						this.AdminPic = fields[5].ToString();
						this.AdminPoint = fields[6].ToString();
					}
				}
			}
		}

		protected void showErrMsg(string msg, string pageType)
		{
			if (pageType != "json")
			{
				base.FinalMessage(msg, "/login", 0);
				return;
			}
			HttpContext.Current.Response.Clear();
			if (!this.AdminIsLogin)
			{
				HttpContext.Current.Response.Write(base.JsonResult(-1, msg));
			}
			else
			{
				HttpContext.Current.Response.Write(base.JsonResult(0, msg));
			}
			HttpContext.Current.Response.End();
		}

		protected DateTime GetDateTime()
		{
			return new DateTimePubDAL().GetDateTime();
		}

		public static DataTable LotteryTime
		{
			get;
			set;
		}

		protected string EncryptDES(string encryptString, string encryptKey)
		{
			string result;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
				byte[] keys = this.Keys;
				byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, keys), CryptoStreamMode.Write);
				cryptoStream.Write(bytes2, 0, bytes2.Length);
				cryptoStream.FlushFinalBlock();
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			catch
			{
				result = encryptString;
			}
			return result;
		}

		protected string DecryptDES(string decryptString, string decryptKey)
		{
			string result;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(decryptKey);
				byte[] keys = this.Keys;
				byte[] array = Convert.FromBase64String(decryptString);
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, keys), CryptoStreamMode.Write);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				result = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			catch
			{
				result = decryptString;
			}
			return result;
		}

		protected void getUserGroupDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = string.Format("select UserGroup,Point from N_User where Id={0}", this.AdminId);
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					this.doh.Reset();
					if (Convert.ToDouble(dataTable.Rows[0]["Point"]) > 130.0)
					{
						this.doh.SqlCmd = string.Format("SELECT [Id],[Name] FROM N_UserGroup where Id<{0} ORDER BY Id desc", dataTable.Rows[0]["UserGroup"]);
					}
					else
					{
						this.doh.SqlCmd = string.Format("SELECT [Id],[Name] FROM N_UserGroup where Id<{0} ORDER BY Id desc", dataTable.Rows[0]["UserGroup"]);
					}
					DataTable dataTable2 = this.doh.GetDataTable();
					if (dataTable2.Rows.Count == 0)
					{
						ddlClassId.Items.Add(new ListItem(" 会员", "0"));
						dataTable2.Clear();
						dataTable2.Dispose();
						return;
					}
					for (int i = 0; i < dataTable2.Rows.Count; i++)
					{
						ddlClassId.Items.Add(new ListItem(" " + dataTable2.Rows[i]["Name"].ToString(), dataTable2.Rows[i]["Id"].ToString()));
					}
					dataTable2.Clear();
					dataTable2.Dispose();
				}
			}
		}

		protected void getEditDropDownList(ref DropDownList ddlClassId, decimal ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT [Id],[Point],[Title],[Bonus],[Score],[Times],[Sort] FROM [N_UserLevel] where point>=100 and point<" + this.AdminPoint;
				DbOperHandler expr_3C = this.doh;
				expr_3C.SqlCmd += " ORDER BY Bonus desc";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					this.doh.Reset();
					this.doh.SqlCmd = "SELECT [Id],[Point],[Title],[Bonus],[Score],[Times],[Sort] FROM [N_UserLevel] where point=" + this.AdminPoint;
					DbOperHandler expr_99 = this.doh;
					expr_99.SqlCmd += " ORDER BY Bonus desc";
					DataTable dataTable2 = this.doh.GetDataTable();
					for (int i = 0; i < dataTable2.Rows.Count; i++)
					{
						ddlClassId.Items.Add(new ListItem(dataTable2.Rows[i]["Bonus"].ToString() + "_" + Convert.ToDecimal(Convert.ToDecimal(dataTable2.Rows[i]["Point"]) / 10m).ToString("0.00") + "%", dataTable2.Rows[i]["Point"].ToString()));
					}
				}
				for (int j = 0; j < dataTable.Rows.Count; j++)
				{
					string text = "";
					ddlClassId.Items.Add(new ListItem(string.Concat(new string[]
					{
						dataTable.Rows[j]["Bonus"].ToString(),
						"_",
						Convert.ToDecimal(Convert.ToDecimal(dataTable.Rows[j]["Point"]) / 10m).ToString("0.00"),
						"% ",
						text
					}), dataTable.Rows[j]["Point"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getEditDropDownList(ref DropDownList ddlClassId, decimal point, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = string.Concat(new object[]
				{
					"SELECT [Id],[Point],[Title],[Bonus],[Score],[Times],[Sort] FROM [N_UserLevel] where point>=",
					point,
					" and point<",
					this.AdminPoint
				});
				DbOperHandler expr_61 = this.doh;
				expr_61.SqlCmd += " ORDER BY Bonus";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					this.doh.Reset();
					this.doh.SqlCmd = "SELECT [Id],[Point],[Title],[Bonus],[Score],[Times],[Sort] FROM [N_UserLevel] where point=" + this.AdminPoint;
					DbOperHandler expr_BE = this.doh;
					expr_BE.SqlCmd += " ORDER BY Bonus";
					DataTable dataTable2 = this.doh.GetDataTable();
					for (int i = 0; i < dataTable2.Rows.Count; i++)
					{
						ddlClassId.Items.Add(new ListItem(dataTable2.Rows[i]["Bonus"].ToString() + "_" + Convert.ToDecimal(Convert.ToDecimal(dataTable2.Rows[i]["Point"]) / 10m).ToString("0.00") + "%", dataTable2.Rows[i]["Point"].ToString()));
					}
				}
				for (int j = 0; j < dataTable.Rows.Count; j++)
				{
					ddlClassId.Items.Add(new ListItem(dataTable.Rows[j]["Bonus"].ToString() + "_" + Convert.ToDecimal(Convert.ToDecimal(dataTable.Rows[j]["Point"]) / 10m).ToString("0.00") + "%", dataTable.Rows[j]["Point"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getLotteryDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT [Id],[Title] FROM Sys_Lottery where IsOpen=0 ORDER BY Id asc";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					dataTable.Clear();
					dataTable.Dispose();
					return;
				}
				ddlClassId.Items.Add(new ListItem(" 所有彩票", ""));
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					ddlClassId.Items.Add(new ListItem(" " + dataTable.Rows[i]["Title"].ToString(), dataTable.Rows[i]["Id"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getSingleDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				ddlClassId.Items.Add(new ListItem(" 所有模式", ""));
				ddlClassId.Items.Add(new ListItem(" 元", "元"));
				ddlClassId.Items.Add(new ListItem(" 角", "角"));
				ddlClassId.Items.Add(new ListItem(" 分", "分"));
			}
		}

		protected void getTypeDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				ddlClassId.Items.Add(new ListItem(" 所有类型", ""));
				ddlClassId.Items.Add(new ListItem(" 账号充值", "1"));
				ddlClassId.Items.Add(new ListItem(" 账号提款", "2"));
				ddlClassId.Items.Add(new ListItem(" 提现失败", "3"));
				ddlClassId.Items.Add(new ListItem(" 投注扣款", "4"));
				ddlClassId.Items.Add(new ListItem(" 追号扣款", "5"));
				ddlClassId.Items.Add(new ListItem(" 追号返款", "6"));
				ddlClassId.Items.Add(new ListItem(" 游戏返点", "7"));
				ddlClassId.Items.Add(new ListItem(" 奖金派送", "8"));
				ddlClassId.Items.Add(new ListItem(" 撤单返款", "9"));
				ddlClassId.Items.Add(new ListItem(" 充值扣费", "10"));
				ddlClassId.Items.Add(new ListItem(" 上级充值", "11"));
				ddlClassId.Items.Add(new ListItem(" 活动礼金", "12"));
				ddlClassId.Items.Add(new ListItem(" 代理分红", "13"));
				ddlClassId.Items.Add(new ListItem(" 管理员减扣", "14"));
				ddlClassId.Items.Add(new ListItem(" 积分兑换", "15"));
			}
		}

		protected void getBankDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT [Id],[Bank] FROM [Sys_Bank] where IsGetCash=0 ORDER BY Id asc";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					ddlClassId.Items.Add(new ListItem(dataTable.Rows[i]["Bank"].ToString(), dataTable.Rows[i]["Id"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getTxBankDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT Id,PayBank+'@'+'************'+substring(Payaccount,len(Payaccount)-3,4) as Name FROM [N_UserBank] where UserId=" + this.AdminId + " ORDER BY Id asc";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					dataTable.Clear();
					dataTable.Dispose();
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					ddlClassId.Items.Add(new ListItem(dataTable.Rows[i]["Name"].ToString(), dataTable.Rows[i]["Id"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getZXChargeSetDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT Id,MerName FROM [Sys_ChargeSet] where IsUsed=0 ORDER BY Sort asc";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					ddlClassId.Items.Add(new ListItem(dataTable.Rows[i]["MerName"].ToString(), dataTable.Rows[i]["Id"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getZXBankDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT Code,Bank FROM [Sys_Bank] where IsCharge=1 ORDER BY Id asc";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					ddlClassId.Items.Add(new ListItem(dataTable.Rows[i]["Bank"].ToString(), dataTable.Rows[i]["Code"].ToString()));
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		protected void getPointDropDownList(ref DropDownList ddlClassId, int ClassDepth)
		{
			if (!this.Page.IsPostBack)
			{
				this.doh.Reset();
				this.doh.SqlCmd = "SELECT [Point],[Bonus] FROM [N_UserLevel] where point=(select point from N_User with(nolock) where Id=1893)";
				DataTable dataTable = this.doh.GetDataTable();
				if (dataTable.Rows.Count == 0)
				{
					dataTable.Clear();
					dataTable.Dispose();
					return;
				}
				DataRow dataRow = dataTable.Rows[0];
				ddlClassId.Items.Add(new ListItem(dataRow["Bonus"].ToString() + "/0.00%", dataRow["Bonus"].ToString() + "/0.00%"));
				ddlClassId.Items.Add(new ListItem("1850/" + Convert.ToDouble(Convert.ToDouble(dataRow["Point"]) / 10.0).ToString("0.00") + "%", "1850/" + Convert.ToDouble(Convert.ToDouble(dataRow["Point"]) / 10.0).ToString("0.00") + "%"));
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public DataSet ConvertXMLToDataSet(string xmlData)
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

		public string GetJsonResult(int result, string Message)
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

		public string GetJsonResult2(int result, string Message)
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

		public string id = "0";

		protected string AdminId = "0";

		protected string AdminName = string.Empty;

		protected string AdminCookiess = string.Empty;

		protected bool AdminIsLogin;

		protected string AdminMoney = "0";

		protected string AdminFreezing = "0";

		protected string AdminScore = "0";

		protected string AdminPic = "1";

		protected string AdminPoint = "1";

		public string loStr = "";

		public string StartTime = DateTime.Now.AddDays(-10.0).ToString("yyyy-MM-dd") + " 00:00:00";

		public string EndTime = DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd") + " 00:00:00";

		private byte[] Keys = new byte[]
		{
			18,
			52,
			86,
			120,
			144,
			171,
			205,
			239
		};
	}
}
