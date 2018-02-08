using System;
using System.Configuration;
using System.Data;
using System.Web;
using Lottery.Collect;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.IPhone
{
	public class ajax : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this._operType = base.q("oper");
			string operType = this._operType;
			switch (operType)
			{
			case "ajaxCheckLogin":
				this.ajaxCheckLogin();
				goto IL_21B;
			case "ajaxAllLottery":
				this.ajaxAllLottery();
				goto IL_21B;
			case "ajaxIndexLottery":
				this.ajaxIndexLottery();
				goto IL_21B;
			case "ajaxLotteryTime":
				this.ajaxLotteryTime();
				goto IL_21B;
			case "ajaxUserInfo":
				this.ajaxUserInfo();
				goto IL_21B;
			case "checkusername":
				this.ajaxCheckUserName();
				goto IL_21B;
			case "ajaxPopInfo":
				this.ajaxPopInfo();
				goto IL_21B;
			case "ajaxRegister":
				this.ajaxRegister();
				goto IL_21B;
			case "login":
				this.ajaxLogin();
				goto IL_21B;
			case "getpwd":
				this.ajaxGetPwd();
				goto IL_21B;
			case "logout":
				this.ajaxLogout();
				goto IL_21B;
			case "ajaxLotteryTime23":
				this.ajaxLotteryTime23();
				goto IL_21B;
			case "GetListLotteryData":
				this.GetListLotteryData();
				goto IL_21B;
			case "GetKaiJiangList":
				this.GetKaiJiangList();
				goto IL_21B;
			case "GetKaiJiangInfo":
				this.GetKaiJiangInfo();
				goto IL_21B;
			case "GetKaiJiangInfoTop10":
				this.GetKaiJiangInfoTop10();
				goto IL_21B;
			case "GetLotteryNumber":
				this.GetLotteryNumber();
				goto IL_21B;
			}
			this.DefaultResponse();
			IL_21B:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			base.Admin_Load("", "json");
			this._response = base.JsonResult(1, "成功登录");
		}

		public void GetLotteryNumber()
		{
			string value = base.q("lid");
			this._response = "{\"result\":\"1\",\"table\": [" + Public.GetOpenListJson(Convert.ToInt32(value)).Replace("[", "").Replace("]", "") + "]}";
		}

		private void ajaxCheckLogin()
		{
			if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
			{
				this.AdminId = base.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
				this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "WebApp", "cookiess");
				if (this.AdminId != "0")
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "IsDel=0 and IsEnable=0 and id=@id and sessionId=@cookiess";
					this.doh.AddConditionParameter("@id", this.AdminId);
					this.doh.AddConditionParameter("@cookiess", this.AdminCookiess);
					int num = this.doh.Count("N_User");
					if (num > 0)
					{
						this._response = base.JsonResult(1, "账号在线");
					}
					else
					{
						this._response = base.JsonResult(0, "账号不在线");
					}
				}
				else
				{
					this._response = base.JsonResult(0, "账号不在线");
				}
			}
			else
			{
				this._response = base.JsonResult(0, "账号不在线");
			}
		}

		private void ajaxAllLottery()
		{
			string text = "";
			this.doh.Reset();
			this.doh.SqlCmd = "select * from [Sys_Lottery] where IsOpen=0 and IphoneIsOpen=0 order by Sort asc";
			DataTable dataTable = this.doh.GetDataTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string text2 = dataTable.Rows[i]["Id"].ToString();
				string text3 = "{\"tid\": \"类别Id\",\"id\": \"彩种Id\",\"name\": \"名称\",\"ordertime\": \"倒计时\",\"remark\": \"说明\"}";
				text3 = text3.Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(text2))).Replace("类别Id", dataTable.Rows[i]["LType"].ToString()).Replace("彩种Id", text2).Replace("说明", dataTable.Rows[i]["IphoneRemark"].ToString());
				DateTime d = DateTime.Now;
				DateTime dateTime = base.GetDateTime();
				string text4 = dateTime.ToString("yyyyMMdd");
				string text5 = dateTime.ToString("HH:mm:ss");
				string text6 = dateTime.ToString("yyyy-MM-dd");
				TimeSpan timeSpan;
				if (text2 == "3002" || text2 == "3003")
				{
					string value = dateTime.ToString("yyyy-MM-dd") + " 20:30:00";
					if (dateTime > Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd") + " 20:30:00"))
					{
						value = dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
					}
					timeSpan = Convert.ToDateTime(value) - dateTime;
				}
				else
				{
					if (UserCenterSession.LotteryTime == null)
					{
						UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
					}
					DataRow[] array = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
					{
						"Time >'",
						text5,
						"' and LotteryId=",
						text2
					}), "Time asc");
					if (array.Length == 0)
					{
						array = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
						{
							"Time <='",
							text5,
							"' and LotteryId=",
							text2
						}), "Time asc");
					}
					d = Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd") + " " + array[0]["Time"].ToString());
					if (Convert.ToDateTime(array[0]["Time"].ToString()) < Convert.ToDateTime(text5))
					{
						d = Convert.ToDateTime(dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " " + array[0]["Time"].ToString());
					}
					timeSpan = d - Convert.ToDateTime(text5);
				}
				string newValue = string.Concat((timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds) * 1000);
				text3 = text3.Replace("倒计时", newValue);
				text = text + text3 + ",";
			}
			this._response = "{\"result\":\"1\",\"table\": [" + text.Substring(0, text.Length - 1) + "]}";
		}

		private void ajaxIndexLottery()
		{
			string text = "";
			this.doh.Reset();
			this.doh.SqlCmd = "select row_number() over (order by Sort asc) as rowid,* from [Sys_Lottery] where IsOpen=0 and Id in (1001,1004,1009,1016,2001,3002) order by Sort asc";
			DataTable dataTable = this.doh.GetDataTable();
			if (dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					string text2 = dataTable.Rows[i]["Id"].ToString();
					string text3 = "{\"rowid\": \"排序Id\",\"tid\": \"类别Id\",\"id\": \"彩种Id\",\"name\": \"名称\",\"ordertime\": \"倒计时\",\"remark\": \"说明\"}";
					text3 = text3.Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(text2))).Replace("排序Id", dataTable.Rows[i]["rowid"].ToString()).Replace("类别Id", dataTable.Rows[i]["LType"].ToString()).Replace("彩种Id", text2).Replace("说明", dataTable.Rows[i]["IphoneRemark"].ToString());
					DateTime d = DateTime.Now;
					DateTime dateTime = base.GetDateTime();
					string text4 = dateTime.ToString("yyyyMMdd");
					string text5 = dateTime.ToString("HH:mm:ss");
					string text6 = dateTime.ToString("yyyy-MM-dd");
					TimeSpan timeSpan;
					if (text2 == "3002" || text2 == "3003")
					{
						string value = dateTime.ToString("yyyy-MM-dd") + " 20:30:00";
						if (dateTime > Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd") + " 20:30:00"))
						{
							value = dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
						}
						timeSpan = Convert.ToDateTime(value) - dateTime;
					}
					else
					{
						if (UserCenterSession.LotteryTime == null)
						{
							UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
						}
						DataRow[] array = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
						{
							"Time >'",
							text5,
							"' and LotteryId=",
							text2
						}), "Time asc");
						if (array.Length == 0)
						{
							array = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
							{
								"Time <='",
								text5,
								"' and LotteryId=",
								text2
							}), "Time asc");
						}
						d = Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd") + " " + array[0]["Time"].ToString());
						if (Convert.ToDateTime(array[0]["Time"].ToString()) < Convert.ToDateTime(text5))
						{
							d = Convert.ToDateTime(dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " " + array[0]["Time"].ToString());
						}
						timeSpan = d - Convert.ToDateTime(text5);
					}
					string newValue = string.Concat((timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds) * 1000);
					text3 = text3.Replace("倒计时", newValue);
					text = text + text3 + ",";
				}
				this._response = "{\"result\":\"1\",\"table\": [" + text.Substring(0, text.Length - 1) + "]}";
			}
			else
			{
				this._response = "{\"result\":\"0\",\"table\": []}";
			}
		}

		private void ajaxLotteryTime23()
		{
			string text = base.q("lid");
			string text2 = "{\"name\": \"名称\",\"lotteryid\": \"彩种类别\",\"ordertime\": \"倒计时\",\"closetime\": \"封单时间\",\"nestsn\": \"下期期号\",\"cursn\": \"当前期号\",\"curnumber\": \"开奖号码\"}";
			text2 = text2.Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(text))).Replace("彩种类别", text);
			DateTime now = DateTime.Now;
			DateTime dateTime = base.GetDateTime();
			string text3 = dateTime.ToString("yyyyMMdd");
			string text4 = dateTime.ToString("HH:mm:ss");
			string text5 = dateTime.ToString("yyyy-MM-dd");
			this.doh.Reset();
			this.doh.SqlCmd = "select dbo.f_GetCloseTime(" + text + ") as closetime";
			DataTable dataTable = this.doh.GetDataTable();
			text2 = text2.Replace("封单时间", dataTable.Rows[0]["closetime"].ToString());
			TimeSpan timeSpan = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59") - DateTime.Now;
			string newValue = string.Concat(timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds);
			text2 = text2.Replace("倒计时", newValue);
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT TOP 1 [Title],[Number] FROM [Sys_LotteryData] with(nolock) where Type=" + text + " order by Id desc";
			DataTable dataTable2 = this.doh.GetDataTable();
			if (dataTable2.Rows.Count > 0)
			{
				string newValue2 = dataTable2.Rows[0]["title"].ToString();
                decimal t = Convert.ToDecimal(dataTable2.Rows[0]["title"].ToString());

                string newValue3 = string.Concat(++t);
				text2 = text2.Replace("下期期号", newValue3).Replace("当前期号", newValue2);
				string[] array = dataTable2.Rows[0]["Number"].ToString().Split(new char[]
				{
					','
				});
				string text6 = "<p class='hm'>";
				for (int i = 0; i < array.Length; i++)
				{
					text6 = text6 + "<span>" + array[i] + "</span>";
				}
				text6 += "</p>";
				text2 = text2.Replace("开奖号码", text6);
			}
			else
			{
				string newValue2 = "您还未投注";
				string newValue3 = DateTime.Now.ToString("yyyy") + "000000001";
				text2 = text2.Replace("下期期号", newValue3).Replace("当前期号", newValue2);
				string text6 = "请您先投注";
				text2 = text2.Replace("开奖号码", text6);
			}
			this._response = text2;
		}

		private void ajaxLotteryTime()
		{
			string text = base.q("lid");
			string text2 = "{\"name\": \"名称\",\"lotteryid\": \"彩种类别\",\"ordertime\": \"倒计时\",\"closetime\": \"封单时间\",\"nestsn\": \"下期期号\",\"cursn\": \"当前期号\"}";
			text2 = text2.Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(text))).Replace("彩种类别", text);
			DateTime d = DateTime.Now;
			DateTime dateTime = base.GetDateTime();
			string str = dateTime.ToString("yyyyMMdd");
			string text3 = dateTime.ToString("HH:mm:ss");
			string text4 = dateTime.ToString("yyyy-MM-dd");
			this.doh.Reset();
			this.doh.SqlCmd = "select dbo.f_GetCloseTime(" + text + ") as closetime";
			DataTable dataTable = this.doh.GetDataTable();
			text2 = text2.Replace("封单时间", dataTable.Rows[0]["closetime"].ToString());
			string text6;
			string text7;
			TimeSpan timeSpan;
			if (text == "3002" || text == "3003")
			{
				DateTime dateTime2 = Convert.ToDateTime(dateTime.Year.ToString() + "-01-01 20:30:00");
				this.doh.Reset();
				this.doh.SqlCmd = string.Concat(new string[]
				{
					"select datediff(d,'",
					dateTime2.ToString("yyyy-MM-dd HH:mm:ss"),
					"','",
					dateTime.ToString("yyyy-MM-dd HH:mm:ss"),
					"') as d"
				});
				DataTable dataTable2 = this.doh.GetDataTable();
				int num = Convert.ToInt32(dataTable2.Rows[0]["d"]) - 7;
				num++;
				string text5 = dateTime.AddDays(-1.0).ToString("yyyy-MM-dd") + " 20:30:00";
				string value = dateTime.ToString("yyyy-MM-dd") + " 20:30:00";
				if (dateTime > Convert.ToDateTime(dateTime.ToString(" 20:30:00")))
				{
					value = dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
				}
				else
				{
					num--;
				}
				text6 = dateTime.Year.ToString() + Func.AddZero(num, 3);
				text7 = dateTime.Year.ToString() + Func.AddZero(num + 1, 3);
				timeSpan = Convert.ToDateTime(value) - Convert.ToDateTime(text3);
			}
			else
			{
				if (UserCenterSession.LotteryTime == null)
				{
					UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
				}
				DataRow[] array = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
				{
					"Time >'",
					text3,
					"' and LotteryId=",
					text
				}), "Time asc");
				if (array.Length == 0)
				{
					array = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
					{
						"Time <='",
						text3,
						"' and LotteryId=",
						text
					}), "Time asc");
					text7 = dateTime.AddDays(1.0).ToString("yyyyMMdd") + "-" + array[0]["Sn"].ToString();
				}
				else
				{
					text7 = str + "-" + array[0]["Sn"].ToString();
					d = Convert.ToDateTime(array[0]["Time"].ToString());
					if (dateTime > Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd") + " 00:00:00") && dateTime < Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd") + " 10:00:01"))
					{
						if (text == "1003")
						{
							text7 = dateTime.AddDays(-1.0).ToString("yyyyMMdd") + "-" + array[0]["Sn"].ToString();
						}
					}
				}
				if (Convert.ToDateTime(array[0]["Time"].ToString()) < Convert.ToDateTime(text3))
				{
					d = Convert.ToDateTime(dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " " + array[0]["Time"].ToString());
				}
				timeSpan = d - Convert.ToDateTime(text3);
				DataRow[] array2 = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
				{
					"Time <'",
					text3,
					"' and LotteryId=",
					text
				}), "Time desc");
				if (array2.Length == 0)
				{
					array2 = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
					{
						"LotteryId=",
						text
					}), "Time desc");
					text6 = dateTime.AddDays(-1.0).ToString("yyyyMMdd") + "-" + array2[0]["Sn"].ToString();
				}
				else
				{
					text6 = str + "-" + array2[0]["Sn"].ToString();
					if (dateTime > Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd") + " 00:00:00") && dateTime < Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd") + " 10:00:01"))
					{
						if (text == "1003")
						{
							text6 = dateTime.AddDays(-1.0).ToString("yyyyMMdd") + "-" + array2[0]["Sn"].ToString();
						}
					}
				}
				if (text == "1010" || text == "1017" || text == "3004")
				{
					text6 = string.Concat(new LotteryTimeDAL().GetTsIssueNum(text) + Convert.ToInt32(array2[0]["Sn"].ToString()));
					text7 = string.Concat(Convert.ToInt32(text6) + 1);
				}
				if (text == "1012")
				{
					text6 = string.Concat(new LotteryTimeDAL().GetTsIssueNum("1012") + Convert.ToInt32(array2[0]["Sn"].ToString()));
					text7 = string.Concat(Convert.ToInt32(text6) + 1);
				}
				if (text == "1013")
				{
					text6 = string.Concat(new LotteryTimeDAL().GetTsIssueNum("1013") + Convert.ToInt32(array2[0]["Sn"].ToString()));
					text7 = string.Concat(Convert.ToInt32(text6) + 1);
				}
				if (text == "1014" || text == "1015" || text == "1016")
				{
					text6 = text6.Replace("-", "");
					text7 = text7.Replace("-", "");
				}
				if (text == "1011" || text == "3005")
				{
					text6 = string.Concat(new LotteryTimeDAL().GetTsIssueNum("1011") + Convert.ToInt32(array2[0]["Sn"].ToString()));
					text7 = string.Concat(Convert.ToInt32(text6) + 1);
				}
				if (text == "4001")
				{
					text6 = string.Concat(new LotteryTimeDAL().GetTsIssueNum("4001") + Convert.ToInt32(array2[0]["Sn"].ToString()));
					text7 = string.Concat(Convert.ToInt32(text6) + 1);
				}
				if (text == "1005")
				{
					string text8 = string.Concat(Convert.ToInt32(array2[0]["Sn"].ToString()) - 1);
					if (text8.Length == 1)
					{
						text8 = "000" + text8;
					}
					if (text8.Length == 2)
					{
						text8 = "00" + text8;
					}
					if (text8.Length == 3)
					{
						text8 = "0" + text8;
					}
					string text9 = string.Concat(Convert.ToInt32(array2[0]["Sn"].ToString()));
					if (text9.Length == 1)
					{
						text9 = "000" + text9;
					}
					if (text9.Length == 2)
					{
						text9 = "00" + text9;
					}
					if (text9.Length == 3)
					{
						text9 = "0" + text9;
					}
					text6 = dateTime.ToString("yyyyMMdd") + "-" + text8;
					text7 = dateTime.ToString("yyyyMMdd") + "-" + text9;
				}
			}
			string newValue = string.Concat(timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds);
			text2 = text2.Replace("下期期号", text7).Replace("当前期号", text6).Replace("倒计时", newValue);
			this._response = text2;
		}

		private void ajaxUserInfo()
		{
			string response = "";
			base.UserInfo(ref response);
			this._response = response;
		}

		private void ajaxCheckUserName()
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "username=@username";
			this.doh.AddConditionParameter("@username", base.q("txtUserName"));
			if (this.doh.Exist("N_User"))
			{
				this._response = base.JsonResult(0, "此账号已存在，不能添加");
			}
			else
			{
				this._response = base.JsonResult(1, "帐号不存在，可以添加");
			}
		}

		private void ajaxPopInfo()
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "id=" + base.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
			this.doh.AddFieldItem("OnTime", DateTime.Now.ToString());
			this.doh.AddFieldItem("IsOnLine", 1);
			this.doh.Update("N_User");
			this.doh.Reset();
			this.doh.SqlCmd = "select top 5 Id,Title,Msg from N_UserMessage with(nolock) where IsRead=0 and UserId=" + base.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id")) + " order by Id asc";
			DataTable dataTable = this.doh.GetDataTable();
			if (dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "Id=@Id";
					this.doh.AddConditionParameter("@Id", dataTable.Rows[i]["Id"].ToString());
					this.doh.AddFieldItem("IsRead", "1");
					this.doh.Update("N_UserMessage");
				}
				this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			}
			else
			{
				this._response = "{\"result\" :\"0\",\"returnval\" :\"加载完成\",\"title\":\"0\",\"content\":\"0\"}";
			}
			dataTable.Dispose();
		}

		private void ajaxRegister()
		{
			string text = base.f("name");
			string text2 = base.f("pass");
			string code = base.f("code");
			string text3 = base.f("u");
			string text4 = "";
			try
			{
				if (!ValidateCode.CheckValidateCode(code, ref text4))
				{
					this._response = base.JsonResult(0, "验证码错误");
				}
				else if (text.Length > 0 && text2.Length > 0)
				{
					string decryptKey = ConfigurationManager.AppSettings["DesKey"].ToString();
					string text5 = base.DecryptDES(text3.Replace("@", "+"), decryptKey);
					string text6 = text5.Substring(0, text5.IndexOf('@'));
					this.doh.Reset();
					this.doh.ConditionExpress = "id=@id and Isdel=0";
					this.doh.AddConditionParameter("@id", text6);
					if (this.doh.Count("N_User") < 1)
					{
						this._response = base.JsonResult(0, "对不起，该注册链接已失效！");
					}
					else
					{
						string value = text5.Substring(text5.IndexOf('@') + 1);
						if (Convert.ToDecimal(value) >= 125m)
						{
							this.doh.Reset();
							this.doh.ConditionExpress = "UserId=@UserId and Point=@Point";
							this.doh.AddConditionParameter("@UserId", text6);
							this.doh.AddConditionParameter("@Point", Convert.ToDecimal(Convert.ToDecimal(value) / 10m).ToString("0.00"));
							object[] fields = this.doh.GetFields("V_UserQuota", "ChildNums,useNums,useNums2");
							if (fields == null)
							{
								this._response = base.JsonResult(0, "对不起，此链接的配额不足，请联系上级！");
								return;
							}
							if (Convert.ToDecimal(fields[0]) - (Convert.ToDecimal(fields[1]) + Convert.ToDecimal(fields[2])) < 1m)
							{
								this._response = base.JsonResult(0, "对不起，此链接的配额不足，请联系上级！");
								return;
							}
						}
						int num;
						if (int.TryParse(text6, out num))
						{
							string randomNumberString = base.GetRandomNumberString(64, false);
							int num2 = new UserDAL().Register(text6, text, text2, Convert.ToDecimal(value));
							if (num2 > 0)
							{
								this.doh.Reset();
								this.doh.ConditionExpress = "id=@id";
								this.doh.AddConditionParameter("@id", text6);
								object field = this.doh.GetField("N_User", "UserCode");
								string fieldValue = field + Strings.PadLeft(num2.ToString());
								this.doh.Reset();
								this.doh.ConditionExpress = "id=" + num2;
								this.doh.AddFieldItem("UserCode", fieldValue);
								this.doh.AddFieldItem("UserGroup", "0");
								if (this.doh.Update("N_User") > 0)
								{
									this.doh.Reset();
									this.doh.SqlCmd = "select Id,Point from N_User with(nolock) where Id=" + num2 + " and IsEnable=0 and IsDel=0";
									DataTable dataTable = this.doh.GetDataTable();
									for (int i = 0; i < dataTable.Rows.Count; i++)
									{
										this.doh.Reset();
										this.doh.SqlCmd = "SELECT [Point] FROM [N_UserLevel] where Point>=125.00 and Point<=" + Convert.ToDecimal(dataTable.Rows[i]["Point"]) + " order by [Point] desc";
										DataTable dataTable2 = this.doh.GetDataTable();
										for (int j = 0; j < dataTable2.Rows.Count; j++)
										{
											int num3 = 0;
											string value2 = "0";
											this.doh.Reset();
											this.doh.SqlCmd = string.Concat(new object[]
											{
												"select isnull(ChildNums-useNums-useNums2,0) as num from V_UserQuota with(nolock) where UserId=",
												this.AdminId,
												" and point=",
												Convert.ToDecimal(dataTable2.Rows[j]["Point"]) / 10m
											});
											DataTable dataTable3 = this.doh.GetDataTable();
											if (dataTable3.Rows.Count > 0)
											{
												num3 = Convert.ToInt32(dataTable3.Rows[0]["num"]);
											}
											if (Convert.ToDecimal(dataTable2.Rows[j]["Point"]) == Convert.ToDecimal(dataTable.Rows[i]["Point"]))
											{
												new UserQuotaDAL().SaveUserQuota(dataTable.Rows[i]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[j]["Point"]) / 10m, 0);
											}
											else
											{
												if (num3 <= 5)
												{
													value2 = string.Concat(num3);
												}
												new UserQuotaDAL().SaveUserQuota(dataTable.Rows[i]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[j]["Point"]) / 10m, Convert.ToInt32(value2));
											}
										}
									}
								}
								new LogSysDAL().Save("会员管理", "Id为" + num2 + "的会员注册成功！");
								this._response = base.JsonResult(1, "会员注册成功");
							}
							else
							{
								this._response = base.JsonResult(0, "注册失败，请重新注册");
							}
						}
						else
						{
							this._response = base.JsonResult(0, "链接地址错误！请重新打开");
						}
					}
				}
			}
			catch (Exception arg)
			{
				this._response = base.JsonResult(0, "注册异常：" + arg);
			}
		}

		private void ajaxLogin()
		{
			string text = base.f("name");
			string text2 = base.f("pass");
			string code = base.f("code");
			string text3 = "";
			if (!ValidateCode.CheckValidateCode(code, ref text3))
			{
				this._response = base.GetJsonResult(0, "验证码不正确！");
			}
			else
			{
				int iExpires = 604800;
				string text4 = new UserDAL().ChkLogin(text.Trim(), text2.Trim(), iExpires);
				if (text4.Length < 10)
				{
					new LogUserLoginDAL().Save(text4, "mobile", "", "", IPHelp.ClientIP);
					text4 = base.GetJsonResult(1, "success");
				}
				this._response = text4;
			}
		}

		private void ajaxGetPwd()
		{
			string text = base.f("name");
			string conditionValue = base.f("question");
			string conditionValue2 = base.f("answer");
			string code = base.f("code");
			string str = "";
			if (!ValidateCode.CheckValidateCode(code, ref str))
			{
				this._response = "验证码应该是" + str;
			}
			else
			{
				this.doh.Reset();
				this.doh.ConditionExpress = "userName=@userName";
				this.doh.AddConditionParameter("@userName", text);
				if (!this.doh.Exist("N_User"))
				{
					this._response = "对不起，账号不存在!";
				}
				else
				{
					this.doh.Reset();
					this.doh.ConditionExpress = "userName =@userName and Question=@Question and Answer=@Answer";
					this.doh.AddConditionParameter("@userName", text);
					this.doh.AddConditionParameter("@Question", conditionValue);
					this.doh.AddConditionParameter("@Answer", conditionValue2);
					if (!this.doh.Exist("N_User"))
					{
						this._response = "对不起，验证问题错误!";
					}
					else
					{
						this.doh.Reset();
						this.doh.ConditionExpress = "userName=@userName";
						this.doh.AddConditionParameter("@userName", text);
						this.doh.AddFieldItem("Password", MD5.Last64(MD5.Lower32("123456")));
						this.doh.Update("N_User");
						this._response = "密码也为您重置为：123456，请您登陆系统及时更改密码！";
						new LogSysDAL().Save("会员管理", text + "找回密码！");
					}
				}
			}
		}

		private void ajaxLogout()
		{
			this.doh.Reset();
			this.doh.ConditionExpress = "Id=@Id and IsEnable=0";
			this.doh.AddConditionParameter("@Id", base.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id")));
			this.doh.AddFieldItem("LastTime", DateTime.Now.ToString());
			this.doh.AddFieldItem("IsOnline", 0);
			this.doh.Update("N_User");
			this.doh.Dispose();
			new UserDAL().ChkLogout();
			this._response = base.JsonResult(1, "成功退出");
		}

		private void ajaxHistoryTop5()
		{
			string text = base.q("lid");
			this.doh.Reset();
			if (text == "23")
			{
				this.doh.SqlCmd = "SELECT TOP 10 [IssueNum] as title,(select number from Sys_LotteryData where title=a.IssueNum) as number FROM [N_UserBet] a where lotteryId=23 and UserId=" + base.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id")) + " group by a.IssueNum order by a.IssueNum desc";
			}
			else
			{
				this.doh.SqlCmd = "SELECT TOP 10 [Title],[Number] FROM [Sys_LotteryData] with(nolock) where Type=" + text + " order by replace([Title],'-','') desc";
			}
			DataTable dataTable = this.doh.GetDataTable();
			string text2 = "\"recordcount\":1,\"table\": [信息列表]";
			string text3 = "";
			int num = 1;
			foreach (DataRow dataRow in dataTable.Rows)
			{
				string text4 = string.Concat(new object[]
				{
					"{\"no\":",
					num,
					",\"type\":\"类别\",\"title\": \"",
					dataRow["Title"].ToString(),
					"\","
				});
				if (!string.IsNullOrEmpty(string.Concat(dataRow["Number"])))
				{
					string[] array = dataRow["Number"].ToString().Split(new char[]
					{
						','
					});
					if (array.Length == 3)
					{
						text4 = text4.Replace("类别", "3");
					}
					else
					{
						text4 = text4.Replace("类别", "5");
					}
					for (int i = 0; i < array.Length; i++)
					{
						object obj = text4;
						text4 = string.Concat(new object[]
						{
							obj,
							"\"ball",
							i + 1,
							"\": \"",
							array[i],
							"\","
						});
					}
					text4 = text4.Substring(0, text4.Length - 1) + "}";
				}
				text3 = text3 + text4 + ",";
				num++;
			}
			text2 = text2.Replace("信息列表", text3.Substring(0, (text3.Length > 1) ? (text3.Length - 1) : 0));
			this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + text2 + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxLotteryTime2()
		{
			string text = base.q("lid");
			string text2 = "\"table\": [{\"loid1\": 1,\"ordertime1\": \"倒计时1\",\"nestsn1\": \"下期期号1\",\"loid2\": 9,\"ordertime2\": \"倒计时2\",\"nestsn2\": \"下期期号2\"}]";
			string[] lot = this.GetLot("1");
			text2 = text2.Replace("下期期号1", lot[0]).Replace("倒计时1", lot[1]);
			string[] lot2 = this.GetLot("9");
			text2 = text2.Replace("下期期号2", lot2[0]).Replace("倒计时2", lot2[1]);
			this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + text2 + "}";
		}

		private string[] GetLot(string Lid)
		{
			string[] array = new string[2];
			DateTime d = DateTime.Now;
			DateTime dateTime = base.GetDateTime();
			string str = dateTime.ToString("yyyyMMdd");
			string text = dateTime.ToString("HH:mm:ss");
			string text2 = dateTime.ToString("yyyy-MM-dd");
			if (UserCenterSession.LotteryTime == null)
			{
				UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
			}
			DataRow[] array2 = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
			{
				"Time >'",
				text,
				"' and LotteryId=",
				Lid
			}), "Time asc");
			string text3;
			if (array2.Length == 0)
			{
				array2 = UserCenterSession.LotteryTime.Select(string.Concat(new object[]
				{
					"Time <='",
					text,
					"' and LotteryId=",
					Lid
				}), "Time asc");
				text3 = dateTime.AddDays(1.0).ToString("yyyyMMdd") + "-" + array2[0]["Sn"].ToString();
			}
			else
			{
				text3 = str + "-" + array2[0]["Sn"].ToString();
				d = Convert.ToDateTime(array2[0]["Time"].ToString());
			}
			if (Convert.ToDateTime(array2[0]["Time"].ToString()) < Convert.ToDateTime(text))
			{
				d = Convert.ToDateTime(dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " " + array2[0]["Time"].ToString());
			}
			string text4 = Convert.ToDateTime((d - Convert.ToDateTime(text)).ToString()).ToString("HH:mm:ss");
			string text5 = text4.Substring(0, 2);
			string text6 = text4.Substring(3, 2);
			string text7 = text4.Substring(6, 2);
			string text8;
			if (int.Parse(text5) == 0)
			{
				text8 = string.Concat(new string[]
				{
					"<span class='k'>",
					text6.Substring(0, 1),
					"</span><span class='k'>",
					text6.Substring(1, 1),
					"</span><span class='i'>:</span><span class='k'>",
					text7.Substring(0, 1),
					"</span><span class='k'>",
					text7.Substring(1, 1),
					"</span>"
				});
			}
			else
			{
				text8 = string.Concat(new string[]
				{
					"<span class='k'>",
					text5.Substring(0, 1),
					"</span><span class='k'>",
					text5.Substring(1, 1),
					"</span><span class='i'>:</span><span class='k'>",
					text6.Substring(0, 1),
					"</span><span class='k'>",
					text6.Substring(1, 1),
					"</span>"
				});
			}
			array[0] = text3;
			array[1] = text8;
			return array;
		}

		public void GetListBetTop20()
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT top 20 dbo.f_GetUserName([UserId]) as userName,dbo.f_GetLotteryName(LotteryId) as LotteryName,dbo.f_GetPlayName(PlayId) as PlayName,WinBonus,STime2,IssueNum\r\n                                FROM [N_UserBet] where State=3 or State=4 order by Id desc";
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		public void GetListLotteryData()
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT top 5000 * from Sys_LotteryData order by Id desc";
			DataTable dataTable = this.doh.GetDataTable();
			this._response = dtHelp.DT2JSONAIR(dataTable, 10);
			dataTable.Clear();
			dataTable.Dispose();
		}

		public void GetKaiJiangList()
		{
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT top 30 * from V_KaiJiangNotice order by LotteryId asc";
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		public void GetKaiJiangInfo()
		{
			string str = base.q("lid");
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT top 120 *,dbo.f_GetLotteryName(type) as LotteryName from Sys_LotteryData where Type=" + str + " order by title desc";
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
		}

		public void GetKaiJiangInfoTop10()
		{
			string str = base.q("lid");
			this.doh.Reset();
			this.doh.SqlCmd = "SELECT top 10 *,dbo.f_GetLotteryName(type) as LotteryName from Sys_LotteryData where Type=" + str + " order by title desc";
			DataTable dataTable = this.doh.GetDataTable();
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
			dataTable.Clear();
			dataTable.Dispose();
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

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
